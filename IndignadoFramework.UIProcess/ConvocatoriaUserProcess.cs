using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.Entities;
using IndignadoFramework.UIProcess.FrontOfficeService;

namespace IndignadoFramework.UIProcess
{
    public class ConvocatoriaUserProcess : IConvocatoriaUserProcess
    {
        public Convocatoria ObtenerConvocatoriaXId(int idConvocatoria)
        {
            var proxy = new FrontOfficeServiceClient();
            return proxy.ObtenerConvocatoriaXId(idConvocatoria);
        }

        public Convocatoria AgregarConvocatoria(Convocatoria convocatoria)
        {
            var proxy = new FrontOfficeServiceClient();
            return proxy.AgregarConvocatoria(convocatoria);
        }

        public List<WebFeed> GetFeedsConvocatoria(int idConvocatoria)
        {
            var proxy = new FrontOfficeServiceClient();
            return new List<WebFeed>(proxy.GetFeedsConvocatoria(idConvocatoria));
        }

        public void ConfirmarAsistenciaConvocatoria(int idConvocatoria, int idUsuario)
        {
            var proxy = new FrontOfficeServiceClient();
            proxy.ConfirmarAsistenciaConvocatoria(idConvocatoria, idUsuario);
        }

        public List<Convocatoria> ObtenerConvocatoriasMovimiento(int idMovimiento)
        {
            var proxy = new FrontOfficeServiceClient();
            return new List<Convocatoria>(proxy.ObtenerConvocatoriasMovimiento(idMovimiento));
        }

        public List<Convocatoria> ObtenerConvocatoriasPaginando(int pageNumber, int pageSize)
        {
            var proxy = new FrontOfficeServiceClient();
            //return new List<Convocatoria>(proxy.ObtenerConvocatoriasPaginando(pageNumber,pageSize));
            return new List<Convocatoria>();
        }
    }
}
