using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientProxy : ChannelFactory<IBiblioteka>, IBiblioteka, IDisposable

    {
        IBiblioteka factory;

        public ClientProxy(NetTcpBinding binding, string address) : base(binding,address)
        {
            factory = this.CreateChannel();
        }

        public bool DodajAutora(int idAutora, Autor autor)
        {
            bool retValue = false;
            try
            {
                retValue = factory.DodajAutora(idAutora, autor);
                Console.WriteLine("Dodavanje autora uspesno.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool DodajKnjigu(int idKnjige, Knjiga knjiga)
        {
            bool retValue = false;
            try
            {
                retValue = factory.DodajKnjigu(idKnjige, knjiga);
                Console.WriteLine("Dodavanje knjige uspesno.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool DodajKorisnika(int idKorisnika, Korisnik korisnik)
        {
           bool retValue = false;
           try
            {
                retValue = factory.DodajKorisnika(idKorisnika, korisnik);
                Console.WriteLine("Dodavanje korisnika uspesno.");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool IzmijeniAutora(int idAutora, Autor autor)
        {
            bool retValue = false;
            try
            {
                retValue = factory.IzmijeniAutora(idAutora, autor);
                Console.WriteLine("Izmena autora uspesna.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool IzmijeniKnjigu(int idKnjige, Knjiga knjiga)
        {
            bool retValue = false;
            try
            {
                retValue = factory.IzmijeniKnjigu(idKnjige, knjiga);
                Console.WriteLine("Izmena knjige uspesna.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool IzmijeniKorisnika(int idKorisnika, Korisnik korisnik)
        {
            bool retValue = false;
            try
            {
                retValue = factory.IzmijeniKorisnika(idKorisnika, korisnik);
                Console.WriteLine("Izmena korisnika uspesna.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool IznajmiKnjigu(int idKorisnika, string nazivKnjige)
        {
            bool retValue = false;
            try
            {
                retValue = factory.IznajmiKnjigu(idKorisnika, nazivKnjige);
                Console.WriteLine("Iznajmljivanje knjige uspesno.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool ObrisiAutora(int idAutora)
        {
            bool retValue = false;
            try
            {
                retValue = factory.ObrisiAutora(idAutora);
                Console.WriteLine("Brisanje autora uspesno.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool ObrisiKnjigu(int idKnjige)
        {
            bool retValue = false;
            try
            {
                retValue = factory.ObrisiKnjigu(idKnjige);
                Console.WriteLine("Brisanje knjige uspesno.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public bool ObrisiKorisnika(int idKorisnika)
        {
            bool retValue = false;
            try
            {
                retValue = factory.ObrisiKorisnika(idKorisnika);
                Console.WriteLine("Brisanje korisnika uspesno.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }
    }
}
