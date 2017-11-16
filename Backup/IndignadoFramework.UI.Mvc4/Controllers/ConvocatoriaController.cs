using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using IndignadoFramework.UI.Mvc4.FrontOffice;
using IndignadoFramework.Entities;
using IndignadoFramework.UI.Mvc4.Models;
using System.Web.Security;
using System.Reflection;
using System.Net;
using System.IO;

namespace IndignadoFramework.UI.Mvc4.Controllers
{
    public class ConvocatoriaController : SiteController
    {
        //
        // GET: /Convocatoria/

        
        public void IndexAsync()
        {
            AsyncManager.OutstandingOperations.Increment();

            var front = new FrontOffice.FrontOfficeServiceClient();
            front.ObtenerConvocatoriasMovimientoCompleted += (s, e) =>
            {
                AsyncManager.Parameters["items"] = e.Result;
                AsyncManager.Parameters["exito"] = !e.Cancelled;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ObtenerConvocatoriasMovimientoAsync((int)Session["idMov"]);
        }

        public ActionResult IndexCompleted(Convocatoria[] items,bool exito)
        {
            if(exito)
                return View(items);
            else
                return View("../Shared/Error"); 
        }

        //
        // GET: /Convocatoria/Details/5

        public void DetailsAsync(int idConv)
        {
            AsyncManager.OutstandingOperations.Increment(3);
            var front = new FrontOffice.FrontOfficeServiceClient();
            AsyncManager.Parameters["confirmo"] = true;
            front.GetFuentesDatosMovimientoCompleted += (s, e) =>
            {
                AsyncManager.Parameters["fweb"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.GetFuentesDatosMovimientoAsync((int)Session["idMov"]);

            front.ObtenerConvocatoriaXIdCompleted += (s, e) =>
            {
                AsyncManager.Parameters["conv"] = e.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            front.ObtenerConvocatoriaXIdAsync(idConv);
            
            if ((Session["logueado"] != null && (bool)Session["logueado"]) && Membership.FindUsersByName((String)Session["emailUs"]).Count > 0)
            {
                    front.ConfirmoAsistenciaUsuarioCompleted += (s, e) =>
                    {
                        AsyncManager.Parameters["confirmo"] = e.Result;
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                    front.ConfirmoAsistenciaUsuarioAsync(idConv, (int)Session["idUsr"]);
            }
            else
                AsyncManager.OutstandingOperations.Decrement();

        }

        public ActionResult DetailsCompleted(FuenteWEB[] fweb, Convocatoria conv, bool confirmo)
        {
            ViewBag.BotonConfirmar = !confirmo;
            ViewBag.Feeds = CargarYLeerFeeds(fweb, conv.CategoriaTematica.Nombre);
            return View(conv);

        }

        public void ConfirmarAsistenciaAsync(int idConv)
        {
            var logueado = Session["logueado"];
            if (logueado != null)
            {
                if ((bool)logueado)
                {
                    var front = new FrontOffice.FrontOfficeServiceClient();
                    AsyncManager.OutstandingOperations.Increment();
                    front.ConfirmarAsistenciaConvocatoriaCompleted += (s, e) =>
                    {
                        AsyncManager.OutstandingOperations.Decrement();
                    };
                    front.ConfirmarAsistenciaConvocatoriaAsync(idConv, (int)Session["idUsr"]);
                }
            }
        }

        public string ConfirmarAsistenciaCompleted()
        {
            return "<p>Gracias. Su confirmación esta siendo procesada.</p>";

        }
        //
        // GET: /Convocatoria/Create

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
                Convocatoria conv = new Convocatoria();
                conv.Inicio = DateTime.Now.AddMonths(1);
                conv.Quorum = 10;
                conv.UbicacionLatitud = (double)Session["latitudMov"];
                conv.UbicacionLongitud = (double)Session["longitudMov"];
                return View(conv);
        }

        //
        // POST: /Convocatoria/Create

        [ActionName("Create"), HttpPost]
        public void CreatePostAsync(Convocatoria conv, string UbicacionLatitud,string UbicacionLongitud)
        {

                if (System.Math.Abs(float.Parse(UbicacionLatitud)) > 500)
                {
                    //esto es para que funcione bien localmente
                    conv.UbicacionLatitud = float.Parse(UbicacionLatitud.Replace(".", ","));
                    conv.UbicacionLongitud = float.Parse(UbicacionLongitud.Replace(".", ","));
                }
                else
                {
                    //esto en la nube
                    conv.UbicacionLatitud = float.Parse(UbicacionLatitud);
                    conv.UbicacionLongitud = float.Parse(UbicacionLongitud);
                }
                AsyncManager.OutstandingOperations.Increment();
                conv.MovimientoId = (int)Session["idMov"];
                //conv.CategoriaTematicaId = 1;
          
                //creo la fecha a partir del input inicio
                string inicio = Request["inicio"];
                string[] datetime = inicio.Split(' ');
                string[] date = datetime[0].Split('/');
                string[] time = datetime[1].Split(':');
                DateTime dt = new DateTime(int.Parse(date[2]),int.Parse(date[1]),int.Parse(date[0]),int.Parse(time[0]),int.Parse(time[1]),0);
                //asigno la fecha creada a el atributo inicio de la convocatoria
                conv.Inicio = dt;

                var front = new FrontOffice.FrontOfficeServiceClient();
                front.AgregarConvocatoriaCompleted += (s, e) =>
                {
                    AsyncManager.Parameters["exito"] = !e.Cancelled;
                    AsyncManager.OutstandingOperations.Decrement();
                };

                front.AgregarConvocatoriaAsync(conv);
            
        }

    
        public ActionResult CreatePostCompleted(bool exito, CategoriaTematica[] cat)
        {
            if (exito)
            {
                return RedirectToAction("Index");
            }
            else
                return View("../Shared/Error");
        }

        public List<WebFeed> CargarYLeerFeeds(FuenteWEB[] fweb, string filtrado)
        {
            List<WebFeed> listaWF = new List<WebFeed>();
            WebFeed wf = null;
            foreach (FuenteWEB fuente in fweb)
            {
                wf = getWebFeed(fuente, filtrado);
                if(wf != null)
                    listaWF.Add(wf);
            }
            return listaWF;
        }

        private byte[] GetBytesFromUrl(string url)
        {
            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            //int i;
            using (BinaryReader br = new BinaryReader(stream))
            {
                //i = (int)(stream.Length);
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();
            return b;
        }

        public WebFeed getWebFeed(FuenteWEB fuente, string filtrado)
        {
            /*
            WebClient wc = new WebClient();
            string var = String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
            string tmpFile = "tmpfile" + var + ".dll";
            wc.DownloadFile(fuente.UrlDll, tmpFile);
            AssemblyName an = AssemblyName.GetAssemblyName(tmpFile);
            */
            WebFeed webfeed = null;
            byte[] bytes = GetBytesFromUrl(fuente.UrlDll);
            Assembly a = Assembly.Load(bytes);
            try
            {
            foreach (Type type in a.GetTypes ()) {
                if (type.IsClass == true) {
                    ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
                    object responder = ci.Invoke(null);
                    MethodInfo mi = type.GetMethod("getWebFeed");
                    return (WebFeed)mi.Invoke(null, new object[]{ fuente.Url, filtrado});
                }
            }
            }
            catch (TargetInvocationException)
            {
            }
            return null;
        }
    }
}
