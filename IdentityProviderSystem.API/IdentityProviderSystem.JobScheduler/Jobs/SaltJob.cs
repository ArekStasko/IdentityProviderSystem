using IdentityProviderSystem.JobScheduler.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace IdentityProviderSystem.JobScheduler.ScheduleServices;

public class SaltJob : WorkerJob
{
    public static string JobName = "SaltJob";
    public static string JobGroup = "SaltGroup";
    public static string TriggerName = "SaltTrigger";
    public static string TriggerGroup = "SaltTriggerGroup";

    public SaltJob() : base(
        JobName, JobGroup, TriggerName, TriggerGroup, 
        "SaltTriggerTime", typeof(SaltJob)
    ){}
    public override IJob GetJob(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<SaltJob>();
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        try
        {
            Console.WriteLine("Running Salt Service");
            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            return Task.FromException(e);
        }
    }
}