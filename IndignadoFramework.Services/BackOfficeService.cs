using System;
using System.Collections.Generic;
using System.ServiceModel;
using IndignadoFramework.Entities;
using IndignadoFramework.Services.Contracts;
using IndignadoFramework.Business;


namespace IndignadoFramework.Services
{

    [ServiceBehavior(UseSynchronizationContext = false,
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerCall)]
    public class BackOfficeService : IBackOfficeService
    {
        public BackOfficeService()
        {
        }

        public string GetHello()
        {
            return "Hello from my WCF service in Windows Azure!";
        }

        public void AgregarFuente(FuenteWEB fuente)
        {
            try
            {
                var bc = new BackOffice();
                bc.AgregarFuente(fuente);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }


        public List<Movimiento> ObtenerTodosMovimientos()
        {
            try
            {
                var bc = new BackOffice();
                return bc.ObtenerTodosMovimientos();
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public Movimiento AgregarMovimiento(Movimiento movimiento)
        {
            try
            {
                var bc = new BackOffice();
                return bc.AgregarMovimiento(movimiento);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public void BorrarMovimiento(Movimiento movimiento)
        {
            try
            {
                var bc = new BackOffice();
                bc.BorrarMovimiento(movimiento);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }

        }

        public void ModificarMovimiento(Movimiento movimiento)
        {
            try
            {
                var bc = new BackOffice();
                bc.ModificarMovimiento(movimiento);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public Movimiento ObtenerMovimientoXId(int idMovimiento)
        {
            try
            {
                var bc = new BackOffice();
                return bc.ObtenerMovimientoXId(idMovimiento);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public Movimiento ObtenerMovimientoXNombre(string nombreMovimiento)
        {
            try
            {
                var bc = new BackOffice();
                return bc.ObtenerMovimientoXNombre(nombreMovimiento);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public List<Movimiento> ObtenerMovimientosPaginando(int pageNumber, int pageSize)
        {
            try
            {
                var bc = new BackOffice();
                return bc.ObtenerMovimientosPaginando(pageNumber,pageSize);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }


        public List<Contenido> ObtenerContenidosMovimientoPorInadecuado(int idMovimiento)
        {
            try
            {
                ContenidoHandler ch = new ContenidoHandler();
                return ch.ObtenerContenidosMovimientoPorInadecuado(idMovimiento);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public void EliminarEspecificacionUsuario(int idus) 
        {
            try
            {
                UsuarioHandler uh = new UsuarioHandler();
                uh.EliminarEspecificacionUsuario(idus);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        
        }


    }
}
