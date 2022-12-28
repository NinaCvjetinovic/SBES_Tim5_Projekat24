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

            if (!certificate.Issuer.Equals(srvCert.Issuer))
            {
                throw new Exception("Klijentski sertifikat nije izdat od strane istog tela kao serverski.");
            }/*
            DateTime expirationDate = DateTime.Parse(certificate.GetExpirationDateString());
            if (expirationDate < DateTime.Now.AddMonths(3))
            {
                throw new Exception($"Sertifikat istice [{expirationDate}]");
            }*/
        }
    }
}
