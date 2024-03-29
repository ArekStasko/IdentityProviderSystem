using IdentityProviderSystem.JobScheduler.ScheduleServices;
using Quartz;
using Quartz.Spi;

namespace IdentityProviderSystem.JobScheduler.Factories;

public class SaltJobFactory : IJobFactory
{
    public static readonly List<WorkerJob> Workers = new List<WorkerJob>()
    {
        new SaltScheduleService()
    };
    
    readonly IServiceProvider _serviceProvider;
    readonly IScheduler _workerScheduler;

    public SaltJobFactory(IServiceProvider serviceProvider, IScheduler workerScheduler)
    {
        _serviceProvider = serviceProvider;
        _workerScheduler = workerScheduler;

    }
    
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        throw new NotImplementedException();
    }

    public void ReturnJob(IJob job)
    {
        throw new NotImplementedException();
    }
}