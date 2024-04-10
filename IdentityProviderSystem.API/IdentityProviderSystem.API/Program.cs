using Coravel;
using IdentityProviderSystem.Domain.Services;
using IdentityProviderSystem.JobScheduler;
using IdentityProviderSystem.JobScheduler.Jobs;
using IdentityProviderSystem.Persistance;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataContext();
builder.Services.AddRepositories();
builder.Services.AddDomainServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddServicesMapperProfile();
builder.Services.AddJobs();

var logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

app.MigrateReadDatabase();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<SaltJob>().EveryFiveMinutes();
    scheduler.Schedule<TokenJob>().EveryMinute();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();