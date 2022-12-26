using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public enum zanrKnjige {[EnumMember] Triler, [EnumMember] Horor, [EnumMember] Drama, [EnumMember] Misterija, [EnumMember] Komedija }

    [DataContract]
    public class Knjiga
    {
        private zanrKnjige zanr;
        public string nazivKnjige;
        public Autor autor;

        public Knjiga(zanrKnjige zanr, string nazivKnjige, Autor autor)
        {
            this.nazivKnjige = nazivKnjige;
            this.autor = autor;
            this.Zanr = zanr;
        }

        [DataMember]
        public string NazivKnjige { get => nazivKnjige; set => nazivKnjige = value; }

        [DataMember]
        public Autor Autor { get => autor; set => autor = value; }

        [DataMember]
        public zanrKnjige Zanr { get => zanr; set => zanr = value; }

    }
}
