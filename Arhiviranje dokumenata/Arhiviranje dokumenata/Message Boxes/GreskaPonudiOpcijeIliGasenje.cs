using System;
using System.Windows.Forms;

namespace Arhiviranje_dokumenata
{
    public partial class GreskaPonudiOpcijeIliGasenje : Form
    {

        Form1 parentInstance = null;

        public GreskaPonudiOpcijeIliGasenje(Form1 parent)
        {
            InitializeComponent();

            parentInstance = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            parentInstance.Close();
        }

        private void btnPodesavanjaPrograma_Click(object sender, EventArgs e)
        {
            Podesavanja podesavanja = new Podesavanja(parentInstance);
            podesavanja.ShowDialog();
            this.Close();
        }
    }
}
