using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IBiblioteka
    {
        [OperationContract]
        bool DodajKnjigu(int idKnjige, Knjiga knjiga);
        [OperationContract]
        bool ObrisiKnjigu(int idKnjige);
        [OperationContract]
        bool IzmijeniKnjigu(int idKnjige, Knjiga knjiga);
        [OperationContract]
        bool DodajAutora(int idAutora,Autor autor);
        [OperationContract]
        bool ObrisiAutora(int idAutora);
        [OperationContract]
        bool IzmijeniAutora(int idAutora, Autor autor);
        [OperationContract]
        bool DodajKorisnika(int idKorisnika, Korisnik korisnik);
        [OperationContract]
        bool ObrisiKorisnika(int idKorisnika);
        [OperationContract]
        bool IzmijeniKorisnika(int idKorisnika, Korisnik korisnik);
        [OperationContract]
        bool IznajmiKnjigu(int idKorisnika, string nazivKnjige);
    }
}
