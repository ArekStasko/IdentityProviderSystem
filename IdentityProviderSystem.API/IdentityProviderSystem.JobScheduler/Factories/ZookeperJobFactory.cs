using IdentityProviderSystem.JobScheduler.Models;
using IdentityProviderSystem.JobScheduler.ScheduleServices;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace IdentityProviderSystem.JobScheduler.Factories;

public class ZookeperJobFactory : IJobFactory
{
    public static readonly List<WorkerJob> Workers = new()
    {
        new SaltJob(),
    };
    readonly IServiceProvider _serviceProvider;
    readonly IScheduler _workerScheduler;

    public ZookeperJobFactory(IServiceProvider serviceProvider, IScheduler workerScheduler)
    {
        _serviceProvider = serviceProvider;
        _workerScheduler = workerScheduler;

    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        IJob job = null;
        if(bundle.JobDetail.Key.Name == ZookeeperJob.JobName)
        {
            var zookeeperJob = _serviceProvider.GetService<ZookeeperJob>();
            zookeeperJob.AddWorkers(_workerScheduler, Workers);
            job = zookeeperJob;
        } else
        {
            foreach(var worker in Workers)
            {
                if(bundle.JobDetail.Key.Name == worker.GetJobName)
                {
                    job = worker.GetJob(_serviceProvider);
                }
            }
        }
        return job;
    }

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        disposable?.Dispose();
    }
}