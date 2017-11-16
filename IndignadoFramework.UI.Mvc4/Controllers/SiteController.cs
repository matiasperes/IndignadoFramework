using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using IndignadoFramework.Entities;




namespace IndignadoFramework.UI.Mvc4.Controllers
{
    [HandleError]
    public abstract class SiteController : AsyncController
    {
        private Movimiento _site;
        private string _controller;

        public SiteController()
        {
            //_site = new Site();
        }


        protected override void Initialize(RequestContext requestContext)
        {
            
            //string host = ;
            //string[] words = host.;
            //_site = words[1];
            var mov = requestContext.HttpContext.Request.Path.Split('/')[1];
            if (mov.Equals("Backoffice"))
            {
                _site = new Movimiento();
                _site.Nombre = "Backoffice";
            }
            else
            {                
                var back = new BackOffice.BackOfficeServiceClient();
                _site = back.ObtenerMovimientoXNombre(mov);

                if (requestContext.HttpContext.Request.Path.Split('/').Length > 2)
                    _controller = requestContext.HttpContext.Request.Path.Split('/')[2];
                else
                    _controller = null;
            }

            base.Initialize(requestContext);
        }

        //public ActionResult Error()
        //{
        //    return View("../Shared/Error");
        //}

        private void SetearParametrosSesion(Movimiento mov)
        {
            string vnom = (String)Session["nomMov"];
            if (vnom != null && vnom != mov.Nombre)// si cambio de movimiento deslogueo al usuario
            {
                Session["logueado"] = false;
                if (Request.IsAuthenticated)
                    FormsAuthentication.SignOut();
            }
            Session["idMov"] = mov.Id;
            Session["nomMov"] = mov.Nombre;
            Session["cssMov"] = mov.Estilo;
            Session["latitudMov"] = mov.UbicacionLatitud;
            Session["longitudMov"] = mov.UbicacionLongitud;
            Session["logMov"] = mov.Logo;
            Membership.ApplicationName = mov.Nombre;
            Roles.ApplicationName = mov.Nombre;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Session["idMov"] == null || _site == null)//(int)Session["idMov"] != mov.Id)
            {
                if (_site == null)
                {
                    filterContext.HttpContext.Response.Clear(); // gets rid of any garbage
                    filterContext.Result = View("Error", null);
                    Session["idMov"] = null;
                    //ViewBag.ErrorMessage = "Url mala.";
                }
                else
                {

                    if (_site.Nombre.Equals("Backoffice"))
                    {
                        Session["idMov"] = 0;
                        Session["nomMov"] = "Backoffice";
                        //if (_controller != null)
                        //{
                        //    if (!_controller.Equals("Movimiento"))
                        //    {
                        //        RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        //        redirectTargetDictionary.Add("action", "Index");
                        //        redirectTargetDictionary.Add("controller", "Error");
                        //        filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                        //    }
                        //}
                        //else
                        //{
                        //    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        //    redirectTargetDictionary.Add("action", "Index");
                        //    redirectTargetDictionary.Add("controller", "Error");
                        //    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                        //}
                    }
                    else
                    {
                        //var back = new BackOffice.BackOfficeServiceClient();
                        //Movimiento mov = back.ObtenerMovimientoXNombre(_site);

                        //if (mov == null)
                        //{
                        //    //Session["idMov"] = -1;
                        //    //filterContext.HttpContext.Response.Redirect("acceso/Error");

                        //    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        //    redirectTargetDictionary.Add("action", "Index");
                        //    redirectTargetDictionary.Add("controller", "Error");
                        //    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);

                        //}
                        //else
                        //{
                        SetearParametrosSesion(_site);
                        //}

                    }
                }
            }
            else
            {
                if (!_site.Nombre.Equals((string)Session["nomMov"]))
                {

                    if (_site.Equals("Backoffice"))
                    {
                        Session["idMov"] = 0;
                        Session["nomMov"] = "Backoffice";
                        //if (_controller != null)
                        //{
                        //    if (!_controller.Equals("Movimiento"))
                        //    {
                        //        RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        //        redirectTargetDictionary.Add("action", "Index");
                        //        redirectTargetDictionary.Add("controller", "Error");
                        //        filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                        //    }
                        //}
                        //else
                        //{
                        //    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        //    redirectTargetDictionary.Add("action", "Index");
                        //    redirectTargetDictionary.Add("controller", "Error");
                        //    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                        //}
                    }
                    else
                    {


                        //var back = new BackOffice.BackOfficeServiceClient();
                        //Movimiento mov = back.ObtenerMovimientoXNombre(_site);
                        SetearParametrosSesion(_site);
                    }
                }

            }

            base.OnActionExecuting(filterContext);
            
        }


        //private bool AccesoCorrecto()
        //{
        //    if ((int)Session["idMov"] < 1)
        //        return false;
        //    else
        //        return true;
        //}


        public string Site
        {
            get
            {
                return _site.Nombre;
            }
        }

    }
}