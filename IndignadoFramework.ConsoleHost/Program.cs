using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using System.ServiceModel;
using System.Messaging;

using IndignadoFramework.Services;
using IndignadoFramework.Business;


using System.ServiceModel.Description;
using System.ServiceModel.Activities;

namespace IndignadoFramework.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost frontSvcHost = null;
            ServiceHost backSvcHost = null;
            ServiceHost integracionSvcHost = null;
            
            try
            {
                Console.WriteLine("Starting Indignado Host ...");

                backSvcHost = LoadBackOfficeService();
                frontSvcHost = LoadFrontOfficeService();
                integracionSvcHost = LoadIntegracionOfficeService();

                Console.WriteLine("Starting Indignado WatchDog ...");
                LoadWatchDog();

                Console.WriteLine("\nIndignado ConsoleHost started. Press any key to exit.\n\n");
                Console.Read();
            }
            catch 
            {
                Console.WriteLine("\nHost initialization has failed.");
                Console.WriteLine("Press any key to terminate.");
                Console.Read();
            }
            finally
            {
                if (backSvcHost != null)
                    backSvcHost.Close();
                if (frontSvcHost != null)
                    frontSvcHost.Close();
                if (integracionSvcHost != null)
                    integracionSvcHost.Close();
            }

        }

        private static ServiceHost LoadIntegracionOfficeService()
        {
            ServiceHost svcHost = null;
            try
            {
                Console.Write("Loading IntegracionOffice Service ... ");

                // Create Service Host.
                svcHost = new ServiceHost(typeof(IntegracionService));
                svcHost.Open();

                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError Loading IntegracionOffice  Service. ");
                Console.WriteLine("Exception : " + ex.Message);
                throw;
            }

            return svcHost;
        }

        private static ServiceHost LoadFrontOfficeService()
        {
            ServiceHost svcHost = null;
            try
            {
                Console.Write("Loading FrontOffice Service ... ");

                // Create Service Host.
                svcHost = new ServiceHost(typeof(FrontOfficeService));
                svcHost.Open();

                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError Loading FrontOffice Service. ");
                Console.WriteLine("Exception : " + ex.Message);
                throw;
            }

            return svcHost;
        }
        
        private static ServiceHost LoadBackOfficeService()
        {
            ServiceHost svcHost = null;
            try
            {
                Console.Write("Loading BackOffice Service ... ");

                // Create Service Host.
                svcHost = new ServiceHost(typeof(BackOfficeService));
                svcHost.Open();

                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError Loading BackOffice Service. ");
                Console.WriteLine("Exception : " + ex.Message);
                throw;
            }

            return svcHost;
        }

        private static void LoadWatchDog()
        {
            try
            {
                WatchDogHandler.StarWatchDog();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError Loading WatchDog. ");
                Console.WriteLine("Exception : " + ex.Message);
                throw;
            }

        }

      
    }
}
