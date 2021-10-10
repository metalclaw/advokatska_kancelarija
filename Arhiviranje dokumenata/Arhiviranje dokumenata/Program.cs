using System;
using System.Threading;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Message_Boxes;

namespace Arhiviranje_dokumenata
{
    static class Program
    {
        static LoadingScreen mySplashForm;
        static Form1 myMainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new DatabaseFix());

            mySplashForm = new LoadingScreen();
            if (mySplashForm != null)
            {
                Thread splashThread = new Thread(new ThreadStart(
                    () => { Application.Run(mySplashForm); }));
                splashThread.SetApartmentState(ApartmentState.STA);
                splashThread.Start();
            }
            //Create and Show Main Form
            myMainForm = new Form1();
            myMainForm.LoadCompleted += MainForm_LoadCompleted;
            myMainForm.TopMost = true;
            Application.Run(myMainForm);
            if (!(mySplashForm == null || mySplashForm.Disposing || mySplashForm.IsDisposed))
                mySplashForm.Invoke(new Action(() => {
                     mySplashForm.Activate();
                }));
        }

        private static void MainForm_LoadCompleted(object sender, EventArgs e)
        {
            if (mySplashForm == null || mySplashForm.Disposing || mySplashForm.IsDisposed)
                return;
            mySplashForm.Invoke(new Action(() => { mySplashForm.Close(); }));
            mySplashForm.Dispose();
            mySplashForm = null;
            myMainForm.TopMost = true;
            myMainForm.Activate();
            myMainForm.TopMost = false;
        }
    }
}
