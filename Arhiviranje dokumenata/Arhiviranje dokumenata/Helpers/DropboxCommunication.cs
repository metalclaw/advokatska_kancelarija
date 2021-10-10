using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Dropbox.Api;
using Dropbox.Api.Files;
using System.Net.Http;
using System.IO;
using System.Globalization;

namespace Arhiviranje_dokumenata.Helpers
{
    class DropboxCommunication
    {
        #region save excel table to dropbox
        public async Task uploadExcel(Form1 parent, string filePath, bool deleteFile)
        {
            var client = createConnection(string.Empty);

            var path = GlobalVariables.excel_dropbox_backup_path;
            var folder = await CreateFolder(client, path);

            var fileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);

            bool error = await Upload(parent, client, path, fileName, File.ReadAllBytes(filePath));

            if (!error)
            {
                parent.showMessage("Bekap excel tabele na Dropbox uspešan!");
                parent.dropboxUploadDone(deleteFile, filePath);
            }
            else
            {
                parent.dropboxUploadDone(deleteFile, filePath);
            }
        }
        #endregion

        #region check connection to account
        public async Task checkConnectionToDropbox(string providedAccessToken) {
            try
            {
                var client = createConnection(providedAccessToken);

                var full = await client.Users.GetCurrentAccountAsync();

                MessageBox.Show("Povezivanje sa Dropbox nalogom je uspešno!");
            }
            catch (Exception e) {
                MessageBox.Show("Došlo je do greške, proverite da li računar ima vezu sa internetom i da li je token u redu!", "Greška!");
            }
        }
        #endregion

        #region database backup
        public async Task<int> startBackup(Form1 parent, string pathToFolder, string newFolderName)
        {
            try
            {
                var client = createConnection(string.Empty);

                string[] fileEntries = Directory.GetFiles(pathToFolder);

                bool backupError = await backUp(parent, client, newFolderName, fileEntries);

                if (!backupError)
                {
                    parent.showMessage("Bekap na Dropbox uspešan!");
                }

                parent.updateBackupInfo("Brišem stare bekape sa dropboxa");
                bool cleanBackupError = await clearDropboxBackup(parent, client);

                if (!cleanBackupError)
                {
                    parent.showMessage("Stari bekap je uspešno uklonjen sa dropboxa!");
                }
            }
            catch (HttpException e)
            {
                parent.showMessage("Exception reported from RPC layer. Status code: " + e.StatusCode + "\nMessage: " + e.Message);
            }
            finally {
                parent.backupUploadDone();
            }

            return 0;
        }

        private async Task<bool> backUp(Form1 parent, DropboxClient client, string newFolderName, string[] fileEntries)
        {
            var path = GlobalVariables.dropbox_backup_root_folder + newFolderName;
            var folder = await CreateFolder(client, path);
            bool error = false;

            foreach (string file in fileEntries)
            {
                string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                if (!error)
                {
                    error = await Upload(parent, client, path, fileName, File.ReadAllBytes(file));
                }
            }
            return error;
        }

        private async Task<FolderMetadata> CreateFolder(DropboxClient client, string path)
        {
            FolderMetadata folder = new FolderMetadata();
            try
            {
                var folderArg = new CreateFolderArg(path);
                folder = await client.Files.CreateFolderAsync(folderArg);
            }
            catch (Exception e){
            }
            return folder;
        }
        #endregion

        #region database backup cleaning

        public async Task<bool> clearDropboxBackup(Form1 parent, DropboxClient client) {
            var list = await client.Files.ListFolderAsync(GlobalVariables.dropbox_backup_root_folder);

            List<string> dates = new List<string>();
            var listForRemoval = new List<string>();

            foreach (var item in list.Entries.Where(i => i.IsFolder))
            {
                dates.Add(item.Name);
            }

            if (dates.Count > GlobalVariables.number_of_database_backups_to_keep)
            {
                var orderedList = dates.OrderByDescending(x => DateTime.ParseExact(x, GlobalVariables.database_backup_name_string_pattern, CultureInfo.InvariantCulture)).ToList();

                //sa kraja teba da skida
                while (orderedList.Count > GlobalVariables.number_of_database_backups_to_keep)
                {
                    listForRemoval.Add(orderedList[orderedList.Count - 1]);
                    orderedList.RemoveAt(orderedList.Count - 1);
                }
            }
           
            if (listForRemoval.Count > 0) {
                bool error = false;
                foreach (string folder in listForRemoval) {
                    if (!error)
                    {
                        error = await deleteFolder(parent, client, folder);
                    }
                    else {
                        break;
                    }
                }
                return error;
            }
            return false;
        }

        private async Task<bool> deleteFolder(Form1 parent, DropboxClient client, string folder) {
            try
            {
                await client.Files.DeleteAsync(GlobalVariables.dropbox_backup_root_folder + folder);
                return false;
            }
            catch (Exception e) {
                parent.showMessage("Došlo je do greške pri čišćenju starog bekapa: " + e.Message);
                return true;
            }
        }
        #endregion

        #region helper methods
        private async Task<bool> Upload(Form1 parent, DropboxClient client, string folder, string fileName, byte[] fileContent)
        {
            using (var stream = new MemoryStream(fileContent))
            {
                try
                {
                    var response = await client.Files.UploadAsync(folder + "/" + fileName, WriteMode.Overwrite.Instance, body: stream);
                    return false;
                }
                catch (Exception e)
                {
                    parent.showMessage("Došlo je do greške kod bekapa na dropbox: " + e.Message);
                    return true;
                }
            }
        }

        private DropboxClient createConnection(string providedAccessToken) {
            var httpClient = new HttpClient(new WebRequestHandler { ReadWriteTimeout = 10 * 1000 })
            {
                Timeout = TimeSpan.FromMinutes(20)
            };

            var config = new DropboxClientConfig("ArhiviranjeDokumenata")
            {
                HttpClient = httpClient
            };

            string accessToken = "";

            if (String.IsNullOrEmpty(providedAccessToken))
            {
                accessToken = (string)Properties.Settings.Default["dropboxAccessToken"];
            }
            else
            {
                accessToken = providedAccessToken;
            }

            return new DropboxClient(accessToken, config);
        }
        #endregion
    }
}
