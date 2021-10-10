using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using Arhiviranje_dokumenata.Helpers;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using Arhiviranje_dokumenata.Message_Boxes;
using MongoDB.Bson;

namespace Arhiviranje_dokumenata
{
    public partial class Predmet : Form
    {
        PredmetData podaciPredmetaGlobal = new PredmetData();
        bool predmetChanged = false;
        bool canClose = false;
        Form1 mainForm = null;
        private PrintDocument printBeleske = new PrintDocument();
        private int checkPrint = 0;
        private List<Finansije> finansije;
        bool skipGoogleCalendarUpdates = false;

        public Predmet(int brojPredmetaBr, int brojPredmetaGod, Form1 mainFormInstance)
        {
            InitializeComponent();

            printBeleske.BeginPrint += printBeleske_BeginPrint;
            printBeleske.PrintPage += printBeleske_PrintPage;

            BackColor = GlobalVariables.background_color;

            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("3");
            comboBox1.Items.Add("4");

            comboBox1.SelectedItem = "4";

            btnIzmeniEvidenciju.Enabled = false;
            btnObrisiEvidenciju.Enabled = false;
            btnObrisiRociste.Enabled = false;
            btnIzmeniRociste.Enabled = false;

            mainForm = mainFormInstance;

            List<KategorijePredmeta> listaKategorija = DatabaseCommunication.getKategorijePredmeta();

            bool raznoPostoji = false;

            foreach (KategorijePredmeta item in listaKategorija)
            {
                cbKategorijaPredmetaObaveznoPolje.Items.Add(item.naziv);
                if (item.naziv == "Razno")
                {
                    raznoPostoji = true;
                }
            }

            List<Radnik> listaRadnika = DatabaseCommunication.getRadnici();

            foreach (Radnik item in listaRadnika) {
                ListViewItem radnik = new ListViewItem(item.ime);
                radnik.SubItems.Add(item.id.ToString());
                lvZaposleni.Items.Add(radnik);
            }

            if (!raznoPostoji)
            {
                DatabaseCommunication.upisNovuKategorijuPredmetaUBazu(mainForm, "Razno", false);
            }

            if (brojPredmetaBr == -1 || brojPredmetaGod == -1)
            {
                upisiNoviBrojPredmeta();
            }
            else {
                panelIzmenaPredmeta.Visible = true;
                panelNoviPredmet.Visible = false;
                if (DatabaseCommunication.testDbConnection(mainForm)) {
                    popuniPodatkePredmeta(DatabaseCommunication.getPredmetByBrojPredmeta(brojPredmetaBr, brojPredmetaGod));
                }
            }
        }

        private void upisiNoviBrojPredmeta()
        {
            panelIzmenaPredmeta.Visible = false;
            panelNoviPredmet.Visible = true;
            string godina = DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2);
            tbBrojPredmetaBrObaveznoPolje.Text = DatabaseCommunication.getBrojPredmetaUBazi().ToString();//DatabaseCommunication.getPoslednjiBrojPredmetaZaDatuGodinu(godina).ToString() + "/" + godina;
            tbBrojPredmetaGodObaveznoPolje.Text = godina;
        }

        private void printBeleske_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            checkPrint = rtbBeleske.Print(checkPrint, rtbBeleske.TextLength, e);

            // Check for more pages
            e.HasMorePages = checkPrint < rtbBeleske.TextLength;
        }

        private void printBeleske_BeginPrint(object sender, PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void btnDodajNovuEvidenciju_Click(object sender, EventArgs e)
        {
            changeChecker(sender, e);
            if (tbTekstEvidencije.Text != string.Empty)
            {
                ListViewItem novaEvidencija = new ListViewItem(dtpDatumEvidencije.Value.ToString(GlobalVariables.date_string_pattern));
                novaEvidencija.SubItems.Add(tbTekstEvidencije.Text);
                novaEvidencija.SubItems.Add(comboBox1.SelectedItem.ToString());
                novaEvidencija.BackColor = getPriorityColor(comboBox1.SelectedItem.ToString());
                lvEvidencija.Items.Add(novaEvidencija);
                tbTekstEvidencije.Text = string.Empty;
            }
        }

        private void btnDodajNovoRociste_Click(object sender, EventArgs e)
        {
            changeChecker(sender, e);
            if (tbTekstRocista.Text != string.Empty)
            {
                ListViewItem novoRociste = new ListViewItem(dtpDatumRocista.Value.ToString(GlobalVariables.date_string_pattern));
                novoRociste.SubItems.Add(tbTekstRocista.Text);
                lvRocista.Items.Add(novoRociste);
                tbTekstRocista.Text = string.Empty;
            }
        }

        private void btnObrisiRociste_Click(object sender, EventArgs e)
        {
            changeChecker(sender, e);
            lvRocista.SelectedItems[0].Remove();
        }

        private void tbTekstEvidencije_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDodajNovuEvidenciju.PerformClick();
            }
        }

        private void lvEvidencija_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEvidencija.SelectedItems.Count != 0)
            {
                btnIzmeniEvidenciju.Enabled = true;
                btnObrisiEvidenciju.Enabled = true;
                btnDodajNovuEvidenciju.Enabled = false;
                tbTekstEvidencije.Text = lvEvidencija.SelectedItems[0].SubItems[1].Text;
                lvEvidencija.SelectedItems[0].BackColor = getPriorityColor(lvEvidencija.SelectedItems[0].SubItems[2].Text);
                DateTime dt;
                if (DateTime.TryParseExact(lvEvidencija.SelectedItems[0].SubItems[0].Text, GlobalVariables.date_string_pattern, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out dt))
                {
                    dtpDatumEvidencije.Value = dt; // dt is the parsed value
                }
                else
                {
                    MessageBox.Show("Error parsing date"); // Invalid string
                }

                btnDodajUkloniEvidencijuSaKalendara.Enabled = true;

                if (lvEvidencija.SelectedItems[0].Checked)
                {
                    btnDodajUkloniEvidencijuSaKalendara.Text = "Ukloni sa kalendara";
                }
                else
                {
                    btnDodajUkloniEvidencijuSaKalendara.Text = "Dodaj na kalendar";
                }
            }
            else
            {
                btnIzmeniEvidenciju.Enabled = false;
                btnObrisiEvidenciju.Enabled = false;
                btnDodajNovuEvidenciju.Enabled = true;
                tbTekstEvidencije.Text = string.Empty;
                dtpDatumEvidencije.Value = DateTime.Now;
                btnDodajUkloniEvidencijuSaKalendara.Enabled = false;
                btnDodajUkloniEvidencijuSaKalendara.Text = "Dodaj na kalendar";
            }
        }

        private void btnObrisiEvidenciju_Click(object sender, EventArgs e)
        {
            changeChecker(sender, e);
            lvEvidencija.SelectedItems[0].Remove();
        }

        private void btnIzmeniEvidenciju_Click(object sender, EventArgs e)
        {
            changeChecker(sender, e);
            lvEvidencija.SelectedItems[0].SubItems[1].Text = tbTekstEvidencije.Text;
            lvEvidencija.SelectedItems[0].SubItems[2].Text = comboBox1.SelectedItem.ToString();
            lvEvidencija.SelectedItems[0].BackColor = getPriorityColor(comboBox1.SelectedItem.ToString());
            lvEvidencija.SelectedItems[0].SubItems[0].Text = dtpDatumEvidencije.Value.ToString("dd.MM.yyyy");

            lvEvidencija.SelectedItems[0].Selected = false;
        }

        private bool validateMandatoryInputFields()
        {
            bool error = false;
            var boxes = Controls.OfType<TextBox>();
            errorProvider1.Clear();

            if (cbKategorijaPredmetaObaveznoPolje.SelectedItem == null) {
                errorProvider1.SetError(cbKategorijaPredmetaObaveznoPolje, "Obavezno polje nije popunjeno!");
                error = true;
            }

            foreach (var box in boxes)
            {
                if (string.IsNullOrWhiteSpace(box.Text) && box.Name.Contains("ObaveznoPolje"))
                {
                    errorProvider1.SetError(box, "Obavezno polje nije popunjeno!");
                    error = true;
                }
            }

            return error;
        }

        private PredmetData collectData()
        {
            PredmetData podaciPredmeta = new PredmetData();
            int brPredmetaBr, BrPredmetaGod;

            if (int.TryParse(tbBrojPredmetaBrObaveznoPolje.Text, out brPredmetaBr))
            {
                podaciPredmeta.brojPredmetaBr = brPredmetaBr;
            }
            else
            {
                MessageBox.Show("Broj predmeta nije u odgovarajućem formatu!", "Greška");
                throw new FormatException();
            }

            if (int.TryParse(tbBrojPredmetaGodObaveznoPolje.Text, out BrPredmetaGod))
            {
                podaciPredmeta.brojPredmetaGod = BrPredmetaGod;
            }
            else {
                MessageBox.Show("Broj predmeta nije u odgovarajućem formatu!", "Greška");
                throw new FormatException();
            }

            podaciPredmeta.kategorija = cbKategorijaPredmetaObaveznoPolje.SelectedItem.ToString();
            podaciPredmeta.stranka = tbStrankaObaveznoPolje.Text;
            podaciPredmeta.vrstaPredmeta = tbVrstaPredmeta.Text;
            podaciPredmeta.sud = tbSud.Text;
            podaciPredmeta.poslovniBroj = tbPoslovniBroj.Text;
            podaciPredmeta.imeSudije = tbImeSudije.Text;
            podaciPredmeta.brojSudnice = tbBrojSudnice.Text;
            podaciPredmeta.suprotnaStrana = tbSuprotnaStrana.Text;
            podaciPredmeta.brTelefona = tbBrojTelefona.Text;
            podaciPredmeta.placanjaDugovanja = podaciPredmetaGlobal.placanjaDugovanja;
            podaciPredmeta.finansije = finansije;
            podaciPredmeta.radnik = new List<Radnik>();

            foreach (ListViewItem item in lvZaposleni.Items) {
                if (item.Checked) {
                    Radnik radnik = new Radnik();
                    radnik.ime = item.SubItems[0].Text;
                    radnik.id =  ObjectId.Parse(item.SubItems[1].Text);
                    podaciPredmeta.radnik.Add(radnik);
                }
            }

            if (panelNoviPredmet.Visible)
            {
                podaciPredmeta.predmetJeAktivan = rbNovAktivan.Checked;
            }
            else {
                podaciPredmeta.predmetJeAktivan = rbIzmenaAktivan.Checked;
            }
            podaciPredmeta.beleske = rtbBeleske.Rtf;
            podaciPredmeta.datumFormiranja = dtpDarumFormiranjaObaveznoPolje.Value;

            podaciPredmeta.listaEvidencija = new List<ListaEvidencija>();

            if (lvEvidencija.Items.Count > 0) {
                foreach (ListViewItem item in lvEvidencija.Items) {
                    DateTime dt;
                    if (DateTime.TryParseExact(item.SubItems[0].Text, GlobalVariables.date_string_pattern, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out dt))
                    {
                        ListaEvidencija evidencija = new ListaEvidencija();
                        evidencija.datum = dt;
                        evidencija.tekstEvidencije = item.SubItems[1].Text;
                        evidencija.prioritet = Convert.ToInt32(item.SubItems[2].Text);
                        evidencija.imaEventNaGoogleKalendaru = item.Checked;
                        podaciPredmeta.listaEvidencija.Add(evidencija);
                    }
                    else {
                        MessageBox.Show("Greška kod parsiranja datuma evidencije!");
                        break;
                    }
                }
            }

            podaciPredmeta.listaRocista = new List<Rociste>();

            if (lvRocista.Items.Count > 0)
            {
                foreach (ListViewItem item in lvRocista.Items)
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(item.SubItems[0].Text, GlobalVariables.date_string_pattern, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out dt))
                    {
                        Rociste novoRociste = new Rociste();
                        novoRociste.datum = dt;
                        novoRociste.text = item.SubItems[1].Text;
                        podaciPredmeta.listaRocista.Add(novoRociste);
                    }
                    else
                    {
                        MessageBox.Show("Greška kod parsiranja datuma ročišta!");
                        break;
                    }
                }
            }

            return podaciPredmeta;
        }

        private void btnSacuvajPredmet_Click(object sender, EventArgs e)
        {
            if (!validateMandatoryInputFields())
            {
                if (!proveriRocista())
                {
                    canClose = false;
                    return;
                }

                if (lvEvidencija.Items.Count > 0 || rbNovArhiviran.Checked ||lvRocista.Items.Count > 0)
                {
                    if (DatabaseCommunication.testDbConnection(mainForm, null, null))
                    {
                        try
                        {
                            PredmetData data = collectData();//collect data
                            PredmetData postojeci = DatabaseCommunication.getPredmetByBrojPredmeta(data.brojPredmetaBr, data.brojPredmetaGod);

                            if (postojeci.brojPredmetaBr != data.brojPredmetaBr && postojeci.brojPredmetaGod != data.brojPredmetaGod)
                            {
                                data.datumUnosaUBazu = DateTime.Now;//set current date and time as date of creation
                                bool writtenToDb = DatabaseCommunication.upisiNoviPredmetUBazu(mainForm, data); //save to database

                                //ovako radimo jer mongo sam dodeljuje id-ove rocistima pri upisu u bazu a id-ovi nam trebaju zbog google kalendara
                                PredmetData predmet = DatabaseCommunication.getPredmetByBrojPredmeta(data.brojPredmetaBr, data.brojPredmetaGod);

                                string predmetIStranka = GlobalVariables.spojBrojPredmeta(predmet.brojPredmetaBr, predmet.brojPredmetaGod) + " " + predmet.stranka;

                                if (data.listaRocista.Count > 0 && writtenToDb)
                                {
                                    GoogleCalendarCommunication.createRocista(mainForm, predmet.listaRocista, false, predmetIStranka);
                                }

                                if (data.listaEvidencija.Find(ev => ev.imaEventNaGoogleKalendaru) != null && writtenToDb)
                                {
                                    GoogleCalendarCommunication.createEvidencije(mainForm, predmet.listaEvidencija, false, predmetIStranka);
                                }

                                GoogleCalendarCommunication.executeBatch();

                                formCleanup();
                                mainForm.backupDatabaseAsExcelToDropbox();
                                upisiNoviBrojPredmeta();
                                mainForm.bekapujBazu(false);
                            }
                            else {
                                MessageBox.Show("Predmet sa tim brojem već postoji u bazi!", "Greška!");
                            }
                        }
                        catch (FormatException ex) {
                            
                        }
                    }
                    canClose = true;
                }
                else
                {
                    canClose = false;
                    MessageBox.Show("Nije uneta ni jedna evidencija!", "Greška!");
                }
            }
        }

        private void formCleanup() {
            foreach (Control ctrl in this.Controls) {
                if (ctrl is TextBox || ctrl is RichTextBox)
                {
                    ctrl.Text = string.Empty;
                }
                else if (ctrl is DateTimePicker)
                {
                    ((DateTimePicker)ctrl).Value = DateTime.Now;
                }
                else if (ctrl is GroupBox) {
                    foreach (Control subCtrl in ctrl.Controls)
                    {
                        if (subCtrl is ListView)
                        {
                            ((ListView)subCtrl).Items.Clear();
                        } else if (subCtrl is TextBox) {
                            subCtrl.Text = string.Empty;
                        }else if (subCtrl is DateTimePicker)
                        {
                            ((DateTimePicker)subCtrl).Value = DateTime.Now;
                        }
                    }
                }
            }

            predmetChanged = false;
        }

        private bool proveriRocista() {
            if (!rbIzmenaArhiviran.Checked && !rbNovArhiviran.Checked) {
                if (cbNemaRocista.Checked)
                {
                    if (lvRocista.Items.Count > 0)
                    {
                        MessageBox.Show("Kućica \"Nema ročišta\" je obeležena, a lista ročišta nije prazna!", "Greška!");
                        return false;
                    }
                }
                else
                {
                    if (lvRocista.Items.Count == 0)
                    {
                        MessageBox.Show("Kućica \"Nema ročišta\" nije obeležena, a lista ročišta je prazna!", "Greška!");
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSacuvajIzmene_Click(object sender, EventArgs e)
        {
            predmetChanged = false;

            try
            {
                PredmetData noviPredmet = collectData();

                noviPredmet.Id = podaciPredmetaGlobal.Id;
                noviPredmet.datumUnosaUBazu = podaciPredmetaGlobal.datumUnosaUBazu;

                if (!proveriRocista())
                {
                    canClose = false;
                    return;
                }

                bool imaNovaEvidencija = false;

                foreach (var item in noviPredmet.listaEvidencija)
                {
                    if (item.datum.Date >= DateTime.Now.Date)
                    {
                        imaNovaEvidencija = true;
                    }
                }

                if (rbIzmenaArhiviran.Checked)
                {
                    imaNovaEvidencija = true;

                    if (lvEvidencija.Items.Count > 0)
                    {
                        MessageBox.Show("Nemoguće arhivirati predmet dok ima evidencije!", "Greška!");
                        return;
                    }
                }

                if (imaNovaEvidencija || lvRocista.Items.Count > 0)
                {
                    if (DatabaseCommunication.testDbConnection(mainForm, null, null))
                    {
                        DatabaseCommunication.updatePredmet(mainForm, noviPredmet, true);
                        mainForm.backupDatabaseAsExcelToDropbox();
                        mainForm.bekapujBazu(false);

                        canClose = true;

                        if (!skipGoogleCalendarUpdates)
                        {
                            GoogleCalendarCommunication.deleteRocista(mainForm, podaciPredmetaGlobal.listaRocista, true);
                            GoogleCalendarCommunication.deleteEvidencije(mainForm, podaciPredmetaGlobal.listaEvidencija, true);
                        }

                        var predmet = DatabaseCommunication.getPredmetByBrojPredmeta(noviPredmet.brojPredmetaBr, noviPredmet.brojPredmetaGod);
                        if (!skipGoogleCalendarUpdates) {
                            string predmetIStranka = GlobalVariables.spojBrojPredmeta(predmet.brojPredmetaBr, predmet.brojPredmetaGod) + " " + predmet.stranka;
                            GoogleCalendarCommunication.createRocista(mainForm, predmet.listaRocista, true, predmetIStranka);
                            GoogleCalendarCommunication.createEvidencije(mainForm, predmet.listaEvidencija, true, predmetIStranka);
                         }
                        skipGoogleCalendarUpdates = false;
                        podaciPredmetaGlobal = predmet;
                    }
                }
                else
                {
                    canClose = false;
                    predmetChanged = true;
                    MessageBox.Show("Morate dodati novu evidenciju pri snimanju izmena predmeta!", "Greška!");
                }
                
            }
            catch (FormatException ex) {

            }
        }

        private void popuniPodatkePredmeta(PredmetData podaci) {
            podaciPredmetaGlobal = podaci;

            if(podaciPredmetaGlobal.finansije == null)
            {
                podaciPredmetaGlobal.finansije = new List<Finansije>();
            }

            if (string.IsNullOrEmpty(podaci.kategorija)) {
                cbKategorijaPredmetaObaveznoPolje.SelectedIndex = cbKategorijaPredmetaObaveznoPolje.FindStringExact("Razno");
                MessageBox.Show("Nije pronađena kategorija predmeta. Predmet će biti smešten u kategoriju \"Razno\".");
                podaci.kategorija = "Razno";
                DatabaseCommunication.updatePredmet(mainForm, podaci, false);
            }

            tbBrojPredmetaBrObaveznoPolje.Text = podaci.brojPredmetaBr.ToString();
            tbBrojPredmetaGodObaveznoPolje.Text = podaci.brojPredmetaGod.ToString();
            cbKategorijaPredmetaObaveznoPolje.SelectedIndex = cbKategorijaPredmetaObaveznoPolje.FindStringExact(podaci.kategorija) == -1 ? cbKategorijaPredmetaObaveznoPolje.FindStringExact("Razno") : cbKategorijaPredmetaObaveznoPolje.FindStringExact(podaci.kategorija);
            tbStrankaObaveznoPolje.Text = podaci.stranka;
            tbVrstaPredmeta.Text = podaci.vrstaPredmeta;
            tbSud.Text = podaci.sud;
            tbPoslovniBroj.Text = podaci.poslovniBroj;
            tbImeSudije.Text = podaci.imeSudije;
            tbBrojSudnice.Text = podaci.brojSudnice;
            tbBrojTelefona.Text = podaci.brTelefona;
            finansije = podaci.finansije;

            if (podaci.radnik != null)
            {
                foreach (Radnik rd in podaci.radnik)
                {
                    foreach (ListViewItem item in lvZaposleni.Items)
                    {
                        if (item.SubItems[0].Text == rd.ime)
                        {
                            item.Checked = true;
                        }
                    }
                }
            }
            if (podaci.predmetJeAktivan)
            {
                rbIzmenaAktivan.Checked = true;
            }
            else
            {
                rbIzmenaArhiviran.Checked = true;
            }

            tbSuprotnaStrana.Text = podaci.suprotnaStrana;
            dtpDarumFormiranjaObaveznoPolje.Value = podaci.datumFormiranja;

            if (GlobalVariables.IsValidRtf(podaci.beleske))
            {
                rtbBeleske.Rtf = podaci.beleske;
            }
            else
            {
                rtbBeleske.Text = podaci.beleske;
            }

            foreach (ListaEvidencija item in podaci.listaEvidencija) {
                ListViewItem novaEvidencija = new ListViewItem(item.datum.ToString(GlobalVariables.date_string_pattern));
                novaEvidencija.SubItems.Add(item.tekstEvidencije);
                novaEvidencija.SubItems.Add(item.prioritet.ToString());
                novaEvidencija.BackColor = getPriorityColor(item.prioritet.ToString());
                novaEvidencija.Checked = item.imaEventNaGoogleKalendaru;
                lvEvidencija.Items.Add(novaEvidencija);
            }

            if (podaci.listaRocista != null)
            {
                if (podaci.listaRocista.Count == 0)
                {
                    cbNemaRocista.Checked = true;
                }
                else
                {
                    foreach (Rociste item in podaci.listaRocista)
                    {
                        ListViewItem novoRociste = new ListViewItem(item.datum.ToString(GlobalVariables.date_string_pattern));
                        novoRociste.SubItems.Add(item.text);
                        lvRocista.Items.Add(novoRociste);
                    }
                }
            }
            predmetChanged = false;
        }

        private void btnObrisiPredmet_Click(object sender, EventArgs e)
        {
            if (podaciPredmetaGlobal.Id != ObjectId.Empty)
            {
                if (DatabaseCommunication.testDbConnection(mainForm, null, null))
                {
                    DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da izbrišete ovaj predmet?", "Upozorenje!", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        DatabaseCommunication.deletePredmet(mainForm, podaciPredmetaGlobal.Id);
                        GoogleCalendarCommunication.deleteRocista(mainForm, podaciPredmetaGlobal.listaRocista, false);
                        GoogleCalendarCommunication.deleteEvidencije(mainForm, podaciPredmetaGlobal.listaEvidencija, true);
                        mainForm.bekapujBazu(false);
                        this.Close();
                    }
                }
            }
        }

        private void Predmet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (predmetChanged) {
                UpozorenjeMessageBox msg = new UpozorenjeMessageBox(this);
                msg.Show();
                e.Cancel = true;
            }

            if (mainForm != null) {
                mainForm.reloadData();
            }
        }

        public void closeWithoutSaving() {
            predmetChanged = false;
            this.Close();
        }

        public void saveAndClose() {
            if (!validateMandatoryInputFields())
            {
                if (panelIzmenaPredmeta.Visible)
                {
                    btnSacuvajIzmene.PerformClick();
                }
                else
                {
                    btnSacuvajPredmet.PerformClick();
                }
                if (canClose)
                {
                    this.Close();
                }
            }
        }

        private void changeChecker(object sender, EventArgs e) {
                predmetChanged = true;
        }

        private Color getPriorityColor(string priority) {           
            switch (priority) {
                case "1": return GlobalVariables.evidencija_priority_1;
                case "2": return GlobalVariables.evidencija_priority_2;
                case "3": return GlobalVariables.evidencija_priority_3;
                case "4": return GlobalVariables.evidencija_priority_default;
                default: return GlobalVariables.evidencija_priority_default;
            }
        }

        private void btnDanasnjiDatum_Click(object sender, EventArgs e)
        {
            dtpDatumEvidencije.Value = DateTime.Now;
        }

        private void lvRocista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRocista.SelectedItems.Count != 0)
            {
                btnIzmeniRociste.Enabled = true;
                btnObrisiRociste.Enabled = true;
                btnDodajRociste.Enabled = false;
                tbTekstRocista.Text = lvRocista.SelectedItems[0].SubItems[1].Text;
                DateTime dt;
                if (DateTime.TryParseExact(lvRocista.SelectedItems[0].SubItems[0].Text, GlobalVariables.date_string_pattern, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out dt))
                {
                    dtpDatumRocista.Value = dt; // dt is the parsed value
                }
                else
                {
                    MessageBox.Show("Error parsing date"); // Invalid string
                }
            }
            else
            {
                btnIzmeniRociste.Enabled = false;
                btnObrisiRociste.Enabled = false;
                btnDodajRociste.Enabled = true;
                tbTekstRocista.Text = string.Empty;
                dtpDatumRocista.Value = DateTime.Now;
            }
        }

        private void btnIzmeniRociste_Click(object sender, EventArgs e)
        {
            changeChecker(sender, e);
            lvRocista.SelectedItems[0].SubItems[1].Text = tbTekstRocista.Text;
            lvRocista.SelectedItems[0].SubItems[0].Text = dtpDatumRocista.Value.ToString("dd.MM.yyyy");

            lvRocista.SelectedItems[0].Selected = false;
        }

        #region overwrites

        private void combobox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox) sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = getPriorityColor(n);
                Brush b = new SolidBrush(c);
                g.FillRectangle(b, rect.X + 5, rect.Y + 3,
                    rect.Width - 30, rect.Height - 7);
                g.DrawString(n, f, Brushes.Black, rect.Width - 20, rect.Top);
            }
        }

        public class NoDoubleClickAutoCheckListview : ListView
        {
            private bool checkFromDoubleClick = false;

            protected override void OnItemCheck(ItemCheckEventArgs ice)
            {
                if (this.checkFromDoubleClick)
                {
                    ice.NewValue = ice.CurrentValue;
                    this.checkFromDoubleClick = false;
                }
                else
                    base.OnItemCheck(ice);
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                // Is this a double-click?
                if ((e.Button == MouseButtons.Left) && (e.Clicks > 1))
                {
                    this.checkFromDoubleClick = true;
                }
                base.OnMouseDown(e);
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                this.checkFromDoubleClick = false;
                base.OnKeyDown(e);
            }
        }
        #endregion

        private void btnBold_Click(object sender, EventArgs e)
        {
            string err = "";
            try
            {
                err += "1,";
                if (!rtbBeleske.SelectionFont.Bold)
                {
                    err += "2,";
                    rtbBeleske.SelectionFont = new Font(rtbBeleske.Font, FontStyle.Bold);
                    err += "3,";
                    rtbBeleske.SelectionStart = rtbBeleske.SelectionStart + rtbBeleske.SelectionLength;
                    err += "4,";
                    rtbBeleske.SelectionLength = 0;
                    err += "5,";
                    // Set font immediately after selection
                    rtbBeleske.SelectionFont = rtbBeleske.Font;
                    err += "6,";
                }
                else
                {
                    err += "7,";
                    rtbBeleske.SelectionFont = rtbBeleske.Font;
                    err += "8,";
                    rtbBeleske.SelectionStart = rtbBeleske.SelectionStart + rtbBeleske.SelectionLength;
                    err += "9,";
                    rtbBeleske.SelectionLength = 0;
                    err += "10,";
                }
            }
            catch (Exception ex) {
                MessageBox.Show(err + " " + ex.Message);
            }
        }

        private void btnPrintBeleske_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printBeleske.Print();
            }
        }

        private void btnEvidencijeOcistiSelekciju_Click(object sender, EventArgs e)
        {
            btnIzmeniEvidenciju.Enabled = false;
            btnObrisiEvidenciju.Enabled = false;
            btnDodajNovuEvidenciju.Enabled = true;
            tbTekstEvidencije.Text = string.Empty;
            dtpDatumEvidencije.Value = DateTime.Now;
            lvEvidencija.SelectedItems.Clear();
        }

        private void btnBeleskeOcistiSelekciju_Click(object sender, EventArgs e)
        {
            btnIzmeniRociste.Enabled = false;
            btnObrisiRociste.Enabled = false;
            btnDodajRociste.Enabled = true;
            tbTekstRocista.Text = string.Empty;
            dtpDatumRocista.Value = DateTime.Now;
            lvRocista.SelectedItems.Clear();
        }

        private void lvRocista_DoubleClick(object sender, EventArgs e)
        {
            if (lvRocista.SelectedItems.Count == 1)
            {
                MessageBox.Show(lvRocista.SelectedItems[0].SubItems[0].Text + "\n\n" + lvRocista.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void lvEvidencija_DoubleClick(object sender, EventArgs e)
        {
            if (lvEvidencija.SelectedItems.Count == 1)
            {
                MessageBox.Show(lvEvidencija.SelectedItems[0].SubItems[0].Text + "\n\n" + lvEvidencija.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void btnPlacanjaDugovanja_Click(object sender, EventArgs e)
        {
            TraziSifru traziSifru = new TraziSifru(null, this);
            traziSifru.ShowDialog();
            traziSifru.Dispose();
        }

        public void otvoriPlacanjaDugovanjaZaPolaSekunde()
        {
            System.Timers.Timer timer = new System.Timers.Timer(200);
            timer.Elapsed += async (sender, e) => await otvoriPlacanjaDugovanja(this, timer);
            timer.Start();
        }

        private async Task otvoriPlacanjaDugovanja(Predmet guiThread, System.Timers.Timer timer) {
            timer.Stop();
            timer.Dispose();

            guiThread.openPlacanjaDugovanja();
        }

        public void updateFinansije(List<Finansije> updatedFinansije)
        {
            finansije = updatedFinansije;
            skipGoogleCalendarUpdates = true;
            btnSacuvajIzmene.PerformClick();
        }

        delegate void openPlacanjDugovanjaCallback();

        public void openPlacanjaDugovanja()
        {
            if (IsHandleCreated)
            {
                if (InvokeRequired)
                {

                    openPlacanjDugovanjaCallback d = new openPlacanjDugovanjaCallback(openPlacanjaDugovanja);
                    Invoke(d);
                }
                else
                {
                    ZakljucaneBeleske beleske = new ZakljucaneBeleske(mainForm, this, podaciPredmetaGlobal.finansije);
                    beleske.ShowDialog();
                    beleske.Dispose();
                }
            }
        }

        private void lvEvidencija_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvEvidencija.FocusedItem != null)
            {
                changeChecker(sender, e);
            }
        }

        private void btnDodajUkloniEvidencijuSaKalendara_Click(object sender, EventArgs e)
        {
            if(lvEvidencija.SelectedItems.Count > 0)
            {
                lvEvidencija.SelectedItems[0].Checked = !lvEvidencija.SelectedItems[0].Checked;

                if (lvEvidencija.SelectedItems[0].Checked)
                {
                    btnDodajUkloniEvidencijuSaKalendara.Text = "Ukloni sa kalendara";
                }
                else
                {
                    btnDodajUkloniEvidencijuSaKalendara.Text = "Dodaj na kalendar";
                }
            }
        }

        private void boldShortcutKey_KeyDown(object sender, KeyEventArgs e)
        {
            //check if ctrl+b and bold if yes
            GlobalVariables.boldShortcutKey_KeyDown(sender, e);
            //check if ctrl+v or shift+insert and paste only text without formatting
            GlobalVariables.ignoreFormattingWhenPasting_KeyDown(sender, e);
        }
    }
}