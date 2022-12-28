using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public enum ZanrKnjige {[EnumMember] Triler, [EnumMember] Horor, [EnumMember] Drama, [EnumMember] Misterija, [EnumMember] Komedija }

    [DataContract]
    public class Knjiga
    {
       
        private ZanrKnjige zanr;
        private string nazivKnjige;
        private Autor autor;

        public Knjiga(ZanrKnjige zanr, string nazivKnjige, Autor autor)
        {
            
            this.Zanr = zanr;
            this.NazivKnjige = nazivKnjige;
            this.Autor = autor;
        }

        [DataMember]
        public string NazivKnjige { get => nazivKnjige; set => nazivKnjige = value; }

        [DataMember]
        public Autor Autor { get => autor; set => autor = value; }

        [DataMember]
        public ZanrKnjige Zanr { get => zanr; set => zanr = value; }

        public override string ToString()
        {
            return String.Format("Knjiga: {0} {1}, Autor: {2}", NazivKnjige, Zanr, Autor);
        }

    }
}
