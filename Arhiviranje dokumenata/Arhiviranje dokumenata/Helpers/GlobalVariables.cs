using System.Drawing;
using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Configuration;
using Arhiviranje_dokumenata.Properties;

namespace Arhiviranje_dokumenata.Helpers
{
    static class GlobalVariables
    {
        #region konstante
        public const string google_calendar_timezone = "Europe/Belgrade";
        public const string google_calendar_datetime_pattern = "yyyy-MM-ddTHH:mm:ss";
        public const string date_string_pattern = "dd.MM.yyyy";
        public const string date_time_string_pattern = "dd.MM.yyyy - HH:mm";
        public const string database_backup_name_string_pattern = "ddMMyyyyhhmm";//dan mesec godina sat minut
        public const string dropbox_backup_root_folder = "/mongo_backup/";
        public const string excel_dropbox_backup_path = "/Excel tabela";
        public const string mongodb_connection_prefix = "mongodb://";
        public const int number_of_database_backups_to_keep = 50;
        public static bool mongo_is_installed = false;
        public const int page_size_for_svi_predmeti_list = 10;
        public readonly static Color evidencija_priority_1 = Color.FromArgb(211, 18, 18);
        public readonly static Color evidencija_priority_2 = Color.FromArgb(120, 156, 247);
        public readonly static Color evidencija_priority_3 = Color.FromArgb(143, 247, 120);
        public readonly static Color evidencija_priority_default = Color.FromArgb(255, 255, 255);
        public readonly static Color arhivirana_evidencija_color = Color.Silver;
        public readonly static Color background_color = Color.FromKnownColor(KnownColor.Control);

        //KnownColor.Thistle
        //KnownColor.Control

        public readonly static string GoogleCalendarColorEvidencije = "10";
        public readonly static string GoogleCalendarColorRocista = "11";

        #endregion

        #region funkcije
        public static int[] razbijBrojPredmeta(string brojPredmeta)
        {
            int[] arr = new int[2];
            int stop = brojPredmeta.IndexOf("/");

            if (stop != -1)
            {
                arr[0] = Convert.ToInt32(brojPredmeta.Substring(0, stop));
                arr[1] = Convert.ToInt32(brojPredmeta.Substring(stop + 1, brojPredmeta.Length - 1 - stop));
            }
            else
            {
                arr[0] = Convert.ToInt32(brojPredmeta);
            }

            return arr;
        }

        public static string spojBrojPredmeta(int brPredmetaBr, int brPredmetaGod)
        {
            return brPredmetaBr.ToString() + "/" + brPredmetaGod.ToString();
        }

        public static string spremiZaSrpskuRegexPretragu(string input)
        {
            return input.Replace("C", "[CČĆ]").Replace("c", "[cčć]").Replace("S", "[SŠ]").Replace("s", "[sš]").Replace("Z", "[ZŽ]").Replace("z", "[zž]");
        }

        public static bool IsValidRtf(string text)
        {
            var rtb = new RichTextBox();
            try
            {
                rtb.Rtf = text;
            }
            catch (ArgumentException)
            {
                return false;
            }
            finally
            {
                rtb.Dispose();
            }

            return true;
        }

        public static bool IsOnline()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool proveriSifru(string password, Form1 parent)
        {
            string savedPasswordHash = DatabaseCommunication.getSifraZaZakljucanDeoPrograma(parent);

            //ako je defaultna sifra vrati true
            if (savedPasswordHash == "123456")
            {
                return true;
            }

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static string RTFToText(string rtf)
        {
            if (IsValidRtf(rtf))
            {
                using (RichTextBox rtb = new RichTextBox())
                {
                    rtb.Rtf = rtf;
                    return rtb.Text;
                }
            }
            else
            {
                return rtf;
            }
        }


        public static void boldShortcutKey_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.KeyCode == Keys.B)
            {
                var textbox = ((RichTextBox)sender);

                try
                {
                    if (!textbox.SelectionFont.Bold)
                    {
                        textbox.SelectionFont = new Font(textbox.Font, FontStyle.Bold);
                        textbox.SelectionStart = textbox.SelectionStart + textbox.SelectionLength;
                        textbox.SelectionLength = 0;
                        // Set font immediately after selection
                        textbox.SelectionFont = textbox.Font;
                    }
                    else
                    {
                        textbox.SelectionFont = textbox.Font;
                        textbox.SelectionStart = textbox.SelectionStart + textbox.SelectionLength;
                        textbox.SelectionLength = 0;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(err);
                }
                e.SuppressKeyPress = true;
            }
        }

        public static void ignoreFormattingWhenPasting_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.V) || (e.Shift && e.KeyCode == Keys.Insert))
            {
                ((RichTextBox)sender).Paste(DataFormats.GetFormat("Text"));
                e.Handled = true;
            }
        }

        #endregion
    }
}