using System;
using Arhiviranje_dokumenata.Helpers;
using System.Windows.Forms;

namespace Arhiviranje_dokumenata
{
    public partial class UpozorenjeMessageBox : Form
    {
        Predmet predmetForma = null;
        public UpozorenjeMessageBox(Predmet predmetForm)
        {
            InitializeComponent();
            BackColor = GlobalVariables.background_color;
            predmetForma = predmetForm;
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            predmetForma.closeWithoutSaving();
            this.Close();
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            predmetForma.saveAndClose();
            this.Close();
        }
    }
}
