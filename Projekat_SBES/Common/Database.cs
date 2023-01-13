using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Database
    {
        public static Dictionary<int, Knjiga> knjige = new Dictionary<int, Knjiga>();
        public static Dictionary<int, Autor> autori = new Dictionary<int, Autor>();
        public static Dictionary<int, Korisnik> korisnici = new Dictionary<int, Korisnik>();

        public static Dictionary<int, Knjiga> VratiSveKnjige()
        {
            return knjige;
        }

        static Database()
        {
            Knjiga k1 = new Knjiga(ZanrKnjige.Drama, "Alhemicar", new Autor("Paulo", "Koeljo", "1947"));
            Knjiga k2 = new Knjiga(ZanrKnjige.Horor, "Isijavanje", new Autor("Stephen", "King", "1947"));

            knjige.Add(1, k1);
            knjige.Add(2, k2);

            Autor a1 = new Autor("Ivo", "Andric", "1892");
            Autor a2 = new Autor("Mesa", "Selimovic", "1910");

            autori.Add(1, a1);
            autori.Add(2, a2);


            Korisnik ko1 = new Korisnik("Marko", "Markovic", false, 0);
            Korisnik ko2 = new Korisnik("Pavle", "Pavlovic", true, 7);

            korisnici.Add(1, ko1);
            korisnici.Add(2, ko2);


        }

    }
}
