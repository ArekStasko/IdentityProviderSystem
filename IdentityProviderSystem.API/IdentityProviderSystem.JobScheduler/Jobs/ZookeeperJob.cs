using IdentityProviderSystem.JobScheduler.Models;
using Quartz;

namespace IdentityProviderSystem.JobScheduler.ScheduleServices;

public class ZookeeperJob : IJob
{
    public static string JobName = "ZookeeperJob";
    public static string JobGroup = "ZookeeperJobGroup";
    public static string TriggerName = "ZookeeperTrigger";
    public static string TriggerGroup = "ZookeeperTriggerGroup";

    IScheduler _workerScheduler = null;
    List<WorkerJob> _workers = new List<WorkerJob>();
    
    public void AddWorkers(IScheduler workerScheduler, List<WorkerJob> workers)
    {
        _workerScheduler = workerScheduler;
        _workers = workers;
    }
    ITrigger getNewTrigger(string name, string group, string schedule)
    {
        return TriggerBuilder.Create()
            .WithIdentity(name, group)
            .WithCronSchedule(schedule)
            .Build();
    }
    async Task rescheduleJob()
    {
        if (_workerScheduler != null)
        {
            foreach(var worker in _workers)
            {
                var trigger = await _workerScheduler.GetTrigger(worker.GetTriggerKey);
                if(trigger != null)
                {
                    var cronTrigger = (ICronTrigger)trigger;
                    var newExpression = "";

                    if (cronTrigger.CronExpressionString != newExpression)
                    {
                        var newTrigger = getNewTrigger(trigger.Key.Name, trigger.Key.Group, newExpression);
                        await _workerScheduler.RescheduleJob(trigger.Key, newTrigger);
                    }
                }
            }
                
        }
    }
    
    public Task Execute(IJobExecutionContext context)
    {
        try
        {
            var triggerTime = "";
            
            var task = Task.Run(async () => await rescheduleJob());
            task.Wait();

            if (_workerScheduler != null && !_workerScheduler.IsStarted)
            {
                _workerScheduler.Start();
            }

            return Task.CompletedTask;
        } catch(Exception e)
        {
            return Task.FromException(e);
        }
    }
}