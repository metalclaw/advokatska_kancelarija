using System;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;

namespace Arhiviranje_dokumenata
{
    public partial class DatabaseFix : Form
    {
        public DatabaseFix()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          /*  var data = DatabaseCommunication.databaseFixHelper();

            int minutesToAdd = 0;

            foreach (PredmetData predmet in data) {
                if (predmet.datumUnosaUBazu == DateTime.MinValue) {
                    predmet.datumUnosaUBazu = DateTime.Now.AddMinutes(minutesToAdd);

                    DatabaseCommunication.databaseFixUpdatePredmet(predmet);

                    minutesToAdd++;
                }
            }

            MessageBox.Show("Collection updated successfully!");*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // DatabaseCommunication.srediShit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
          //  DatabaseCommunication.dbFixRadnikStringToArray();
        }

        private void button4_Click(object sender, EventArgs e)
        {
         //   DatabaseCommunication.dbFixSveEvidencijaZaDanUNazad();
         //   MessageBox.Show("Gotovo");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var predmeti = DatabaseCommunication.getSviPredmeti();
            int brojac = 0;
            foreach (PredmetData item in predmeti) {
                if (item.placanjaDugovanja != null && item.placanjaDugovanja != "") {
                    DatabaseCommunication.databaseFixUpdatePredmet(item);
                    brojac++;
                }
            }

            MessageBox.Show("Konvertovano je " + brojac.ToString() + " predmeta.");
        }
    }
}
