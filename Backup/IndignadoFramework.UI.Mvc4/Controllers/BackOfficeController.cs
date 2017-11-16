using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Security;
using IndignadoFramework.UI.Mvc4.Models;
using IndignadoFramework.UI.Mvc4.FrontOffice;
using IndignadoFramework.Entities;
using System.Web.UI.DataVisualization.Charting;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using System.IO;

namespace IndignadoFramework.UI.Mvc4.Controllers
{
    public class BackOfficeController : SiteController
    {
        //
        // GET: /BackOffice/

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public void LoginAsync()
        {

        }

        [AllowAnonymous]
        public ActionResult LoginCompleted()
        {
            return View();
        }

        //
        // POST: /Account/JsonLogin
        public void LogOffAsync()
        {

        }

        public ActionResult LogOffCompleted()
        {
            FormsAuthentication.SignOut();
            Session["logueado2"] = false;

            return RedirectToAction("Index", "Home");
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public JsonResult JsonLogin(LoginModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        if (Request["admingen"] == "true,false")
        //        {
        //            Membership.ApplicationName = this.Site;
        //            Roles.ApplicationName = this.Site;
        //            if (Roles.IsUserInRole(model.nombre, "AdministradorGeneral"))
        //            {
        //                if (Membership.ValidateUser(model.nombre, model.contraseña))
        //                {
        //                    FormsAuthentication.SetAuthCookie(model.nombre, model.RememberMe);
        //                    Session["logueado2"] = true;
        //                    Session["emailUs2"] = model.nombre;
        //                    return Json(new { success = true, redirect = returnUrl });
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos.");
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "El usuario ingresado no es administrador");
        //            }
        //        }
        //        else
        //        {
        //            string nommov = Request["nommov"];
        //            Session["nommov2"] = nommov;
        //            Membership.ApplicationName = nommov;
        //            Roles.ApplicationName = nommov;
        //            if (Roles.IsUserInRole(model.nombre, "Administrador"))
        //            {
        //                if (Membership.ValidateUser(model.nombre, model.contraseña))
        //                {
        //                    FormsAuthentication.SetAuthCookie(model.nombre, model.RememberMe);
        //                    Session["logueado2"] = true;
        //                    Session["emailUs2"] = model.nombre;
        //                    return Json(new { success = true, redirect = returnUrl });
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos.");
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "El usuario ingresado no es administrador");
        //            }
        //        }
        //    }

        //    // If we got this far, something failed
        //    return Json(new { errors = GetErrorsFromModelState() });
        //}

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var tipo = Request["Administradores"];
                if (tipo == "1")
                {
                    Membership.ApplicationName = this.Site;
                    Roles.ApplicationName = this.Site;
                    if (Roles.IsUserInRole(model.nombre, "AdministradorGeneral"))
                    {
                        if (Membership.ValidateUser(model.nombre, model.contraseña))
                        {
                            FormsAuthentication.SetAuthCookie(model.nombre, model.RememberMe);
                            Session["logueado2"] = true;
                            Session["emailUs2"] = model.nombre;
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "El usuario ingresado no es administrador");
                    }
                }
                else if (tipo == "2")
                {

                    string nommov = Request["nommov"];
                    Session["nommov2"] = nommov;
                    Membership.ApplicationName = nommov;
                    Roles.ApplicationName = nommov;
                    if (Roles.IsUserInRole(model.nombre, "Administrador"))
                    {
                        if (Membership.ValidateUser(model.nombre, model.contraseña))
                        {
                            FormsAuthentication.SetAuthCookie(model.nombre, model.RememberMe);
                            Session["logueado2"] = true;
                            Session["emailUs2"] = model.nombre;
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "El usuario ingresado no es administrador");
                    }
                }
                else
                    ModelState.AddModelError("", "Elija una opcion de administrador.");
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /*
        [AllowAnonymous]
        public ActionResult RegisterAdminGeneral()
        {
            RegisterModel mod = new RegisterModel { latitud = -35, longitud = -55 };
            return View(mod);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterAdminGeneral(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.ApplicationName = this.Site;
                Roles.ApplicationName = this.Site;
                if (!Roles.RoleExists("AdministradorGeneral"))
                    Roles.CreateRole("AdministradorGeneral");
                Membership.CreateUser(model.Email, model.contraseña, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(model.Email, "AdministradorGeneral");
                    FormsAuthentication.SetAuthCookie(model.Email, createPersistentCookie: false);
                    Session["logueado2"] = true;
                    Session["emailUs2"] = model.nombre;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Home");
        }*/

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }



        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Ya existe un nombre de usuario para el mail dado.";

                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña es invalida.";

                case MembershipCreateStatus.InvalidEmail:
                    return "El email es invalido.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "El mail ya esta registrado en el sistema.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "Ha ocurrido un error inesperado, verifique sus datos y vuelva a intentarlo.";
            }
        }
        #endregion
    }
}
