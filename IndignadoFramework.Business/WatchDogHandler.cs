using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using IndignadoFramework.Business.TasksWatchDog;

namespace IndignadoFramework.Business
{
    static public class WatchDogHandler
    {
        //public int minutos = 40;

        static public bool StarWatchDog()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            


            // jobs can be scheduled before sched.start() has been called

            // get a "nice round" time a few seconds in the future...
            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 30);


            

            // job1 will only fire once at date/time "ts"
            IJobDetail job = JobBuilder.Create<TaskVariable>()
                .WithIdentity("job1", "group1")
                .Build();


            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                                           .WithIdentity("trigger1", "group1")
                                           .StartAt(startTime)
                                           .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                                           //.ForJob(job)
                                           .Build();


            // schedule it to run!
            DateTimeOffset? ft = sched.ScheduleJob(job,trigger);

            IJobDetail job2 = JobBuilder.Create<TaskQuorum>()
                .WithIdentity("job2", "group1")
                .Build();


            ISimpleTrigger trigger2 = (ISimpleTrigger)TriggerBuilder.Create()
                                           .WithIdentity("trigger2", "group1")
                                           .StartAt(startTime)
                                           .WithSimpleSchedule(x => x.WithIntervalInMinutes(3).RepeatForever())
                //.ForJob(job)
                                           .Build();


            
            ft = sched.ScheduleJob(job2, trigger2);

            // schedule it to run!
            sched.Start();

            return true;
        }
    }
}
