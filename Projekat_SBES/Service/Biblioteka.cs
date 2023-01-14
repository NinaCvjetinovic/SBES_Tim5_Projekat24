using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service
{
    public class Biblioteka : IBiblioteka
    {

        public readonly string _textFilePath;
        public readonly string _xmlFilePath;
        public readonly FileLogger _fileLogger;


        public Biblioteka()
        {
            _textFilePath = @"C:\Users\Nina\Desktop\ProjekatSBES\SBES_Tim5_Projekat24\Projekat_SBES\Manager\logWindows.txt";
            _xmlFilePath = @"C:\Users\Nina\Desktop\ProjekatSBES\SBES_Tim5_Projekat24\Projekat_SBES\Manager\logCertificates.xml";
            _fileLogger = new FileLogger(_textFilePath, _xmlFilePath);
        }

        public Biblioteka(string textFilePath = @"C:\Users\Nina\Desktop\ProjekatSBES\SBES_Tim5_Projekat24\Projekat_SBES\Manager\logWindows.txt",
                          string xmlFilePath = @"C:\Users\Nina\Desktop\ProjekatSBES\SBES_Tim5_Projekat24\Projekat_SBES\Manager\logCertificates.xml")
        {
            _textFilePath = textFilePath;
            _xmlFilePath = xmlFilePath;
            _fileLogger = new FileLogger(_textFilePath, _xmlFilePath);
        }

        public void uspjesnaAutorizacija(string action)
        {
            LogEntry log = new LogEntry()
            {
                Timestamp = DateTime.Now,
                Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                Action = action,
                Result = "Uspjesna autorizacija"
            };
            string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            if (i != null)
            {
                if (!i.Contains(','))
                {
                    _fileLogger.LogToTextFile(log);
                }
                else
                {
                    _fileLogger.LogToXmlFile(log);
                }

            }
        }

        public void neuspjesnaAutorizacija(string action)
        {
            var log = new LogEntry
            {
                Timestamp = DateTime.Now,
                Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                Action = action,
                Result = "Neuspjesna autorizcija"
            };
            string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            if (i != null)
            {
                if (!i.Contains(','))
                {
                    _fileLogger.LogToTextFile(log);
                }
                else
                {
                    _fileLogger.LogToXmlFile(log);
                }
            }
        }



        public bool DodajAutora(int idAutora,Autor autor)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;

            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                

                if (username == ime)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "read")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }
                }
                    if (provera)
                        break;

                else
                {

                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "read")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.autori.ContainsKey(idAutora))
                {
                    Console.WriteLine("Autor sa datim id-em vec postoji.");

                    uspjesnaAutorizacija("Read");
                    return false;
                }
                else
                {
                    Database.autori.Add(idAutora, autor);
                    Console.WriteLine("Autor uspesno dodat");

                    uspjesnaAutorizacija("Read");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                neuspjesnaAutorizacija("Read");
            }
            return false;
        }

        

        public bool DodajKnjigu(int idKnjige, Knjiga knjiga)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provjera = false;

            foreach (var userElement in users)
            {

                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "read")
                        {
                            hasPermission = true;
                            provjera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                        
                        }
                    }
                
                }
                    if (provjera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();
                        foreach (var i in permissions)
                        {
                            if (i == "read")
                            {
                                hasPermission = true;
                                provjera = true;
                                break;
                            }
                            else
                            {
                                hasPermission = false;
                            }
                        }
                    }
                    if (provjera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.knjige.ContainsKey(idKnjige))
                {
                    Console.WriteLine("Knjiga sa datim id-em vec postoji.");

                    uspjesnaAutorizacija("Read");
                    return false;
                }
                else
                {
                    Database.knjige.Add(idKnjige, knjiga);
                    Console.WriteLine("Knjiga je uspesno dodata.");

                    uspjesnaAutorizacija("Read");
                    
                    
                    return true;
                }
                   
            }
                    
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Read");
            }
            return false;
        }

        

        public bool DodajKorisnika(int idKorisnika, Korisnik korisnik)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "read")
                        {
                            hasPermission = true;
                            provera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                           
                        }
                    }

                }
                    if (provera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "read")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.korisnici.ContainsKey(idKorisnika))
                {
                    Console.WriteLine("Korisnik sa datim id-em vec postoji.");

                    uspjesnaAutorizacija("Read");
                    return false;
                }
                else
                {
                    Database.korisnici.Add(idKorisnika, korisnik);
                    Console.WriteLine("Korisnik uspesno dodat.");

                    uspjesnaAutorizacija("Read");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Read");
            }
            return false;
        }

        public bool IzmijeniAutora(int idAutora, Autor autor)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "modify")
                        {
                            hasPermission = true;
                            provera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                       
                        }
                    }

                }
                    if (provera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "modify")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.autori.ContainsKey(idAutora))
                {
                    Database.autori[idAutora] = autor;
                    Console.WriteLine("Autor uspesno izmenjen.");

                    uspjesnaAutorizacija("Modify");
                    return true;
                }
                else
                {
                    Console.WriteLine("Autor sa datim id-em ne postoji.");

                    uspjesnaAutorizacija("Modify");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Modify");
            }
            return false;
            
        }

        public bool IzmijeniKnjigu(int idKnjige, Knjiga knjiga)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "modify")
                        {
                            hasPermission = true;
                            provera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                         
                        }
                    }

                }
                    if (provera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "modify")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.knjige.ContainsKey(idKnjige))
                {
                    Database.knjige[idKnjige] = knjiga;
                    Console.WriteLine("Knjiga uspesno izmenjena.");

                    uspjesnaAutorizacija("Modify");
                    return true;
                    
                }
                else
                {
                    Console.WriteLine("Knjiga sa datim id-em ne postoji.");

                    uspjesnaAutorizacija("Modify");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Modify");
            }
            return false;
            
        }

        public bool IzmijeniKorisnika(int idKorisnika, Korisnik korisnik)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "modify")
                        {
                            hasPermission = true;
                            provera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                        
                        }
                    }

                }
                    if (provera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "modify")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.korisnici.ContainsKey(idKorisnika))
                {
                    Database.korisnici[idKorisnika] = korisnik;
                    Console.WriteLine("Korisnik uspesno izmenjen");

                    uspjesnaAutorizacija("Modify");
                    return true;
                }
                else
                {
                    Console.WriteLine("Korisnik sa tim id-em ne postoji");

                    uspjesnaAutorizacija("Modify");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Modify");
            }
            return false;
        }

        public bool IznajmiKnjigu(int idKorisnika, string nazivKnjige)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "manage")
                        {
                            hasPermission = true;
                            provera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                            
                        }
                    }

                }
                    if (provera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "manage")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.korisnici.ContainsKey(idKorisnika))
                {
                    if (Database.korisnici[idKorisnika].Aktivan == true)
                    {
                        if (Database.korisnici[idKorisnika].BrojKnjiga <= 5)
                        {
                            Database.korisnici[idKorisnika].BrojKnjiga++;
                            Console.WriteLine("Knjiga uspesno iznajmljena");

                            uspjesnaAutorizacija("Manage");
                            return true;
                        }
                        Console.WriteLine("Korisnik ima vise od pet knjiga");
                        return false;
                    }
                    Console.WriteLine("Korisnik nije aktivan clan biblioteke");
                    return false;
                }
                else
                {
                    Console.WriteLine("Korisnik sa tim id-em ne postoji");

                    uspjesnaAutorizacija("Manage");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Manage");
            }
            return false;
            
        }

        public bool ObrisiAutora(int idAutora)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "delete")
                        {
                            hasPermission = true;
                            provera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                        
                        }
                    }

                }
                    if (provera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "delete")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.autori.ContainsKey(idAutora))
                {
                    Database.autori.Remove(idAutora);
                    Console.WriteLine("Autor uspesno obrisan");

                    uspjesnaAutorizacija("Delete");
                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji autor sa datim id-em");

                    uspjesnaAutorizacija("Delete");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Delete");
            }
            return false;
        }

        public bool ObrisiKnjigu(int idKnjige)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "delete")
                        {
                            hasPermission = true;
                            provera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                         
                        }
                    }

                }
                    if (provera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "delete")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.knjige.ContainsKey(idKnjige))
                {
                    Database.knjige.Remove(idKnjige);
                    Console.WriteLine("Knjiga uspesno obrisana");

                    uspjesnaAutorizacija("Delete");
                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji knjiga sa datim id-em");

                    uspjesnaAutorizacija("Delete");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Delete");
            }
            return false;
        }

        public bool ObrisiKorisnika(int idKorisnika)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("../../../Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                string ime = "";
                if (client.Contains(','))
                {
                    ime = client.Split(',')[0].Split('=')[1];
                }

                if (username == ime)
                {
                    List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                    foreach (var i in permissions)
                    {
                        if (i == "delete")
                        {
                            hasPermission = true;
                            provera = true;
                            break;

                        }
                        else
                        {
                            hasPermission = false;
                            
                        }
                    }

                }
                    if (provera)
                        break;
                else
                {
                    if (username == client)
                    {
                        List<string> permissions = userElement.Elements("permission").Select(p => p.Attribute("type").Value).ToList();

                        foreach (var i in permissions)
                        {
                            if (i == "delete")
                            {
                                hasPermission = true;
                                provera = true;
                                break;

                            }
                            else
                            {
                                hasPermission = false;

                            }
                        }

                    }
                    if (provera)
                        break;
                }
            }
            if (hasPermission == true)
            {
                if (Database.korisnici.ContainsKey(idKorisnika))
                {
                    Database.korisnici.Remove(idKorisnika);
                    Console.WriteLine("Korisnik uspesno obrisan");

                    uspjesnaAutorizacija("Delete");
                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji korisnik sa datim id-em");

                    uspjesnaAutorizacija("Delete");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");

                neuspjesnaAutorizacija("Delete");
            }
            return false;
            
        }
    }
}
