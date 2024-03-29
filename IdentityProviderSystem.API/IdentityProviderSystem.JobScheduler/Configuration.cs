using IdentityProviderSystem.JobScheduler.Factories;
using IdentityProviderSystem.JobScheduler.ScheduleServices;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace IdentityProviderSystem.JobScheduler;

public class Configuration
{
    static async Task<IScheduler> ConfigureWorkers(IScheduler scheduler)
    {
        foreach(var worker in ZookeperJobFactory.Workers)
        {
            scheduler = await worker.ConfigureScheduler(scheduler);
        }
        return scheduler;
    }
    
    static async Task<IScheduler> ConfigureSaltScheduleService(IScheduler saltScheduleService)
    {
        var saltTriggerTime = "";
        var saltJob = JobBuilder.Create<SaltJob>()
            .WithIdentity(SaltJob.JobName, SaltJob.JobGroup)
            .Build();
        
        var saltTrigger = TriggerBuilder.Create()
            .WithIdentity(SaltJob.TriggerName, SaltJob.TriggerGroup)
            .WithCronSchedule(saltTriggerTime)
            .Build();

        await saltScheduleService.ScheduleJob(saltJob, saltTrigger);            
        return saltScheduleService;
    }
    
    public static async Task Configure(IServiceCollection services)
    {
        try
        {
            var serviceProvider = services.BuildServiceProvider();
            
            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            scheduler.JobFactory = new ZookeperJobFactory(serviceProvider, scheduler);

            await ConfigureSaltScheduleService(scheduler);

            await scheduler.Start();
        }
        catch (Exception e)
        {
            throw;
        }
    }
}