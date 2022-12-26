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
            this.imeAutora = imeAutora;
            this.prezimeAutora = prezimeAutora;
            this.godinaRodjenja = godinaRodjenja;
        }

        [DataMember]
        public string ImeAutora { get => imeAutora; set => imeAutora = value; }

        [DataMember]
        public string PrezimeAutora { get => prezimeAutora; set => prezimeAutora = value; }

        [DataMember]
        public string GodinaRodjenja { get => godinaRodjenja; set => godinaRodjenja = value; }

    }
}
