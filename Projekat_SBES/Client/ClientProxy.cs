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
    public class ClientProxy : ChannelFactory<IBiblioteka>, IBiblioteka, IDisposable

    {
        IBiblioteka factory;
        

        public ClientProxy(NetTcpBinding binding1, EndpointAddress address1) : base(binding1, address1)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);


            //binding1.Security.Mode = SecurityMode.Transport;
            // binding1.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            factory = this.CreateChannel();
        }


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
                //retValue = factory1.DodajAutora(idAutora, autor);
                if (retValue == true)
                {
                    Console.WriteLine("Dodavanje autora uspesno.");
                }
                else
                {
                    Console.WriteLine("Dodavanje autora neuspesno.");
                }
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
               // retValue = factory1.DodajKnjigu(idKnjige, knjiga);
                if (retValue == true)
                {
                    Console.WriteLine("Dodavanje knjige uspesno.");
                }
                else
                {
                    Console.WriteLine("Dodavanje knjige neuspesno.");
                }
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
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
               // retValue = factory1.DodajKorisnika(idKorisnika, korisnik);
                if (retValue == true)
                {
                    Console.WriteLine("Dodavanje korisnika uspesno.");
                }
                else
                {
                    Console.WriteLine("Dodavanje korisnika neuspesno.");
                }
                
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
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
                //retValue = factory1.IzmijeniAutora(idAutora, autor);
                if (retValue == true)
                {
                    Console.WriteLine("Izmena autora uspesna.");
                }
                else
                {
                    Console.WriteLine("Izmena autora neuspesna.");
                }
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
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
                //retValue = factory1.IzmijeniKnjigu(idKnjige, knjiga);
                if (retValue == true)
                {
                    Console.WriteLine("Izmena knjige uspesna.");
                }
                else
                {
                    Console.WriteLine("Izmena knjige neuspesna.");
                }
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
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
                //retValue = factory1.IzmijeniKorisnika(idKorisnika, korisnik);
                if (retValue == true)
                {
                    Console.WriteLine("Izmena korisnika uspesna.");
                }
                else
                {
                    Console.WriteLine("Izmena korisnika neuspesna.");
                }

                
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
                //retValue = factory1.IznajmiKnjigu(idKorisnika, nazivKnjige);
                if (retValue == true)
                {
                    Console.WriteLine("Iznajmljivanje knjige uspesno.");
                }
                else
                {
                    Console.WriteLine("Iznajmljivanje knjige neuspesno.");
                }
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
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
               // retValue = factory1.ObrisiAutora(idAutora);
                if (retValue == true)
                {
                    Console.WriteLine("Brisanje autora uspesno.");
                }
                else
                {
                    Console.WriteLine("Brisanje autora neuspesno.");
                }
                
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
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
                //retValue = factory1.ObrisiKnjigu(idKnjige);
                if (retValue == true)
                {
                    Console.WriteLine("Brisanje knjige uspesno.");
                }
                else
                {
                    Console.WriteLine("Brisanje knjige neuspesno.");
                }
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
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
                //retValue = factory1.ObrisiKorisnika(idKorisnika);
                if (retValue == true)
                {
                    Console.WriteLine("Brisanje korisnika uspesno.");
                }
                else
                {
                    Console.WriteLine("Brisanje korisnika neuspesno.");
                }
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return retValue;
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }
            this.Close();
        }
    }
}
