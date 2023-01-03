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
            NetTcpBinding binding1 = new NetTcpBinding();
            binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address = "net.tcp://localhost:49685/Biblioteka";

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:49673/Biblioteka"),
                                    new X509CertificateEndpointIdentity(srvCert));


            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            Console.WriteLine("Korisnik koji je pokrenuo klijenta:" + WindowsIdentity.GetCurrent().Name);

            Console.WriteLine("------------------------------------------------------");

            // Set the subject name of the certificate to look for.
            string subjectName = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            // Open the store for the current user.
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            // Find the certificate with the specified subject name.
            X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, false);

            // Check if a matching certificate was found.
            if (certificates.Count > 0)
            {
                Console.WriteLine("A certificate with subject name '{0}' was found.", subjectName);
                // A matching certificate was found.

              
                    using (ClientProxySertifikati proxy1 = new ClientProxySertifikati(binding1, address1))
                    {
                        Console.WriteLine("Klijent sertifikatima je uspesno pokrenut.");

                        try
                        {
                            Knjiga k = new Knjiga(ZanrKnjige.Drama, "Lovac na zmajeve", new Autor("Haled", "Hoseini", "1965"));
                            proxy1.DodajKnjigu(3, k);
                            Console.WriteLine(k);
                            Knjiga k1 = new Knjiga(ZanrKnjige.Triler, "Jedini izlaz", new Autor("Marko", "Popovic", "1978"));
                            proxy1.DodajKnjigu(4, k1);
                            Console.WriteLine(k1);
                            Knjiga k2 = new Knjiga(ZanrKnjige.Misterija, "Igra", new Autor("Skot", "Kerso", "1982"));
                            proxy1.IzmijeniKnjigu(4, k2);
                            Console.WriteLine(k2);
                            proxy1.ObrisiKnjigu(3);
                            proxy1.ObrisiKnjigu(6);
                            Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("[StackTrace] {0}", e.StackTrace);
                        }
                    }
             

                
            }
            else
            {
                // No matching certificate was found.
                Console.WriteLine("No certificate with subject name '{0}' was found.", subjectName);

                using (ClientProxy proxy = new ClientProxy(binding, address))
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
                    Console.ReadLine();
                }


            }

            // Close the store.
            store.Close();



            
            

               
          

            Console.ReadLine();
        }
    }
}
