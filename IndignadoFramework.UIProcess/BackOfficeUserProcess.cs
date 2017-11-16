using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.Entities;
using IndignadoFramework.UIProcess.BackOfficeService;

namespace IndignadoFramework.UIProcess
{
    public class BackOfficeUserProcess : IBackOfficeUserProcess
    {

        List<Movimiento> IBackOfficeUserProcess.ObtenerTodosMovimientos()
        {
            var proxy = new BackOfficeServiceClient();
            return new List<Movimiento>(proxy.ObtenerTodosMovimientos());
        }

        Movimiento IBackOfficeUserProcess.AgregarMovimiento(Movimiento movimiento)
        {
            var proxy = new BackOfficeServiceClient();
            return proxy.AgregarMovimiento(movimiento);
        }

        void IBackOfficeUserProcess.BorrarMovimiento(Movimiento movimiento)
        {
            var proxy = new BackOfficeServiceClient();
            proxy.BorrarMovimiento(movimiento);
        }

        void IBackOfficeUserProcess.ModificarMovimiento(Movimiento movimiento)
        {
            var proxy = new BackOfficeServiceClient();
            proxy.ModificarMovimiento(movimiento);
        }
        
        Movimiento IBackOfficeUserProcess.ObtenerMovimientoXId(int idMovimiento)
        {
            var proxy = new BackOfficeServiceClient();
            return proxy.ObtenerMovimientoXId(idMovimiento);
        }

        Movimiento IBackOfficeUserProcess.ObtenerMovimientoXNombre(string nombreMovimiento)
        {
            var proxy = new BackOfficeServiceClient();
            return proxy.ObtenerMovimientoXNombre(nombreMovimiento);
        }

        List<Movimiento> IBackOfficeUserProcess.ObtenerMovimientosPaginando(int pageNumber, int pageSize)
        {
            var proxy = new BackOfficeServiceClient();
            return new List<Movimiento>(proxy.ObtenerMovimientosPaginando(pageNumber, pageSize));
        }
    }
}
