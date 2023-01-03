using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Manager;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
           

            NetTcpBinding binding = new NetTcpBinding();
           
            string address = "net.tcp://localhost:49685/Biblioteka";

            

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            ServiceHost host = new ServiceHost(typeof(Biblioteka));
            host.AddServiceEndpoint(typeof(IBiblioteka), binding, address);


            try
            {
                host.Open();

                Console.WriteLine("Servis je uspesno pokrenut.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            Console.WriteLine("Korisnik koji je pokrenuo servis:" + WindowsIdentity.GetCurrent().Name);
            Console.ReadLine();

        }
    }
}
