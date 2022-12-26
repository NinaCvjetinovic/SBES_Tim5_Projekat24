using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/Biblioteka";

            using(ClientProxy proxy = new ClientProxy(binding,address))
            {
                Console.WriteLine("Klijent je uspesno pokrenut.");
                proxy.DodajKnjigu(3, new Knjiga(ZanrKnjige.Drama, "Lovac na zmajeve", new Autor("Haled", "Hoseini", "1965")));
                proxy.DodajKnjigu(4, new Knjiga(ZanrKnjige.Triler, "Jedini izlaz", new Autor("Marko", "Popovic", "1978")));
                proxy.IzmijeniKnjigu(4, new Knjiga(ZanrKnjige.Misterija, "Igra", new Autor("Skot", "Kerso", "1982")));
                proxy.ObrisiKnjigu(3);
                proxy.ObrisiKnjigu(6);



            }

            Console.ReadLine();
        }
    }
}
