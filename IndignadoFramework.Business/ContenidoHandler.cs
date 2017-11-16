using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.DAC;
using IndignadoFramework.Entities;
using System.Transactions;

namespace IndignadoFramework.Business
{
    public class ContenidoHandler
    {
        ContenidoDAC cdac = new ContenidoDAC();
        MegustaDAC mgdac = new MegustaDAC();

        public Contenido ObtenerContenidoXId(int idContenido)
        {
            Console.WriteLine("Obtener contenido con ID" + idContenido);
            Contenido contenido = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    contenido = cdac.SelectById(idContenido);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Contenido Obtenido");

            return contenido;
        }


        public void CompartirContenido(Contenido contenido)
        {
            contenido.Inadecuado = 0;
            contenido.FechaPosteado = DateTime.Now;
            contenido.CantMeGusta = 0;
            contenido.Tipo = "Tipo";
            contenido.Habilitado = true;
            Console.WriteLine("Compartiendo contenido...");
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    cdac.AddContenido(contenido);
                    ts.Complete();
                    Console.WriteLine("Contenido compartido");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AddInadecuado(int idUsr, int idContenido)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    cdac.AddInadecuado(idUsr, idContenido);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AddMeGusta(int idUsr, int idContenido)
        {
            Megusta megusta = new Megusta { ContenidoId = idContenido, Fecha = DateTime.Now, EspecificacionUsuarioId = idUsr};
            Contenido cont = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    mgdac.AddMegusta(megusta);
                    cont = cdac.SelectById(idContenido);
                    cont.CantMeGusta++;
                    cdac.UpdateContenido(cont);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public OpcionesContenido GetOpcionesContenido(int idUsr, int idContenido)
        {
            OpcionesContenido ocont = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    ocont = cdac.GetOpcionesContenido(idUsr,idContenido);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ocont;
        }

        public List<Contenido> ObtenerContenidoMasRankeado(int idMovimiento, string filtro)
        {
            Console.WriteLine("Obtener contenido mas rankeado para " + idMovimiento);
            List<Contenido> contenidos = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    contenidos = cdac.GetContenidoMovimiento(idMovimiento);
                    ts.Complete();
                    Console.WriteLine("Contenido Obtenido");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            MovimientoDAC mdac = new MovimientoDAC();
            Movimiento movimiento = mdac.GetMovimientoById(idMovimiento);
            int cantidad = movimiento.N;
            if(filtro.Equals(""))
                return contenidos.OrderByDescending(content => content.Megusta.Count).Take(cantidad).ToList();
            else
                return contenidos.Where(content => content.Titulo.ToLower().Contains(filtro.ToLower())).OrderByDescending(content => content.CantMeGusta).Take(cantidad).ToList();
        }

        public List<Contenido> ObtenerContenidoMasReciente(int idMovimiento, string filtro)
        {
            Console.WriteLine("Obtener contenido mas reciente para " + idMovimiento);
            List<Contenido> contenidos = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    contenidos = cdac.GetContenidoMovimiento(idMovimiento);
                    ts.Complete();
                    Console.WriteLine("Contenido Obtenido");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            MovimientoDAC mdac = new MovimientoDAC();
            Movimiento movimiento = mdac.GetMovimientoById(idMovimiento);
            int cantidad = movimiento.M;
            if (filtro.Equals(""))
                return contenidos.OrderByDescending(content => content.FechaPosteado).Take(cantidad).ToList();
            else
                return contenidos.Where(content => content.Titulo.ToLower().Contains(filtro.ToLower())).OrderBy(content => content.FechaPosteado).Take(cantidad).ToList();
        }

        public List<Contenido> ObtenerContenidosMovimientoPorInadecuado(int idMovimiento)
        {
            Console.WriteLine("Obtener contenidos ordenados por inadecuado del movimiento " + idMovimiento);
            List<Contenido> contenidos = null;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    contenidos = cdac.GetContenidosMovimientoPorInadecuado(idMovimiento);
                    Console.WriteLine("Contenidos inadecuados obtenidos");
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return contenidos;

        }
    }
}
