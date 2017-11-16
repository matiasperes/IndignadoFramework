using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.Entities;
using System.Transactions;
using IndignadoFramework.DAC;

namespace IndignadoFramework.Business
{
    public class FrontOffice
    {

        #region Home del sitio
        
        //Mapa con marcadores de las locaciones donde se están realizando actividades, o donde se realizarán.

        //Novedades y recursos compartidos. En esta sección de la vista inicial se deberán desplegar las novedades y contenidos compartidos. Donde el contenido 
        //puede haber sido cargado en el sitio, o puede haber sido tomado de sitios de terceras partes, sitios de noticias, streamtwitter, etc. Los usuarios 
        //registrados podrán categorizar el contenido con una marca de “Me gusta”. 
        
        //Acceso a registro de usuario o login

        //Acceso a realizar convocatoria

        //Lista de convocatorias activas, con acceso al detalle de la convocatoria.

        //Acceso a compartir información

        //Vista de notificación de personas registradas en línea

        //Acceso a Chat con usuario registrado

        #endregion

        #region Registro o Login

        //Una vista que permita al usuario registrarse en el sitio, brindando información de contacto, y
        //para usuarios registrados permita el ingreso al sistema.
        //Para estudiantes de la carrera de Ingeniería el registro deberá soportar la integración con
        //Facebook o Twitter, ahorrándole al usuario tener que especificar un nombre de usuario, y una
        //contraseña. Para estudiantes del tecnólogo informático, esto es opcional.
        //Al registrarse un usuario deberá especificar la región geográfica de acción y la lista de
        //categorías temáticas para las cuales le interesa recibir notificaciones de convocatorias


        #endregion

        #region Convocatorias

        //Realizar convocatoria
        //El soporte para realizar convocatorias implica que un usuario registrado deberá poder proponer
        //un evento, el cual deberá tener coordenadas tiempo espacio específicas, un logo, un titulo, una
        //descripción, un categoría temática y un quórum mínimo.
        
        //Notificaciones
        //Una vez ingresada una convocatoria se deberán enviar notificaciones a todos los usuarios que
        //se encuentren en un radio de menos de 50 km de la zona de la actividad, o que se hayan
        //registrado para recibir notificaciones de dicha temática.
        
        //Detalle de convocatoria
        //Una vez ingresada una convocatoria los usuarios registrados podrán optar por asistir a la
        //actividad, en la vista de detalle de convocatoria, la cual presenta toda la información asociada a
        //una convocatoria.
        //Para que una convocatoria se lleve a cabo se deberán haber comprometido a asistir por lo
        //menos un número de usuarios igual o superior al quórum establecido, con 24 horas de
        //anticipación.

        #endregion

        #region Compartir informacion

        //Compartir información
        //Los usuarios registrados deberán poder compartir información, que podrá ser: imágenes,
        //videos, o links de la web.
        //Para la información compartida los usuarios que la consuman podrán marcarla con una
        //etiqueta de “Me gusta”, o con una etiqueta de contenido inadecuado. El sistema deberá
        //permitir acceder a la información que fue mejor rankeada en el último mes.
        //La información compartida se deberá presentar en la vista inicial, filtrada por los N recursos
        //más rankeados, o temporalmente, mostrando los últimos M recursos compartidos. Con N y M
        //configurable por el módulo de administración del sistema.

        #endregion

        #region Usuarios en linea y chat

        //Usuarios en línea y Chat
        //Los usuarios registrados deberán poder acceder a una vista que les muestre el listado de
        //usuarios en línea, y permitirá a dos usuarios registrados chatear entre si.
        
        #endregion
        
    }
}
