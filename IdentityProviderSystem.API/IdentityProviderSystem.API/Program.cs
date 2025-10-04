using Coravel;
using IdentityProviderSystem.Domain.Services;
using IdentityProviderSystem.JobScheduler;
using IdentityProviderSystem.JobScheduler.Jobs;
using IdentityProviderSystem.Persistance;
using Serilog;

const string AllowSpecifiOrigin = "AllowSpecificOrigin";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options
        .AddPolicy(name: AllowSpecifiOrigin, policy => policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataContext();
builder.Services.AddRepositories();
builder.Services.AddDomainServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddServicesMapperProfile();
builder.Services.AddJobs();
builder.WebHost.UseKestrel();

var logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

app.MigrateDatabase();
app.UseCors(AllowSpecifiOrigin);

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<SaltJob>().EveryFiveMinutes();
    scheduler.Schedule<AccessTokenJob>().EveryMinute();
    scheduler.Schedule<RefreshTokenJob>().EveryFiveMinutes();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();