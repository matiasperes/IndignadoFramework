using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using IndignadoFramework.Entities;
using IndignadoFramework.Data;


namespace IndignadoFramework.DAC
{
    public class ContenidoDAC 
    {

        private const string ENTITY_SET_NAME = "ContenidoSet";
        
        #region CRUD - Create Read Update Delete

        public Contenido SelectById(int id)
        {
            Contenido cont = null;

            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.MergeOption = MergeOption.NoTracking;

                cont = ctx.ContenidoSet.Include("CategoriaTematica").Where(c => c.Id == id).FirstOrDefault();
            }

            return cont;
        }


        public Contenido AddContenido(Contenido contenido)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ContenidoSet.AddObject(contenido);
                ctx.SaveChanges();
            }

            return contenido;
        }

        public void DeleteContenido(int contenidoId)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalContenido = ctx.ContenidoSet.Where(u => u.Id == contenidoId).FirstOrDefault();

                if (originalContenido != null)
                {
                    ctx.ContenidoSet.DeleteObject(originalContenido);
                    ctx.SaveChanges();
                }
            }
        }

        public void UpdateContenido(Contenido contenido)
        {
            if (contenido == null) throw new ArgumentNullException(ENTITY_SET_NAME);
            using (var ctx = new ContextoIndignadoFramework())
            {
                var key = ctx.CreateEntityKey(ENTITY_SET_NAME, contenido);
                object original = null;
                if (ctx.TryGetObjectByKey(key, out original))
                {
                    ctx.ApplyCurrentValues(key.EntitySetName, contenido);
                }
                ctx.SaveChanges();
            }
        }

        public ICollection<Contenido> GetContenidosPaginado(int pageNumber, int pageSize)
        {
            ICollection<Contenido> contenidos = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ContenidoSet.MergeOption = MergeOption.NoTracking;

                contenidos = ctx.ContenidoSet.OrderBy(u => u.Id).Skip(pageSize * pageNumber).Take(pageSize).ToList();
            }
            return contenidos;
        }



        public ICollection<Contenido> GetAllContenido()
        {
            ICollection<Contenido> contenidos = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ContenidoSet.MergeOption = MergeOption.NoTracking;

                contenidos = ctx.ContenidoSet.Include("CategoriaTematica").ToList();
            }
            return contenidos;
        }

        public List<Contenido> GetContenidoMovimiento(int idMovimiento)
        {

            List<Contenido> contenidos = new List<Contenido>();
            
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ContenidoSet.MergeOption = MergeOption.NoTracking;

                var usuarios = (ctx.EspecificacionUsuarioSet.Where(u => u.MovimientoId == idMovimiento).Select(u => u.Id)).ToList();
                ICollection<Contenido> contenido = ctx.ContenidoSet.Include("CategoriaTematica").Include("EspecificacionUsuario").Where(u => u.Habilitado == true).ToList();

                contenidos =    (from content in contenido
                                 where usuarios.Contains(content.EspecificacionUsuarioId)
                                 select content).ToList();
            }
            return contenidos;
        }
        
        #endregion

        public List<Contenido> GetContenidosMovimientoPorInadecuado(int idMov)
        {
            List<Contenido> contenidos = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ContenidoSet.MergeOption = MergeOption.NoTracking;
                contenidos = ctx.ContenidoSet.Include("EspecificacionUsuario").Where(u => u.EspecificacionUsuario.MovimientoId == idMov && u.Inadecuado > 0).OrderByDescending(u => u.Inadecuado).ToList();
            }
            return contenidos;
        }

        public List<Contenido> GetContenidosInadecuados(int idMov, int cantInadecuados)
        {
            List<Contenido> contenidos = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ContenidoSet.MergeOption = MergeOption.NoTracking;
                contenidos = ctx.ContenidoSet.Include("EspecificacionUsuario").Where(u => u.Habilitado == true && u.EspecificacionUsuario.MovimientoId == idMov && u.Inadecuado >= cantInadecuados).OrderByDescending(u => u.Inadecuado).ToList();
            }
            return contenidos;
        }


        public void AddInadecuado(int idUsr, int idCont)
        {
            Contenido cont = null;
            EspecificacionUsuario usuarioRes = null;
            var ctx = new ContextoIndignadoFramework();
            cont = ctx.ContenidoSet.Include("Inadecuados").Where(c => c.Id == idCont).FirstOrDefault();

            cont.Inadecuado++;

            usuarioRes = ctx.EspecificacionUsuarioSet.Where(c => c.Id == idUsr).FirstOrDefault();

            cont.Inadecuados.Add(usuarioRes);

            ctx.ContenidoSet.Attach(cont);
            ctx.ObjectStateManager.ChangeObjectState(cont, EntityState.Modified);
            ctx.SaveChanges();

            /*
            var key = ctx.CreateEntityKey(ENTITY_SET_NAME, cont);
            object original = null;
            if (ctx.TryGetObjectByKey(key, out original))
            {
                ctx.ApplyCurrentValues(key.EntitySetName, cont);
            }
            ctx.SaveChanges();
            */
        }

        public OpcionesContenido GetOpcionesContenido(int idUsr, int id)
        {
            OpcionesContenido ocont = new OpcionesContenido();
            ocont.Inadecuado = true;
            ocont.MeGusta = true;
            Contenido cont;
            using (var ctx = new ContextoIndignadoFramework())
            {
                cont = ctx.ContenidoSet.Include("Megusta").Include("Inadecuados").Where(c => c.Id == id).FirstOrDefault();
                ocont.CantMeGusta = cont.Megusta.Count;
                foreach (EspecificacionUsuario eu in cont.Inadecuados)
                {
                    if (eu.Id == idUsr)
                        ocont.Inadecuado = false;
                }
                foreach (Megusta mg in cont.Megusta)
                {
                    if (mg.EspecificacionUsuarioId == idUsr)
                        ocont.MeGusta = false;
                }
            }
            return ocont;
        }



    }
}
