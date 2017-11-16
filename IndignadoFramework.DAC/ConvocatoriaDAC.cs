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
    public class ConvocatoriaDAC 
    {

        private const string ENTITY_SET_NAME = "ConvocatoriaSet";
        
        #region CRUD - Create Read Update Delete

        public Convocatoria SelectForId(int id)
        {
            Convocatoria convocatoriaRes = null;

            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.MergeOption = MergeOption.NoTracking;

                convocatoriaRes = ctx.ConvocatoriaSet.Include("CategoriaTematica").Where(o => o.Id == id).FirstOrDefault();
            }

            return convocatoriaRes;
        }

        public Convocatoria AddConvocatoria(Convocatoria convocatoria)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.AddObject(convocatoria);
                ctx.SaveChanges();
            }

            return convocatoria;
        }

        public void DeleteConvocatoria(int convocatoriaId)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalConvocatoria = ctx.ConvocatoriaSet.Where(u => u.Id == convocatoriaId).FirstOrDefault();

                if (originalConvocatoria != null)
                {
                    ctx.ConvocatoriaSet.DeleteObject(originalConvocatoria);
                    ctx.SaveChanges();
                }
            }
        }

        public void UpdateConvocatoria(Convocatoria convocatoria)
        {
            if (convocatoria == null) throw new ArgumentNullException(ENTITY_SET_NAME);
            using (var ctx = new ContextoIndignadoFramework())
            {
                var key = ctx.CreateEntityKey(ENTITY_SET_NAME, convocatoria);
                object original = null;
                if (ctx.TryGetObjectByKey(key, out original))
                {
                    ctx.ApplyCurrentValues(key.EntitySetName, convocatoria);
                }
                ctx.SaveChanges();
            }
        }

        #endregion

        public List<Convocatoria> GetConvocatoriasPaginado(int pageNumber, int pageSize)
        {
            List<Convocatoria> convocatorias = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.MergeOption = MergeOption.NoTracking;

                convocatorias = ctx.ConvocatoriaSet.OrderBy(u => u.Id).Skip(pageSize * pageNumber).Take(pageSize).ToList();
            }
            return convocatorias;
        }

        public List<Convocatoria> GetAllConvocatoria()
        {
            List<Convocatoria> convocatorias = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.MergeOption = MergeOption.NoTracking;

                convocatorias = ctx.ConvocatoriaSet.ToList();
            }
            return convocatorias;
        }

        public List<Convocatoria> GetAllConvocatoriaExpired()
        {
            List<Convocatoria> convocatorias = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.MergeOption = MergeOption.NoTracking;
                DateTime d1 = DateTime.Now;
                d1 = d1.AddHours(-3);
                DateTime d2 = d1.AddHours(24);
                convocatorias = ctx.ConvocatoriaSet.Where(u => u.Suspendida == false && (u.Inicio  > d1) && (u.Inicio < d2) && (u.Quorum > u.CantUsuariosConfirmados)).ToList();
            }
            return convocatorias;
        }



        public List<Convocatoria> GetAllConvocatoriaMovimiento(int idMov)
        {
            List<Convocatoria> convocatorias = null;
            Movimiento movimiento = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.MergeOption = MergeOption.NoTracking;

                movimiento = ctx.MovimientoSet.Where(o => o.Id == idMov).FirstOrDefault();

                ctx.LoadProperty(movimiento, m => m.Convocatorias);

                convocatorias = movimiento.Convocatorias.ToList();
            }
            return convocatorias;
        }


        public int AddAsistenciaConvocatoria(int idConvocatoria, int idUsuario)
        {
            EspecificacionUsuario eu = null;
            Convocatoria conv = null;
            var ctx = new ContextoIndignadoFramework();
            eu      = ctx.EspecificacionUsuarioSet.Where(c => c.Id == idUsuario).FirstOrDefault();
            conv    = ctx.ConvocatoriaSet.Where(o => o.Id == idConvocatoria).FirstOrDefault();
            conv.UsuariosConfirmados.Add(eu);
            conv.CantUsuariosConfirmados++;

            ctx.ConvocatoriaSet.Attach(conv);
            ctx.ObjectStateManager.ChangeObjectState(conv, EntityState.Modified);
            ctx.SaveChanges();

            /*
            var key = ctx.CreateEntityKey(ENTITY_SET_NAME, conv);
            object original = null;
            if (ctx.TryGetObjectByKey(key, out original))
            {
                ctx.ApplyCurrentValues(key.EntitySetName, conv);
            }
            ctx.SaveChanges();
             * */
            return conv.CantUsuariosConfirmados;
        }


        public bool ConfirmoAsistenciaUsuario(int idConvocatoria, int idUsuario)
        {
            Convocatoria conv = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                conv = ctx.ConvocatoriaSet.Include("UsuariosConfirmados").Where(o => o.Id == idConvocatoria).FirstOrDefault();
                EspecificacionUsuario eu = ctx.EspecificacionUsuarioSet.Where(c => c.Id == idUsuario).FirstOrDefault();
                return conv.UsuariosConfirmados.Contains(eu);
            }
        }
    }
}
