using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using IndignadoFramework.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest
{
    [TestClass]
    public class FrontOfficeUnitTest
    {
        [TestMethod]
        public void AgregarConvocatoriaTestMethod()
        {
            var front = new FrontOffice.FrontOfficeServiceClient();
            var conv = new Convocatoria {Descripcion = "test 1",Inicio = DateTime.Now,Titulo = "titulo 1"};

            conv.Movimiento = new Movimiento { Nombre = "test 1"};
            conv.CategoriaTematica = new CategoriaTematica { Nombre = "test 1",Descripcion = "desc 1"};
            
            Assert.IsNotNull(front.AgregarConvocatoria(conv));
        }
    }
}
