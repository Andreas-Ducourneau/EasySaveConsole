using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;

namespace EasySaveApp
{
    //Here we create a progress bar for each backup
    public partial class ProgressBar : Window
    {
        public static string[] srcFile;
        public static string destDir;
        public static string nameSave;
        private DateTime NowDate = DateTime.Now;
        int i = 0;

        public ProgressBar(string[] originalFiles, string name, string destinationDir)
        {
            srcFile = originalFiles;
            destDir = destinationDir;
            nameSave = name;

            //We open the window of the progress bar
            InitializeComponent();
            this.Show();
            SName(name);

            //We create a thread to update in asynchronous mode the progress bar
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        //We set the name of the backup in the window
        private void SName(string name)
        {
            saveName.Inlines.Add(name);
        }

        //This function allows us to calculate the progress of the backup
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            String date = NowDate.ToString("yyyy-MM-dd,HH.mm.ss");
            string dirname = destDir + nameSave + " Le." + date;

            while (i <= srcFile.Length)
            {
                if (i == srcFile.Length)
                {
                    int max = 100;
                    (sender as BackgroundWorker).ReportProgress(max);
                }
                else
                {
                    string filename = System.IO.Path.GetFileName(srcFile[i]);
                    string path = dirname + "\\" + filename;
                    if (!File.Exists(path))
                    {
                        Thread.Sleep(500);
                        worker_DoWork(sender, e);
                    }
                    else
                    {
                        int progress = (int)((float)i / srcFile.Length * 100.0);
                        (sender as BackgroundWorker).ReportProgress(progress);
                    }
                }
                i++;
            }
        }

        //This function is to update the progress bar
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
