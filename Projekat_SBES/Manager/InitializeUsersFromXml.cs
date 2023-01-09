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
        private void InitializeUsersFromXml1(string filePath)
        {
            XDocument doc = XDocument.Load("acl.xml");

            var users = doc.Root.Elements("user");
            foreach (var userElement in users)
            {
                string username = userElement.Attribute("name").Value;
                //string organization = userElement.Attribute("organization").Value;
                var privileges = userElement.Elements("permission").Select(p => p.Value);

            }
        }

   
    }
}

