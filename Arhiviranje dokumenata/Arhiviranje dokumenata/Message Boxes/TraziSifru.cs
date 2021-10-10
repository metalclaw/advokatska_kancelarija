using System;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;

namespace Arhiviranje_dokumenata.Message_Boxes
{
    public partial class TraziSifru : Form
    {
        private Form1 glavnaForma;
        private Predmet predmetForma;

        public TraziSifru(Form1 mainFormInstance = null, Predmet predmetInstance = null)
        {
            InitializeComponent();
            glavnaForma = mainFormInstance;
            predmetForma = predmetInstance;
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnPrihvati_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.proveriSifru(tbPass.Text, glavnaForma))
            {
                if (predmetForma != null)
                {
                    predmetForma.otvoriPlacanjaDugovanjaZaPolaSekunde();

                }else if (glavnaForma != null)
                {
                    glavnaForma.otvoriListuPlacanjaDugovanja();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Pogrešna šifra!");
            }

            tbPass.Text = string.Empty;
        }

        private void tbPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPrihvati.PerformClick();
            }
        }
    }
}
