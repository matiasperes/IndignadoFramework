using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.Entities;

namespace IndignadoFramework.UIProcess
{
    public interface IConvocatoriaUserProcess
    {
        Convocatoria ObtenerConvocatoriaXId(int idConvocatoria);

        Convocatoria AgregarConvocatoria(Convocatoria convocatoria);

        List<WebFeed> GetFeedsConvocatoria(int idConvocatoria);

        void ConfirmarAsistenciaConvocatoria(int idConvocatoria, int idUsuario);

        List<Convocatoria> ObtenerConvocatoriasMovimiento(int idMovimiento);

        List<Convocatoria> ObtenerConvocatoriasPaginando(int pageNumber, int pageSize);
    }
}
