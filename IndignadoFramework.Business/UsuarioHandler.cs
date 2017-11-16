using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.Entities;
using IndignadoFramework.DAC;
using System.Transactions;

namespace IndignadoFramework.Business
{
    public class UsuarioHandler
    {
        EspecificacionUsuarioDAC espusdac = new EspecificacionUsuarioDAC();

        public EspecificacionUsuario AgregarUsuario(EspecificacionUsuario espus, String nomMov, String[] categs)
        {
            espus.BajasContenido = 0;
            Console.WriteLine("Creando Usuario... ");
            Console.WriteLine(espus.ToString());
            BackOffice bo = new BackOffice();
            //Modificado por matias. No se si estara bien.
            //espus.MovimientoId = bo.ObtenerMovimientoXNombre(nomMov).Id;
            try
            {
                return espusdac.AddEspecificacionUsuario(espus, categs);
            }
            catch (Exception ex)
            {
                Notificaciones.enviarNotificacion("-1", "bdi_mail@hotmail.com", "bdi_mail@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                Notificaciones.enviarNotificacion("-1", "zonagonza@hotmail.com", "zonagonza@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public EspecificacionUsuario ObtenerEspecificacionUsuarioXId(int idUsuario)
        {
            try
            {
                return espusdac.SelectForId(idUsuario);
            }
            catch (Exception ex)
            {
                Notificaciones.enviarNotificacion("-1", "bdi_mail@hotmail.com", "bdi_mail@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                Notificaciones.enviarNotificacion("-1", "zonagonza@hotmail.com", "zonagonza@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public EspecificacionUsuario ObtenerEspecificacionUsuarioXMembership(string membership, int idMov)
        {
            Console.WriteLine("Obtener usuario por membership: " + membership+" "+idMov);
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var eudac = new EspecificacionUsuarioDAC();
                    return eudac.GetEspecificacionUsuarioByNombre(membership, idMov);
                }
                catch (Exception ex)
                {
                    Notificaciones.enviarNotificacion("-1", "bdi_mail@hotmail.com", "bdi_mail@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                    Notificaciones.enviarNotificacion("-1", "zonagonza@hotmail.com", "zonagonza@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public void ModificarEspecificacionUsuario(EspecificacionUsuario espus, String[] categs)
        {
            Console.WriteLine("Modificando Usuario " + espus.Membership);

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    
                    UpdateEspecificacionUsuario(espus, categs);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Notificaciones.enviarNotificacion("-1", "bdi_mail@hotmail.com", "bdi_mail@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                    Notificaciones.enviarNotificacion("-1", "zonagonza@hotmail.com", "zonagonza@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        private void UpdateEspecificacionUsuario(EspecificacionUsuario espus, String[] categs)
        {
            Console.WriteLine(espus.ToString());

            // Persist data.
            var dac = new EspecificacionUsuarioDAC();
            try
            {
                
                dac.UpdateEspecificacionUsuario(espus, categs);
            }
            catch (Exception ex)
            {
                Notificaciones.enviarNotificacion("-1", "bdi_mail@hotmail.com", "bdi_mail@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                Notificaciones.enviarNotificacion("-1", "zonagonza@hotmail.com", "zonagonza@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void EliminarEspecificacionUsuario(int idus)
        {

            Console.WriteLine("Eliminar Usuario " + idus);

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    Console.WriteLine("Se eliminara el usuario " + idus);

                    // Persist data.
                    var dac = new EspecificacionUsuarioDAC();
                    try
                    {
                        dac.DeleteEspecificacionUsuario(idus);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                    Console.WriteLine("Se elimino el usuario con exito");
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Notificaciones.enviarNotificacion("-1", "bdi_mail@hotmail.com", "bdi_mail@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                    Notificaciones.enviarNotificacion("-1", "zonagonza@hotmail.com", "zonagonza@hotmail.com", "error", "Matias", "nada", ex.ToString(), "");
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
            
    }
}
