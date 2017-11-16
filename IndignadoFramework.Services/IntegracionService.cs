using System;
using System.Collections.Generic;
using System.ServiceModel;
using IndignadoFramework.Business;
using IndignadoFramework.Entities;
using IndignadoFramework.Services.Contracts;


namespace IndignadoFramework.Services
{

    [ServiceBehavior(UseSynchronizationContext = false,
   ConcurrencyMode = ConcurrencyMode.Multiple,
   InstanceContextMode = InstanceContextMode.PerCall)]
    public class IntegracionService : IIntegracionService
    {

        private static object syncObject = new object();

        //private Dictionary<string, MensajesDelChat> rooms = null;

        //public IntegracionService()
        //{
        //    //rooms = new Dictionary<string, MensajesDelChat>();
        //}

        public void agregarMensaje(MensajeChat mensaje)
        {
            try
            {
                System.Console.WriteLine("agregarMensaje idRoom:" + mensaje.idRoom);
                lock (syncObject)
                {
                    var bc = new Integracion();
                    bc.AgregarMensajeChat(mensaje);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public List<MensajeChat> obtenerMensajes(int idRoom)
        {
            try
            {
                lock (syncObject)
                {
                    System.Console.WriteLine("obtenerMensajes " + DateTime.Now + " idRoom:" + idRoom);
                    var bc = new Integracion();
                    return bc.ObtenerTodosMensajeChatsByIdRoom(idRoom);
                    //return !rooms.ContainsKey(idRoom)
                    //           ? new MensajesDelChat {mensajes = new List<MensajeInformacion>()}
                    //           : rooms[idRoom];

                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }


    }
}
