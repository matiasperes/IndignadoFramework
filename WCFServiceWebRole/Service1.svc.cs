using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole
{

    public class Service1 : IService1
    {
        public string GetHello()
        {
            return "Hello from my WCF service in Windows Azure!";
        }
    }
}