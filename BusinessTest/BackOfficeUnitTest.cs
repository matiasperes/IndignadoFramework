using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using IndignadoFramework.Entities;

namespace BusinessTest
{
    [TestClass]
    public class BackOfficeUnitTest
    {
        [TestMethod]
        public void AgregarMovimientoTestMethod()
        {
            var back = new BusinessTest.BackOffice.BackOfficeServiceClient();
            var mov = new Movimiento {Nombre = "Test"};
            
            Assert.IsNotNull(back.AgregarMovimiento(mov));

        }

        [TestMethod]
        public void ObtenerMovimientoXNombreTestMethod()
        {
            var back = new BusinessTest.BackOffice.BackOfficeServiceClient();
            var mov = new Movimiento { Nombre = "Test1" };
            back.AgregarMovimiento(mov);
            Assert.IsNotNull(back.ObtenerMovimientoXNombre("Test1"));
        }

        [TestMethod]
        public void ObtenerMovimientoXIdTestMethod()
        {

            var back = new BusinessTest.BackOffice.BackOfficeServiceClient();
            var mov = new Movimiento { Nombre = "Test2" };
            back.AgregarMovimiento(mov);
            
            mov = back.ObtenerMovimientoXNombre("Test2");
            Assert.IsNotNull(back.ObtenerMovimientoXId(mov.Id));
        }

        [TestMethod]
        public void BorrarMovimientoTestMethod()
        {
            var back = new BusinessTest.BackOffice.BackOfficeServiceClient();
            var mov = new Movimiento { Nombre = "Test3" };
            back.AgregarMovimiento(mov);
            
            mov = back.ObtenerMovimientoXNombre("Test3");
            var idMov = mov.Id;
            back.BorrarMovimiento(mov);
            Assert.IsNull(back.ObtenerMovimientoXId(idMov));
        }

        [TestMethod]
        public void ModificarMovimientoTestMethod()
        {
            var back = new BusinessTest.BackOffice.BackOfficeServiceClient();
            var mov = new Movimiento { Nombre = "Test4" };
            back.AgregarMovimiento(mov);

            mov = back.ObtenerMovimientoXNombre("Test4");
            mov.Nombre = "Test444";
            back.ModificarMovimiento(mov);
            mov = back.ObtenerMovimientoXNombre("Test444");
            Assert.IsNotNull(mov);
        }

        [TestMethod]
        public void ObtenerMovimientosPaginandoTestMethod()
        {
            var back = new BusinessTest.BackOffice.BackOfficeServiceClient();
            var mov = new Movimiento { Nombre = "Test4" };
            back.AgregarMovimiento(mov);

            
            Assert.AreNotEqual(back.ObtenerMovimientosPaginando(1,1).Count() , 0);
        }

        [TestMethod]
        public void ObtenerTodosMovimientosTestMethod()
        {
            var back = new BusinessTest.BackOffice.BackOfficeServiceClient();
            var mov = new Movimiento { Nombre = "Test4" };
            back.AgregarMovimiento(mov);

            Assert.AreNotEqual(back.ObtenerTodosMovimientos().Count(), 0);
        }
    }
}
