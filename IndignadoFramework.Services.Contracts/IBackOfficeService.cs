using System;
using System.Collections.Generic;
using System.ServiceModel;
using IndignadoFramework.Entities;


namespace IndignadoFramework.Services.Contracts
{

    [ServiceContract]
    public interface IBackOfficeService
    {

        [OperationContract]
        string GetHello();

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        void AgregarFuente(FuenteWEB fuente);

        [OperationContract]
        [FaultContract(typeof (ProcessExecutionFault))]
        List<Movimiento> ObtenerTodosMovimientos();

        [OperationContract]
        [FaultContract(typeof (ProcessExecutionFault))]
        Movimiento AgregarMovimiento(Movimiento movimiento);

        [OperationContract]
        [FaultContract(typeof (ProcessExecutionFault))]
        void BorrarMovimiento(Movimiento movimiento);

        [OperationContract]
        [FaultContract(typeof (ProcessExecutionFault))]
        void ModificarMovimiento(Movimiento movimiento);

        [OperationContract]
        [FaultContract(typeof (ProcessExecutionFault))]
        Movimiento ObtenerMovimientoXNombre(string nombreMovimiento);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        Movimiento ObtenerMovimientoXId(int idMovimiento);

        [OperationContract]
        [FaultContract(typeof (ProcessExecutionFault))]
        List<Movimiento> ObtenerMovimientosPaginando(int pageNumber, int pageSize);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        List<Contenido> ObtenerContenidosMovimientoPorInadecuado(int idMovimiento);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        void EliminarEspecificacionUsuario(int idus); 
    }



}
