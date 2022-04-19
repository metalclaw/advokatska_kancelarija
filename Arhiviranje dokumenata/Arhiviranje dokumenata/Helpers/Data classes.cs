using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Arhiviranje_dokumenata
{
    class ListaDanasnjihEvidencija
    {
        public string brojPredmeta { get; set; }
        public string stranka { get; set; }
        public string suprotnaStrana { get; set; }
        public string tekstEvidencije { get; set; }
        public int prioritet { get; set; }
        public string radnik { get; set; }
        public ObjectId idEvidencije { get; set; }
    }

    class PredmetData
    {
        public ObjectId Id { get; set; }
        public int brojPredmetaBr { get; set; }
        public int brojPredmetaGod { get; set; }
        public string stranka { get; set; }
        public string vrstaPredmeta { get; set; }
        public string sud { get; set; }
        public string poslovniBroj { get; set; }
        public string imeSudije { get; set; }
        public string brojSudnice { get; set; }
        public string suprotnaStrana { get; set; }
        public DateTime datumFormiranja { get; set; }
        public bool predmetJeAktivan { get; set; }
        public string beleske { get; set; }
        public List<ListaEvidencija> listaEvidencija { get; set; }
        public List<Rociste> listaRocista { get; set; }
        public DateTime datumUnosaUBazu { get; set; }
        public string brTelefona { get; set; }
        public string kategorija { get; set; }
        public string placanjaDugovanja { get; set; }
        public List<Finansije> finansije { get; set; }
        public List<Radnik> radnik { get; set; }
    }

    public class Finansije
    {
        public ObjectId Id { get; set; }
        public DateTime datumUnosa { get; set; }
        public string datumUnosaString { get; set; }
        public string text { get; set; }
        public bool placeno { get; set; }

        public Finansije Clone() {
            return new Finansije
            {
                Id = Id,
                datumUnosa = datumUnosa,
                text = text,
                placeno = placeno
            };
        }
    }

    class PredmetDataOlv
    {
        public string brojPredmeta { get; set; }
        public string stranka { get; set; }
        public string vrstaPredmeta { get; set; }
        public string poslovniBroj { get; set; }
        public string imeSudije { get; set; }
        public string suprotnaStrana { get; set; }
        public string datumFormiranja { get; set; }
        public string predmetJeAktivan { get; set; }
    }

    class DanasnjeEvidencijeOLV {
        public string brojPredmeta { get; set; }
        public string stranka { get; set; }
        public string tekstEvidencije { get; set; }
    }

    class ListaEvidencija
    {
        public ObjectId id { get; set; }
        public DateTime datum { get; set; }
        public string tekstEvidencije { get; set; }
        public int prioritet { get; set; }
        public ObjectId radnikId { get; set; }
        public bool imaEventNaGoogleKalendaru { get; set; }
    }

    public class ListaEvidencijaZaNarednih15Dana {
        public DateTime datum { get; set; }
        public int brojEvidencija { get; set; }
    }

    class ListaEvidencijaZaNarednih15DanaOLV
    {
        public string datum { get; set; }
        public int brojEvidencija { get; set; }
    }

    class PaginacijaPodaci {
        public int maxPages { get; set; }
        public List<PredmetData> items { get; set; }
    }

    class ListaRocistaZaVremenskiRaspon {
        public int brojPredmetaBr { get; set; }
        public int brojPredmetaGod { get; set; }
        public string stranka { get; set; }
        public string tekst { get; set; }
        public string sud { get; set; }
        public string sudskiBr { get; set; }
        public string brSudnice { get; set; }
    }

    public class DnevneBeleske {
        public ObjectId id { get; set; }
        public ObjectId Id { get; set; }
        public string textBeleske { get; set; }
        public bool zakljucano { get; set; }
        public string zakljucaoIme { get; set; }
    }

    class Rociste {
        public ObjectId id { get; set; }
        public ObjectId Id { get; set; }
        public DateTime datum { get; set; }
        public string text { get; set; }
    }

    class ListaRocista {
        public ObjectId id { get; set; }
        public ObjectId Id { get; set; }
        public string brojPredmeta { get; set; }
        public string stranka { get; set; }
        public string tekstRocista { get; set; }
        public string sud { get; set; }
        public string sudskiBroj { get; set; }
        public string brojSudnice { get; set; }
    }

    class PredmetDataOld
    {
        public ObjectId Id { get; set; }
        public string brojPredmeta { get; set; }
        public string stranka { get; set; }
        public string vrstaPredmeta { get; set; }
        public string sud { get; set; }
        public string poslovniBroj { get; set; }
        public string imeSudije { get; set; }
        public string brojSudnice { get; set; }
        public string suprotnaStrana { get; set; }
        public DateTime datumFormiranja { get; set; }
        public bool predmetJeAktivan { get; set; }
        public string beleske { get; set; }
        public List<ListaEvidencija> listaEvidencija { get; set; }
        public DateTime datumUnosaUBazu { get; set; }
        public string brTelefona { get; set; }
    }

    class KategorijePredmeta {
        public ObjectId id { get; set; }
        public string naziv { get; set; }
    }

    class ListaPredmetiPoKategorijama {
        public string brojPredmeta { get; set; }
        public string stranka { get; set; }
        public bool predmetJeAktivan { get; set; }
    }

    class ListaPredmetiPoRadnicima {
        public string brojPredmeta { get; set; }
        public string stranka { get; set; }
        public bool predmetJeAktivan { get; set; }
        public string textEvidencije { get; set; }
        public string datumEvidencije { get; set; }
    }

    class Radnik {
        public ObjectId id { get; set; }
        public ObjectId Id { get; set; }
        public string ime { get; set; }
    }

    class Sifre
    {
        public ObjectId id { get; set; }
        public ObjectId Id { get; set; }
        public string namena { get; set; }
        public string sifra { get; set; }
    }

    class ListaPlacanjaDugovanjaClass
    {
        public string brojPredmeta { get; set; }
        public string ime { get; set; }
        public string beleska { get; set; }
        public bool placeno { get; set; }
    }

    class PrioritetiEvidencija
    {
        public ObjectId id { get; set; }
        public int prioritet { get; set; }
        public string boja { get; set; }
    }

    #region db fix helpers
    class DBFixPredmetData
    {
        public ObjectId Id { get; set; }
        public int brojPredmetaBr { get; set; }
        public int brojPredmetaGod { get; set; }
        public string stranka { get; set; }
        public string vrstaPredmeta { get; set; }
        public string sud { get; set; }
        public string poslovniBroj { get; set; }
        public string imeSudije { get; set; }
        public string brojSudnice { get; set; }
        public string suprotnaStrana { get; set; }
        public DateTime datumFormiranja { get; set; }
        public bool predmetJeAktivan { get; set; }
        public string beleske { get; set; }
        public List<ListaEvidencija> listaEvidencija { get; set; }
        public List<Rociste> listaRocista { get; set; }
        public DateTime datumUnosaUBazu { get; set; }
        public string brTelefona { get; set; }
        public string kategorija { get; set; }
        public string radnik { get; set; }
    }
    #endregion
}
