using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using IndignadoFramework.DAC;
using IndignadoFramework.Entities;

namespace IndignadoFramework.Business
{
    public class Integracion
    {
        public MensajeChat AgregarMensajeChat(MensajeChat mensajeChat)
        {
            Console.WriteLine("Creando MensajeChat... ");

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    mensajeChat = CreateMensajeChat(mensajeChat);
                    //LogStatus(MensajeChat);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("Nuevo MensajeChat = " + mensajeChat.idMensaje);

            return mensajeChat;
        }

        private MensajeChat CreateMensajeChat(MensajeChat mensajeChat)
        {
            // Business logic.
            //MensajeChat.IsCompleted = false;
            //MensajeChat.DateSubmitted = DateTime.Now;
            //MensajeChat.DateModified = DateTime.Now;

            Console.WriteLine(mensajeChat.ToString());

            // Persist data.
            var dac = new MensajeChatDAC();
            try
            {
                return dac.AddMensajeChat(mensajeChat);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void BorrarMensajeChat(MensajeChat mensajeChat)
        {
            Console.WriteLine("Borrando MensajeChat" + mensajeChat.idMensaje);

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DeleteMensajeChat(mensajeChat);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("MensajeChat Borrado");
        }

        public void DeleteMensajeChat(MensajeChat mensajeChat)
        {
            // Business logic.
            //MensajeChat.IsCompleted = false;
            //MensajeChat.DateSubmitted = DateTime.Now;
            //MensajeChat.DateModified = DateTime.Now;

            Console.WriteLine(mensajeChat.ToString());

            // Persist data.
            var dac = new MensajeChatDAC();
            try
            {
                dac.DeleteMensajeChatByKey(mensajeChat.idRoom, mensajeChat.idMensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void BorrarMensajeChatByRoomAndDate(int idroom, DateTime date)
        {
            Console.WriteLine("Borrando MensajeChat del room " + idroom);

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DeleteMensajeChatByRoomAndDate(idroom, date);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("MensajeChat Borrado");
        }

        public void DeleteMensajeChatByRoomAndDate(int idroom, DateTime date)
        {
            // Business logic.
            //MensajeChat.IsCompleted = false;
            //MensajeChat.DateSubmitted = DateTime.Now;
            //MensajeChat.DateModified = DateTime.Now;

            Console.WriteLine(idroom);

            // Persist data.
            var dac = new MensajeChatDAC();
            try
            {
                dac.DeleteMensajeChatByIdRoomAndDate(idroom, date);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void ModificarMensajeChat(MensajeChat mensajeChat)
        {
            Console.WriteLine("Modificando MensajeChat" + mensajeChat.idMensaje);

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    UpdateMensajeChat(mensajeChat);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("MensajeChat Modificado");
        }

        private void UpdateMensajeChat(MensajeChat mensajeChat)
        {
            // Business logic.
            //MensajeChat.IsCompleted = false;
            //MensajeChat.DateSubmitted = DateTime.Now;
            //MensajeChat.DateModified = DateTime.Now;

            Console.WriteLine(mensajeChat.ToString());

            // Persist data.
            var dac = new MensajeChatDAC();
            try
            {
                dac.UpdateMensajeChat(mensajeChat);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<MensajeChat> ObtenerTodosMensajeChats()
        {
            Console.WriteLine("Obtener MensajeChats");
            List<MensajeChat> mensajeChats;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    mensajeChats = GetAllMensajeChats();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("MensajeChats Obtenidos");
            return mensajeChats;
        }

        private List<MensajeChat> GetAllMensajeChats()
        {
            // Business logic.
            //MensajeChat.IsCompleted = false;
            //MensajeChat.DateSubmitted = DateTime.Now;
            //MensajeChat.DateModified = DateTime.Now;

            //Console.WriteLine(nombreMensajeChat);

            // Persist data.
            var dac = new MensajeChatDAC();
            try
            {
                return dac.GetAllMensajeChat();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public List<MensajeChat> ObtenerTodosMensajeChatsByIdRoom(int idRoom)
        {
            Console.WriteLine("Obtener MensajeChats");
            List<MensajeChat> mensajeChats;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    mensajeChats = GetAllMensajeChatsByIdRoom(idRoom);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("MensajeChats Obtenidos");
            return mensajeChats;
        }

        private List<MensajeChat> GetAllMensajeChatsByIdRoom(int idRoom)
        {
            // Business logic.
            //MensajeChat.IsCompleted = false;
            //MensajeChat.DateSubmitted = DateTime.Now;
            //MensajeChat.DateModified = DateTime.Now;

            //Console.WriteLine(nombreMensajeChat);

            // Persist data.
            var dac = new MensajeChatDAC();
            try
            {
                return dac.GetMensajeChatByIdRoom(idRoom);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
