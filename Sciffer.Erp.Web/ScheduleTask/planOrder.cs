using Quartz;
using Quartz.Impl;

namespace Sciffer.Erp.Web.ScheduleTask
{
    public class planOrder
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<jobOrder>().Build();
            IJobDetail job1 = JobBuilder.Create<TaskSchedule>().Build(); // for task creation written by chandan

            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("trigger1", "group1")
              .WithSimpleSchedule(x => x.WithIntervalInSeconds(60).RepeatForever())
              //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 15))
              .Build();

             ITrigger trigger1 = TriggerBuilder.Create()
            .WithIdentity("ITaskJob", "ITask")
            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(12, 00))
            .Build(); 

            scheduler.ScheduleJob(job, trigger);
            scheduler.ScheduleJob(job1, trigger1);
        }
    }
}