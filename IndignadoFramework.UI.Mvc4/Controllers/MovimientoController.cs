using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using IndignadoFramework.UIProcess;
using System.Web.Security;
using IndignadoFramework.UI.Mvc4.BackOffice;
using IndignadoFramework.UI.Mvc4.Models;
using System.Web.Mvc.Async;
using IndignadoFramework.Entities;
using Microsoft.WindowsAzure.StorageClient;
using System.Web.UI.DataVisualization.Charting;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
//using PDFCharting.Models;

namespace IndignadoFramework.UI.Mvc4.Controllers
{
    public class MovimientoController : SiteController 
    {

        public void IndexAsync()
        {
            AsyncManager.OutstandingOperations.Increment();

            var back = new BackOffice.BackOfficeServiceClient();
            back.ObtenerTodosMovimientosCompleted += (s, e) =>
            {
                AsyncManager.Parameters["items"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.ObtenerTodosMovimientosAsync();
        }

        public ActionResult IndexCompleted(Movimiento[] items)
        {
            if (this.Site == "Backoffice")
            {
                //Roles.ApplicationName = this.Site;
                var logueado = Session["logueado2"];
                Session["movimientos"] = items;
                if (logueado != null && (bool)logueado)
                    return View(items);
                else// el usuario esta logueado
                    return RedirectToAction("Login", "BackOffice");
            }
           else
               return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            Movimiento mov = new Movimiento { UbicacionLatitud = -35,UbicacionLongitud = -55};
            return View(mov);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Movimiento movimiento, string UbicacionLatitud,string UbicacionLongitud)
        {

            if (System.Math.Abs(float.Parse(UbicacionLatitud)) > 500)
            {
                //esto es para que funcione bien localmente
                movimiento.UbicacionLatitud = float.Parse(UbicacionLatitud.Replace(".", ","));
                movimiento.UbicacionLongitud = float.Parse(UbicacionLongitud.Replace(".", ","));
            }
            else
            {
                //esto en la nube
                movimiento.UbicacionLatitud = float.Parse(UbicacionLatitud);
                movimiento.UbicacionLongitud = float.Parse(UbicacionLongitud);
            }

            var tipo = Request["Estilo"];
            switch (tipo)
            {
                case "0": movimiento.Estilo = "Blue"; break;
                case "1": movimiento.Estilo = "Black"; break;
                case "2": movimiento.Estilo = "Red"; break;
            }
            Session["nmovimiento"] = movimiento;

            return RedirectToAction("RegisterAdmin");
        }


        [ActionName("Edit"), HttpGet]
        public void EditGetAsync(int id)
        {
            AsyncManager.OutstandingOperations.Increment();

            var back = new BackOffice.BackOfficeServiceClient();
            back.ObtenerMovimientoXIdCompleted += (s, e) =>
            {
                AsyncManager.Parameters["item"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.ObtenerMovimientoXIdAsync(id);
        }

        [ActionName("Edit"), HttpGet]
        public ActionResult EditGetCompleted(Movimiento item)
        {
            if (item.Id != null)
            {
                Session["idMovEdit"] = item.Id;
                MyStorageService storage = new MyStorageService();
                List<CloudBlob> blobs;
                storage.ListBlobs("imagenes"+item.Id.ToString(), out blobs);
                ViewBag.Blobs = blobs;
            }
            return View(item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Edit"), HttpPost]
        public void EditPostAsync(Movimiento movimiento, string UbicacionLatitud, string UbicacionLongitud)
        {

            if (System.Math.Abs(float.Parse(UbicacionLatitud)) > 500)
            {
                //esto es para que funcione bien localmente
                movimiento.UbicacionLatitud = float.Parse(UbicacionLatitud.Replace(".", ","));
                movimiento.UbicacionLongitud = float.Parse(UbicacionLongitud.Replace(".", ","));
            }
            else
            {
                //esto en la nube
                movimiento.UbicacionLatitud = float.Parse(UbicacionLatitud);
                movimiento.UbicacionLongitud = float.Parse(UbicacionLongitud);
            }
            AsyncManager.OutstandingOperations.Increment();
            var tipo = Request["Estilo"];
            switch (tipo)
            {
                case "0": movimiento.Estilo = "Blue"; break;
                case "1": movimiento.Estilo = "Black"; break;
                case "2": movimiento.Estilo = "Red"; break;
            }
            var back = new BackOffice.BackOfficeServiceClient();
            back.ModificarMovimientoCompleted += (s, e) =>
            {
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.ModificarMovimientoAsync(movimiento);
        }

        [ActionName("Edit"), HttpPost]
        public ActionResult EditPostCompleted()
        {
            return RedirectToAction("Index");
        }

        [ActionName("Delete"), HttpGet]
        public void DeleteGetAsync(int id)
        {
            AsyncManager.OutstandingOperations.Increment();

            var back = new BackOffice.BackOfficeServiceClient();
            back.ObtenerMovimientoXIdCompleted += (s, e) =>
            {
                AsyncManager.Parameters["item"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.ObtenerMovimientoXIdAsync(id);
        }

        [ActionName("Delete"), HttpGet]
        public ActionResult DeleteGetCompleted(Movimiento item)
        {            
            return View(item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Delete"), HttpPost]
        public void DeletePostAsync(Movimiento movimiento)
        {
            AsyncManager.OutstandingOperations.Increment();

            var back = new BackOffice.BackOfficeServiceClient();
            back.BorrarMovimientoCompleted += (s, e) =>
            {
                AsyncManager.Parameters["item"] = movimiento.Id;
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.BorrarMovimientoAsync(movimiento);
        }

        [ActionName("Delete"), HttpPost]
        public ActionResult DeletePostCompleted(int item)
        {
            MyStorageService storage = new MyStorageService();
            storage.DeleteContainer("imagenes" + item);
            storage.DeleteContainer("contenido" + item);
            return RedirectToAction("Index");
        }


        public void DetailsAsync(int id)
        {
            AsyncManager.OutstandingOperations.Increment();

            var back = new BackOffice.BackOfficeServiceClient();
            back.ObtenerMovimientoXIdCompleted += (s, e) =>
            {
                AsyncManager.Parameters["item"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.ObtenerMovimientoXIdAsync(id);
        }

        public ActionResult DetailsCompleted(Movimiento item)
        {
            return View(item);
        }


        [HttpPost]
        [ActionName("upfile")]
        public ActionResult _upfilePost(Movimiento item, HttpPostedFileBase fileBase)
        {
            if (fileBase.ContentLength > 0)
            {
                
                MyStorageService storage = new MyStorageService();                
                storage.Upload("imagenes" + (int)Session["idMovEdit"], fileBase);
            }

            return RedirectToAction("Edit", new { id = (int)Session["idMovEdit"] });
        }

        public ActionResult DeleteImg(string id)
        {
            MyStorageService storage = new MyStorageService();
            storage.DeleteBlob("imagenes" + (int)Session["idMovEdit"], id);
            return RedirectToAction("index");
        }

        #region Registro del Administrador

        [AllowAnonymous]
        public void RegisterAdminAsync()
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
        public ActionResult RegisterAdminCompleted(CategoriaTematica[] categs)
        {
            ViewBag.categs = categs;
            Session["Categorias"] = categs;
            RegisterModel mod = new RegisterModel { latitud = -35, longitud = -55 };
            return View(mod);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterAdmin(RegisterModel model, string longitud, string latitud)
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
                Movimiento movimiento = (Movimiento)Session["nmovimiento"];
                Membership.ApplicationName = movimiento.Nombre;
                Roles.ApplicationName = movimiento.Nombre;
                if (!Roles.RoleExists("Administrador"))
                    Roles.CreateRole("Administrador");
                Membership.CreateUser(model.Email, model.contraseña, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(model.Email, "Administrador");
                    AgregarMovimientoAsync(movimiento,model);
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

        private void AgregarEspecificacionUsuarioAsync(RegisterModel model)
        {
            EspecificacionUsuario espus = new EspecificacionUsuario();
            espus.Membership = model.Email;
            espus.Nombre = model.nombre + " " + model.apellido;
            CategoriaTematica[] categs = (CategoriaTematica[])Session["Categorias"];
            espus.UbicacionLatitud = model.latitud;
            espus.UbicacionLongitud = model.longitud;
            espus.MovimientoId = (int)Session["idnmov"];

            var front = new FrontOffice.FrontOfficeServiceClient();

            AsyncManager.OutstandingOperations.Increment();
            front.AgregarUsuarioCompleted += (s, e) =>
            {
                EspecificacionUsuario aux = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.AgregarUsuarioAsync(espus, null, null);

        }

        public void AgregarEspecificacionUsuarioCompleted()
        {

        }


        private void AgregarMovimientoAsync(Movimiento movimiento, RegisterModel model)
        {
            AsyncManager.OutstandingOperations.Increment();
            var back = new BackOffice.BackOfficeServiceClient();
            back.AgregarMovimientoCompleted += (s, e) =>
            {
                AsyncManager.Parameters["item"] = e.Result;
                Session["idnmov"] = e.Result.Id;
                AgregarEspecificacionUsuarioAsync(model);
                AsyncManager.OutstandingOperations.Decrement();

            };
            back.AgregarMovimientoAsync(movimiento);
        }

        public void AgregarMovimientoCompleted(Movimiento item)
        {
            Session["idnmov"] = item.Id;

        }

        public FileContentResult Graficas(String name, int grafNum)
        {
            if (grafNum == 0)
                return File(Chart0(name), "image/png");
            else if (grafNum == 1)
                return File(Chart1(name), "image/png");
            else
                return File(Chart2(), "image/png");
        }

        public ActionResult Estadisticas(String name)
        {
            ViewBag.Name = name;
            return View();
        }

        private Byte[] Chart0(String name)// Grafica de usuarios registrados por mes
        {

            var chart = new Chart
            {
                Width = 500,
                Height = 400,
                RenderType = RenderType.ImageTag,
                AntiAliasing = AntiAliasingStyles.All,
                TextAntiAliasingQuality = TextAntiAliasingQuality.High
            };
            chart.BackSecondaryColor = Color.White;
            chart.BackColor = Color.SkyBlue;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.Titles.Add("Registros en el Tiempo");
            chart.Titles[0].Font = new Font("Arial", 16f);
            chart.ChartAreas.Add("");
            chart.ChartAreas[0].AxisX.Title = "Meses";
            chart.ChartAreas[0].AxisY.Title = "Usuarios Registrados";
            chart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12f);
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12f);
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10f);
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart.ChartAreas[0].AxisY.LabelStyle.Interval = 1;
            chart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].BackSecondaryColor = Color.SkyBlue;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            Membership.ApplicationName = name;
            MembershipUserCollection users = Membership.GetAllUsers();

            int[] meses = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            String[] nmeses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre" };
            foreach (MembershipUser user in users)
            {
                if (user.CreationDate.Year == DateTime.Now.Year)
                {
                    meses[user.CreationDate.Month - 1] += 1;
                }
            }
            chart.Series.Add("");
            chart.Series[0].ChartType = SeriesChartType.Column;

            for (int i = 0; i < 12; i++)
            {
                chart.Series[0].Points.AddXY(nmeses[i], meses[i]);
            }
            using (var chartimage = new MemoryStream())
            {
                chart.SaveImage(chartimage, ChartImageFormat.Png);
                return chartimage.GetBuffer();
            }
        }

        private Byte[] Chart1(String name)//Grafica de usuarios registrados por año
        {

            var chart = new Chart
            {
                Width = 500,
                Height = 400,
                RenderType = RenderType.ImageTag,
                AntiAliasing = AntiAliasingStyles.All,
                TextAntiAliasingQuality = TextAntiAliasingQuality.High
            };
            chart.BackSecondaryColor = Color.White;
            chart.BackColor = Color.Olive;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.Titles.Add("Registros en el Tiempo");
            chart.Titles[0].Font = new Font("Arial", 16f);
            chart.ChartAreas.Add("");
            chart.ChartAreas[0].AxisX.Title = "Años";
            chart.ChartAreas[0].AxisY.Title = "Usuarios Registrados";
            chart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12f);
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12f);
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10f);
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
            chart.ChartAreas[0].AxisY.LabelStyle.Interval = 1;
            chart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].BackSecondaryColor = Color.Olive;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            Membership.ApplicationName = name;
            MembershipUserCollection users = Membership.GetAllUsers();

            int[] años = { 0, 0, 0, 0, 0 };
            foreach (MembershipUser user in users)
            {
                if (user.CreationDate.Year > DateTime.Now.Year - 5)
                {
                    años[user.CreationDate.Year - (DateTime.Now.Year - 4)] += 1;
                }
            }
            chart.Series.Add("");
            chart.Series[0].ChartType = SeriesChartType.Bar;

            for (int i = 4; i >= 0; i--)
            {
                chart.Series[0].Points.AddXY((DateTime.Now.Year - i).ToString(), años[4 - i]);
            }
            using (var chartimage = new MemoryStream())
            {
                chart.SaveImage(chartimage, ChartImageFormat.Png);
                return chartimage.GetBuffer();
            }
        }

        private Byte[] Chart2()//Grafica de usuarios registrados por año
        {

            var chart = new Chart
            {
                Width = 500,
                Height = 400,
                RenderType = RenderType.ImageTag,
                AntiAliasing = AntiAliasingStyles.All,
                TextAntiAliasingQuality = TextAntiAliasingQuality.High
            };
            chart.BackSecondaryColor = Color.White;
            chart.BackColor = Color.SkyBlue;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.Titles.Add("Registros en el Tiempo");
            chart.Titles[0].Font = new Font("Arial", 16f);
            chart.ChartAreas.Add("");
            chart.ChartAreas[0].AxisX.Title = "Movimientos";
            chart.ChartAreas[0].AxisY.Title = "Usuarios Registrados";
            chart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12f);
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12f);
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10f);
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart.ChartAreas[0].AxisY.LabelStyle.Interval = 1;
            chart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].BackSecondaryColor = Color.AliceBlue;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            Movimiento[] movs = (Movimiento[])Session["movimientos"]; 
            String[] movns = new String[movs.Length];
            int[] umovns = new int[movs.Length];
            double total = 0.0;
            for (int i = 0; i < movs.Length; i++)
            {
                movns[i] = movs[i].Nombre;
                Membership.ApplicationName = movs[i].Nombre;
                int uss = Membership.GetAllUsers().Count;
                umovns[i] = uss;
                total += uss;
            }
            chart.Series.Add("");
            chart.Series[0].ChartType = SeriesChartType.Pie;
            chart.Series[0]["PieLabelStyle"] = "Outside";
            chart.Series[0]["PieLineColor"] = "Blue";


            for (int i = 0; i < movs.Length; i++)
            {
                chart.Series[0].Points.AddXY(movns[i] + " " + String.Format("{0:0.00}", ((umovns[i] * 100) / total))  +"%" , umovns[i]);
            }
            using (var chartimage = new MemoryStream())
            {
                chart.SaveImage(chartimage, ChartImageFormat.Png);
                return chartimage.GetBuffer();
            }
        }

        public FilePathResult GetPdf(String name)
        {
            var doc = new Document();
            var pdf = Server.MapPath("PDF/Chart.pdf");
            String[] pepe = pdf.Split('\\');
            int i = 0;
            String ruta = "";
            while (pepe[i] != "Backoffice"){
                ruta += pepe[i] + "//";
                i++;
            }
            ruta += "Chart.pdf";
            PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
            doc.Open();


            doc.Add(new Paragraph("Usuarios registrados por mes en el ultimo año\n"));
            var image = Image.GetInstance(Chart0(name));
            image.ScalePercent(75f);
            doc.Add(image);
            var image2 = Image.GetInstance(Chart1(name));
            doc.Add(new Paragraph("Usuarios registrados por año en los ultimos 5 años\n"));
            image2.ScalePercent(75f);
            doc.Add(image2);
            doc.Close();

            return File(ruta, "application/pdf", "Chart.pdf");
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

        #endregion

        #region Contenidos Inadecuados

        public void ContenidosInadecuadosAsync(int id, String nommov)
        {
            AsyncManager.OutstandingOperations.Increment();
            Session["nommovdelete"] = nommov;
            var back = new BackOffice.BackOfficeServiceClient();
            back.ObtenerContenidosMovimientoPorInadecuadoCompleted += (s, e) =>
            {
                AsyncManager.Parameters["items"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.ObtenerContenidosMovimientoPorInadecuadoAsync(id);
        }

        public ActionResult ContenidosInadecuadosCompleted(Contenido[] items) 
        {
            return View(items);
        }

        #endregion 

        #region Eliminar Usuario

        public ActionResult EliminarUsuario(int id, String membership)
        {
            String nommov = (String) Session["nommovdelete"];
            Membership.ApplicationName = nommov;
            MembershipUser muUser = Membership.GetUser(membership);
            muUser.IsApproved = false;
            Membership.UpdateUser(muUser);
            /*AsyncManager.OutstandingOperations.Increment();

            var back = new BackOffice.BackOfficeServiceClient();
            back.EliminarEspecificacionUsuarioCompleted += (s, e) =>
            {
                AsyncManager.Parameters["idm"] = membership;
                AsyncManager.Parameters["exito"] = !e.Cancelled;
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.EliminarEspecificacionUsuarioAsync(id);*/

            return RedirectToAction("index");

        }

        /*
        public void EliminarUsuarioAsync(int id, String membership)
        {

            AsyncManager.OutstandingOperations.Increment();

            var back = new BackOffice.BackOfficeServiceClient();
            back.EliminarEspecificacionUsuarioCompleted += (s, e) =>
            {
                AsyncManager.Parameters["idm"] = membership;
                AsyncManager.Parameters["exito"] = !e.Cancelled;
                AsyncManager.OutstandingOperations.Decrement();
            };
            back.EliminarEspecificacionUsuarioAsync(id);
        }

        public ActionResult EliminarUsuarioCompleted(String idm, bool exito)
        {
           if (exito)
            {
                String nommov = (String)Session["nommovdelete"];
                String aux = Membership.ApplicationName;
                Membership.ApplicationName = nommov;
                Membership.DeleteUser(idm);
                Membership.ApplicationName = aux;
            }
            return RedirectToAction("index");
        }*/

        #endregion


        public void FuentesAsync(int id, string url, string dll)
        {
            AsyncManager.OutstandingOperations.Increment(2);
            var back = new BackOffice.BackOfficeServiceClient();
            
            back.AgregarFuenteCompleted += (s, e) =>
            {
                AsyncManager.OutstandingOperations.Decrement();
                back.ObtenerMovimientoXIdAsync(id);
            };
            back.ObtenerMovimientoXIdCompleted += (s, e) =>
            {
                AsyncManager.Parameters["fuentes"] = e.Result.FuentesWEB;
                AsyncManager.OutstandingOperations.Decrement();
            };
            FuenteWEB nueva = new FuenteWEB();
            nueva.Url = url;
            nueva.UrlDll = dll;
            nueva.Tipo = "a";
            nueva.MovimientoId = id;
            back.AgregarFuenteAsync(nueva);
        }

        public ActionResult FuentesCompleted(FuenteWEB[] fuentes)
        {
            return PartialView("_Fuentes",fuentes);
        }

    }


}
