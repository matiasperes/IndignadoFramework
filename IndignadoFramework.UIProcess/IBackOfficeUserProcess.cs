using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.Entities;

namespace IndignadoFramework.UIProcess
{
    public interface IBackOfficeUserProcess
    {
        List<Movimiento> ObtenerTodosMovimientos();

        Movimiento AgregarMovimiento(Movimiento movimiento);

        void BorrarMovimiento(Movimiento movimiento);

        void ModificarMovimiento(Movimiento movimiento);

        Movimiento ObtenerMovimientoXNombre(string nombreMovimiento);

        Movimiento ObtenerMovimientoXId(int idMovimiento);

        List<Movimiento> ObtenerMovimientosPaginando(int pageNumber, int pageSize);

    }
}
