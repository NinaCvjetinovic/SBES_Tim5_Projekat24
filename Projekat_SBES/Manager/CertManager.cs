using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CertManager
    {
		/// <summary>
		/// Get a certificate with the specified subject name from the predefined certificate storage
		/// Only valid certificates should be considered
		/// </summary>
		/// <param name="storeName"></param>
		/// <param name="storeLocation"></param>
		/// <param name="subjectName"></param>
		/// <returns> The requested certificate. If no valid certificate is found, returns null. </returns>
		public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)  
		{
			X509Store store = new X509Store(storeName, storeLocation);
			store.Open(OpenFlags.ReadOnly);
		

			X509Certificate2Collection certificate2Collection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

			foreach (X509Certificate2 cert in certificate2Collection)
			{

				string organizacija = cert.SubjectName.Name.Split(',')[1].Split('=')[1];
				string ime = cert.SubjectName.Name.Split(',')[0].Split('=')[1];
				if (cert.SubjectName.Name.Equals(string.Format("CN={0},OU={1}", ime, organizacija))) ;
				
					return cert;
				
				
				
			}
			return null;
		}
	}
}
