using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Windows.Forms;
using System.Reflection;
using MongoDB.Bson.Serialization;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Collections;

namespace Arhiviranje_dokumenata.Helpers
{
    class DatabaseCommunication
    {
        #region veza sa bazom
        private static MongoClient client = new MongoClient(GlobalVariables.mongodb_connection_prefix + (string)Properties.Settings.Default["databaseServerAddress"] + ":" + (string)Properties.Settings.Default["databasePort"]);
        private static IMongoDatabase database = client.GetDatabase("Arhiviranje_Dokumenata");
        private static IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("predmet");
        private static IMongoCollection<BsonDocument> beleskeCollection = database.GetCollection<BsonDocument>("beleske");
        private static IMongoCollection<BsonDocument> kategorijePredmetaCollection = database.GetCollection<BsonDocument>("kategorije_predmeta");
        private static IMongoCollection<BsonDocument> radniciCollection = database.GetCollection<BsonDocument>("radnici");
        private static IMongoCollection<BsonDocument> sifreCollection = database.GetCollection<BsonDocument>("sifre");
        private static IMongoCollection<BsonDocument> prioritetiEvidencijaCollection = database.GetCollection<BsonDocument>("prioritet_evidencija");
        

        private static int dbPingWaitLength = 4000;//ms

        public static void updateMongoConnection(string ipServera, string port) {
            if (string.IsNullOrEmpty(ipServera))
            {
                ipServera = (string)Properties.Settings.Default["databaseServerAddress"];

            }

            if (string.IsNullOrEmpty(port)) {
                port = (string)Properties.Settings.Default["databasePort"];
            }

            client = new MongoClient(GlobalVariables.mongodb_connection_prefix + ipServera + ":" + port);
            database = client.GetDatabase("Arhiviranje_Dokumenata");
            collection = database.GetCollection<BsonDocument>("predmet");
        }

        public static bool testDbConnection(Form1 parent, string ipServera = null, string port = null)
        {
            try
            {
                updateMongoConnection(ipServera, port);
                var pingTask = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(dbPingWaitLength);

                return pingTask;
            }
            catch (Exception e)
            {
                parent.showMessage(e.Message);
                return false;
            }
        }
        #endregion

        #region pretraga baze
        public static List<PredmetData> getAktivniPredmetiSaEvidencijamaZaPrethodneDane()
        {
            updateCollection();

            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Lt("listaEvidencija.datum", DateTime.UtcNow.Date) & builder.Eq("predmetJeAktivan", true);

            List<BsonDocument> results = collection.Find(filter).ToList();
            List<PredmetData> listaPredmeta = new List<PredmetData>();

            foreach (BsonDocument doc in results)
            {
                listaPredmeta.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            return listaPredmeta;
        }

        public static List<PredmetData> getAktivniPredmetiSaRocistimaZaPrethodneDane()
        {
            updateCollection();

            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Lt("listaRocista.datum", DateTime.UtcNow.Date) & builder.Eq("predmetJeAktivan", true);

            List<BsonDocument> results = collection.Find(filter).ToList();
            List<PredmetData> listaPredmeta = new List<PredmetData>();

            foreach (BsonDocument doc in results)
            {
                listaPredmeta.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            return listaPredmeta;
        }

        public static PredmetData getPredmetByBrojPredmeta(int brojPredmetaBr, int brojPredmetaGod)
        {
            updateCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("brojPredmetaBr", brojPredmetaBr) & builder.Eq("brojPredmetaGod", brojPredmetaGod);
            List<BsonDocument> result = collection.Find(filter).ToList();

            if (result.Count > 1)
            {
                MessageBox.Show("Pronađeno je više od jednog predmeta sa datim brojem, ovo ne bi smelo da se desi, proverite bazu! Otvoren je prvi koji je pronađen.", "Upozorenje!");
            }

            PredmetData output = new PredmetData();

            if (result.Count > 0)
            {
                output = BsonSerializer.Deserialize<PredmetData>(result[0]);
            }
            
            return output;
        }

        public static List<PrioritetiEvidencija> getPrioritetiEvidencija() {
            updatePrioritetiEvidencijaCollection();
            List<PrioritetiEvidencija> result = new List<PrioritetiEvidencija>();
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

            List<BsonDocument> results = prioritetiEvidencijaCollection.Find(filter).Sort(Builders<BsonDocument>.Sort.Ascending("prioritet")).ToList();

            foreach (BsonDocument doc in results)
            {
                result.Add(BsonSerializer.Deserialize<PrioritetiEvidencija>(doc));
            }

            return result;
        }

        public static List<PredmetData> getPredmetiByBrojPredmeta(string brojPredmeta)
        {
            List<PredmetData> output = new List<PredmetData>();

            try
            {
                updateCollection();
                FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
                bool complex = false;
                int[] brojevi = new int[2];

                if (brojPredmeta.IndexOf("/") != -1) {
                    brojevi = GlobalVariables.razbijBrojPredmeta(brojPredmeta);
                    complex = true;
                }

                FilterDefinition<BsonDocument> filter;

                if (complex)
                {
                    filter = builder.Eq("brojPredmetaBr", brojevi[0]) & builder.Eq("brojPredmetaGod", brojevi[1]);
                }
                else {
                    filter = builder.Eq("brojPredmetaBr", Convert.ToInt32(brojPredmeta));
                }

                //FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Regex("brojPredmeta", new BsonRegularExpression(brojPredmetaBr,, "i"));

                List<BsonDocument> results = collection.Find(filter).ToList();

                foreach (BsonDocument doc in results)
                {
                    output.Add(BsonSerializer.Deserialize<PredmetData>(doc));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške pri traženju predmeta po broju predmeta: " + e.Message);
            }

            return output;
        }

        public static List<PredmetData> getPredmetiByImeStranke(string imeStranke)
        {
            List<PredmetData> output = new List<PredmetData>();
            try
            {
                updateCollection();
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Regex("stranka", new BsonRegularExpression(GlobalVariables.spremiZaSrpskuRegexPretragu(imeStranke), "i"));

                List<BsonDocument> results = collection.Find(filter).ToList();

                foreach (BsonDocument doc in results)
                {
                    output.Add(BsonSerializer.Deserialize<PredmetData>(doc));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške pri traženju predmeta po imenu stranke: " + e.Message);
            }

            return output;
        }

        public static List<PredmetData> getPredmetiByProtivnaStrana(string protivnaStrana)
        {
            List<PredmetData> output = new List<PredmetData>();
            try
            {
                updateCollection();
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Regex("suprotnaStrana", new BsonRegularExpression(GlobalVariables.spremiZaSrpskuRegexPretragu(protivnaStrana), "i"));

                List<BsonDocument> results = collection.Find(filter).ToList();

                foreach (BsonDocument doc in results)
                {
                    output.Add(BsonSerializer.Deserialize<PredmetData>(doc));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške pri traženju predmeta po protivnoj strani: " + e.Message);
            }

            return output;
        }

        /*
         * 
         * vraca broj sledeceg predmeta za datu godinu (ako je u 2017 god poslednji bio 800 u 2018 poslednji ce biti 1 etc.)
         * 
        public static int getPoslednjiBrojPredmetaZaDatuGodinu(string godina)
        {
            int brojPredmeta = 0;

            if (godina.Length > 2)
            {
                godina = godina.Substring(godina.Length - 2);
            }

            var predmeti = getPredmetiByBrojPredmeta("/" + godina);

            if (predmeti.Count > 0)
            {
                foreach (PredmetData predmet in predmeti)
                {
                    string[] brPredmetaRazbijen = predmet.brojPredmeta.Split('/');

                    int brPredmetaUBazi = Convert.ToInt32(brPredmetaRazbijen[0]);

                    if (brPredmetaUBazi > brojPredmeta)
                    {
                        brojPredmeta = brPredmetaUBazi;
                    }
                }
            }

            return ++brojPredmeta;
        }
        */

        public static int getBrojPredmetaUBazi() {
            updateCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

            List<BsonDocument> results = collection.Find(filter).Sort(Builders<BsonDocument>.Sort.Descending("brojPredmetaGod").Descending("brojPredmetaBr")).Limit(1).ToList();

            List<PredmetData> listaPredmeta = new List<PredmetData>();

            foreach (BsonDocument doc in results)
            {
                listaPredmeta.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            int broj = 0;

            try
            {
                broj = listaPredmeta[0].brojPredmetaBr;
            }
            catch (Exception e) {
                MessageBox.Show("Došlo je do greške pri traženju broja poslednjeg predmeta. \n\nDetalji greške: \n" + e.Message + "\n\nAutomatsko popunjavanje broja predmeta je nemoguće u ovom trenutku.");
            }

            return ++broj;
        }

        public static List<ListaDanasnjihEvidencija> getDanasnjeEvidencije(DateTime datum)
        {
            updateCollection();

            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Lte("listaEvidencija.datum", datum.AddDays(1)) & builder.Gte("listaEvidencija.datum", datum.Date);

            List<BsonDocument> results = collection.Find(filter).ToList();

            List<PredmetData> listaPredmeta = new List<PredmetData>();

            foreach (BsonDocument doc in results)
            {
                listaPredmeta.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            List<ListaDanasnjihEvidencija> output = new List<ListaDanasnjihEvidencija>();
            //ovaj deo zajebava
            foreach (PredmetData predmet in listaPredmeta)
            {
                foreach (ListaEvidencija evidencija in predmet.listaEvidencija)
                {
                    if (evidencija.datum.Date == datum.Date)
                    {
                        ListaDanasnjihEvidencija item = new ListaDanasnjihEvidencija();
                        item.brojPredmeta = GlobalVariables.spojBrojPredmeta(predmet.brojPredmetaBr, predmet.brojPredmetaGod);
                        item.stranka = predmet.stranka;
                        item.suprotnaStrana = predmet.suprotnaStrana;
                        item.tekstEvidencije = evidencija.tekstEvidencije;

                        int prioritet = evidencija.prioritet;

                        if (prioritet == 0)
                        {
                            prioritet = 4;
                        }

                        item.prioritet = prioritet;
                        item.idEvidencije = evidencija.id;
                        item.radnik = "";
                        if (predmet.radnik is IEnumerable)
                        {
                            if (predmet.radnik.Count > 0)
                            {
                                foreach (Radnik rd in predmet.radnik)
                                {
                                    item.radnik += rd.ime + ", ";
                                }
                                item.radnik = item.radnik.Remove(item.radnik.Length - 2);
                            }
                        }
                        output.Add(item);
                    }
                }
            };
            return output;
        }

        public static List<ListaEvidencijaZaNarednih15Dana> getBrojEvidencijaZaNaredneDane()
        {
            updateCollection();

            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Gt("listaEvidencija.datum", DateTime.UtcNow.Date) & builder.Lt("listaEvidencija.datum", DateTime.UtcNow.Date.AddDays(17));

            List<BsonDocument> results = collection.Find(filter).ToList();
            List<PredmetData> listaPredmeta = new List<PredmetData>();

            foreach (BsonDocument doc in results)
            {
                listaPredmeta.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            List<ListaEvidencijaZaNarednih15Dana> output = new List<ListaEvidencijaZaNarednih15Dana>();

            for (int i = 0; i <= 15; i++)
            {
                ListaEvidencijaZaNarednih15Dana temp = new ListaEvidencijaZaNarednih15Dana();
                temp.datum = DateTime.Now.AddDays(i + 1);
                temp.brojEvidencija = 0;
                output.Add(temp);
            }

            foreach (PredmetData predmet in listaPredmeta)
            {
                foreach (ListaEvidencija evidencija in predmet.listaEvidencija)
                {
                    ListaEvidencijaZaNarednih15Dana listItem = output.Find(x => x.datum.Date == evidencija.datum.Date);
                    if (listItem != null)
                    {
                        listItem.brojEvidencija++;
                    }
                }
            };

            return output;
        }

        public static List<PredmetData> getAllDataForExcel()
        {
            List<PredmetData> output = new List<PredmetData>();
            try
            {
                updateCollection();
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

                List<BsonDocument> results = collection.Find(filter).Sort(Builders<BsonDocument>.Sort.Ascending("datumUnosaUBazu")).ToList();

                foreach (BsonDocument doc in results)
                {
                    output.Add(BsonSerializer.Deserialize<PredmetData>(doc));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške pri traženju podataka za excel: " + e.Message);
            }

            return output;
        }

        public static PaginacijaPodaci getPage(int pageNumber) {
            updateCollection();

            pageNumber--;

            PaginacijaPodaci result = new PaginacijaPodaci();

            try
            {
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

                var query = collection.Find(filter);

                var totalTask = query.Count();

                //query = query.Sort(Builders<BsonDocument>.Sort.Descending("brojPredmeta"));
                query = query.Sort(Builders<BsonDocument>.Sort.Descending("brojPredmetaGod").Descending("brojPredmetaBr"));

                var itemsTask = query.Skip(pageNumber * GlobalVariables.page_size_for_svi_predmeti_list).Limit(GlobalVariables.page_size_for_svi_predmeti_list).ToList();
                List<PredmetData> listaPredmeta = new List<PredmetData>();

                foreach (BsonDocument doc in itemsTask)
                {
                    listaPredmeta.Add(BsonSerializer.Deserialize<PredmetData>(doc));
                }

                result.maxPages = (int)Math.Ceiling((double)totalTask / 10);
                result.items = listaPredmeta;
            }
            catch (Exception e) {
                MessageBox.Show("Došlo je do greške pri traženju stranice za listu svih predmeta: " + e.Message);
            }
            return result;
        }

        public static DnevneBeleske getDnevneBeleske() {
            updateBeleskeCollection();
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

            List<BsonDocument> result = beleskeCollection.Find(filter).ToList();

            if (result.Count > 1)
            {
                MessageBox.Show("Ima više od jednog unosa za dnevne beleške, ovo ne bi smelo da se desi!", "Upozorenje!");
            }

            if (result.Count == 0) {
                return new DnevneBeleske();
            }

            DnevneBeleske rezultat = BsonSerializer.Deserialize<DnevneBeleske>(result[0]);
            return rezultat;
        }

        public static List<PredmetData> getPredmetiByPoslovniBroj(string poslovniBroj)
        {
            List<PredmetData> output = new List<PredmetData>();

            try
            {
                updateCollection();
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Regex("poslovniBroj", new BsonRegularExpression(poslovniBroj, "i"));

                List<BsonDocument> results = collection.Find(filter).ToList();

                foreach (BsonDocument doc in results)
                {
                    output.Add(BsonSerializer.Deserialize<PredmetData>(doc));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške pri traženju predmeta po poslovnom broju: " + e.Message);
            }

            return output;
        }

        public static int getBrojArhiviranihIliAktivnihPredmetaUBazi(bool aktivni) {
            int br = 0;

            try
            {
                updateCollection();

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("predmetJeAktivan", aktivni);
                br = Convert.ToInt32(collection.Count(filter));
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške pri brojanju arhiviranih ili aktivnih predmeta: " + e.Message);
            }

            return br;
        }

        public static PredmetData getPredmetByIdEvidencije(ObjectId idEvidencije) {
            updateCollection();

            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("listaEvidencija._id", idEvidencije);

            int br = Convert.ToInt32(collection.Count(filter));

            if (br == 1)
            {
                List<BsonDocument> results = collection.Find(filter).ToList();

                return BsonSerializer.Deserialize<PredmetData>(results[0]);
            }
            else if (br <= 0)
            {
                //greska nije nista nasao
            }
            else
            {
                //greska ima ih vise
            }

            return new PredmetData();
        }

        public static List<KategorijePredmeta> getKategorijePredmeta()
        {
            updateKategorijePredmetaCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

            List<BsonDocument> results = kategorijePredmetaCollection.Find(filter).ToList();

            List<KategorijePredmeta> listaKategorija = new List<KategorijePredmeta>();

            foreach (BsonDocument doc in results)
            {
                listaKategorija.Add(BsonSerializer.Deserialize<KategorijePredmeta>(doc));
            }

            return listaKategorija;
        }

        public static List<Radnik> getRadnici() {
            updateRadniciCollection();

            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

            List<BsonDocument> results = radniciCollection.Find(filter).ToList();

            List<Radnik> listaRadnika = new List<Radnik>();

            foreach (BsonDocument doc in results)
            {
                listaRadnika.Add(BsonSerializer.Deserialize<Radnik>(doc));
            }

            return listaRadnika;
        }

        public static BsonObjectId getDaLiPostojiKategorijaPoNazivu(string naziv)
        {
            updateKategorijePredmetaCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("naziv", naziv);

            List<BsonDocument> results = kategorijePredmetaCollection.Find(filter).ToList();

            List<KategorijePredmeta> listaKategorija = new List<KategorijePredmeta>();

            foreach (BsonDocument doc in results)
            {
                listaKategorija.Add(BsonSerializer.Deserialize<KategorijePredmeta>(doc));
            }

            return listaKategorija.Count > 0 ? listaKategorija[0].id : BsonObjectId.Empty;
        }

        public static BsonObjectId getDaLiPostojiRadnikPoImenu(string ime)
        {
            updateRadniciCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("ime", ime);

            List<BsonDocument> results = radniciCollection.Find(filter).ToList();

            List<Radnik> listaRadnika = new List<Radnik>();

            foreach (BsonDocument doc in results)
            {
                listaRadnika.Add(BsonSerializer.Deserialize<Radnik>(doc));
            }

            return listaRadnika.Count > 0 ? (listaRadnika[0].id == BsonObjectId.Empty ? listaRadnika[0].Id : listaRadnika[0].id) : BsonObjectId.Empty;
        }

        public static List<PredmetData> getPredmetiByKategorija(string nazivKategorije)
        {
            updateCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("kategorija", nazivKategorije);
            List<BsonDocument> results = collection.Find(filter).ToList();

            List<PredmetData> predmeti = new List<PredmetData>();

            foreach (BsonDocument doc in results) {
                predmeti.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            return predmeti;
        }

        public static List<PredmetData> getPredmetiByRadnik(string imeRadnika)
        {
            updateCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("radnik.ime", imeRadnika);
            List<BsonDocument> results = collection.Find(filter).ToList();

            List<PredmetData> predmeti = new List<PredmetData>();

            foreach (BsonDocument doc in results)
            {
                predmeti.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            return predmeti;
        }

        public static List<PredmetData> getSviPredmeti() {
            updateCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

            List<BsonDocument> results = collection.Find(filter).ToList();

            List<PredmetData> listaPredmeta = new List<PredmetData>();

            foreach (BsonDocument doc in results)
            {
                listaPredmeta.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            return listaPredmeta;
        }

        public static List<ListaRocista> getRocistaZaVremensiRaspon(DateTime pocetniDatum, DateTime zavrsniDatum) {
            updateCollection();

            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Gt("listaRocista.datum", pocetniDatum.Date.ToUniversalTime()) & builder.Lt("listaRocista.datum", zavrsniDatum.Date.AddDays(1).ToUniversalTime());

            List<BsonDocument> results = collection.Find(filter).ToList();
            List<PredmetData> listaPredmeta = new List<PredmetData>();

            foreach (BsonDocument doc in results)
            {
                listaPredmeta.Add(BsonSerializer.Deserialize<PredmetData>(doc));
            }

            List<ListaRocista> listaRocista = new List<ListaRocista>();

            foreach (PredmetData predmet in listaPredmeta) {
                foreach (Rociste roc in predmet.listaRocista) {
                    if (roc.datum.Date >= pocetniDatum.Date && roc.datum.Date <= zavrsniDatum.Date)
                    {
                        ListaRocista rociste = new ListaRocista();
                        rociste.brojPredmeta = GlobalVariables.spojBrojPredmeta(predmet.brojPredmetaBr, predmet.brojPredmetaGod);
                        rociste.brojSudnice = predmet.brojSudnice;
                        rociste.id = roc.id;
                        rociste.stranka = predmet.stranka;
                        rociste.sud = predmet.sud;
                        rociste.sudskiBroj = predmet.poslovniBroj;
                        rociste.tekstRocista = roc.text;

                        listaRocista.Add(rociste);
                    }
                }
            }

            return listaRocista;
        }

        public static string getSifraZaZakljucanDeoPrograma(Form1 parent)
        {
            updateSifreCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("namena", "zakljucanDeoPrograma");
            List<BsonDocument> results = sifreCollection.Find(filter).ToList();

            Sifre sifra = null;

            if (results.Count() > 1)
            {
                parent.showMessage("Pronađeno je više od jedne šifre! Ovo ne bi smelo da se desi!");
            }

            if (results.Count == 0)
            {
                parent.showMessage("problem");
                return "123456";
            }

            sifra = BsonSerializer.Deserialize<Sifre>(results[0]);
            
            return sifra.sifra;
        }

        #endregion

        #region upis u bazu

        public static bool upisiNovPrioritetEvidencije(Form1 parent, PrioritetiEvidencija prioritet)
        {
            updatePrioritetiEvidencijaCollection();

            bool success = true;

            try
            {
               BsonDocument spakovano = new BsonDocument {
                    { "prioritet", prioritet.prioritet },
                    { "boja", prioritet.boja},
                };

                prioritetiEvidencijaCollection.InsertOne(spakovano);
            }
            catch (Exception e)
            {
                success = false;
                parent.showMessage("Došlo je do greške pri upisivanju novog prioriteta evidencija u bazu: " + e.Message);
            }

            return success;
        }

        public static bool upisiNoviPredmetUBazu(Form1 parent, PredmetData podaciPredmeta) {
            updateCollection();
            bool success = true;
            try
            {
                collection.InsertOne(packDataForSaving(podaciPredmeta));
                parent.showMessage("Predmet je uspešno upisan u bazu!");
            }
            catch (Exception e)
            {
                success = false;
                parent.showMessage("Došlo je do greške pri upisivanju novog predmeta u bazu: " + e.Message);
            }

            return success;
        }

        public static void upisiDnevneBeleskeUBazu(Form1 parent, string beleskeText, bool zakljucaj, string ime) {
            try
            {
                if (getDnevneBeleske().ToJson() == new DnevneBeleske().ToJson()) { 
                    BsonDocument doc = new BsonDocument {
                        { "textBeleske", beleskeText },
                        { "zakljucano", zakljucaj},
                        { "zakljucaoIme", ime}
                    };

                    beleskeCollection.InsertOne(doc);
                } else {
                    updateDnevneBeleske(parent, beleskeText, zakljucaj, ime);
                }
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri upisu novih beleški u bazu: " + e.Message);
            }
        }

        public static void upisNovuKategorijuPredmetaUBazu(Form1 parent, string nazivKategorije, bool prijaviUspeh)
        {
            if (getDaLiPostojiKategorijaPoNazivu(nazivKategorije) == BsonObjectId.Empty)
            {
                try
                {
                    BsonDocument doc = new BsonDocument {
                        { "naziv", nazivKategorije }
                    };
                    kategorijePredmetaCollection.InsertOne(doc);

                    if (prijaviUspeh)
                    {
                        parent.showMessage("Nova kategorija je uspešno upisana u bazu!");
                    }
                }
                catch (Exception e)
                {
                    parent.showMessage("Došlo je do greške pri upisu nove kategorije predmeta u bazu: " + e.Message);
                }
            }
            else {
                parent.showMessage("Kategorija sa unetim nazivom već postoji u bazi!");
            }
        }

        public static void upisNovogRadnikaUBazu(Form1 parent, string ime, bool prijaviUspeh) {
            if (getDaLiPostojiRadnikPoImenu(ime) == BsonObjectId.Empty)
            {
                try {
                    BsonDocument doc = new BsonDocument {
                        { "ime", ime}
                    };

                    radniciCollection.InsertOne(doc);

                    if (prijaviUspeh)
                    {
                        parent.showMessage(ime + " je uspešno upisan u bazu!");
                    }
                } catch (Exception e) {
                    parent.showMessage("Došlo je do greške pri upisu novog radnika u bazu: " + e.Message);
                }
            }
            else {
                parent.showMessage("Radnik već postoji u bazi!");
            }
        }

        public static void upisiSifruZaZakljucanDeoProgramaUBazu(Form1 parent, string sifra)
        {
            try
            {
                BsonDocument doc = new BsonDocument {
                    { "namena", "zakljucanDeoPrograma" },
                    { "sifra", sifra}
                };
                sifreCollection.InsertOne(doc);
                parent.showMessage("Šifra je uspešno uneta u bazu!");
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri upisu šifre u bazu: " + e.Message);
            }
        }
        #endregion

        #region brisanje iz baze
        public static void deletePrioritetEvidencije(Form1 parent, string prioritet)
        {
            updatePrioritetiEvidencijaCollection();
            try
            {
                prioritetiEvidencijaCollection.DeleteOne(new BsonDocument("prioritet", Convert.ToInt32(prioritet)));
                parent.showMessage("Prioritet evidencije je uspešno obrisan!");
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri brisanju prioriteta evidencije: " + e.Message);
            }
        }

        public static void deletePredmet(Form1 parent, ObjectId _id) {
            updateCollection();
            try
            {
                collection.DeleteOne(new BsonDocument("_id", _id));
                parent.showMessage("Predmet je uspešno obrisan!");
            }
            catch (Exception e) {
                parent.showMessage("Došlo je do greške pri brisanju predmeta: " + e.Message);
            }
        }

        public static void deleteKategorijaPredmet(Form1 parent, string nazivKategorije)
        {
            updateKategorijePredmetaCollection();
            try
            {
                BsonObjectId katId = getDaLiPostojiKategorijaPoNazivu(nazivKategorije);
                if (katId != BsonObjectId.Empty)
                {
                    kategorijePredmetaCollection.DeleteOne(new BsonDocument("_id", katId));
                    parent.showMessage("Kategorija predmeta je uspešno obrisana!");
                }
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri brisanju kategorije predmeta: " + e.Message);
            }
        }

        public static void deleteRadnik(Form1 parent, string imeRadnika)
        {
            updateRadniciCollection();
            try
            {
                BsonObjectId radId = getDaLiPostojiRadnikPoImenu(imeRadnika);
                if (radId != BsonObjectId.Empty)
                {
                    radniciCollection.DeleteOne(new BsonDocument("_id", radId));
                    parent.showMessage("Zaposleni je uspešno obrisan!");
                }
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri brisanju zaposlenog: " + e.Message);
            }
        }

        #endregion

        #region update baze
        private static void updateBeleskeCollection() {
            beleskeCollection = database.GetCollection<BsonDocument>("beleske");
        }

        private static void updateKategorijePredmetaCollection() {
            kategorijePredmetaCollection = database.GetCollection<BsonDocument>("kategorije_predmeta");
        }

        private static void updateRadniciCollection() {
            radniciCollection = database.GetCollection<BsonDocument>("radnici");
        }

        private static void updateSifreCollection() {
            sifreCollection = database.GetCollection<BsonDocument>("sifre");
        }

        private static void updateCollection() {
            collection = database.GetCollection<BsonDocument>("predmet");
        }

        private static void updatePrioritetiEvidencijaCollection()
        {
            prioritetiEvidencijaCollection = database.GetCollection<BsonDocument>("prioriteti_evidencija");
        }

        public static void updatePrioritetEvidencije(Form1 parent, PrioritetiEvidencija prioritetEvidencija)
        {
            updatePrioritetiEvidencijaCollection();
            try
            {
                BsonDocument newDoc = new BsonDocument {
                        { "prioritet", prioritetEvidencija.prioritet },
                        { "boja", prioritetEvidencija.boja},
                };
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("prioritet", prioritetEvidencija.prioritet);
                var res = prioritetiEvidencijaCollection.ReplaceOne(filter, newDoc);
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri izmeni prioriteta evidencija: " + e.Message);
            }
        }

        public static void updateDnevneBeleske(Form1 parent, string textBeleske, bool zakljucaj, string ime) {
            updateBeleskeCollection();
            try {
                BsonDocument newDoc = new BsonDocument {
                        { "textBeleske", textBeleske },
                        { "zakljucano", zakljucaj},
                        { "zakljucaoIme", ime}
                };

                DnevneBeleske oldBeleska = getDnevneBeleske();

                if (oldBeleska != new DnevneBeleske())
                {
                    BsonObjectId id;

                    if (oldBeleska.id != BsonObjectId.Empty) {
                        id = oldBeleska.id;
                    }
                    else if (oldBeleska.Id != BsonObjectId.Empty) {
                        id = oldBeleska.Id;
                    }
                    else {
                        parent.showMessage("Problem sa identifikatorom: Nije pronađen identifikator, izmena beleški nemoguća.");
                    }
                    FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", oldBeleska.Id);
                    var res = beleskeCollection.ReplaceOne(filter, newDoc);

                   /* if (!zakljucaj && res.IsAcknowledged && res.ModifiedCount > 0) {
                        parent.showMessage("Dnevne beleške otključane!");
                    }*/
                }
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri osvežavanju beleške: " + e.Message);
            }
        }

        public static void updatePredmet(Form1 parent, PredmetData noviPredmet, bool prijaviRezultat)
        {
            updateCollection();
            bool packed = false;
            try
            {
                BsonDocument newDoc = packDataForSaving(noviPredmet);
                packed = true;
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", noviPredmet.Id);

                collection.ReplaceOne(filter, newDoc);

                if (prijaviRezultat)
                {
                    parent.showMessage("Izmene su uspešno sačuvane!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške pri osvežavanju predmeta: " + e.Message + "\n\npacked = " + packed.ToString() + "\n\n" + noviPredmet.ToJson());
            }
        }

        public static void updateNazivKategorijePredmeta(Form1 parent, string nazivKategorije, string noviNazivKategorije)
        {
            updateKategorijePredmetaCollection();
            try
            {
                BsonObjectId katId = getDaLiPostojiKategorijaPoNazivu(nazivKategorije);
                if (katId != BsonObjectId.Empty)
                {
                    FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", katId);
                    BsonDocument doc = new BsonDocument {
                        { "naziv", noviNazivKategorije }
                    };

                    kategorijePredmetaCollection.ReplaceOne(filter, doc);
                    parent.showMessage("Kategorija predmeta je uspešno izmenjena!");
                }
                else {
                    parent.showMessage("Kategorija nije pronađena u bazi");
                }
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri osvežavanju naziva kategorije predmeta: " + e.Message);
            }
        }

        public static void updateImeRadnika(Form1 parent, string imeRadnika, string novoImeRadnika)
        {
            updateRadniciCollection();
            try
            {
                BsonObjectId radId = getDaLiPostojiRadnikPoImenu(imeRadnika);
                if (radId != BsonObjectId.Empty)
                {
                    FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", radId);
                    BsonDocument doc = new BsonDocument {
                        { "ime", novoImeRadnika }
                    };

                    radniciCollection.ReplaceOne(filter, doc);
                    parent.showMessage("Ime radnika je uspešno izmenjeno!");
                }
                else
                {
                    parent.showMessage("Radnik nije pronađen u bazi");
                }
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške osvežavanju imena zaposlenog: " + e.Message);
            }
        }


        public static void updateSifruZaZakljucanDeoPrograma(Form1 parent, string sifra)
        {
            updateSifreCollection();
            try
            {
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("namena", "zakljucanDeoPrograma");
                BsonDocument doc = new BsonDocument {
                    { "namena", "zakljucanDeoPrograma" },
                    { "sifra", sifra}
                };

                var res = sifreCollection.ReplaceOne(filter, doc);

                if (res.ModifiedCount > 0)
                {
                    parent.showMessage("Šifra je uspešno izmenjena!");
                }
                else
                {
                    upisiSifruZaZakljucanDeoProgramaUBazu(parent, sifra);
                }
            }
            catch (Exception e)
            {
                parent.showMessage("Došlo je do greške pri osvežavanju šifre u bazi: " + e.Message);
            }
        }
        #endregion

        #region helpers

        public static BsonDocument packDataForSaving(PredmetData podaciPredmeta)
        {
            BsonArray listaEvidencija = new BsonArray();
            foreach (ListaEvidencija item in podaciPredmeta.listaEvidencija)
            {
                BsonDocument evidencija = new BsonDocument {
                    { "datum", item.datum.AddDays(1) },
                    { "tekstEvidencije", item.tekstEvidencije},
                    { "prioritet", item.prioritet},
                    { "imaEventNaGoogleKalendaru", item.imaEventNaGoogleKalendaru }
                };

                var evidencijaId = ObjectId.GenerateNewId();

                if (item.id != ObjectId.Empty)
                {
                    evidencijaId = item.id;
                }
                
                evidencija.Add(new BsonElement("_id", evidencijaId));

                listaEvidencija.Add(evidencija);
            }

            if (podaciPredmeta.finansije == null)
            {
                podaciPredmeta.finansije = new List<Finansije>();
            }

            BsonArray listaFinansija = new BsonArray();
            foreach (Finansije item in podaciPredmeta.finansije) {
                BsonDocument finans = new BsonDocument {
                    {"datumUnosa", item.datumUnosa.AddDays(1) },
                    { "text", item.text },
                    { "placeno", item.placeno }
                };

                var finansijeId = ObjectId.GenerateNewId();

                if(item.Id != ObjectId.Empty)
                {
                    finans.Add(new BsonElement("_id", finansijeId));
                }
                listaFinansija.Add(finans);
            }

            if (podaciPredmeta.placanjaDugovanja != null && podaciPredmeta.placanjaDugovanja != "") {
                BsonDocument finans = new BsonDocument {
                    { "_id", ObjectId.GenerateNewId() },
                    {"datumUnosa", DateTime.Now },
                    { "text", podaciPredmeta.placanjaDugovanja },
                    { "placeno", false }
                };

                listaFinansija.Add(finans);                
            }

            BsonArray listaRocista = new BsonArray();

            if (podaciPredmeta.listaRocista != null)
            {
                foreach (Rociste item in podaciPredmeta.listaRocista) { 
                    BsonDocument rociste = new BsonDocument {
                        { "datum", item.datum.AddDays(1) },
                        { "text", item.text}
                    };

                    var rocisteId = ObjectId.GenerateNewId();

                    var actualId = item.id == ObjectId.Empty ? item.Id : item.id;

                    if (actualId != ObjectId.Empty)
                    {
                        rocisteId = actualId;
                    }

                    rociste.Add(new BsonElement("_id", rocisteId));
                    listaRocista.Add(rociste);
                }
            }

            BsonArray listaRadnika = new BsonArray();

            if (podaciPredmeta.radnik != null)
            {
                foreach (Radnik item in podaciPredmeta.radnik)
                {
                    BsonDocument radnik = new BsonDocument {
                        { "ime", item.ime}
                    };

                    listaRadnika.Add(radnik);
                }
            }

            BsonDocument doc = new BsonDocument {
                { "brojPredmetaBr", podaciPredmeta.brojPredmetaBr },
                { "brojPredmetaGod", podaciPredmeta.brojPredmetaGod },
                { "stranka", podaciPredmeta.stranka },
                { "datumFormiranja", podaciPredmeta.datumFormiranja },
                { "predmetJeAktivan", podaciPredmeta.predmetJeAktivan },
                { "listaEvidencija", listaEvidencija },
                { "listaRocista", listaRocista },
                { "radnik", listaRadnika },
                { "datumUnosaUBazu", podaciPredmeta.datumUnosaUBazu},
                { "kategorija", podaciPredmeta.kategorija},
                { "finansije", listaFinansija}
            };

            if (podaciPredmeta.Id != ObjectId.Empty)
            {
                doc.Add(new BsonElement("_id", podaciPredmeta.Id));
            }

            foreach (PropertyInfo prop in typeof(PredmetData).GetProperties())
            {
                if (prop.GetValue(podaciPredmeta) is string && prop.Name != "brojPredmetaBr" && prop.Name != "brojPredmetaGod" && prop.Name != "stranka" && prop.Name != "kategorija" && prop.Name != "placanjaDugovanja")
                {
                    if (string.IsNullOrWhiteSpace((string)prop.GetValue(podaciPredmeta)))
                    {
                        doc.Add(new BsonElement(prop.Name, BsonNull.Value));
                    }
                    else
                    {
                        doc.Add(new BsonElement(prop.Name, (string)prop.GetValue(podaciPredmeta)));
                    }
                }
            }
            return doc;
        }


        #endregion

        #region database fix helpers

        public static List<PredmetData> databaseFixHelper()
        {
            List<PredmetData> output = new List<PredmetData>();
            try
            {
                updateCollection();
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

                List<BsonDocument> results = collection.Find(filter).Sort(Builders<BsonDocument>.Sort.Ascending("brojPredmeta")).ToList();

                foreach (BsonDocument doc in results)
                {
                    output.Add(BsonSerializer.Deserialize<PredmetData>(doc));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške db fix helper: " + e.Message);
            }

            return output;
        }

        public static void srediShit() {
            List<PredmetData> output = new List<PredmetData>();
            try
            {
                updateCollection();
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("datumUnosaUBazu", getPredmetByBrojPredmeta(690,16).datumUnosaUBazu);

               // MessageBox.Show(getPredmetByBrojPredmeta("533/16").datumUnosaUBazu.ToString());
                //MessageBox.Show(DateTime.MinValue.AddMinutes(1).ToString());

                List<BsonDocument> results = collection.Find(filter).ToList();

                
               foreach (BsonDocument doc in results)
                {
                    output.Add(BsonSerializer.Deserialize<PredmetData>(doc));
                }

               // MessageBox.Show(output.Count.ToString());
                 
                 foreach (PredmetData predmet in output) {                     
                    PredmetData prethodniPredmet = getPredmetByBrojPredmeta(predmet.brojPredmetaBr-1, predmet.brojPredmetaGod);

             //       MessageBox.Show(predmet.listaEvidencija.Count.ToString());

                    PredmetData noviPredmet = predmet;

            //        MessageBox.Show(noviPredmet.listaEvidencija.Count.ToString());

                    noviPredmet.datumUnosaUBazu = prethodniPredmet.datumUnosaUBazu.AddMinutes(1);

                    databaseFixUpdatePredmet(noviPredmet);
                 }
                 MessageBox.Show("done.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Došlo je do greške sredi shit: " + e.Message);
            }
        }

        public static void pretvoriBrojPredmetaUDvaPolja() {

            updateCollection();
            //get everything
            List<BsonDocument> results = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();
            int brojac = 0;
            foreach (BsonDocument doc in results)
            {
                var brojPredmeta = doc.GetValue("brojPredmeta", null);

                if (brojPredmeta != null) {
                    PredmetDataOld predmet = BsonSerializer.Deserialize<PredmetDataOld>(doc);
                    PredmetData predmetNew = new PredmetData();

                    var brPredmeta = GlobalVariables.razbijBrojPredmeta(predmet.brojPredmeta);

                    predmetNew.Id = predmet.Id;
                    predmetNew.beleske = predmet.beleske;
                    predmetNew.brojPredmetaBr = brPredmeta[0];
                    predmetNew.brojPredmetaGod = brPredmeta[1];
                    predmetNew.brojSudnice = predmet.brojSudnice;
                    predmetNew.datumFormiranja = predmet.datumFormiranja;
                    predmetNew.datumUnosaUBazu = predmet.datumUnosaUBazu;
                    predmetNew.imeSudije = predmet.imeSudije;
                    predmetNew.listaEvidencija = predmet.listaEvidencija;
                    predmetNew.poslovniBroj = predmet.poslovniBroj;
                    predmetNew.predmetJeAktivan = predmet.predmetJeAktivan;
                    predmetNew.stranka = predmet.stranka;
                    predmetNew.sud = predmet.sud;
                    predmetNew.suprotnaStrana = predmet.suprotnaStrana;
                    predmetNew.vrstaPredmeta = predmet.vrstaPredmeta;
                    predmetNew.brTelefona = predmet.brTelefona;

                    databaseFixUpdatePredmet(predmetNew);
                    brojac++;
                }
            }
            MessageBox.Show("Prepravljeno je " + brojac.ToString() + " predmeta.");
        }
        
        public static void databaseFixUpdatePredmet(PredmetData noviPredmet)
        {
            updateCollection();

            //  try
            {
                BsonDocument newDoc = databaseFixPackDataForSaving(noviPredmet);

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", noviPredmet.Id);

                collection.ReplaceOne(filter, newDoc);

            }
    //        catch (Exception e)
            {
   //             MessageBox.Show("Došlo je do greške: " + e.Message);
            }

        }

        public static void dbFixSveEvidencijaZaDanUNazad() {
            List<PredmetData> svi = getSviPredmeti();

            foreach(PredmetData pd in svi)
            {
                dbFixUpdatePredmet(pd);
            }
        }

        public static void dbFixUpdatePredmet(PredmetData noviPredmet)
        {
            updateCollection();

            BsonDocument newDoc = databaseFixPackDataForSavingMinusDan(noviPredmet);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", noviPredmet.Id);

            collection.ReplaceOne(filter, newDoc);
        }

        public static BsonDocument databaseFixPackDataForSaving(PredmetData podaciPredmeta)
        {
            BsonArray listaEvidencija = new BsonArray();
            foreach (ListaEvidencija item in podaciPredmeta.listaEvidencija)
            {
                BsonDocument evidencija = new BsonDocument {
                    { "datum", item.datum },
                    { "tekstEvidencije", item.tekstEvidencije},
                    { "prioritet", item.prioritet},
                    { "imaEventNaGoogleKalendaru", item.imaEventNaGoogleKalendaru }
                };

                var evidencijaId = ObjectId.GenerateNewId();

                if (item.id != ObjectId.Empty)
                {
                    evidencijaId = item.id;
                }

                evidencija.Add(new BsonElement("_id", evidencijaId));

                listaEvidencija.Add(evidencija);
            }

            BsonArray listaRocista = new BsonArray();

            if (podaciPredmeta.listaRocista != null)
            {
                foreach (Rociste item in podaciPredmeta.listaRocista){
                    BsonDocument rociste = new BsonDocument {
                        { "datum", item.datum },
                        { "text", item.text}
                    };
                    var rocisteId = ObjectId.GenerateNewId();

                    var actualId = item.id == ObjectId.Empty ? item.Id : item.id;

                    if (actualId != ObjectId.Empty)
                    {
                        rocisteId = actualId;
                    }

                    rociste.Add(new BsonElement("_id", rocisteId));
                    listaRocista.Add(rociste);
                }
            }

            BsonArray listaRadnika = new BsonArray();
            if (podaciPredmeta.radnik != null) {
                foreach (Radnik item in podaciPredmeta.radnik) {
                    BsonDocument radnik = new BsonDocument {
                        { "ime", item.ime},
                        { "id", item.id }
                    };

                    listaRadnika.Add(radnik);
                }
            }

            if(podaciPredmeta.finansije == null)
            {
                podaciPredmeta.finansije = new List<Finansije>();
            }

            BsonArray listaFinansija = new BsonArray();
            foreach (Finansije item in podaciPredmeta.finansije)
            {
                BsonDocument finans = new BsonDocument {
                    {"datumUnosa", item.datumUnosa },
                    { "text", item.text },
                    { "placeno", item.placeno }
                };

                var finansijeId = ObjectId.GenerateNewId();

                if (item.Id != ObjectId.Empty)
                {
                    finans.Add(new BsonElement("_id", finansijeId));
                }
                listaFinansija.Add(finans);
            }

            if (podaciPredmeta.placanjaDugovanja != null && podaciPredmeta.placanjaDugovanja != "")
            {
                BsonDocument finans = new BsonDocument {
                    { "_id", ObjectId.GenerateNewId() },
                    {"datumUnosa", DateTime.Now },
                    { "text", podaciPredmeta.placanjaDugovanja },
                    { "placeno", false }
                };

                listaFinansija.Add(finans);
            }

            BsonDocument doc = new BsonDocument {
                { "brojPredmetaBr", podaciPredmeta.brojPredmetaBr },
                { "brojPredmetaGod", podaciPredmeta.brojPredmetaGod },
                { "stranka", podaciPredmeta.stranka },
                { "datumFormiranja", podaciPredmeta.datumFormiranja },
                { "predmetJeAktivan", podaciPredmeta.predmetJeAktivan },
                { "listaEvidencija", listaEvidencija },
                { "listaRocista", listaRocista },
                { "radnik", listaRadnika },
                { "datumUnosaUBazu", podaciPredmeta.datumUnosaUBazu},
                { "kategorija", podaciPredmeta.kategorija==null?"":podaciPredmeta.kategorija},
                { "finansije", listaFinansija}
            };

            if (podaciPredmeta.Id != ObjectId.Empty)
            {
                doc.Add(new BsonElement("_id", podaciPredmeta.Id));
            }

            foreach (PropertyInfo prop in typeof(PredmetData).GetProperties())
            {
                if (prop.GetValue(podaciPredmeta) is string && prop.Name != "brojPredmetaBr" && prop.Name != "brojPredmetaGod" && prop.Name != "stranka" && prop.Name != "kategorija" && prop.Name != "placanjaDugovanja")
                {
                    if (string.IsNullOrWhiteSpace((string)prop.GetValue(podaciPredmeta)))
                    {
                        doc.Add(new BsonElement(prop.Name, BsonNull.Value));
                    }
                    else
                    {
                        doc.Add(new BsonElement(prop.Name, (string)prop.GetValue(podaciPredmeta)));
                    }
                }
            }
            return doc;
        }

        public static BsonDocument databaseFixPackDataForSavingMinusDan(PredmetData podaciPredmeta)
        {
            BsonArray listaEvidencija = new BsonArray();
            foreach (ListaEvidencija item in podaciPredmeta.listaEvidencija)
            {
                BsonDocument evidencija = new BsonDocument {
                    { "datum", item.datum.AddDays(-1) },
                    { "tekstEvidencije", item.tekstEvidencije},
                    { "prioritet", item.prioritet},
                    { "imaEventNaGoogleKalendaru", item.imaEventNaGoogleKalendaru }
                };

                var evidencijaId = ObjectId.GenerateNewId();

                if (item.id != ObjectId.Empty)
                {
                    evidencijaId = item.id;
                }

                evidencija.Add(new BsonElement("_id", evidencijaId));

                listaEvidencija.Add(evidencija);
            }

            BsonArray listaRocista = new BsonArray();

            if (podaciPredmeta.listaRocista != null)
            {
                foreach (Rociste item in podaciPredmeta.listaRocista)
                {
                    BsonDocument rociste = new BsonDocument {
                        { "datum", item.datum.AddDays(-1) },
                        { "text", item.text}
                    };
                    var rocisteId = ObjectId.GenerateNewId();

                    var actualId = item.id == ObjectId.Empty ? item.Id : item.id;

                    if (actualId != ObjectId.Empty)
                    {
                        rocisteId = actualId;
                    }

                    rociste.Add(new BsonElement("_id", rocisteId));
                    listaRocista.Add(rociste);
                }
            }

            BsonArray listaRadnika = new BsonArray();
            if (podaciPredmeta.radnik != null)
            {
                foreach (Radnik item in podaciPredmeta.radnik)
                {
                    BsonDocument radnik = new BsonDocument {
                        { "ime", item.ime},
                        { "id", item.id }
                    };

                    listaRadnika.Add(radnik);
                }
            }

            if (podaciPredmeta.finansije == null)
            {
                podaciPredmeta.finansije = new List<Finansije>();
            }

            BsonArray listaFinansija = new BsonArray();
            foreach (Finansije item in podaciPredmeta.finansije)
            {
                BsonDocument finans = new BsonDocument {
                    {"datumUnosa", item.datumUnosa.AddDays(-1) },
                    { "text", item.text },
                    { "placeno", item.placeno }
                };

                var finansijeId = ObjectId.GenerateNewId();

                if (item.Id != ObjectId.Empty)
                {
                    finans.Add(new BsonElement("_id", finansijeId));
                }
                listaFinansija.Add(finans);
            }

            if (podaciPredmeta.placanjaDugovanja != null && podaciPredmeta.placanjaDugovanja != "")
            {
                BsonDocument finans = new BsonDocument {
                    { "_id", ObjectId.GenerateNewId() },
                    { "datumUnosa", DateTime.Now },
                    { "text", podaciPredmeta.placanjaDugovanja },
                    { "placeno", false }
                };

                listaFinansija.Add(finans);
            }

            BsonDocument doc = new BsonDocument {
                { "brojPredmetaBr", podaciPredmeta.brojPredmetaBr },
                { "brojPredmetaGod", podaciPredmeta.brojPredmetaGod },
                { "stranka", podaciPredmeta.stranka },
                { "datumFormiranja", podaciPredmeta.datumFormiranja },
                { "predmetJeAktivan", podaciPredmeta.predmetJeAktivan },
                { "listaEvidencija", listaEvidencija },
                { "listaRocista", listaRocista },
                { "radnik", listaRadnika },
                { "datumUnosaUBazu", podaciPredmeta.datumUnosaUBazu},
                { "kategorija", podaciPredmeta.kategorija==null?"":podaciPredmeta.kategorija},
                { "finansije", listaFinansija}
            };

            if (podaciPredmeta.Id != ObjectId.Empty)
            {
                doc.Add(new BsonElement("_id", podaciPredmeta.Id));
            }

            foreach (PropertyInfo prop in typeof(PredmetData).GetProperties())
            {
                if (prop.GetValue(podaciPredmeta) is string && prop.Name != "brojPredmetaBr" && prop.Name != "brojPredmetaGod" && prop.Name != "stranka" && prop.Name != "kategorija" && prop.Name != "placanjaDugovanja")
                {
                    if (string.IsNullOrWhiteSpace((string)prop.GetValue(podaciPredmeta)))
                    {
                        doc.Add(new BsonElement(prop.Name, BsonNull.Value));
                    }
                    else
                    {
                        doc.Add(new BsonElement(prop.Name, (string)prop.GetValue(podaciPredmeta)));
                    }
                }
            }
            return doc;
        }

        public static List<DBFixPredmetData> dbFixRadnikStringToArrayGetSviPredmeti()
        {
            updateCollection();
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

            List<BsonDocument> results = collection.Find(filter).ToList();

            List<DBFixPredmetData> listaPredmeta = new List<DBFixPredmetData>();

            foreach (BsonDocument doc in results)
            {
                listaPredmeta.Add(BsonSerializer.Deserialize<DBFixPredmetData>(doc));
            }

            return listaPredmeta;
        }

        public static void dbFixRadnikStringToArray() {
            List<DBFixPredmetData> predmeti = dbFixRadnikStringToArrayGetSviPredmeti();
            int brojac = 0;
            foreach (DBFixPredmetData predmet in predmeti)
            {
                List<Radnik> radnici = new List<Radnik>();

                if (predmet.radnik != null)
                {
                    Radnik rd = new Radnik();
                    rd.ime = predmet.radnik;
                    rd.id = ObjectId.GenerateNewId();
                    radnici.Add(rd);
                }

                PredmetData newPd = new PredmetData();
                newPd.beleske = predmet.beleske;
                newPd.brojPredmetaBr = predmet.brojPredmetaBr;
                newPd.brojPredmetaGod = predmet.brojPredmetaGod;
                newPd.brojSudnice = predmet.brojSudnice;
                newPd.brTelefona = predmet.brTelefona;
                newPd.datumFormiranja = predmet.datumFormiranja;
                newPd.datumUnosaUBazu = predmet.datumUnosaUBazu;
                newPd.Id = predmet.Id;
                newPd.imeSudije = predmet.imeSudije;
                newPd.kategorija = predmet.kategorija;
                newPd.listaEvidencija = predmet.listaEvidencija;
                newPd.listaRocista = predmet.listaRocista;
                newPd.poslovniBroj = predmet.poslovniBroj;
                newPd.predmetJeAktivan = predmet.predmetJeAktivan;
                newPd.radnik = radnici;
                newPd.stranka = predmet.stranka;
                newPd.sud = predmet.sud;
                newPd.suprotnaStrana = predmet.suprotnaStrana;
                newPd.vrstaPredmeta = predmet.vrstaPredmeta;

                databaseFixUpdatePredmet(newPd);

                brojac++;
            }

            MessageBox.Show("Prepravljeno je " + brojac.ToString() + " predmeta.");
        }

        #endregion

        #region database backup cleaning
        public static void cleanBackup(Form1 parent) {
            string backupFolder = AppDomain.CurrentDomain.BaseDirectory + @"mongo_backup\";
            string[] listOfBackups = Directory.GetDirectories(backupFolder);
            List<string> dates = new List<string>();
            List<string> listForRemoval = new List<string>();

            foreach (string folder in listOfBackups) {
                dates.Add(new DirectoryInfo(folder).Name);
            }

            try
            {
                if (dates.Count > GlobalVariables.number_of_database_backups_to_keep)
                {
                    var orderedList = dates.OrderByDescending(x => DateTime.ParseExact(x, GlobalVariables.database_backup_name_string_pattern, CultureInfo.InvariantCulture)).ToList();

                    //sa kraja teba da skida
                    while (orderedList.Count > GlobalVariables.number_of_database_backups_to_keep)
                    {
                        listForRemoval.Add(orderedList[orderedList.Count - 1]);
                        orderedList.RemoveAt(orderedList.Count - 1);
                    }

                    if (listForRemoval.Count > 0)
                    {
                        bool error = false;
                        foreach (string folder in listForRemoval)
                        {
                            if (!error)
                            {
                                var match = listOfBackups.FirstOrDefault(stringToCheck => stringToCheck.Contains(folder));
                                if (match != null) {
                                    deleteDirectory(match);
                                    if (Directory.Exists(match))
                                    {
                                        error = true;
                                    }
                                }
                                else {
                                    error = true;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (!error)
                        {
                            parent.showMessage("Stari bekap je uspešno uklonjen sa diska!");
                        }
                    }
                }
            }
            catch (Exception e) {
                parent.showMessage("Došlo je do greške pri čišćenju bekapa: " + e.Message);
            }
        }
        private static void deleteDirectory(string folderPath) {
            System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            Thread.Sleep(1);
            Directory.Delete(folderPath, true);
        }
        #endregion
    }
}
