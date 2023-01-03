using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSertifikati
{
    class Program
    {
        static void Main(string[] args)
        {

			try
			{
				/// srvCertCN.SubjectName should be set to the service's username. .NET WindowsIdentity class provides information about Windows user running the given process
				string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

				NetTcpBinding binding1 = new NetTcpBinding();
				binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

				string address1 = "net.tcp://localhost:49673/Biblioteka";
				ServiceHost host = new ServiceHost(typeof(Biblioteka));
				host.AddServiceEndpoint(typeof(IBiblioteka), binding1, address1);

				///Custom validation mode enables creation of a custom validator - CustomCertificateValidator
				host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
				host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

				///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
				host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

				///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
				host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

				//binding1.Security.Mode = SecurityMode.Transport;
				//binding1.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


				try
				{
					host.Open();
					Console.WriteLine("Servis za autentifikaciju sertifikatima je pokrenut");
					Console.WriteLine("Korisnik koji je pokrenuo servis:" + WindowsIdentity.GetCurrent().Name);
					Console.ReadLine();
				}
				catch (Exception e)
				{
					Console.WriteLine("[ERROR] {0}", e.Message);
					Console.WriteLine("[StackTrace] {0}", e.StackTrace);
					Console.ReadLine();
				}

			}catch(Exception ex)
            {
				Console.WriteLine("[ERROR] {0}", ex.Message);
			}
			
		}
    }
}
