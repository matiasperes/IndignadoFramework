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
    public class FuenteWEBDAC 
    {

        private const string ENTITY_SET_NAME = "FuentesWEBSet";
        
        #region CRUD - Create Read Update Delete

        public FuenteWEB AddFuenteWEB(FuenteWEB fuenteWEB)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.FuenteWEBSet.AddObject(fuenteWEB);
                ctx.SaveChanges();
            }

            return fuenteWEB;
        }

        public void DeleteFuenteWEB(int fuenteWEBId)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalFuenteWEB = ctx.FuenteWEBSet.Where(u => u.Id == fuenteWEBId).FirstOrDefault();

                if (originalFuenteWEB != null)
                {
                    ctx.FuenteWEBSet.DeleteObject(originalFuenteWEB);
                    ctx.SaveChanges();
                }
            }
        }

        public void UpdateFuenteWEB(FuenteWEB fuenteWEB)
        {
            if (fuenteWEB == null) throw new ArgumentNullException(ENTITY_SET_NAME);
            using (var ctx = new ContextoIndignadoFramework())
            {
                var key = ctx.CreateEntityKey(ENTITY_SET_NAME, fuenteWEB);
                object original = null;
                if (ctx.TryGetObjectByKey(key, out original))
                {
                    ctx.ApplyCurrentValues(key.EntitySetName, fuenteWEB);
                }
                ctx.SaveChanges();
            }
        }

        public ICollection<FuenteWEB> GetFuenteWEBsPaginado(int pageNumber, int pageSize)
        {
            ICollection<FuenteWEB> fuenteWEBs = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.FuenteWEBSet.MergeOption = MergeOption.NoTracking;

                fuenteWEBs = ctx.FuenteWEBSet.OrderBy(u => u.Id).Skip(pageSize * pageNumber).Take(pageSize).ToList();
            }
            return fuenteWEBs;
        }

        public ICollection<FuenteWEB> GetAllFuenteWEB()
        {
            ICollection<FuenteWEB> fuenteWEBs = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.FuenteWEBSet.MergeOption = MergeOption.NoTracking;

                fuenteWEBs = ctx.FuenteWEBSet.ToList();
            }
            return fuenteWEBs;
        }
        
        #endregion


    }
}
