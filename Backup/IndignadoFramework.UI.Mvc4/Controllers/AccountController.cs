using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Security;
using IndignadoFramework.UI.Mvc4.Models;
using IndignadoFramework.UI.Mvc4.FrontOffice;
using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using IndignadoFramework.Entities;
using System.Net.Mail;


namespace IndignadoFramework.UI.Mvc4.Controllers
{

    [Authorize]
    public class AccountController : SiteController
    {

        //
        // GET: /Account/Login
        #region Membership

        [AllowAnonymous]
        public void LoginAsync()
        {

        }

        [AllowAnonymous]
        public ActionResult LoginCompleted()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                Membership.ApplicationName = this.Site;
                Roles.ApplicationName = this.Site;
                if (Membership.ValidateUser(model.nombre, model.contraseña))
                {
                    FormsAuthentication.SetAuthCookie(model.nombre, model.RememberMe);
                    int idmov = (int)Session["idMov"];
                    SetearLogueoSessionAsync(model.nombre, idmov);
                    Session["nommov2"] = this.Site;
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
                    ModelState.AddModelError("", "El usuario o contraseña son incorrectos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public void SetearLogueoSessionAsync(String membership, int idmov) 
        {
            AsyncManager.OutstandingOperations.Increment();
            var front = new FrontOffice.FrontOfficeServiceClient();

            front.ObtenerEspecificacionUsuarioXMembershipCompleted += (s, e) =>
            {
                AsyncManager.Parameters["espus"] = e.Result;
                if (e.Result != null)
                {
                    Session["idUsr"] = e.Result.Id;
                    Session["logueado"] = true;
                    Session["emailUs"] = e.Result.Membership;
                    AsyncManager.OutstandingOperations.Decrement();
                }
                else
                {
                    Session["logueado"] = false;
                }
            };
            front.ObtenerEspecificacionUsuarioXMembershipAsync(membership, idmov);
        
        }

        public void SetearLogueoSessionCompleted()
        {


        }

        //
        // GET: /Account/LogOff

        public void LogOffAsync()
        {

        }

        public ActionResult LogOffCompleted()
        {
            FormsAuthentication.SignOut();
            Session["logueado"] = false;
            return RedirectToAction("Index", "Home");
        }

        #region Editar Usuario

        [AllowAnonymous]
        public void EditAsync()
        {
            String membership = (String)Session["emailUs"];
            int idmov = (int)Session["idMov"];
            AsyncManager.OutstandingOperations.Increment(2);
            var front = new FrontOffice.FrontOfficeServiceClient();
            front.ObtenerCategoriasTematicasCompleted += (s, e) =>
            {
                AsyncManager.Parameters["categs"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ObtenerCategoriasTematicasAsync();

            front.ObtenerEspecificacionUsuarioXMembershipCompleted += (s, e) =>
            {
                AsyncManager.Parameters["espus"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ObtenerEspecificacionUsuarioXMembershipAsync(membership, idmov);

        }

        [AllowAnonymous]
        public ActionResult EditCompleted(CategoriaTematica[] categs, EspecificacionUsuario espus)
        {
            ViewBag.categs = categs;
            Session["CategoriasEdit"] = categs;
            Session["EspUsEdit"] = espus;
            EditModel mod = new EditModel { nombre = espus.Nombre, latitud = espus.UbicacionLatitud, longitud = espus.UbicacionLongitud, Email = espus.Membership };
            return View(mod);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Edit(EditModel model, string longitud, string latitud)
        {
            if (System.Math.Abs(float.Parse(latitud)) > 500)
            {
                //esto es para que funcione bien localmente
                model.latitud = float.Parse(latitud.Replace(".", ","));
                model.longitud = float.Parse(longitud.Replace(".", ","));
            }
            else
            {
                //esto en la nube
                model.latitud = float.Parse(latitud);
                model.longitud = float.Parse(longitud);
            }
            //if (ModelState.IsValid)
            //{

                    EditarEspecificacionUsuarioAsync(model);
                    return RedirectToAction("Index", "Home");
            //}

            // If we got this far, something failed, redisplay form
            //return View(model);
        }



        private void EditarEspecificacionUsuarioAsync(EditModel model)
        {

            EspecificacionUsuario espus = (EspecificacionUsuario)Session["EspUsEdit"];
            espus.Nombre = model.nombre;
            CategoriaTematica[] categs = (CategoriaTematica[])Session["CategoriasEdit"];
            int j = 0;
            for (var i = 0; i < categs.Length; i++)
            {
                if (Request[categs[i].Nombre] == "true,false")
                {
                    j++;
                }

            }
            espus.UbicacionLatitud = model.latitud;
            espus.UbicacionLongitud = model.longitud;
            String[] icol = new String[j];
            int z = 0;
            for (var i = 0; i < categs.Length; i++)
            {
                if (Request[categs[i].Nombre] == "true,false")
                {
                    icol[z] = categs[i].Nombre;
                    z++;
                }
            }

            var front = new FrontOffice.FrontOfficeServiceClient();

            AsyncManager.OutstandingOperations.Increment();
            front.ModificarEspecificacionUsuarioCompleted += (s, e) =>
            {
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ModificarEspecificacionUsuarioAsync(espus, icol);

        }

        public void EditarEspecificacionUsuarioCompleted()
        {

        }

        #endregion


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public void RegisterAsync()
        {
            AsyncManager.OutstandingOperations.Increment();

            var front = new FrontOffice.FrontOfficeServiceClient();
            front.ObtenerCategoriasTematicasCompleted += (s, e) =>
            {
                AsyncManager.Parameters["categs"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ObtenerCategoriasTematicasAsync();

        }

        [AllowAnonymous]
        public ActionResult RegisterCompleted(CategoriaTematica[] categs)
        {
            Session["Categorias"] = categs;
            RegisterModel mod = new RegisterModel { latitud = -35, longitud = -55 };
            return View(mod);
        }

        //
        // POST: /Account/JsonRegister

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model, string longitud, string latitud)
        {
            if (System.Math.Abs(float.Parse(latitud)) > 500)
            {
                //esto es para que funcione bien localmente
                model.latitud = float.Parse(latitud.Replace(".", ","));
                model.longitud = float.Parse(longitud.Replace(".", ","));
            }
            else
            {
                //esto en la nube
                model.latitud = float.Parse(latitud);
                model.longitud = float.Parse(longitud);
            }
            //if (ModelState.IsValid)
            if (!string.IsNullOrEmpty(model.contraseña) && !string.IsNullOrEmpty(model.ccontraseña) && model.contraseña.Equals(model.ccontraseña))
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.ApplicationName = this.Site;
                Roles.ApplicationName = this.Site;
                if (!Roles.RoleExists("Usuario"))
                {
                    Roles.CreateRole("Usuario");
                    if (this.Site == "GreenPeace")
                    {
                        Membership.CreateUser("pepe", "123456", "pepe", passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                        Roles.AddUserToRole("pepe", "Usuario");
                    }

                }
                Membership.CreateUser(model.Email, model.contraseña, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(model.Email, "Usuario");
                    EnviarMailRegistro(model.Email, model.contraseña);
                    FormsAuthentication.SetAuthCookie(model.Email, createPersistentCookie: false);
                    AgregarEspecificacionUsuarioAsync(model);
                    Session["facebook"] = false;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void EnviarMailRegistro(string mail, string contraseña)
        {
            try
            {
                Notificaciones.enviarNotificacion((string)Session["nomMov"], mail, "no-reply-" + ((string)Session["nomMov"]).ToLower() + "@indignadoframework.com",
                                                            "Bienvenido", mail, "te damos la bienvenida al movimiento " + (string)Session["nomMov"] + " de Indignado Framework.",
                                                            "Para iniciar sesión debe utilizar su dirección de e-mail y contraseña." + "</br>" +
                                                            "Su contraseña es: \"" + contraseña + "\". No se la digas a nadie.",
                                                            "http://developer.lulu.com/files/API_Key_Icon.png");
                /*
                Mensaje mn = new Mensaje();
                MailMessage mnsj = new MailMessage();

                mnsj.Subject = "Registro Exitoso";

                mnsj.To.Add(new MailAddress(mail));

                mnsj.From = new MailAddress("indignadoframework3@gmail.com", "Indignado Framework");

                /* Si deseamos Adjuntar algún archivo
                //mnsj.Attachments.Add(new Attachment("C:\\archivo.pdf"));

                mnsj.Body = "  Usted se ha registrado con exito a " + this.Site + " \n\n Su mail es: \"" + mail + "\"\n\n y su contraseña es: \"" + contraseña + "\"\n\nGracias por unirte a nuestra comunidad!!";

                mn.MandarCorreo(mnsj);
                */
            }
            catch (Exception ex)
            {
            }
        }

        private void AgregarEspecificacionUsuarioAsync(RegisterModel model)
        {
            EspecificacionUsuario espus = new EspecificacionUsuario();
            espus.Membership = model.Email;
            espus.Nombre = model.nombre + " " + model.apellido;
            CategoriaTematica[] categs = (CategoriaTematica[])Session["Categorias"];
            int j = 0;
            for (var i = 0; i < categs.Length; i++)
            {
                if (Request[categs[i].Nombre] == "true,false")
                {
                    j++;
                }

            }
            espus.UbicacionLatitud = model.latitud;
            espus.UbicacionLongitud = model.longitud;
            espus.MovimientoId = (int)Session["idMov"];
            String[] icol = new String[j];
            int z = 0;
            for (var i = 0; i < categs.Length; i++)
            {
                if (Request[categs[i].Nombre] == "true,false")
                {
                    icol[z] = categs[i].Nombre;
                    z++;
                }
            }

            var front = new FrontOffice.FrontOfficeServiceClient();

            AsyncManager.OutstandingOperations.Increment();
            front.AgregarUsuarioCompleted += (s, e) =>
            {
                EspecificacionUsuario aux = e.Result;
                Session["logueado"] = true;
                Session["idUsr"] = e.Result.Id;
                Session["emailUs"] = e.Result.Membership;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.AgregarUsuarioAsync(espus, this.Site, icol);

        }

        public void AgregarEspecificacionUsuarioCompleted()
        {

        }



        //
        // GET: /Account/ChangePassword

        #region Cambiar contrasena

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.vcontraseña, model.ncontraseña);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        #endregion

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #endregion
        #region Facebook

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            return View();
        }

        [AllowAnonymous]
        public void Facebook2Async()
        {
            AsyncManager.OutstandingOperations.Increment();

            var front = new FrontOffice.FrontOfficeServiceClient();
            front.ObtenerCategoriasTematicasCompleted += (s, e) =>
            {
                AsyncManager.Parameters["categs"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ObtenerCategoriasTematicasAsync();
        }


        [AllowAnonymous]
        public ActionResult Facebook2Completed(CategoriaTematica[] categs)
        {
            ViewBag.categs = categs;
            Session["Categorias"] = categs;
            RegisterModel mod = new RegisterModel { Email = "aaaaaaa", contraseña = "111111", ccontraseña = "111111", latitud = -35, longitud = -55 };
            return View(mod);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Facebook2(RegisterModel model2, string longitud, string latitud)
        {
            if (System.Math.Abs(float.Parse(latitud)) > 500)
            {
                //esto es para que funcione bien localmente
                model2.latitud = float.Parse(latitud.Replace(".", ","));
                model2.longitud = float.Parse(longitud.Replace(".", ","));
            }
            else
            {
                //esto en la nube
                model2.latitud = float.Parse(latitud);
                model2.longitud = float.Parse(longitud);
            }
            // Attempt to register the user
            RegisterModel model = (RegisterModel)Session["regmodel"];
            MembershipCreateStatus createStatus;
            Membership.ApplicationName = this.Site;
            Roles.ApplicationName = this.Site;
            model.contraseña = Membership.GeneratePassword(6, 2);
            model.ccontraseña = model.contraseña;
            model.latitud = model2.latitud;
            model.longitud = model2.longitud;
            if (!Roles.RoleExists("Usuario"))
                Roles.CreateRole("Usuario");
            if (Membership.FindUsersByName(model.Email).Count == 0)//llamar al modificar usuario
            {

                Membership.CreateUser(model.Email, model.contraseña, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                Roles.AddUserToRole(model.Email, "Usuario");
                EnviarMailRegistro(model.Email, model.contraseña);
                AgregarEspecificacionUsuarioAsync(model);
                Session["facebook"] = true;
                return RedirectToAction("Index", "Home");

            }//mandar mail con contrasena generada
            return RedirectToAction("Index", "Home");

        }


        public IDictionary<string, object> DecodePayload(string payload)
        {
            string base64 = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=')
                .Replace('-', '+').Replace('_', '/');
            string json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            return (IDictionary<string, object>)new JavaScriptSerializer().DeserializeObject(json);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Facebook(object postData)
        {
            FacebookClient fc = new FacebookClient();
            fc.AppId = "261519683946122";
            fc.AppSecret = "05c37e77dbad0ebeed5b9ceb7e239cd8";
            IDictionary<string, object> sr = (IDictionary<string, object>)fc.ParseSignedRequest(Request["signed_request"]);
            IDictionary<string, object> registration = (IDictionary<string, object>)sr["registration"];
            string name = (string)registration["name"];
            string email = (string)registration["email"];
            RegisterModel regmodel = new RegisterModel();
            regmodel.nombre = name;
            regmodel.Email = email;
            Session["regmodel"] = regmodel;
            return RedirectToAction("Facebook2", "Account");

        }

        [AllowAnonymous]
        public void FacebookLogin()
        {

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult FacebookLogin(object postData)
        {
            var accessToken = Request["accessToken"];
            Session["AccessToken"] = accessToken;
            if(accessToken != null){
                Membership.ApplicationName = this.Site;
                var client = new FacebookClient(accessToken);
                dynamic result = client.Get("me", new { fields = "name,email" });
                MembershipUser mu = Membership.GetUser(result.email);
                if (mu !=null && mu.IsApproved)
                {
                    Session["facebook"] = true;
                    SetearLogueoSessionAsync(result.email, (int)Session["idMov"]);
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult FacebookLogout(object postData)
        {
            Session["AccessToken"] = null;
            Session["logueado"] = false;
            Session["facebook"] = false;
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public void FacebookLogout()
        {
        }
        #endregion
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
