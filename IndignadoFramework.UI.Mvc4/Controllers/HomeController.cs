using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.WindowsAzure.StorageClient;
using IndignadoFramework.Entities;

namespace IndignadoFramework.UI.Mvc4.Controllers
{
    public class HomeController : SiteController
    {

        MyStorageService _myStorageService = new MyStorageService();

        public void IndexAsync()
        {
            if ((int)Session["idMov"] != 0)
            {
                AsyncManager.OutstandingOperations.Increment();

                var front = new FrontOffice.FrontOfficeServiceClient();
                front.ObtenerConvocatoriasMovimientoCompleted += (s, e) =>
                {
                    AsyncManager.Parameters["items"] = e.Result;
                    AsyncManager.Parameters["items2"] = null;
                    AsyncManager.OutstandingOperations.Decrement();
                };
                front.ObtenerConvocatoriasMovimientoAsync((int)Session["idMov"]);
                bool logueado = ((Session["logueado"] != null && (bool)Session["logueado"]) && Membership.FindUsersByName((String)Session["emailUs"]).Count > 0);
                if (logueado) {
                    AsyncManager.OutstandingOperations.Increment();
                    var chatService = new ChatService.IntegracionServiceClient();
                    chatService.obtenerMensajesCompleted += (s, e) =>
                    {
                        Session["chatMessages"] = e.Result;

                        AsyncManager.OutstandingOperations.Decrement();
                        chatService.Close();
                    };
                    chatService.obtenerMensajesAsync((int)Session["idMov"]);
                }                
            }
            else
            {
                AsyncManager.Parameters["items"] = null;
                AsyncManager.Parameters["items2"] = null;
            }
        }


        public ActionResult IndexCompleted(Convocatoria[] items, Movimiento[] items2)
        {

            if ((int)Session["idMov"] != 0)
            {
                ViewBag.Convocatorias = items;

                List<CloudBlob> blobs;
                bool ok = _myStorageService.ListBlobs("imagenes" + (int)Session["idMov"], out blobs);

                //CloudBlobContainer blobContainer = _myStorageService.GetStorageContainer("pictures");
                //List<string> blobs = new List<string>();
                //foreach (var blobItem in blobContainer.ListBlobs())
                //{
                //    blobs.Add(blobItem.Uri.ToString());
                //}

                return View(blobs);
            }
            else
            {
                Membership.ApplicationName = this.Site;
                Roles.ApplicationName = this.Site;
                if (!Roles.RoleExists("AdministradorGeneral"))
                {
                    MembershipCreateStatus createStatus;
                    Roles.CreateRole("AdministradorGeneral");
                    Membership.CreateUser("adming@indignado.com", "12345678", "adming@indignado.com", passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                    Roles.AddUserToRole("adming@indignado.com", "AdministradorGeneral");
                    return RedirectToAction("Login","BackOffice");
                }

                else
                {
                    if (Membership.GetUser() != null)//el usuario es administrador
                    {
                        return View();
                    }
                    else
                    {
                        var logueado = Session["logueado2"];
                        if (logueado != null && (bool)logueado)
                            return View();
                        else// el usuario esta logueado
                            return RedirectToAction("Login", "BackOffice");

                        /*
                        string nommov = (string)Session["nommov2"];
                        if (nommov != null)//verifico q no sea null
                        {
                            Membership.ApplicationName = nommov;
                            Roles.ApplicationName = nommov;
                            if (Membership.GetUser() == null)
                                return RedirectToAction("Login", "BackOffice");
                            else// el usuario esta logueado
                                return View();
                        }
                        else
                            return RedirectToAction("Login", "BackOffice");*/
                    }
                }
               // return View();
            }
        }


        [HttpPost]
        [ActionName("index")]
        public ActionResult _indexPost(HttpPostedFileBase fileBase)
        {
            if (fileBase.ContentLength > 0)
            {
                _myStorageService.Upload("imagenes"+(int)Session["idMov"], fileBase);
            }

            return RedirectToAction("index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }


        public ActionResult DeleteImg(string id)
        {
            _myStorageService.DeleteBlob("imagenes" + (int)Session["idMov"], id);
            return RedirectToAction("index");
        }

        public ActionResult ObtenerGaleriaImagenes()
        {
            List<CloudBlob> blobs;
            bool ok = _myStorageService.ListBlobs("imagenes" + (int)Session["idMov"], out blobs);
            ViewBag.Galeria = blobs;
            return PartialView("../Shared/_GaleriaImagenes", blobs);
        }

        public void ObtenerMapaAsync()
        {
            AsyncManager.OutstandingOperations.Increment();

            var front = new FrontOffice.FrontOfficeServiceClient();
            front.ObtenerConvocatoriasMovimientoCompleted += (s, e) =>
            {
                AsyncManager.Parameters["items"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ObtenerConvocatoriasMovimientoAsync((int)Session["idMov"]);
        }

        public ActionResult ObtenerMapaCompleted(Convocatoria[] items)
        {
            ViewBag.Convocatorias = items;
            return PartialView("_Mapa");
        }

    }
}
