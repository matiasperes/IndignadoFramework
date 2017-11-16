using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using IndignadoFramework.DAC;
using IndignadoFramework.Entities;



namespace IndignadoFramework.Business.TasksWatchDog
{
    class TaskQuorum : IJob
    {
        public virtual void Execute(IJobExecutionContext context)
        {
            try
            {

                Console.WriteLine("TaskQuorum is executing.");

                ConvocatoriaDAC dacConv = new ConvocatoriaDAC();

                List<Convocatoria> convocatorias = dacConv.GetAllConvocatoriaExpired();

                foreach (Convocatoria conv in convocatorias)
                {
                    conv.Suspendida = true;
                    dacConv.UpdateConvocatoria(conv);
                }

                Console.WriteLine("TaskQuorum to complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
