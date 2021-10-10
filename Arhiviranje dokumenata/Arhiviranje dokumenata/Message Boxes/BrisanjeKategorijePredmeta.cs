using System;
using System.Windows.Forms;

namespace Arhiviranje_dokumenata.Message_Boxes
{
    public partial class BrisanjeKategorijePredmeta : Form
    {
        Podesavanja parent = null;
        bool radnici = false;
        public BrisanjeKategorijePredmeta(Podesavanja parentInstance)
        {
            InitializeComponent();
            parent = parentInstance;
        }

        public BrisanjeKategorijePredmeta(Podesavanja parentInstance, string text)
        {
            InitializeComponent();
            parent = parentInstance;
            label1.Text = text;
            radnici = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //da
            if (!radnici)
            {
                parent.obrisiKategorijuPredmeta();
            }
            else {
                parent.obrisiRadnika();
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ne
            Close();
        }
    }
}
