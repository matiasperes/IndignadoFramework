using System;
using System.Collections.Generic;
using System.ServiceModel;
using IndignadoFramework.Entities;


namespace IndignadoFramework.Services.Contracts
{

    // [ServiceContract(
    //Namespace = "http://serena-yeoh//LayerSample/Expense/2007/08",
    //SessionMode = SessionMode.Allowed)]
    [ServiceContract]
    public interface IIntegracionService
    {

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        void agregarMensaje(MensajeChat mensaje);

        [OperationContract]
        [FaultContract(typeof(ProcessExecutionFault))]
        List<MensajeChat> obtenerMensajes(int idRoom);

    }



}
