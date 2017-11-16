using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace IndignadoFramework.UI.Mvc4.Models
{
    class Notificaciones
    {
        public static void enviarNotificacion(string nomMov, string destinatario, string remitente, string asunto, string nombreDestinatario, string cabecera, string cuerpo, string urlImagen)
        {
            MailMessage objMail = new MailMessage();
            objMail.From = new MailAddress(remitente); //Remitente
            objMail.To.Add(destinatario); //Destinatario
            objMail.Subject = "I.F. - " + asunto; //Asunto
            objMail.Body = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; = charset=3Dutf-8\"><title>I.F.</title></head><body style=\"margin: 0; padding: 0;\" dir=\"ltr\"><table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"40\"><tr><td bgcolor=\"#f7f7f7\" width=\"100%\" style=\"font-family: 'lucida grande', tahoma, verdana, arial, sans-serif;\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"620\"><tr><td style=\"background: #011C0E; color: #fff; font-weight: bold; font-family: 'Trebuchet MS', tahoma, verdana, arial, sans-serif; vertical-align: middle; padding: 20px 30px; font-size: 30px; letter-spacing: -0.03em; text-align: left; text-transform: uppercase;\">indignado <span style=\" color: #7ab7e2;\">framework</span></td><td style=\"background: #011C0E; color: #fff; font-weight: bold; font-family: 'lucida grande', tahoma, verdana, arial, sans-serif; vertical-align: middle; padding: 20px 30px; font-size: 11px; text-align: right;\"></td></tr><tr><td colspan=\"2\" style=\"background-color: #FFFFFF; border-bottom: 1px solid #011C0E; border-left: 1px solid #CCCCCC; border-right: 1px solid #CCCCCC; font-family: 'lucida grande', tahoma, verdana, arial, sans-serif; padding: 15px;\" valign=\"top\"><table width=\"100%\"><tr><td width=\"100%\" style=\"font-size: 12px;\" valign=\"top\" align=\"left\"><div style=\"margin-bottom: 15px; font-size: 12px;\"> Hola "
								+ nombreDestinatario
								+ ", </div><div style=\"margin-bottom: 15px;\">"
								+ cabecera
								+ "</div><div style=\"margin-bottom: 15px;\"><div style=\"border-bottom: 1px solid #ccc; line-height: 5px;\">&nbsp;</div><table cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse; margin-top: 5px;\"><tr style=\"vertical-align: top\"><td style=\"padding-right: 5px\"><img src=\"" + urlImagen + "\" style=\"border: 0;\" /></td><td>"
								+ cuerpo
								+ "<br /><br /></td></tr></table><div style=\"border-bottom: 1px solid #ccc; line-height: 5px;\">&nbsp;</div></div><div style=\"margin-bottom: 15px; margin: 0;\">Gracias,<br />El equipo de I.F.</div></td></tr></table></td></tr><tr><td colspan=\"2\" style=\"color: #999999; padding: 10px; font-size: 12px; font-family: 'lucida grande', tahoma, verdana, arial, sans-serif;\">Este mensaje fue enviado a "
								+ destinatario
								+ ". Si no quieres seguir recibiendo estos mensajes de " + nomMov + ".indignadoframework.com ingresa a tu perfil para modificar tus opciones personales. I.F., Inc. Attention: Department Facultad de Ingenier&iacute;a - Universidad de la Republica Julio Herrera y Reissig 565.</td></tr></table></td></tr></table></body></html>";

            objMail.IsBodyHtml = true;
            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Host = "mail.fing.edu.uy"; //el nombre del servidor de correo
            SmtpMail.Port = 25; //asignamos el numero de puerto
            SmtpMail.Credentials = new System.Net.NetworkCredential("tprog122@fing.edu.uy", "056_C9uM");
            SmtpMail.Send(objMail); //Enviamos el correo
        }
    }
}
