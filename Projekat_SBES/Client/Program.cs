using Common;
using Manager;
using Service;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
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
            Biblioteka biblioteka = new Biblioteka();
            FileLogger fileLogger = biblioteka._fileLogger;

     
            string srvCertCN = "wcfservice";

            NetTcpBinding binding = new NetTcpBinding();
            NetTcpBinding binding1 = new NetTcpBinding();
            binding1.Security.Mode = SecurityMode.Transport;
            binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address = "net.tcp://localhost:49685/Biblioteka";

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:49673/Biblioteka"),
                                    new X509CertificateEndpointIdentity(srvCert));


            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;



            Console.WriteLine("Korisnik koji je pokrenuo klijenta:" + WindowsIdentity.GetCurrent().Name);

            Console.WriteLine("------------------------------------------------------------------------");

            string subjectName = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, false);



            if (certificates.Count > 0)
            {
                Console.WriteLine("A certificate with subject name '{0}' was found.", subjectName);



                using (ClientProxy proxy1 = new ClientProxy(binding1, address1))
                {
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(certificates[0].SubjectName.Name.Split(',')[0].Split('=')[1]),
                        Action = "",
                        Result = "Uspjesna autentifikacija"
                    };
                    string i = Formatter.ParseName(certificates[0].SubjectName.Name);
                    if (i != null)
                    {
                        if (!i.Contains(','))
                        {
                            fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            fileLogger.LogToXmlFile(log);
                        }

                    }

                    Console.WriteLine("Klijent sertifikatima je uspesno pokrenut.");

                    try
                    {
                      

                        Knjiga k = new Knjiga(ZanrKnjige.Drama, "Lovac na zmajeve", new Autor("Haled", "Hoseini", "1965"));
                        Korisnik kor = new Korisnik("Milica", "Milutinovic", true, 3);
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
                        proxy1.DodajKorisnika(888, kor);
                        proxy1.IznajmiKnjigu(888, "Lovac na zmajeve");

                        Autor a = new Autor("Ivo", "Andric", "1444");
                        proxy1.DodajAutora(100, a);
                        Korisnik kor2 = new Korisnik("Mika", "Peric", true, 2);
                        proxy1.DodajKorisnika(500, kor2);
                        proxy1.IznajmiKnjigu(500, "Bio jednom jedan strah");
                        Console.ReadLine();


                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("[StackTrace] {0}", e.StackTrace);
                    }
                }


                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No certificate with subject name '{0}' was found.", subjectName);
                Console.WriteLine("------------------------------------------------------------------------");
               
                    using (ClientProxy proxy = new ClientProxy(binding, address))
                    {

                        Console.WriteLine("Klijent - Windows je uspesno pokrenut.\n");

                        LogEntry log = new LogEntry()
                        {
                            Timestamp = DateTime.Now,
                            Username = Formatter.ParseName(WindowsIdentity.GetCurrent().Name),
                            Action = "",
                            Result = "Uspjesna autentifikacija"
                        };
                        string i = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
                        if (i != null)
                        {
                            if (!i.Contains(','))
                            {
                                fileLogger.LogToTextFile(log);
                            }
                            else
                            {
                                fileLogger.LogToXmlFile(log);
                            }

                        }

                        Knjiga k = new Knjiga(ZanrKnjige.Drama, "Lovac na zmajeve", new Autor("Haled", "Hoseini", "1965"));
                        proxy.DodajKnjigu(3, k);
                        Database.knjige.Add(3, k);
                        Knjiga k1 = new Knjiga(ZanrKnjige.Triler, "Jedini izlaz", new Autor("Marko", "Popovic", "1978"));
                        proxy.DodajKnjigu(4, k1);
                        Database.knjige.Add(4, k1);
                        
                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine("Knjige koje se trenutno nalaze u biblioteci:");
                        foreach (Knjiga knjiga in Database.knjige.Values)
                        {
                            Console.WriteLine(knjiga);
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine("------------------------------------------------------------------------");
                        Knjiga k2 = new Knjiga(ZanrKnjige.Misterija, "Igra", new Autor("Skot", "Kerso", "1982"));
                        proxy.IzmijeniKnjigu(4, k2);
                        Knjiga k3 = new Knjiga(ZanrKnjige.Komedija, "Bio jednom jedan strah", new Autor("Jovica", "Tisma", "1958"));
                        proxy.DodajKnjigu(5, k3);
                        proxy.ObrisiKnjigu(3);
                        Database.knjige.Remove(3);
                        proxy.ObrisiKnjigu(6);
                        
                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine("Knjige koje se nalaze u biblioteci posle brisanja:");
                        foreach (Knjiga knjiga in Database.knjige.Values)
                        {
                            Console.WriteLine(knjiga);
                        }
                        Console.WriteLine("------------------------------------------------------------------------\n");


                        Autor a = new Autor("Ivo", "Andric", "1444");
                        proxy.DodajAutora(100, a);
                        Korisnik kor = new Korisnik("Mika", "Peric", true, 2);
                        proxy.DodajKorisnika(500, kor);
                        proxy.IznajmiKnjigu(500, "Bio jednom jedan strah");
                        Console.WriteLine("\n");
                        Console.WriteLine("------------------------------------------------------------------------\n");
                        Console.WriteLine("Korisnici: ");
                        foreach (Korisnik korisnik in Database.korisnici.Values)
                        {
                            Console.WriteLine(korisnik);
                        }
                        Console.ReadLine();
                    }
                      

                    
               

                store.Close();
                Console.ReadLine();
            }
        }
    }
}
