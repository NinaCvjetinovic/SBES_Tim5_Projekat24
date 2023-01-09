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
        X509Chain chain;

        public ClientCertValidator()
        {
            X509Certificate2 trustedRoot = new X509Certificate2(@"C:\TrustedRoot\TestCA.cer");
            chain = new X509Chain();
            chain.ChainPolicy.ExtraStore.Add(trustedRoot);
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;

        }


        public override void Validate(X509Certificate2 certificate)
        {
       
            if (certificate.Subject.Equals(certificate.Issuer))
            {
                throw new Exception("Sertifikat je self-signed.");
            }
            else if (DateTime.Now > certificate.NotAfter || DateTime.Now.AddMonths(15) <= certificate.NotAfter)
            {

                throw new Exception("Serverski sertifikat nije trenutno validan ili njegova valjanost ističe u roku od 15 meseci.");
            }
            else if(!chain.Build(certificate))
            {
                throw new Exception("Serverski sertifikat nije izdat od strane tela kome verujemo.");
            }
            else
            {
                Console.WriteLine("Serverski sertifikat je validan.");
            }

        }
    }
}
