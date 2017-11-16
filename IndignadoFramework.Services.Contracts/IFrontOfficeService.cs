using System;
using System.Collections.Generic;
using System.ServiceModel;
using IndignadoFramework.Entities;
//using System.Web;
//using System.Web.Security;


namespace IndignadoFramework.Services.Contracts
{

    [ServiceContract(
    Namespace = "http://serena-yeoh//LayerSample/Expense/2007/08",
    SessionMode = SessionMode.Allowed)]
    public interface IFrontOfficeService
    {
        #region Usuarios

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        EspecificacionUsuario AgregarUsuario(EspecificacionUsuario espus, String nomMov, String[] categs);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        EspecificacionUsuario ObtenerEspecificacionUsuarioXId(int idUsuario);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        EspecificacionUsuario ObtenerEspecificacionUsuarioXMembership(String membership, int idMov);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        void ModificarEspecificacionUsuario(EspecificacionUsuario espus, String[] categs);
        /*
        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        bool ValidateUser(string userName, string password);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        MembershipCreateStatus CreateUser(string userName, string password, string email);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        */
        #endregion


        /***************CONVOCATORIAS******************************/
        #region Convocatorias

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        List<FuenteWEB> GetFuentesDatosMovimiento(int idMov);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        Convocatoria ObtenerConvocatoriaXId(int idConvocatoria);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        Convocatoria AgregarConvocatoria(Convocatoria convocatoria);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        int ConfirmarAsistenciaConvocatoria(int idConvocatoria, int idUsuario);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        List<Convocatoria> ObtenerConvocatoriasMovimiento(int idMovimiento);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        List<Convocatoria> ObtenerConvocatoriasPaginando(int pageNumber, int pageSize);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        List<CategoriaTematica> ObtenerCategoriasTematicas();

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        bool ConfirmoAsistenciaUsuario(int idConvocatoria, int idUsuario);

        #endregion
        
        /****************CONTENIDO*********************************/
        #region Contenido

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        Contenido ObtenerContenidoXId(int idContenido);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        void CompartirContenido(Contenido contenido);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        void AddInadecuado(int idUsr, int idContenido);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        void AddMeGusta(int idUsr, int idContenido);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        OpcionesContenido GetOpcionesContenido(int idUsr, int idContenido);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        List<Contenido> ObtenerContenidoMasRankeado(int idMovimiento, string filtro);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        List<Contenido> ObtenerContenidoMasReciente(int idMovimiento, string filtro);

        #endregion
    

    }



}
