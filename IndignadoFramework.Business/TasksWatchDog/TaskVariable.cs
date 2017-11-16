using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using IndignadoFramework.DAC;
using IndignadoFramework.Entities;

namespace IndignadoFramework.Business.TasksWatchDog
{
    class TaskVariable : IJob
    {
        public virtual void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("TaskVariables is executing.");
            MovimientoDAC dacMov = new MovimientoDAC();
            List<Movimiento>  movs = dacMov.GetAllMovimiento();

            foreach ( Movimiento m in movs)
            {

                ContenidoDAC dacCont = new ContenidoDAC();
                List<Contenido> conts = dacCont.GetContenidosInadecuados(m.Id, m.X);
                foreach (Contenido cont in conts)
                {
                    cont.Habilitado = false;
                    dacCont.UpdateContenido(cont);
                    EspecificacionUsuarioDAC dacEU = new EspecificacionUsuarioDAC();
                    if (cont.EspecificacionUsuario.BajasContenido >= m.Z - 1)
                    {

                        dacEU.DeleteEspecificacionUsuario(cont.EspecificacionUsuario.Id);
                    }
                    else
                    {
                        cont.EspecificacionUsuario.BajasContenido += 1;
                        dacEU.UpdateEspecificacionUsuario(cont.EspecificacionUsuario, new String[0]);
                    }                    
                }
            }
            Console.WriteLine("TaskVariables to complete.");

        }
    }
}
