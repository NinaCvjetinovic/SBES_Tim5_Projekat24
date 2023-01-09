using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service
{
    public class Biblioteka : IBiblioteka
    {
        public bool DodajAutora(int idAutora,Autor autor)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;

            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.autori.ContainsKey(idAutora))
                {
                    Console.WriteLine("Autor sa datim id-em vec postoji.");
                    return false;
                }
                else
                {
                    Database.autori.Add(idAutora, autor);
                    Console.WriteLine("Autor uspesno dodat");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
        }

        

        public bool DodajKnjigu(int idKnjige, Knjiga knjiga)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provjera = false;

            foreach (var userElement in users)
            {

                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provjera = true;
                            break;
                        }
                    }

                }
                if (provjera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.knjige.ContainsKey(idKnjige))
                {
                    Console.WriteLine("Knjiga sa datim id-em vec postoji.");
                    return false;
                }
                else
                {
                    Database.knjige.Add(idKnjige, knjiga);
                    Console.WriteLine("Knjiga je uspesno dodata.");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
        }

        

        public bool DodajKorisnika(int idKorisnika, Korisnik korisnik)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.korisnici.ContainsKey(idKorisnika))
                {
                    Console.WriteLine("Korisnik sa datim id-em vec postoji.");
                    return false;
                }
                else
                {
                    Database.korisnici.Add(idKorisnika, korisnik);
                    Console.WriteLine("Korisnik uspesno dodat.");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
        }

        public bool IzmijeniAutora(int idAutora, Autor autor)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.autori.ContainsKey(idAutora))
                {
                    Database.autori[idAutora] = autor;
                    Console.WriteLine("Autor uspesno izmenjen.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Autor sa datim id-em ne postoji.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
            
        }

        public bool IzmijeniKnjigu(int idKnjige, Knjiga knjiga)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.knjige.ContainsKey(idKnjige))
                {
                    Database.knjige[idKnjige] = knjiga;
                    Console.WriteLine("Knjiga uspesno izmenjena.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Knjiga sa datim id-em ne postoji.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
            
        }

        public bool IzmijeniKorisnika(int idKorisnika, Korisnik korisnik)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.korisnici.ContainsKey(idKorisnika))
                {
                    Database.korisnici[idKorisnika] = korisnik;
                    Console.WriteLine("Korisnik uspesno izmenjen");
                    return true;
                }
                else
                {
                    Console.WriteLine("Korisnik sa tim id-em ne postoji");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
        }

        public bool IznajmiKnjigu(int idKorisnika, string nazivKnjige)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
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
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
            
        }

        public bool ObrisiAutora(int idAutora)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.autori.ContainsKey(idAutora))
                {
                    Database.autori.Remove(idAutora);
                    Console.WriteLine("Autor uspesno obrisan");

                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji autor sa datim id-em");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
        }

        public bool ObrisiKnjigu(int idKnjige)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.knjige.ContainsKey(idKnjige))
                {
                    Database.knjige.Remove(idKnjige);
                    Console.WriteLine("Knjiga uspesno obrisana");

                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji knjiga sa datim id-em");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
        }

        public bool ObrisiKorisnika(int idKorisnika)
        {
            bool hasPermission = false;
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");
            var users = doc.Root.Elements("user");
            bool provera = false;
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

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
                            provera = true;
                            break;
                        }
                    }

                }
                if (provera)
                    break;
            }
            if (hasPermission == true)
            {
                if (Database.korisnici.ContainsKey(idKorisnika))
                {
                    Database.korisnici.Remove(idKorisnika);
                    Console.WriteLine("Korisnik uspesno obrisan");

                    return true;
                }
                else
                {
                    Console.WriteLine("Ne postoji korisnik sa datim id-em");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nema permisiju");
            }
            return false;
            
        }
    }
}
