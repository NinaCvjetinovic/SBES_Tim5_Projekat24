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
                
                if (retValue == true)
                {
                    Console.WriteLine("DODAVANJE AUTORA USPESNO.");
                }
                else
                {
                    Console.WriteLine("DODAVANJE AUTORA NEUSPESNO.");
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
               
                if (retValue == true)
                {
                    Console.WriteLine("DODAVANJE KNJIGE USPESNO.");
                }
                else
                {
                    Console.WriteLine("DODAVANJE KNJIGE NEUSPESNO.");
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
               
                if (retValue == true)
                {
                    Console.WriteLine("DODAVANJE KORISNIKA USPESNO.");
                }
                else
                {
                    Console.WriteLine("DODAVANJE KORISNIKA NESUPESNO.");
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
                
                if (retValue == true)
                {
                    Console.WriteLine("IZMENA AUTORA USPESNA.");
                }
                else
                {
                    Console.WriteLine("IZMENA AUTORA NEUSPESNA.");
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
                
                if (retValue == true)
                {
                    Console.WriteLine("IZMENA KNJIGE USPESNA.");
                }
                else
                {
                    Console.WriteLine("IZMENA KNJIGE NEUSPESNA.");
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
                
                if (retValue == true)
                {
                    Console.WriteLine("IZMENA KORISNIKA USPESNA.");
                }
                else
                {
                    Console.WriteLine("IZMENA KORISNIKA NEUSPESNA.");
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
                
                if (retValue == true)
                {
                    Console.WriteLine("IZNAJMLJIVANJE KNJIGE USPESNO.");
                }
                else
                {
                    Console.WriteLine("IZNAJMLJIVANJE KNJIGE NEUSPESNO.");
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
               
                if (retValue == true)
                {
                    Console.WriteLine("BRISANJE AUTORA USPESNO.");
                }
                else
                {
                    Console.WriteLine("BRISANJE AUTORA NEUSPESNO.");
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
                
                if (retValue == true)
                {
                    Console.WriteLine("BRISANJE KNJIGE USPESNO.");
                }
                else
                {
                    Console.WriteLine("BRISANJE KNJIGE NEUSPESNO.");
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
                
                if (retValue == true)
                {
                    Console.WriteLine("BRISANJE KORISNIKA USPESNO.");
                }
                else
                {
                    Console.WriteLine("BRISANJE KORISNIKA NEUSPESNO.");
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
