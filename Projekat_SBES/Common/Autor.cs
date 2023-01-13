using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Autor
    {
        
        private string imeAutora;
        private string prezimeAutora;
        private string godinaRodjenja;


        public Autor(string imeAutora, string prezimeAutora, string godinaRodjenja)
        {
            
            this.ImeAutora = imeAutora;
            this.PrezimeAutora = prezimeAutora;
            this.GodinaRodjenja = godinaRodjenja;
        }

        [DataMember]
        public string ImeAutora { get => imeAutora; set => imeAutora = value; }

        [DataMember]
        public string PrezimeAutora { get => prezimeAutora; set => prezimeAutora = value; }

        [DataMember]
        public string GodinaRodjenja { get => godinaRodjenja; set => godinaRodjenja = value; }

        public override string ToString()
        {
            return String.Format("Ime autora: {0}, prezime autora: {1}, godina rodjenja: {2}.", ImeAutora, PrezimeAutora, GodinaRodjenja);
        }

    }
}
