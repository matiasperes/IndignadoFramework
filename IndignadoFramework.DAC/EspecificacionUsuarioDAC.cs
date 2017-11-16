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
    public class EspecificacionUsuarioDAC 
    {

        private const string ENTITY_SET_NAME = "EspecificacionUsuarioSet";
        
        #region CRUD - Create Read Update Delete

        public EspecificacionUsuario SelectForId(int id)
        {
            EspecificacionUsuario usuarioRes = null;

            using (var ctx = new ContextoIndignadoFramework())
            {

                // Si no encuentra un unico usuario el metodo Single tira InvalidOperationException
                usuarioRes = ctx.EspecificacionUsuarioSet.Single(c => c.Id == id);
            }

            return usuarioRes;
        }

        public EspecificacionUsuario AddEspecificacionUsuario(EspecificacionUsuario especificacionUsuario, String[] categs)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.EspecificacionUsuarioSet.AddObject(especificacionUsuario);
                ctx.SaveChanges();  
            }

            if (categs != null)
            {
                UpdateEspecificacionUsuario(especificacionUsuario, categs);
            }

            return especificacionUsuario;
        }

        public void DeleteEspecificacionUsuario(int especificacionUsuarioId)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalEspecificacionUsuario = ctx.EspecificacionUsuarioSet.Include("Contenido").Include("CategoriasTematicas").Include("AsisteAConvocatorias").Where(u => u.Id == especificacionUsuarioId).FirstOrDefault();

                if (originalEspecificacionUsuario != null)
                {
                    List<CategoriaTematica> aux = new List<CategoriaTematica>();
                    foreach (var cat in originalEspecificacionUsuario.CategoriasTematicas)// remuevo las categorias tematicas anteriores
                    {
                        aux.Add(cat);
                    }
                    foreach (var cat in aux)// remuevo las categorias tematicas anteriores
                    {
                        originalEspecificacionUsuario.CategoriasTematicas.Remove(cat);
                    }
                    Console.WriteLine("Paso 4");
                    ctx.SaveChanges();

                    List<Contenido> contenidos = ctx.ContenidoSet.Include("Megusta").Include("Inadecuados").Where(u => u.EspecificacionUsuarioId == originalEspecificacionUsuario.Id).ToList();
                    foreach (var contenido in contenidos) 
                    {
                        List<Megusta> megustas = contenido.Megusta.ToList();
                        foreach (var megusta in megustas)
                        {
                            megusta.Contenido.CantMeGusta -= 1;
                            ctx.MegustaSet.DeleteObject(megusta);
                        }
                        Console.WriteLine("Paso 1");
                        List<EspecificacionUsuario> inadecuados = contenido.Inadecuados.ToList();
                        foreach (var inadecuado in inadecuados)
                        {
                            inadecuado.Contenido.Remove(contenido);
                            contenido.Inadecuados.Remove(inadecuado);
                        }
                        Console.WriteLine("Paso 2");
                        ctx.ContenidoSet.DeleteObject(contenido);
                        Console.WriteLine("Paso 3");
                    }
                    List<Convocatoria> convs = originalEspecificacionUsuario.AsisteAConvocatorias.ToList();
                    foreach (var conv in convs)
                    {
                        conv.CantUsuariosConfirmados -= 1;
                        conv.UsuariosConfirmados.Remove(originalEspecificacionUsuario);
                    }
                    List<Contenido> conts = originalEspecificacionUsuario.Contenido.ToList();
                    foreach (var cont in conts)
                    {
                        cont.Inadecuado -= 1;
                        cont.Inadecuados.Remove(originalEspecificacionUsuario);
                    }
                    ctx.EspecificacionUsuarioSet.DeleteObject(originalEspecificacionUsuario);
                    ctx.SaveChanges();
                }
            }
        }

        public void UpdateEspecificacionUsuario(EspecificacionUsuario especificacionUsuario, String[] categs)
        {
            if (especificacionUsuario == null) throw new ArgumentNullException(ENTITY_SET_NAME);
            using (var ctx = new ContextoIndignadoFramework())
            {
                /*
                var key = ctx.CreateEntityKey(ENTITY_SET_NAME, especificacionUsuario);
                object original = null;
                if (ctx.TryGetObjectByKey(key, out original))
                {*/
                    EspecificacionUsuario espus = ctx.EspecificacionUsuarioSet.Include("CategoriasTematicas").Where(u => u.Id == especificacionUsuario.Id).Single();
                    espus.CategoriasTematicas.Clear();
                    
                    for (int i = 0; i < categs.Length; i++)
                    {
                        String nombre = categs[i];
                        var categoriaTematica = ctx.CategoriaTematicaSet.Where(o => o.Nombre == nombre).FirstOrDefault();
                        espus.CategoriasTematicas.Add(categoriaTematica);
                    }
                    if (especificacionUsuario.Nombre != null)
                        espus.Nombre = especificacionUsuario.Nombre;
                    if(especificacionUsuario.UbicacionLatitud != null)
                        espus.UbicacionLatitud = especificacionUsuario.UbicacionLatitud;
                    if (especificacionUsuario.UbicacionLongitud != null)
                        espus.UbicacionLongitud = especificacionUsuario.UbicacionLongitud;
                    ctx.EspecificacionUsuarioSet.Attach(espus);
                    ctx.ObjectStateManager.ChangeObjectState(espus, EntityState.Modified);
                    ctx.SaveChanges();
                /*}*/
                ctx.SaveChanges();
            }
        }

        public ICollection<EspecificacionUsuario> GetEspecificacionUsuariosPaginado(int pageNumber, int pageSize)
        {
            ICollection<EspecificacionUsuario> especificacionUsuarios = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.EspecificacionUsuarioSet.MergeOption = MergeOption.NoTracking;

                especificacionUsuarios = ctx.EspecificacionUsuarioSet.OrderBy(u => u.Id).Skip(pageSize * pageNumber).Take(pageSize).ToList();
            }
            return especificacionUsuarios;
        }

        public ICollection<EspecificacionUsuario> GetAllEspecificacionUsuario()
        {
            ICollection<EspecificacionUsuario> especificacionUsuarios = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.EspecificacionUsuarioSet.MergeOption = MergeOption.NoTracking;

                especificacionUsuarios = ctx.EspecificacionUsuarioSet.Include("CategoriasTematicas").ToList();
            }
            return especificacionUsuarios;
        }
        
        #endregion



        public EspecificacionUsuario GetEspecificacionUsuarioByNombre(string membership, int idMov)
        {
            EspecificacionUsuario espus = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.EspecificacionUsuarioSet.MergeOption = MergeOption.NoTracking;
                Console.WriteLine("entro: " + membership + " " + idMov);
                espus = ctx.EspecificacionUsuarioSet.Where(o => o.Membership == membership && o.MovimientoId == idMov).FirstOrDefault();
            }
            return espus;
        }
        /*
        public List<EspecificacionUsuario> GetAllEspecificacionUsuariosInadecuados(int idMov)
        {
            List<EspecificacionUsuario> especificacionUsuarios = null;
            List<EspecificacionUsuario> resespus = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.EspecificacionUsuarioSet.MergeOption = MergeOption.NoTracking;

                especificacionUsuarios = ctx.EspecificacionUsuarioSet.Include("Contenidos").Where(u => u.MovimientoId ==  idMov && u.Contenidos.Count > 0).ToList();
                foreach (var espus in especificacionUsuarios)
                {
                    
                }

            }
            return especificacionUsuarios;
        }*/
    }
}
