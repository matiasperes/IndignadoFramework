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
    public class CategoriaTematicaDAC 
    {

        private const string ENTITY_SET_NAME = "CategoriaTematicaSet";
        
        #region CRUD - Create Read Update Delete

        public CategoriaTematica SelectForId(int id)
        {
            CategoriaTematica categoria = null;

            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.MergeOption = MergeOption.NoTracking;

                categoria = ctx.CategoriaTematicaSet.Where(o => o.Id == id).FirstOrDefault();
            }

            return categoria;
        }

        public CategoriaTematica AddCategoriaTematica(CategoriaTematica categoriaTematica)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.CategoriaTematicaSet.AddObject(categoriaTematica);
                ctx.SaveChanges();
            }

            return categoriaTematica;
        }

        public void DeleteCategoriaTematica(int categoriaTematicaId)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalCategoriaTematica = ctx.CategoriaTematicaSet.Where(u => u.Id == categoriaTematicaId).FirstOrDefault();

                if (originalCategoriaTematica != null)
                {
                    ctx.CategoriaTematicaSet.DeleteObject(originalCategoriaTematica);
                    ctx.SaveChanges();
                }
            }
        }

        public void UpdateCategoriaTematica(CategoriaTematica categoriaTematica)
        {
            if (categoriaTematica == null) throw new ArgumentNullException(ENTITY_SET_NAME);
            using (var ctx = new ContextoIndignadoFramework())
            {
                var key = ctx.CreateEntityKey(ENTITY_SET_NAME, categoriaTematica);
                object original = null;
                if (ctx.TryGetObjectByKey(key, out original))
                {
                    ctx.ApplyCurrentValues(key.EntitySetName, categoriaTematica);
                }
                ctx.SaveChanges();
            }
        }

        public CategoriaTematica GetCategoriaTematicaByNombre(string nombre)
        {
            CategoriaTematica categoriaTematica = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.CategoriaTematicaSet.MergeOption = MergeOption.NoTracking;

                categoriaTematica = ctx.CategoriaTematicaSet.Where(o => o.Nombre == nombre).FirstOrDefault();
            }
            return categoriaTematica;
        }

        public ICollection<CategoriaTematica> GetCategoriaTematicasPaginado(int pageNumber, int pageSize)
        {
            ICollection<CategoriaTematica> categoriaTematicas = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.CategoriaTematicaSet.MergeOption = MergeOption.NoTracking;

                categoriaTematicas = ctx.CategoriaTematicaSet.OrderBy(u => u.Id).Skip(pageSize * pageNumber).Take(pageSize).ToList();
            }
            return categoriaTematicas;
        }

        public List<CategoriaTematica> GetAllCategoriaTematica()
        {
            List<CategoriaTematica> categoriaTematicas = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.CategoriaTematicaSet.MergeOption = MergeOption.NoTracking;

                categoriaTematicas = ctx.CategoriaTematicaSet.ToList();
            }
            return categoriaTematicas;
        }
        
        #endregion


    }
}
