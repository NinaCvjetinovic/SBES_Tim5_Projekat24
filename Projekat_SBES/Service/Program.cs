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
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            NetTcpBinding binding = new NetTcpBinding();
           
            string address = "net.tcp://localhost:49685/Biblioteka";

            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            //binding.Security.Mode = SecurityMode.Transport;
            //binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            //binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            ServiceHost host = new ServiceHost(typeof(Biblioteka));
            host.AddServiceEndpoint(typeof(IBiblioteka), binding, address);

           
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

  
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
          
          
            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

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
