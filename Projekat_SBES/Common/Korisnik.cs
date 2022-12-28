using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Korisnik
    {
       
        private string imeKorisnika;
        private string prezimeKorisnika;
        private bool aktivan;
        private int brojKnjiga;
      

        [DataMember]
        public string ImeKorisnika { get => imeKorisnika; set => imeKorisnika = value; }

        [DataMember]
        public string PrezimeKorisnika { get => prezimeKorisnika; set => prezimeKorisnika = value; }

        [DataMember]
        public bool Aktivan { get => aktivan; set => aktivan = value; }

        [DataMember]
        public int BrojKnjiga { get => brojKnjiga; set => brojKnjiga = value; }

       

        public Korisnik(string imeKorisnika, string prezimeKorisnika, bool aktivan, int brojKnjiga)
        {
            
            this.ImeKorisnika = imeKorisnika;
            this.PrezimeKorisnika = prezimeKorisnika;
            this.Aktivan = aktivan;
            this.BrojKnjiga = brojKnjiga;
           
        }

        public override string ToString()
        {
            return String.Format("Ime korisnika : {0}, prezime korisnika : {1}, broj knjiga : {2}", ImeKorisnika, PrezimeKorisnika, BrojKnjiga);
        }
    }
}
