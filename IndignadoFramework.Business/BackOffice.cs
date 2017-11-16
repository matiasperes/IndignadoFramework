using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndignadoFramework.Entities;
using System.Transactions;
using IndignadoFramework.DAC;

namespace IndignadoFramework.Business
{
    public class BackOffice
    {
        #region Gestion de movivientos de protesta

        //Gestión de movimientos de protesta
        //Como parte del módulo de administración se debe brindar un mantenimiento de movimientos
        //de protesta, el cual permita gestionar la instanciación del sistema para nuevos movimientos.
        //Como parte de la información que se debe manejar se encuentra:
        //· Nombre del movimiento
        //· Ubicación geográfica central
        //· Descripción
        //· Logo, y estilos CSS
        //· Galería de imágenes
        //· Configuración de fuentes de orígenes de datos desde los cuales tomar noticias,
        //imágenes, y videos. Se deben brindar mecanismos ya sea por interfaz de usuario, o por
        //la distribución de librerías que permitan sin recompilar, ni reinstalar el sistema,
        //especificar los orígenes de datos (YouTube, CNN, BBC, Wikipedia, RSS, etc), y los filtros
        //sobre estas fuentes de datos.

        public Movimiento AgregarMovimiento(Movimiento movimiento)
        {
            Console.WriteLine("Creando movimiento... ");
            
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    movimiento = CreateMovimiento(movimiento);
                    //LogStatus(movimiento);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("Nuevo movimiento = " + movimiento.Id);
            
            return movimiento;
        }

        public void AgregarFuente(FuenteWEB fuente)
        {
            FuenteWEBDAC fdac = new FuenteWEBDAC();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    fdac.AddFuenteWEB(fuente);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }


        private Movimiento CreateMovimiento(Movimiento movimiento)
        {
            // Business logic.
            //movimiento.IsCompleted = false;
            //movimiento.DateSubmitted = DateTime.Now;
            //movimiento.DateModified = DateTime.Now;

            Console.WriteLine(movimiento.ToString());

            // Persist data.
            var dac = new MovimientoDAC();
            try
            {
                return dac.AddMovimiento(movimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void BorrarMovimiento(Movimiento movimiento)
        {
            Console.WriteLine("Borrando movimiento"+movimiento.Id);

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DeleteMovimiento(movimiento);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("Movimiento Borrado");
        }

        public void DeleteMovimiento(Movimiento movimiento)
        {
            // Business logic.
            //movimiento.IsCompleted = false;
            //movimiento.DateSubmitted = DateTime.Now;
            //movimiento.DateModified = DateTime.Now;

            Console.WriteLine(movimiento.ToString());

            // Persist data.
            var dac = new MovimientoDAC();
            try
            {
                dac.DeleteMovimiento(movimiento.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void ModificarMovimiento(Movimiento movimiento)
        {
            Console.WriteLine("Modificando movimiento" + movimiento.Id);

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    UpdateMovimiento(movimiento);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("Movimiento Modificado");
        }
  
        private void UpdateMovimiento(Movimiento movimiento)
        {
            // Business logic.
            //movimiento.IsCompleted = false;
            //movimiento.DateSubmitted = DateTime.Now;
            //movimiento.DateModified = DateTime.Now;

            Console.WriteLine(movimiento.ToString());

            // Persist data.
            var dac = new MovimientoDAC();
            try
            {
                dac.UpdateMovimiento(movimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Movimiento ObtenerMovimientoXId(int idMovimiento)
        {
            Console.WriteLine("Obtener movimiento con ID" + idMovimiento);
            Movimiento movimiento;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    movimiento = GetMovimientoById(idMovimiento);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("Movimiento Obtenido");
            return movimiento;
        }

        private Movimiento GetMovimientoById(int idMovimiento)
        {
            // Business logic.
            //movimiento.IsCompleted = false;
            //movimiento.DateSubmitted = DateTime.Now;
            //movimiento.DateModified = DateTime.Now;

            Console.WriteLine(idMovimiento);

            // Persist data.
            var dac = new MovimientoDAC();
            try
            {
                return dac.GetMovimientoById(idMovimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public Movimiento ObtenerMovimientoXNombre(string nombreMovimiento)
        {
            Console.WriteLine("Obtener movimiento con nombre" + nombreMovimiento);
            Movimiento movimiento;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    movimiento = GetMovimientoByNombre(nombreMovimiento);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("Movimiento Obtenido");
            return movimiento;
        }
  
        private Movimiento GetMovimientoByNombre(string nombreMovimiento)
        {
            // Business logic.
            //movimiento.IsCompleted = false;
            //movimiento.DateSubmitted = DateTime.Now;
            //movimiento.DateModified = DateTime.Now;

            Console.WriteLine(nombreMovimiento);

            // Persist data.
            var dac = new MovimientoDAC();
            try
            {
                return dac.GetMovimientoByNombre(nombreMovimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<Movimiento> ObtenerMovimientosPaginando(int pageNumber, int pageSize)
        {
            Console.WriteLine("Obtener movimientos paginando \nnumero de pagina =" + pageNumber + "\ntamaño de pagina ="+ pageSize);
            List<Movimiento> movimientos;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    movimientos = GetMovimientosPaginando( pageNumber, pageSize);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("Movimientos Obtenidos");
            return movimientos;
        }

        private List<Movimiento> GetMovimientosPaginando(int pageNumber, int pageSize)
        {
            // Business logic.
            //movimiento.IsCompleted = false;
            //movimiento.DateSubmitted = DateTime.Now;
            //movimiento.DateModified = DateTime.Now;

            //Console.WriteLine(nombreMovimiento);

            // Persist data.
            var dac = new MovimientoDAC();
            try
            {
                return dac.GetMovimientosPaginado(pageNumber,pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<Movimiento> ObtenerTodosMovimientos()
        {
            Console.WriteLine("Obtener movimientos");
            List<Movimiento> movimientos;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    movimientos = GetAllMovimientos();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            Console.WriteLine("Movimientos Obtenidos");
            return movimientos;
        }

        private List<Movimiento> GetAllMovimientos()
        {
            // Business logic.
            //movimiento.IsCompleted = false;
            //movimiento.DateSubmitted = DateTime.Now;
            //movimiento.DateModified = DateTime.Now;

            //Console.WriteLine(nombreMovimiento);

            // Persist data.
            var dac = new MovimientoDAC();
            try
            {
                return dac.GetAllMovimiento();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        
        #endregion  
        
        #region Gestión de Usuarios Registrados

        //Gestión de Usuarios Registrados
        //El módulo de administración deberá permitir dar de baja a usuarios que hayan subido
        //contenidos marcados como inapropiados, luego de una revisión de dichos contenidos.
        
        #endregion

        #region Watchdog

        //Watchdog
        //El módulo de administración deberá contar con un proceso auxiliar de control que dé de baja
        //todos los contenidos con más de X marcas de inadecuado, y si es la vez número Z de veces que
        //se dio de baja contenidos de un mismo usuario, entonces dar de baja al usuario.
        
        #endregion

        #region Vista de Reportes

        //Vista de Reportes
        //Como parte del módulo de administración se debe brindar una vista que incluya los siguientes
        //reportes:
        //· Reporte de registros en el tiempo. Un reporte, que liste y/o grafique la cantidad de
        //usuarios registrados en el tiempo.
        //· Reporte de contenidos inadecuados. Un listado que detalle todos los contenidos que
        //hayan sido marcados como inadecuados, ordenándolos por la cantidad de marcas que
        //recibió.

        #endregion

        #region Gestión de variables del sistema

        //Gestión de variables del sistema
        //Una vista que permita configurar todos los parámetros del sistema, tales como las variables: N,
        //M, X y Z mencionadas a lo largo de la letra.

        #endregion
        
    }
}
