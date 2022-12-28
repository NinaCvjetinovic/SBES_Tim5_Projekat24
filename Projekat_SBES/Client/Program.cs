using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            string srvCertCN = "wcfservice";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            //string address = "net.tcp://localhost:9999/Biblioteka";

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Biblioteka"),
                                      new X509CertificateEndpointIdentity(srvCert));

        

     
            //binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            
           
            
           // binding.Security.Mode = SecurityMode.Transport;
            //binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            //binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            
            Console.WriteLine("Korisnik koji je pokrenuo klijenta:" + WindowsIdentity.GetCurrent().Name);

            Console.WriteLine("------------------------------------------------------");

            using (ClientProxy proxy = new ClientProxy(binding,address))
            {
                Console.WriteLine("Klijent je uspesno pokrenut.");
               

                Knjiga k = new Knjiga(ZanrKnjige.Drama, "Lovac na zmajeve", new Autor("Haled", "Hoseini", "1965"));
                proxy.DodajKnjigu(3, k);
                Console.WriteLine(k);
                Knjiga k1 = new Knjiga(ZanrKnjige.Triler, "Jedini izlaz", new Autor("Marko", "Popovic", "1978"));
                proxy.DodajKnjigu(4, k1);
                Console.WriteLine(k1);
                Knjiga k2 = new Knjiga(ZanrKnjige.Misterija, "Igra", new Autor("Skot", "Kerso", "1982"));
                proxy.IzmijeniKnjigu(4, k2);
                Console.WriteLine(k2);
                proxy.ObrisiKnjigu(3);
                proxy.ObrisiKnjigu(6);
            }

            Console.ReadLine();
        }
    }
}
