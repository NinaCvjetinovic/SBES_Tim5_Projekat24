using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class ClientCertValidator : X509CertificateValidator
    {

        public override void Validate(X509Certificate2 certificate)
        {
            //DateTime notAfter = certificate.NotAfter;
            if (certificate.Subject.Equals(certificate.Issuer))
            {
                throw new Exception("Sertifikat je self-signed.");
            }
            /*if (notAfter < DateTime.Now.AddMonths(15))
            {
                throw new Exception("Sertifikat vazi manje od 15 meseci.");
            }*//*
            {
                DateTime expirationDate = DateTime.Parse(certificate.GetExpirationDateString());
                if (expirationDate < DateTime.Now.AddMonths(15))
                {
                    throw new Exception($"Sertifikat istice [{expirationDate}]");
                }

            }*/
        }
    }
}
