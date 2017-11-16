using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IndignadoFramework.Entities;
using System.Web.Security;

namespace IndignadoFramework.UI.Mvc4.Controllers
{



    public class ChatController : AsyncController
    {
        //
        // GET: /Chat/

        public void IndexAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            var chatService = new ChatService.IntegracionServiceClient();
            chatService.obtenerMensajesCompleted += (s, e) =>
            {
                AsyncManager.Parameters["response"] = e.Result;

                AsyncManager.OutstandingOperations.Decrement();
                chatService.Close();
            };
            chatService.obtenerMensajesAsync((int)Session["idMov"]);


        }

         public ActionResult IndexCompleted(List<MensajeChat> response)
        {
           var listMostrar = new List<MensajeChat>();
            bool logueado = ((Session["logueado"] != null && (bool)Session["logueado"]) && Membership.FindUsersByName((String)Session["emailUs"]).Count > 0);
            if (Session["chatMessages"] == null)
            {
                if (logueado)
                    Session["chatMessages"] = response;
                listMostrar = response;
            }
            else
            {
                var list = ((List<MensajeChat>)Session["chatMessages"]);
                foreach (var msj in response)
                {
                    if (list.Where(o => o.idMensaje == msj.idMensaje).FirstOrDefault() == null)
                    {
                        if (logueado)
                            list.Add(msj);
                        listMostrar.Add(msj);
                    }
                }
            }
            return Json(listMostrar);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("New"), HttpPost]
        public void NewAsync(string user, string msg)
        {
            AsyncManager.OutstandingOperations.Increment();
            var chatService = new ChatService.IntegracionServiceClient();
            chatService.agregarMensajeCompleted += (s, e) =>
            {
                AsyncManager.OutstandingOperations.Decrement();
                chatService.Close();
            };
            var mensaje = new MensajeChat() { usuarioNombre = user, mensaje = msg, idRoom = (int)Session["idMov"], idMensaje = Guid.NewGuid().ToString(),fecha = DateTime.Now};
            chatService.agregarMensajeAsync(mensaje);


        }

        [ActionName("New"), HttpPost]
        public ActionResult NewCompleted()
        {
            return Json(new
            {
                d = 1
            });
        }

    }
}
