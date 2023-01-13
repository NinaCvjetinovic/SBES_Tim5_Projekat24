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

        private readonly string _textFilePath;
        private readonly string _xmlFilePath;
        private readonly FileLogger _fileLogger;


        public Biblioteka()
        {
            _textFilePath = @"C:\Users\Vedrana\Desktop\ProjekatSBES\SBES_Tim5_Projekat24\Projekat_SBES\Manager\logWindows.txt";
            _xmlFilePath = @"C:\Users\Vedrana\Desktop\ProjekatSBES\SBES_Tim5_Projekat24\Projekat_SBES\Manager\logCertificates.xml";
            _fileLogger = new FileLogger(_textFilePath, _xmlFilePath);
        }

        public Biblioteka(string textFilePath = @"C:\Users\Vedrana\Desktop\ProjekatSBES\SBES_Tim5_Projekat24\Projekat_SBES\Manager\logWindows.txt",
                          string xmlFilePath = @"C:\Users\Vedrana\Desktop\ProjekatSBES\SBES_Tim5_Projekat24\Projekat_SBES\Manager\logCertificates.xml")
        {
            _textFilePath = textFilePath;
            _xmlFilePath = xmlFilePath;
            _fileLogger = new FileLogger(_textFilePath, _xmlFilePath);
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
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Dodavanje autora",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
                else
                {
                    Database.autori.Add(idAutora, autor);
                    Console.WriteLine("Autor uspesno dodat");
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Dodavanje autora",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Dodavanje autora",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                    //kreiramo LogEntry i logujemo u odgovarajuću datoteku
                    
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Dodavanje knjige",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }
                        
                    }
                    return false;
                }
                else
                {
                    Database.knjige.Add(idKnjige, knjiga);
                    Console.WriteLine("Knjiga je uspesno dodata.");
                    //logovanje uspeha
                    
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Dodavanje knjige",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }
                        
                    }
                    
                    
                    return true;
                }
                   
            }
                    
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Dodavanje knjige",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Dodavanje korisnika",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
                else
                {
                    Database.korisnici.Add(idKorisnika, korisnik);
                    Console.WriteLine("Korisnik uspesno dodat.");
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Dodavanje korisnika",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Dodavanje korisnika",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Izmena autora",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Autor sa datim id-em ne postoji.");
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Izmena autora",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Izmena autora",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Izmena knjige",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return true;
                    
                }
                else
                {
                    Console.WriteLine("Knjiga sa datim id-em ne postoji.");
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Izmena knjige",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Izmena knjige",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Izmena korisnika",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Korisnik sa tim id-em ne postoji");
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Izmena korisnika",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Izmena korisnika",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                            var log = new LogEntry
                            {
                                Timestamp = DateTime.Now,
                                Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                                Action = "Iznajmljivanje knjige",
                                Result = "Autorizacija uspesna"
                            };
                            string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                            if (i != null)
                            {
                                if (i.Contains(','))
                                {
                                    _fileLogger.LogToTextFile(log);
                                }
                                else
                                {
                                    _fileLogger.LogToXmlFile(log);
                                }

                            }
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
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Iznajmljivanje knjige",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Iznajmljivanje knjige",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Brisanje autora",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji autor sa datim id-em");
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Brisanje autora",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Brisanje autora",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Brisanje knjige",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji knjiga sa datim id-em");
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Brisanje knjige",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Brisanje knjige",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
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
                    var log = new LogEntry
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Brisanje korisnika",
                        Result = "Autorizacija uspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji korisnik sa datim id-em");
                    LogEntry log = new LogEntry()
                    {
                        Timestamp = DateTime.Now,
                        Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                        Action = "Brisanje knjige",
                        Result = "Autorizacija neuspesna"
                    };
                    string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                    if (i != null)
                    {
                        if (i.Contains(','))
                        {
                            _fileLogger.LogToTextFile(log);
                        }
                        else
                        {
                            _fileLogger.LogToXmlFile(log);
                        }

                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
                var log = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Username = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name),
                    Action = "Brisanje korisnika",
                    Result = "Autorizacija neuspesna"
                };
                string i = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
                if (i != null)
                {
                    if (i.Contains(','))
                    {
                        _fileLogger.LogToTextFile(log);
                    }
                    else
                    {
                        _fileLogger.LogToXmlFile(log);
                    }
                }
            }
            return false;
            
        }
    }
}
