using System;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;
using MongoDB.Bson;
using Arhiviranje_dokumenata.Message_Boxes;
using System.Security.Cryptography;
using Google.Apis.Calendar.v3.Data;

namespace Arhiviranje_dokumenata
{
    public partial class Podesavanja : Form
    {
        DropboxCommunication dropbox = new DropboxCommunication();

        Form1 parentInstance = null;

        IDictionary<String, ColorDefinition> googleKalendarBoje;

        public Podesavanja(Form1 instance)
        {
            InitializeComponent();
            BackColor = GlobalVariables.background_color;

            tbAccessToken.Text = (string)Properties.Settings.Default["dropboxAccessToken"];
            tbAdresaServera.Text = (string)Properties.Settings.Default["databaseServerAddress"];
            tbPort.Text = (string)Properties.Settings.Default["databasePort"];
            tbKorisnik.Text = (string)Properties.Settings.Default["imeKorisnika"];

            if (DatabaseCommunication.testDbConnection(parentInstance, null, null))
            {
                ucitajKategorijePredmeta();
                ucitajListuRadnika();
                ucitajPrioriteteEvidencija();
            }

            parentInstance = instance;
        }

        private void ucitajKategorijePredmeta() {
            btnIzmeniKategoriju.Enabled = false;
            btnObrisiKategoriju.Enabled = false;
            tbNazivKategorije.Text = string.Empty;
            lvKategorijePredmeta.Items.Clear();
            List<KategorijePredmeta> kategorije = DatabaseCommunication.getKategorijePredmeta();
            bool raznoPostoji = false;

            foreach (KategorijePredmeta item in kategorije) {
                lvKategorijePredmeta.Items.Add(item.naziv);
                if (item.naziv == "Razno") {
                    raznoPostoji = true;
                }
            }

            if (!raznoPostoji) {
                DatabaseCommunication.upisNovuKategorijuPredmetaUBazu(parentInstance, "Razno", false);
            }
        }

        private void ucitajListuRadnika()
        {
            btnIzmeniRadnika.Enabled = false;
            btnObrisiRadnika.Enabled = false;
            tbImeRadnika.Text = string.Empty;
            lvRadnici.Items.Clear();
            List<Radnik> radnici = DatabaseCommunication.getRadnici();

            foreach (Radnik item in radnici)
            {
                lvRadnici.Items.Add(item.ime);
            }
        }

        private void btnZatvoriPodesavanja_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            try
            {
                if (DatabaseCommunication.testDbConnection(parentInstance, tbAdresaServera.Text, tbPort.Text))
                {
                    Properties.Settings.Default["dropboxAccessToken"] = tbAccessToken.Text;
                    Properties.Settings.Default["databaseServerAddress"] = tbAdresaServera.Text;
                    Properties.Settings.Default["databasePort"] = tbPort.Text;
                    Properties.Settings.Default["imeKorisnika"] = tbKorisnik.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Podešavanja su uspešno snimljena!");

                    DatabaseCommunication.updateMongoConnection(null, null);

                    parentInstance.reloadData();

                    Close();
                }
                else
                {
                    MessageBox.Show("Veza sa bazom nije uspostavljena, molim proverite podešavanja!", "Greška");
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Došlo je do greške pri snimanju podešavanja: " + ex.Message);
            }
        }

        private void btnTestirajVezuDropbox_Click(object sender, EventArgs e)
        {
            var task = Task.Run(() => dropbox.checkConnectionToDropbox(tbAccessToken.Text));

            task.Wait();
        }

        private void btnTestirajVezuAdresaServera_Click(object sender, EventArgs e)
        {
            if (DatabaseCommunication.testDbConnection(parentInstance, tbAdresaServera.Text, tbPort.Text))
            {
                MessageBox.Show("Veza sa bazom je uspešno uspostavljena!", "Uspeh!");
            }
            else {
                MessageBox.Show("Veza sa bazom nije uspostavljena", "Greška!");
            }
        }

        private void Podesavanja_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DatabaseCommunication.testDbConnection(parentInstance, tbAdresaServera.Text, tbPort.Text))
            {
                //ako nemamo vezu sa bazom zatvori i glavni prozor
                parentInstance.Close();
            }
            else
            {
                //ako imamo proveri timer pa ga nastavi ako treba
                parentInstance.proveriTimer();
                parentInstance.podesavanjaOtvorena = false;
            }
        }

        private void btnObrisiKategoriju_Click(object sender, EventArgs e)
        {
            BrisanjeKategorijePredmeta potvrda = new BrisanjeKategorijePredmeta(this);
            potvrda.ShowDialog();
            potvrda.Dispose();
        }

        public void obrisiKategorijuPredmeta() {

            List<PredmetData> predmeti = DatabaseCommunication.getPredmetiByKategorija(tbNazivKategorije.Text);

            foreach (PredmetData predmet in predmeti)
            {
                predmet.kategorija = "Razno";
                DatabaseCommunication.updatePredmet(parentInstance, predmet, false);
            }

            DatabaseCommunication.deleteKategorijaPredmet(parentInstance, tbNazivKategorije.Text);
            ucitajKategorijePredmeta();
        }

        private void lvKategorijePredmeta_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbNazivKategorije.Text = string.Empty;
            btnIzmeniKategoriju.Enabled = false;
            btnObrisiKategoriju.Enabled = false;

            if (lvKategorijePredmeta.SelectedItems.Count == 1)
            {
                if (lvKategorijePredmeta.SelectedItems[0].Text == "Razno")
                {
                    lvKategorijePredmeta.SelectedItems.Clear();
                }
                else {
                    tbNazivKategorije.Text = lvKategorijePredmeta.SelectedItems[0].Text;
                    btnIzmeniKategoriju.Enabled = true;
                    btnObrisiKategoriju.Enabled = true;
                }
            }
        }

        private void btnDodajKategoriju_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbNazivNoveKategorije.Text))
            {
                DatabaseCommunication.upisNovuKategorijuPredmetaUBazu(parentInstance, tbNazivNoveKategorije.Text, true);
                tbNazivNoveKategorije.Text = string.Empty;
                ucitajKategorijePredmeta();
            }
            else {
                MessageBox.Show("Morate uneti naziv kategorije.");
            }
        }

        private void btnIzmeniKategoriju_Click(object sender, EventArgs e)
        {
            DatabaseCommunication.updateNazivKategorijePredmeta(parentInstance, lvKategorijePredmeta.SelectedItems[0].Text, tbNazivKategorije.Text);
            List<PredmetData> predmeti = DatabaseCommunication.getPredmetiByKategorija(lvKategorijePredmeta.SelectedItems[0].Text);

            foreach (PredmetData predmet in predmeti) {
                predmet.kategorija = tbNazivKategorije.Text;
                DatabaseCommunication.updatePredmet(parentInstance, predmet, false);
            }

            ucitajKategorijePredmeta();
        }

        private void btnSnimiKorisnika_Click(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Save();

        }

        private void btnDodajRadnika_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbImeNovogRadnika.Text))
            {
                DatabaseCommunication.upisNovogRadnikaUBazu(parentInstance, tbImeNovogRadnika.Text, true);
                tbImeNovogRadnika.Text = string.Empty;
                ucitajListuRadnika();
            }
            else
            {
                MessageBox.Show("Morate uneti ime radnika.");
            }
        }

        private void btnIzmeniRadnika_Click(object sender, EventArgs e)
        {
            DatabaseCommunication.updateImeRadnika(parentInstance, lvRadnici.SelectedItems[0].Text, tbImeRadnika.Text);
            List<PredmetData> predmeti = DatabaseCommunication.getPredmetiByRadnik(lvRadnici.SelectedItems[0].Text);

            foreach (PredmetData predmet in predmeti)
            {
                foreach(Radnik rd in predmet.radnik)
                {
                    if (rd.ime == lvRadnici.SelectedItems[0].Text) {
                        rd.ime = tbImeRadnika.Text;
                    }
                }

                DatabaseCommunication.updatePredmet(parentInstance, predmet, false);
            }

            ucitajListuRadnika();
        }

        private void lvListRadnika_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbImeRadnika.Text = string.Empty;
            btnIzmeniRadnika.Enabled = false;
            btnObrisiRadnika.Enabled = false;

            if (lvRadnici.SelectedItems.Count == 1)
            {
                if (lvRadnici.SelectedItems[0].Text == "Razno")
                {
                    lvRadnici.SelectedItems.Clear();
                }
                else
                {
                    tbImeRadnika.Text = lvRadnici.SelectedItems[0].Text;
                    btnIzmeniRadnika.Enabled = true;
                    btnObrisiRadnika.Enabled = true;
                }
            }
        }

        private void btnObrisiRadnika_Click(object sender, EventArgs e)
        {
            BrisanjeKategorijePredmeta potvrda = new BrisanjeKategorijePredmeta(this, "Brisanjem zaposlenog svi predmeti koji su mu bili dodeljeni \nbiće nedodeljeni.\n\nDa li želite da nastavite ? ");
            potvrda.ShowDialog();
            potvrda.Dispose();
        }

        public void obrisiRadnika()
        {
            List<PredmetData> predmeti = DatabaseCommunication.getPredmetiByRadnik(tbImeRadnika.Text);
            List<Radnik> zaBrisanje = new List<Radnik>();

            foreach (PredmetData predmet in predmeti)
            {
                foreach (Radnik rd in predmet.radnik) {
                    if (rd.ime == lvRadnici.SelectedItems[0].Text) {
                        zaBrisanje.Add(rd);
                    }
                }

                foreach (Radnik rd in zaBrisanje) {
                    predmet.radnik.Remove(rd);
                }
                DatabaseCommunication.databaseFixUpdatePredmet(predmet);
            }

            DatabaseCommunication.deleteRadnik(parentInstance, tbImeRadnika.Text);
            ucitajListuRadnika();
        }

        private void btnMenjajSifru_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbStaraSifra.Text))
            {
                parentInstance.showMessage("Neispravan unos stare šifre!");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbNovaSifra.Text))
            {
                parentInstance.showMessage("Neispravan unos nove šifre!");
                return;
            }
            
            if (tbNovaSifra.Text == tbPotvrdaNoveSifre.Text)
            {
                if (GlobalVariables.proveriSifru(tbStaraSifra.Text, parentInstance))
                {
                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                    var pbkdf2 = new Rfc2898DeriveBytes(tbNovaSifra.Text, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);

                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);

                    string savedPasswordHash = Convert.ToBase64String(hashBytes);

                    DatabaseCommunication.updateSifruZaZakljucanDeoPrograma(parentInstance, savedPasswordHash);

                    tbNovaSifra.Text = "";
                    tbStaraSifra.Text = "";
                    tbPotvrdaNoveSifre.Text = "";
                }
                else
                {
                    parentInstance.showMessage("Stara šifra je pogrešna!");
                }
            }
            else
            {
                parentInstance.showMessage("Nova šifra i potvrda nove šifre se ne poklapaju!");
            }
        }

        private Color getPriorityColor(string key)
        {
            ColorDefinition value;
            googleKalendarBoje.TryGetValue(key, out value);
            return ColorTranslator.FromHtml(value.Background);
        }

        #region overwrites

        private void combobox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = getPriorityColor(n);
                Brush b = new SolidBrush(c);
                g.FillRectangle(b, rect.X + 5, rect.Y + 3,
                    rect.Width - 30, rect.Height - 7);
                g.DrawString(n, f, Brushes.Black, rect.Width - 20, rect.Top);
            }
        }

        #endregion

        private void btnPodigniRocistaNaKalendar_Click(object sender, EventArgs e)
        {
            bool imaRocista = false;
            List<PredmetData> sviPredmeti = DatabaseCommunication.getSviPredmeti();
            foreach (PredmetData item in sviPredmeti)
            {
                var rocista = item.listaRocista;

                if (rocista?.Count > 0)
                {
                    imaRocista = true;
                    string predmetIStranka = GlobalVariables.spojBrojPredmeta(item.brojPredmetaBr, item.brojPredmetaGod) + " " + item.stranka;

                    GoogleCalendarCommunication.createRocista(parentInstance, item.listaRocista, true, predmetIStranka);
                }
            }

          /*  if (imaRocista)
            {
                GoogleCalendarCommunication.executeBatch();
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                //selektovan item da menja boju i da se snimi u bazu
                if (lvPrioriteti.SelectedItems.Count == 1)
                {
                    PrioritetiEvidencija prioritet = new PrioritetiEvidencija();
                    prioritet.prioritet = Convert.ToInt32(lvPrioriteti.SelectedItems[0].Text);
                    prioritet.boja = GlobalVariables.argbToHex(colorDialog1.Color);

                    DatabaseCommunication.updatePrioritetEvidencije(parentInstance, prioritet);
                    ucitajPrioriteteEvidencija();
                }
            }
        }

        private void btnDodajNoviPrioritet_Click(object sender, EventArgs e)
        {
            PrioritetiEvidencija pe = new PrioritetiEvidencija();
            pe.prioritet = lvPrioriteti.Items.Count + 1;
            pe.boja = GlobalVariables.argbToHex(GlobalVariables.evidencija_priority_default);
            DatabaseCommunication.upisiNovPrioritetEvidencije(parentInstance, pe);

            ucitajPrioriteteEvidencija();
        }

        private void ucitajPrioriteteEvidencija()
        {
            btnPromeniBojuPrioriteta.Enabled = false;
            btnObrisiPrioritet.Enabled = false;
            lvPrioriteti.Items.Clear();
            List<PrioritetiEvidencija> radnici = DatabaseCommunication.getPrioritetiEvidencija();

            foreach (PrioritetiEvidencija item in radnici)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.prioritet.ToString();
                lvi.BackColor = GlobalVariables.stringToColor(item.boja);
                lvPrioriteti.Items.Add(lvi);
            }
        }

        private void lvPrioriteti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPrioriteti.SelectedItems.Count == 0)
            {
                btnPromeniBojuPrioriteta.Enabled = false;
                btnObrisiPrioritet.Enabled = false;
            } else
            {
                btnPromeniBojuPrioriteta.Enabled = true;
                btnObrisiPrioritet.Enabled = true;
            }
        }

        private void btnObrisiPrioritet_Click(object sender, EventArgs e)
        {
            if (lvPrioriteti.SelectedItems.Count == 1)
            {
                DatabaseCommunication.deletePrioritetEvidencije(parentInstance, lvPrioriteti.SelectedItems[0].Text);
                ucitajPrioriteteEvidencija();
            }
        }
    }
}