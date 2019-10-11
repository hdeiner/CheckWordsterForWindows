using System;
using Nancy.Hosting.Self;

namespace CheckWordsterForWindows.Server
{
    class CheckWordsterStandaloneServer
    {
        static void Main(string[] args)
        {
            var hostConfigs = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            Uri uri = new Uri("http://localhost:9090");
            var host = new NancyHost(hostConfigs, uri);

            host.Start();

            Console.WriteLine("CheckWordsterForWindows.Server is now running");
            Console.WriteLine("Press enter to exit");

            Console.ReadLine();
        }
    }
}