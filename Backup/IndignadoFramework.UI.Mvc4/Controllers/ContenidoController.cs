using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IndignadoFramework.Entities;

namespace IndignadoFramework.UI.Mvc4.Controllers
{
    public class ContenidoController : SiteController
    {
        public ActionResult ObtenerCompartirParcial()
        {
            return PartialView("_CompartirParcial");
        }


        public void ObtenerContenidoAsync(int combo, string filtro)
        {
            AsyncManager.OutstandingOperations.Increment();
            var front = new FrontOffice.FrontOfficeServiceClient();

            if(combo == 2 ){
                front.ObtenerContenidoMasRecienteCompleted += (s, e) =>
                {
                    AsyncManager.Parameters["items"] = e.Result;
                    AsyncManager.Parameters["exito"] = !e.Cancelled;
                    AsyncManager.OutstandingOperations.Decrement();
                };
                front.ObtenerContenidoMasRecienteAsync((int)Session["idMov"],filtro);
            }
            else if(combo == 1)
            {
                front.ObtenerContenidoMasRankeadoCompleted += (s, e) =>
                {
                    AsyncManager.Parameters["items"] = e.Result;
                    AsyncManager.Parameters["exito"] = !e.Cancelled;
                    AsyncManager.OutstandingOperations.Decrement();
                };
                front.ObtenerContenidoMasRankeadoAsync((int)Session["idMov"],filtro);
            }
        }

        public ActionResult ObtenerContenidoCompleted(Contenido[] items, bool exito)
        {
            if (exito)
                return PartialView("_ObtenerContenido",items);
            else
                return View("../Shared/Error"); 
        }

        //
        // GET: /Contenido/Create

        public void CreateAsync()
        {
            AsyncManager.OutstandingOperations.Increment();

            var front = new FrontOffice.FrontOfficeServiceClient();
            front.ObtenerCategoriasTematicasCompleted += (s, e) =>
            {
                AsyncManager.Parameters["cat"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ObtenerCategoriasTematicasAsync();
        }

        public ActionResult CreateCompleted(CategoriaTematica[] cat)
        {
            ViewBag.Categorias = new SelectList(cat, "Id", "Nombre");
            return View("CompartirContenido");
        }

        //
        // POST: /Contenido/Create

        [ActionName("Create"), HttpPost]
        public void CreatePostAsync(Contenido cont, HttpPostedFileBase uploadFile)
        {
            if (uploadFile == null || (uploadFile != null && uploadFile.ContentLength > 0))
            {
                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    MyStorageService _myStorageService = new MyStorageService();
                    _myStorageService.Upload("contenido" + (int)Session["idMov"], uploadFile);
                    cont.Ubicacion = "http://repoindignado.blob.core.windows.net/contenido" + (int)Session["idMov"] + "/" + uploadFile.FileName;
                }

                AsyncManager.OutstandingOperations.Increment();
                cont.EspecificacionUsuarioId = (int)Session["idUsr"];

                var front = new FrontOffice.FrontOfficeServiceClient();
                front.CompartirContenidoCompleted += (s, e) =>
                {
                    AsyncManager.Parameters["exito"] = !e.Cancelled;
                    AsyncManager.OutstandingOperations.Decrement();
                };

                front.CompartirContenidoAsync(cont);
            }
            else
                AsyncManager.Parameters["exito"] = false;

        }


        public ActionResult CreatePostCompleted(bool exito)
        {
            if (exito)
            {
                return RedirectToAction("Index","Home");
            }
            else
                return View("../Shared/Error");
        }

        public void ObtenerOpcionesContenidoAsync(int idCont)
        {

            AsyncManager.OutstandingOperations.Increment();
            var front = new FrontOffice.FrontOfficeServiceClient();

            front.GetOpcionesContenidoCompleted += (s, e) =>
            {
                ViewBag.IdContenido = idCont;
                ViewBag.MeGusta = e.Result.MeGusta;
                ViewBag.Inadecuado = e.Result.Inadecuado;
                ViewBag.CantMegusta = e.Result.CantMeGusta;
                AsyncManager.OutstandingOperations.Decrement();
            };

            front.GetOpcionesContenidoAsync((int)Session["idUsr"], idCont);

        }

        public ActionResult ObtenerOpcionesContenidoCompleted()
        {
            return PartialView("_OpcionesContenido");
        }


        public void MeGustaAsync(int idContenido)
        {
            var logueado = Session["logueado"];
            if (logueado != null)
            {
                if ((bool)logueado)
                {
                    AsyncManager.OutstandingOperations.Increment(2);
                    var front = new FrontOffice.FrontOfficeServiceClient();

                    front.AddMeGustaCompleted += (s, e) =>
                    {
                        AsyncManager.Parameters["idCont"] = idContenido;
                        AsyncManager.Parameters["exito"] = !e.Cancelled;
                        AsyncManager.OutstandingOperations.Decrement();
                        front.GetOpcionesContenidoAsync((int)Session["idUsr"], idContenido);
                    };
                    front.GetOpcionesContenidoCompleted += (s, e) =>
                    {
                        ViewBag.IdContenido = idContenido;
                        ViewBag.MeGusta = e.Result.MeGusta;
                        ViewBag.Inadecuado = e.Result.Inadecuado;
                        ViewBag.CantMegusta = e.Result.CantMeGusta;
                        AsyncManager.OutstandingOperations.Decrement();
                    };

                    front.AddMeGustaAsync((int)Session["idUsr"], idContenido);
                }
            }
        }

        public ActionResult MeGustaCompleted(bool exito)
        {
            if (exito)
                return PartialView("_OpcionesContenido");
            else
                return View("../Shared/Error"); 
        }

        public void InadecuadoAsync(int idContenido)
        {
            var logueado = Session["logueado"];
            if (logueado != null)
            {
                if ((bool)logueado)
                {
                    AsyncManager.OutstandingOperations.Increment(2);
                    var front = new FrontOffice.FrontOfficeServiceClient();

                    front.AddInadecuadoCompleted += (s, e) =>
                    {
                        AsyncManager.Parameters["idCont"] = idContenido;
                        AsyncManager.Parameters["exito"] = !e.Cancelled;
                        AsyncManager.OutstandingOperations.Decrement();
                        front.GetOpcionesContenidoAsync((int)Session["idUsr"], idContenido);
                    };
                    front.GetOpcionesContenidoCompleted += (s, e) =>
                    {
                        ViewBag.IdContenido = idContenido;
                        ViewBag.MeGusta = e.Result.MeGusta;
                        ViewBag.Inadecuado = e.Result.Inadecuado;
                        ViewBag.CantMegusta = e.Result.CantMeGusta;
                        AsyncManager.OutstandingOperations.Decrement();
                    };

                    front.AddInadecuadoAsync((int)Session["idUsr"], idContenido);
                }
            }
        }

        public ActionResult InadecuadoCompleted(bool exito)
        {
            if (exito)
                return PartialView("_OpcionesContenido");
            else
                return View("../Shared/Error");
        }



    }
}
