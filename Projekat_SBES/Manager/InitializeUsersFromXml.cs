using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Manager
{
    public class InitializeUsersFromXml
    {
        private void InitializeUsersFromXml1()
        {
            XDocument doc = XDocument.Load("C:/Users/Vedrana/Desktop/ProjekatSBES/SBES_Tim5_Projekat24/Projekat_SBES/Manager/acl.xml");

            var users = doc.Root.Elements("user");
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                
                var privileges = userElement.Elements("permission").Select(p => p.Value);

            }
        }

   
    }
}

