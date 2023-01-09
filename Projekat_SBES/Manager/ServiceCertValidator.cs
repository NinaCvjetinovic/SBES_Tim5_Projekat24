using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class ServiceCertValidator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine,
                Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

      
            string clientOrganization = certificate.SubjectName.Name.Split(',')[0].Split('=')[1];
            string serverOrganization = srvCert.SubjectName.Name.Split(',')[0].Split('=')[1];
            

            if (!certificate.Issuer.Equals(srvCert.Issuer))
            {
                throw new Exception("Klijentski sertifikat nije izdat od strane istog tela kao serverski.");
            }
            
            
            else if (DateTime.Now > certificate.NotAfter || DateTime.Today.AddMonths(3) <= certificate.NotAfter)
            {

                throw new Exception("Klijentski sertifikat nije trenutno validan ili njegova valjanost ističe u roku od 3 meseca.");
            }

            else if(!clientOrganization.Equals(serverOrganization))
            {
                throw new Exception("Ime organizacije u SubjectName-u klijentskog sertifikata se razlikuje od imena organizacije kod serverskog sertifikata.");
            }
            else
            {
                Console.WriteLine("Klijentski sertifikat je validan.");
            }


        }
    }
}
