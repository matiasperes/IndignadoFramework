using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using IndignadoFramework.Entities;
using IndignadoFramework.Data;


namespace IndignadoFramework.DAC
{
    public class MensajeChatDAC
    {

        private const string ENTITY_SET_NAME = "MensajeChatSet";

        #region CRUD - Create Read Update Delete

        public MensajeChat AddMensajeChat(MensajeChat mensajeChat)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MensajeChatSet.AddObject(mensajeChat);
                ctx.SaveChanges();
            }

            return mensajeChat;
        }

        public void DeleteMensajeChatByIdRoom(int idRoom)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalMensajeChats = ctx.MensajeChatSet.Where(u => u.idRoom == idRoom).ToList();

                if (originalMensajeChats.Count > 0)
                {
                    foreach (var originalMensajeChat in originalMensajeChats)
                    {
                        ctx.MensajeChatSet.Attach(originalMensajeChat);
                        ctx.MensajeChatSet.DeleteObject(originalMensajeChat);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        public void DeleteMensajeChatByIdRoomAndDate(int idRoom, DateTime date)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalMensajeChats = ctx.MensajeChatSet.Where(u => u.idRoom == idRoom && u.fecha <= date).ToList();

                if (originalMensajeChats.Count > 0)
                {
                    foreach (var originalMensajeChat in originalMensajeChats)
                    {
                        ctx.MensajeChatSet.Attach(originalMensajeChat);
                        ctx.MensajeChatSet.DeleteObject(originalMensajeChat);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        public void DeleteMensajeChatByKey(int idRoom, string idMensaje)
        {
            using (var ctx = new ContextoIndignadoFramework())
            {
                var originalMensajeChat = ctx.MensajeChatSet.Where(u => u.idRoom == idRoom && u.idMensaje == idMensaje).FirstOrDefault();

                if (originalMensajeChat != null)
                {
                    ctx.MensajeChatSet.DeleteObject(originalMensajeChat);
                    ctx.SaveChanges();
                }
            }
        }

        public void UpdateMensajeChat(MensajeChat mensajeChat)
        {
            if (mensajeChat == null) throw new ArgumentNullException(ENTITY_SET_NAME);
            using (var ctx = new ContextoIndignadoFramework())
            {
                var key = ctx.CreateEntityKey(ENTITY_SET_NAME, mensajeChat);
                object original = null;
                if (ctx.TryGetObjectByKey(key, out original))
                {
                    ctx.ApplyCurrentValues(key.EntitySetName, mensajeChat);
                }
                ctx.SaveChanges();
            }
        }

        public List<MensajeChat> GetMensajeChatByIdRoom(int idRoom)
        {
            List<MensajeChat> mensajeChats = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MensajeChatSet.MergeOption = MergeOption.NoTracking;
                DateTime unaHoraPasado = DateTime.Now.AddMinutes(-120);
                mensajeChats = ctx.MensajeChatSet.Where(o => o.idRoom == idRoom && o.fecha >= unaHoraPasado).OrderBy(c => c.fecha).ToList();
            }
            return mensajeChats;
        }

        public MensajeChat GetMensajeChatByKey(int idRoom, string idMensaje)
        {
            MensajeChat MensajeChat = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MensajeChatSet.MergeOption = MergeOption.NoTracking;

                MensajeChat = ctx.MensajeChatSet.Where(o => o.idRoom == idRoom && o.idMensaje == idMensaje).FirstOrDefault();
            }
            return MensajeChat;
        }

        public List<MensajeChat> GetAllMensajeChat()
        {
            List<MensajeChat> MensajeChats = null;
            using (var ctx = new ContextoIndignadoFramework())
            {
                ctx.MensajeChatSet.MergeOption = MergeOption.NoTracking;

                MensajeChats = ctx.MensajeChatSet.ToList();
            }
            return MensajeChats;
        }


        #endregion


    }
}
