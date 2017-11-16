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
    public class MovimientoDAC 
    {

        private const string ENTITY_SET_NAME = "MovimientoSet";
        
        #region CRUD - Create Read Update Delete

        public Movimiento AddMovimiento(Movimiento movimiento)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MovimientoSet.AddObject(movimiento);
                ctx.SaveChanges();
            }

            return movimiento;
        }

        public void DeleteMovimiento(int movimientoId)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalMovimiento = ctx.MovimientoSet.Where(u => u.Id == movimientoId).FirstOrDefault();

                if (originalMovimiento != null)
                {
                    ctx.MovimientoSet.DeleteObject(originalMovimiento);
                    ctx.SaveChanges();
                }
            }
        }

        public void UpdateMovimiento(Movimiento movimiento)
        {
            if (movimiento == null) throw new ArgumentNullException(ENTITY_SET_NAME);
            using (var ctx = new ContextoIndignadoFramework())
            {
                var key = ctx.CreateEntityKey(ENTITY_SET_NAME, movimiento);
                object original = null;
                if (ctx.TryGetObjectByKey(key, out original))
                {
                    ctx.ApplyCurrentValues(key.EntitySetName, movimiento);
                }
                ctx.SaveChanges();
            }
        }
        public Movimiento GetMovimientoById(int id)
        {
            Movimiento movimiento = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MovimientoSet.MergeOption = MergeOption.NoTracking;

                movimiento = ctx.MovimientoSet.Include("FuentesWEB").Where(o => o.Id == id).FirstOrDefault();
            }
            return movimiento;
        }

        public Movimiento GetMovimientoByNombre(string nombre)
        {
            Movimiento movimiento = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MovimientoSet.MergeOption = MergeOption.NoTracking;

                movimiento = ctx.MovimientoSet.Where(o => o.Nombre == nombre).FirstOrDefault();
            }
            return movimiento;
        }

        public List<Movimiento> GetMovimientosPaginado(int pageNumber, int pageSize)
        {
            List<Movimiento> movimientos = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MovimientoSet.MergeOption = MergeOption.NoTracking;

                movimientos = ctx.MovimientoSet.OrderBy(u => u.Id).Skip(pageSize * pageNumber).Take(pageSize).ToList();
            }
            return movimientos;
        }

        public List<Movimiento> GetAllMovimiento()
        {
            List<Movimiento> movimientos = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MovimientoSet.MergeOption = MergeOption.NoTracking;

                movimientos = ctx.MovimientoSet.ToList();
            }
            return movimientos;
        }

        public List<FuenteWEB> GetFuentesWebMovimiento(int idMov)
        {
            List<FuenteWEB> fuentes = null;
            Movimiento movimiento = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.ConvocatoriaSet.MergeOption = MergeOption.NoTracking;

                movimiento = ctx.MovimientoSet.Where(o => o.Id == idMov).FirstOrDefault();

                ctx.LoadProperty(movimiento, m => m.FuentesWEB);

                fuentes = movimiento.FuentesWEB.ToList();
            }
            return fuentes;
        }
        
        #endregion


    }
}
