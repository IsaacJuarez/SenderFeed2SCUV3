using System;
using System.ServiceProcess;

namespace FUJI.SenderFeed2SCU.Service
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new SenderFeed2SCUService()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.ReadKey();
            }
        }
    }
}
