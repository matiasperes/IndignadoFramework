using System;
using System.Collections.Generic;
using System.ServiceModel;
using IndignadoFramework.Entities;
using IndignadoFramework.Services.Contracts;
using IndignadoFramework.Business;
/*using System.Web;
using System.Web.Security;*/

namespace IndignadoFramework.Services
{

    [ServiceBehavior(UseSynchronizationContext = false,
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerCall)]
    public class FrontOfficeService : IFrontOfficeService
    {
        public FrontOfficeService()
        {
        }

        #region Usuarios
        #region EspecifiacionUsuario
        public EspecificacionUsuario AgregarUsuario(EspecificacionUsuario espus, String nomMov, String[] categs)
        {
            try
            {
                var fo = new UsuarioHandler();
                return fo.AgregarUsuario(espus, nomMov,categs);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public EspecificacionUsuario ObtenerEspecificacionUsuarioXId(int idUsuario)
        {
            try
            {
                UsuarioHandler uh = new UsuarioHandler();
                return uh.ObtenerEspecificacionUsuarioXId(idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public EspecificacionUsuario ObtenerEspecificacionUsuarioXMembership(String membership, int idMov)
        {
            try
            {
                UsuarioHandler uh = new UsuarioHandler();
                return uh.ObtenerEspecificacionUsuarioXMembership(membership, idMov);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        
        }

        public void ModificarEspecificacionUsuario(EspecificacionUsuario espus, String[] categs)
        {
            try
            {
                UsuarioHandler uh = new UsuarioHandler();
                uh.ModificarEspecificacionUsuario(espus, categs);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }
        #endregion
        /*
        #region Membership

        bool ValidateUser(string userName, string password, string aplication);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        #endregion
        */

        #endregion


        /***************CONVOCATORIAS******************************/
        #region Convocatorios

        public List<FuenteWEB> GetFuentesDatosMovimiento(int idMov)
        {
            try
            {
                ConvocatoriaHandler ch = new ConvocatoriaHandler();
                return ch.GetFuentesDatosMovimiento(idMov);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public Convocatoria ObtenerConvocatoriaXId(int idConvocatoria)
        {
            try
            {
                ConvocatoriaHandler ch = new ConvocatoriaHandler();
                return ch.ObtenerConvocatoriaXId(idConvocatoria);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public Convocatoria AgregarConvocatoria(Convocatoria convocatoria)
        {
            try
            {
                ConvocatoriaHandler ch = new ConvocatoriaHandler();
                return ch.AgregarConvocatoria(convocatoria);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public int ConfirmarAsistenciaConvocatoria(int idConvocatoria, int idUsuario)
        {
            try
            {
                ConvocatoriaHandler ch = new ConvocatoriaHandler();
                return ch.ConfirmarAsistenciaConvocatoria(idConvocatoria, idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public List<Convocatoria> ObtenerConvocatoriasMovimiento(int idMovimiento)
        {
            try
            {
                ConvocatoriaHandler ch = new ConvocatoriaHandler();
                return ch.ObtenerConvocatoriasMovimiento(idMovimiento);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public List<Convocatoria> ObtenerConvocatoriasPaginando(int pageNumber, int pageSize)
        {
            try
            {
                ConvocatoriaHandler ch = new ConvocatoriaHandler();
                return ch.ObtenerConvocatoriasPaginando(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public List<CategoriaTematica> ObtenerCategoriasTematicas()
        {
            try
            {
                ConvocatoriaHandler ch = new ConvocatoriaHandler();
                return ch.ObtenerCategoriasTematicas();
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public bool ConfirmoAsistenciaUsuario(int idConvocatoria, int idUsuario)
        {
            try
            {
                ConvocatoriaHandler ch = new ConvocatoriaHandler();
                return ch.ConfirmoAsistenciaUsuario(idConvocatoria,idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        #endregion

        /****************CONTENIDO*********************************/
        #region Contenido

        public Contenido ObtenerContenidoXId(int idContenido)
        {
            try
            {
                ContenidoHandler ch = new ContenidoHandler();
                return ch.ObtenerContenidoXId(idContenido);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public void CompartirContenido(Contenido contenido)
        {
            try
            {
                ContenidoHandler ch = new ContenidoHandler();
                ch.CompartirContenido(contenido);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public void AddInadecuado(int idUsr, int idContenido)
        {
            try
            {
                ContenidoHandler ch = new ContenidoHandler();
                ch.AddInadecuado(idUsr, idContenido);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public void AddMeGusta(int idUsr, int idContenido)
        {
            try
            {
                ContenidoHandler ch = new ContenidoHandler();
                ch.AddMeGusta(idUsr, idContenido);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public OpcionesContenido GetOpcionesContenido(int idUsr, int idContenido)
        {
            try
            {
                ContenidoHandler ch = new ContenidoHandler();
                return ch.GetOpcionesContenido(idUsr, idContenido);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public List<Contenido> ObtenerContenidoMasRankeado(int idMovimiento, string filtro)
        {
            try
            {
                ContenidoHandler ch = new ContenidoHandler();
                return ch.ObtenerContenidoMasRankeado(idMovimiento, filtro);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        public List<Contenido> ObtenerContenidoMasReciente(int idMovimiento, string filtro)
        {
            try
            {
                ContenidoHandler ch = new ContenidoHandler();
                return ch.ObtenerContenidoMasReciente(idMovimiento, filtro);
            }
            catch (Exception ex)
            {
                throw new FaultException<ProcessExecutionFault>
                    (new ProcessExecutionFault(), ex.Message);
            }
        }

        #endregion

        
    }
}
