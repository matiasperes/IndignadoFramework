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
    public class MegustaDAC 
    {

        private const string ENTITY_SET_NAME = "MegustaSet";
        
        #region CRUD - Create Read Update Delete

        public Megusta AddMegusta(Megusta megusta)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MegustaSet.AddObject(megusta);
                ctx.SaveChanges();
            }

            return megusta;
        }

        public void DeleteMegusta(int megusta)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalMegusta = ctx.MegustaSet.Where(u => u.Id == megusta).FirstOrDefault();

                if (originalMegusta != null)
                {
                    ctx.MegustaSet.DeleteObject(originalMegusta);
                    ctx.SaveChanges();
                }
            }
        }

        public void UpdateMegusta(Megusta megusta)
        {
            if (megusta == null) throw new ArgumentNullException(ENTITY_SET_NAME);
            using (var ctx = new ContextoIndignadoFramework())
            {
                var key = ctx.CreateEntityKey(ENTITY_SET_NAME, megusta);
                object original = null;
                if (ctx.TryGetObjectByKey(key, out original))
                {
                    ctx.ApplyCurrentValues(key.EntitySetName, megusta);
                }
                ctx.SaveChanges();
            }
        }

        public ICollection<Megusta> GetMegustasPaginado(int pageNumber, int pageSize)
        {
            ICollection<Megusta> megustas = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MegustaSet.MergeOption = MergeOption.NoTracking;

                megustas = ctx.MegustaSet.OrderBy(u => u.Id).Skip(pageSize * pageNumber).Take(pageSize).ToList();
            }
            return megustas;
        }

        public ICollection<Megusta> GetAllSerMegustaSet()
        {
            ICollection<Megusta> megustas = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MegustaSet.MergeOption = MergeOption.NoTracking;

                megustas = ctx.MegustaSet.ToList();
            }
            return megustas;
        }


        #endregion


    }
}
