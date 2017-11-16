using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.Entities;
using IndignadoFramework.DAC;
using System.Transactions;

namespace IndignadoFramework.Business
{
    public class ConvocatoriaHandler
    {
        ConvocatoriaDAC cdac = new ConvocatoriaDAC();

        public Convocatoria ObtenerConvocatoriaXId(int idConvocatoria)
        {
            Console.WriteLine("Obtener convocatoria con ID" + idConvocatoria);
            Convocatoria convocatoria = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    convocatoria = GetConvocatoriaById(idConvocatoria);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Convocatoria Obtenida");
            return convocatoria;
        }

        private Convocatoria GetConvocatoriaById(int idConvocatoria)
        {
            try
            {
                return cdac.SelectForId(idConvocatoria);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Convocatoria AgregarConvocatoria(Convocatoria conv)
        {
            Console.WriteLine("Agregando convocatoria... ");
            conv.CantUsuariosConfirmados = 0;
            conv.Suspendida = false;
            Convocatoria convocatoria = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    convocatoria = cdac.AddConvocatoria(conv);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Se ha creado una nueva convocatoria de id = " + convocatoria.Id);

            Console.WriteLine("Enviando notificaciones de nueva convocatoria... ");
            //Envío de notificaciones
            //Una vez ingresada una convocatoria se deberán enviar notificaciones a todos los usuarios que
            //se encuentren en un radio de menos de 50 km de la zona de la actividad, o que se hayan
            //registrado para recibir notificaciones de dicha temática.


            //Obtengo todos los usuarios del movimiento.
            EspecificacionUsuarioDAC euDac = new EspecificacionUsuarioDAC();
            var usuarios = from u in euDac.GetAllEspecificacionUsuario()
                           where u.MovimientoId == conv.MovimientoId
                           select u;
            if(usuarios.Count() > 0)
            {

                BackOffice back = new BackOffice();
                Movimiento movConv = back.ObtenerMovimientoXId(convocatoria.MovimientoId);
                //Para cada uno de los usuarios, chequeo si esta a 50 km o si esta enlazado a la categoria
                //tematica de la convocatoria
                double latConv = convocatoria.UbicacionLatitud;
                double lonConv = convocatoria.UbicacionLongitud;
                double sinLatConv = Math.Sin(latConv);
                double cosLatConv = Math.Cos(latConv);
                double latUsr;
                double lonUsr;
                double distancia;
                bool enviarConfirmacion;
                foreach (var u in usuarios)
                {
                    enviarConfirmacion = false;
                    latUsr = u.UbicacionLatitud; // de grados a radianes
                    lonUsr = u.UbicacionLongitud; //
                    distancia = (111.194) * (Math.Acos(sinLatConv * Math.Sin(latUsr) + cosLatConv * Math.Cos(latUsr) * Math.Cos(lonConv - lonUsr))); // En kilometros
                    Console.WriteLine("Distancia " + distancia);
                    if (Math.Abs(distancia) <= 50.0)
                        enviarConfirmacion = true;

                    if (!enviarConfirmacion)
                    {
                        foreach (CategoriaTematica cat in u.CategoriasTematicas)
                        {
                            if (cat.Id == convocatoria.CategoriaTematicaId)
                                enviarConfirmacion = true;
                        }

                    }

                    if (enviarConfirmacion)
                    {
                        try
                        {
                            Notificaciones.enviarNotificacion(movConv.Nombre, u.Membership, "no-reply-" + movConv.Nombre.ToLower() + "@indignadoframework.com",
                                                            "Nueva Convocatoria", u.Membership, "hay una nueva convocatoria en el movimiento " + movConv.Nombre + ".",
                                                            "Detalles de la convocatoria " + convocatoria.Titulo +":"+ "</br>" +
                                                            "Descripcion: " + convocatoria.Descripcion + "</br>" +
                                                            "Inicio: " + convocatoria.Inicio.ToString() + "</br>" +
                                                            "Ubicacion: " + "<a href=\"" + "http://indignado.cloudapp.net/" + movConv.Nombre + "/Convocatoria/Details?idConv=" + convocatoria.Id + "\">Ver Detalles" + "</a>" + "</br>" +
                                                            "Quorum: " + convocatoria.Quorum,
                                                            "http://www.mrvacandsewaz.com/images/map.png");
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.ToString());
                        }

                    }
            }
            }
        
            return convocatoria;


        }

        public List<FuenteWEB> GetFuentesDatosMovimiento(int idMov)
        {
            MovimientoDAC mdac = new MovimientoDAC();

            try
            {
                return mdac.GetFuentesWebMovimiento(idMov);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            
        }

        // OBSOLETA
        // Devuelve Feeds filtrados segun categoria tematica para origenes de fuentes de datos del Movimiento de la convocatoria
        public List<WebFeed> GetFeedsConvocatoria(int idConvocatoria) 
        {
            Console.WriteLine("Obtener Feeds convocatoria con ID" + idConvocatoria);

            Convocatoria conv = GetConvocatoriaById(idConvocatoria); 
            
            List<WebFeed> lwf = new List<WebFeed>();


            //LINQ TO Entities
            var fuentes =  from f in conv.Movimiento.FuentesWEB
                           select f;

            foreach (var f in fuentes)
            {

                WebFeed wffiltrado = WebFeedReader.ReadFeedFiltrado(f.Url,f.Tipo,conv.CategoriaTematica.Nombre);

                if(wffiltrado.Nodes.Count > 0)
                    lwf.Add(wffiltrado);
            }

            return lwf;
        }

        public int ConfirmarAsistenciaConvocatoria(int idConvocatoria, int idUsuario)
        {
            Console.WriteLine("Confirmando asistencia convocatoria " + idConvocatoria + ", usuario "+idUsuario);

            return cdac.AddAsistenciaConvocatoria(idConvocatoria, idUsuario);
        }

        public bool ConfirmoAsistenciaUsuario(int idConvocatoria, int idUsuario)
        {
            return cdac.ConfirmoAsistenciaUsuario(idConvocatoria, idUsuario);
        }

        public List<Convocatoria> ObtenerConvocatoriasMovimiento(int idMovimiento)
        {
            Console.WriteLine("Obtener convocatorias para " + idMovimiento);
            List<Convocatoria> convocatorias = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    convocatorias = cdac.GetAllConvocatoriaMovimiento(idMovimiento);
                    ts.Complete();
                    Console.WriteLine("Convocatoriass Obtenidas");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return convocatorias;
        }

        public List<Convocatoria> ObtenerConvocatoriasPaginando(int pageNumber, int pageSize)
        {
            Console.WriteLine("Obteneniendo convocatorias paginando \nnumero de pagina = " + pageNumber + "\ntamaño de pagina = " + pageSize);
            List<Convocatoria> convocatorias = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    convocatorias = cdac.GetConvocatoriasPaginado(pageNumber, pageSize);
                    ts.Complete();
                    Console.WriteLine("Convocatorias Obtenidas");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return convocatorias;
        }

        private List<Convocatoria> GetConvocatoriasPaginando(int pageNumber, int pageSize)
        {
            try
            {
                return cdac.GetConvocatoriasPaginado(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<CategoriaTematica> ObtenerCategoriasTematicas()
        {
            CategoriaTematicaDAC ctdac = new CategoriaTematicaDAC();
            try
            {
                return ctdac.GetAllCategoriaTematica();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }


    }
}
