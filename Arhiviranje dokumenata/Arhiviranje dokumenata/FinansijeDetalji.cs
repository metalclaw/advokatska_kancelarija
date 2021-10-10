using Arhiviranje_dokumenata.Helpers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arhiviranje_dokumenata
{
    public partial class FinansijeDetalji : Form
    {
        private PrintDocument printBeleske = new PrintDocument();
        private int checkPrint = 0;
        ZakljucaneBeleske parentInstance;
        Finansije podaci;

        public FinansijeDetalji(ZakljucaneBeleske parent, Finansije detalji = null)
        {
            InitializeComponent();

            parentInstance = parent;

            printBeleske.BeginPrint += printBeleske_BeginPrint;
            printBeleske.PrintPage += printBeleske_PrintPage;

            if (detalji != null)
            {
                if (GlobalVariables.IsValidRtf(detalji.text))
                {
                    rtbBeleske.Rtf = detalji.text;
                }
                else
                {
                    rtbBeleske.Text = detalji.text;
                }

                cbPlaceno.Checked = detalji.placeno;

                podaci = detalji;

                btnObrisi.Enabled = true;
            }
        }

        private void rtbBeleske_KeyDown(object sender, KeyEventArgs e)
        {
            //check if ctrl+b and bold if yes
            GlobalVariables.boldShortcutKey_KeyDown(sender, e);
            //check if ctrl+v or shift+insert and paste only text without formatting
            GlobalVariables.ignoreFormattingWhenPasting_KeyDown(sender, e);
        }

        private void btnPrintBeleske_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printBeleske.Print();
            }
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

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            if (podaci == null) {//snimanje novog shita
                parentInstance.DodajNov(new Finansije
                {
                    Id = ObjectId.GenerateNewId(),
                    datumUnosa = DateTime.Now,
                    placeno = cbPlaceno.Checked,
                    text = rtbBeleske.Rtf
                });
            }
            else
            {//update starog shita
                podaci.text = rtbBeleske.Rtf;
                podaci.placeno = cbPlaceno.Checked;
                parentInstance.OsveziStari(podaci);
            }

            Close();
            Dispose();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            parentInstance.Obrisi(podaci);
            Close();
            Dispose();
        }
    }
}
