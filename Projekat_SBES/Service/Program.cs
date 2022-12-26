using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/Biblioteka";

            ServiceHost host = new ServiceHost(typeof(Biblioteka));
            host.AddServiceEndpoint(typeof(IBiblioteka), binding, address);

            host.Open();

            Console.WriteLine("Servis je uspesno pokrenut.");

            Console.ReadLine();

        }
    }
}
