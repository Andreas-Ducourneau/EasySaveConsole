using Core.Model.Business;
using Core.Model.Service;
using Core.Model.Service.SaveStrategy;
using EasySaveApp.Properties;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace EasySaveApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Nko obj = new Nko();
        Thread thread;
        string Type = "";
        // Here we load everything we need
        public MainWindow()
        {
            InitializeComponent();
            comboBox.Items.Add("Français");
            comboBox.Items.Add("English");
            BackupListDisplay.ItemsSource = SaveList.ImportSaveList();
            ProcessListDisplay.ItemsSource = ProcessList.ImportProcessList();
            TypeListDisplay.ItemsSource = FileTypeList.ImportTypeList();
        }

        // This is the function if the button add backup is triggered
        public void Button_AddBackup(object sender, RoutedEventArgs e)
        {
            bool _checkName = SaveList.SearchNameExist(NBTextbox.Text);
            if (_checkName == false)
            {
                string typeOfSave = "";
                if (Differential_Backup.IsChecked == true)
                {
                    typeOfSave = "Diff";
                }
                else if (Full_Backup.IsChecked == true)
                {
                    typeOfSave = "Full";
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(" you need to choose one button ");
                }
                SaveList.addSave(NBTextbox.Text, SFTextBox.Text, TFTextBox.Text, typeOfSave, LCFTextBox.Text);
                BackupListDisplay.ItemsSource = SaveList.ImportSaveList();
            }
            else if (_checkName == true)
            {
                System.Windows.MessageBox.Show("Name is already Exist");
            }
        }

        // This is the function if the button run backup is triggered
        private void Button_RunBackup(object sender, RoutedEventArgs e)
        {
            Save SaveSelected = (Save)BackupListDisplay.SelectedItem;
            string name = SaveSelected.name;
            string sourceDir = SaveSelected.SourceDirectory;
            string destDir = SaveSelected.DestinationDirectory;
            string lastCompleteDir = SaveSelected.LastCompleteDirectory;
            string TypeOfSave = SaveSelected.TypeOfSave;

            string[] originalFiles = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
            ProgressBar pb = new ProgressBar(originalFiles, name, destDir);

            thread = new Thread(() => SaveService.ServiceSave(name, sourceDir, destDir, TypeOfSave, lastCompleteDir, SaveSelected, obj, Type));
            thread.Start();
            System.Windows.Forms.MessageBox.Show(TypeOfSave);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void BtnSourceFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog BtnSF = new FolderBrowserDialog();
            if (BtnSF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string sourceFile = BtnSF.SelectedPath;
                SFTextBox.Text = sourceFile;
            }
        }

        private void BtnTargetFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog BtnTF = new FolderBrowserDialog();
            if (BtnTF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string targetFile = BtnTF.SelectedPath;
                TFTextBox.Text = targetFile;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                    explanation.Text = Resourcefr.explanation;
                    ListBackUp.Text = Resourcefr.ListBackUp;
                    nameBackup.Text = Resourcefr.nameBackup;
                    SFLabel.Text = Resourcefr.SFLabel;
                    TFLabel.Text = Resourcefr.TFLabel;
                    SelectLabel.Text = Resourcefr.SelectLabel;
                    BtnAdd.Content = Resourcefr.BtnAdd;
                    RunBackup.Content = Resourcefr.RunBackup;
                    BtnDelete.Content = Resourcefr.BtnDelete;
                    RunSequBackup.Content = Resourcefr.OpenLogFile;
                    CurProcess.Text = Resourcefr.CurProcess;
                    langLabel.Content = Resourcefr.langLabel;
                    OpenStateFile.Content = Resourcefr.OpenStateFile;
                    OpenLogFile.Content = Resourcefr.OpenLogFile;
                    Full_Backup.Content = Resourcefr.Full_Backup;
                    Differential_Backup.Content = Resourcefr.Differential_Backup;
                    TFLabel_Copy1.Text = Resourcefr.TFLabel_Copy1;
                    RunSequBackup.Content = Resourcefr.RunSequBackup;
                    Pause.Content = Resourcefr.Pause;
                    Resume.Content = Resourcefr.Resume;
                    TFLabel_Copy.Text = Resourcefr.TFLabel_Copy;
                    Btn_AddProcess.Content = Resourcefr.Btn_AddProcess;
                    Btn_DeleteProcess.Content = Resourcefr.Btn_DeleteProcess;
                    Btn_ThreadAbord.Content = Resourcefr.Btn_ThreadAbord;
                    File_Ext.Content = Resourcefr.File_Ext;
                    Btn_AddFileType.Content = Resourcefr.Btn_AddFileType;
                    Btn_DeleteType.Content = Resourcefr.Btn_DeleteType;
                    Btn_Server.Content = Resourcefr.Btn_Server;
                    break;

                case 1:
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                    explanation.Text = Resourceen.explanation;
                    ListBackUp.Text = Resourceen.ListBackUp;
                    nameBackup.Text = Resourceen.nameBackup;
                    SFLabel.Text = Resourceen.SFLabel;
                    TFLabel.Text = Resourceen.TFLabel;
                    SelectLabel.Text = Resourceen.SelectLabel;
                    BtnAdd.Content = Resourceen.BtnAdd;
                    RunBackup.Content = Resourceen.RunBackup;
                    BtnDelete.Content = Resourceen.BtnDelete;
                    RunSequBackup.Content = Resourceen.OpenLogFile;
                    CurProcess.Text = Resourceen.CurProcess;
                    langLabel.Content = Resourceen.langLabel;
                    OpenStateFile.Content = Resourceen.OpenStateFile;
                    OpenLogFile.Content = Resourceen.OpenLogFile;
                    Full_Backup.Content = Resourceen.Full_Backup;
                    Differential_Backup.Content = Resourceen.Differential_Backup;
                    TFLabel_Copy1.Text = Resourceen.TFLabel_Copy1;
                    RunSequBackup.Content = Resourceen.RunSequBackup;
                    Pause.Content = Resourceen.Pause;
                    Resume.Content = Resourceen.Resume;
                    TFLabel_Copy.Text = Resourceen.TFLabel_Copy;
                    Btn_AddProcess.Content = Resourceen.Btn_AddProcess;
                    Btn_DeleteProcess.Content = Resourceen.Btn_DeleteProcess;
                    Btn_ThreadAbord.Content = Resourceen.Btn_ThreadAbord;
                    File_Ext.Content = Resourceen.File_Ext;
                    Btn_AddFileType.Content = Resourceen.Btn_AddFileType;
                    Btn_DeleteType.Content = Resourceen.Btn_DeleteType;
                    Btn_Server.Content = Resourceen.Btn_Server;
                    break;
            }
        }

        // This is the function if the button delete backup is triggered
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Save SaveSelected = (Save)BackupListDisplay.SelectedItem;
            SaveList.DeleteSave(SaveSelected);
            System.Windows.MessageBox.Show(SaveSelected.name + " is dead");
            BackupListDisplay.ItemsSource = SaveList.ImportSaveList();
        }
        // Here this is the code for the radio button
        private void Differential_Checked(object sender, RoutedEventArgs e)
        {
            BtnAdd.IsEnabled = true;
        }
        private void Full_Checked(object sender, RoutedEventArgs e)
        {
            BtnAdd.IsEnabled = true;
        }

        // Here this is the code to open the log file
        private void Button_Open_Log_File(object sender, RoutedEventArgs e)
        {
            Process stateFile = new Process();
            stateFile.StartInfo = new ProcessStartInfo()
            {
                FileName = "Notepad.exe",
                Arguments = "..\\..\\..\\log.json"
            };
            stateFile.Start();
        }
        // This is the function if the button Sequ backup is triggered
        private void RunSequBackup_Click(object sender, RoutedEventArgs e)
        {
            Nko obj = new Nko();
            foreach (Save saves in BackupListDisplay.SelectedItems)
            {
                string lastCompleteDir = "";
                string name = saves.name.ToString();
                string sourceDir = saves.SourceDirectory.ToString();
                string destDir = saves.DestinationDirectory.ToString();
                string TypeOfSave = saves.TypeOfSave.ToString();

                if (TypeOfSave == "Full")
                {
                    lastCompleteDir = "";
                }
                else if (TypeOfSave == "Diff")
                {
                    lastCompleteDir = saves.LastCompleteDirectory.ToString();
                }
                Type = FileType.Text;
                SaveService.ServiceSave(name, sourceDir, destDir, TypeOfSave, lastCompleteDir, saves, obj, Type);

                //string[] originalFiles = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
                //ProgressBar pb = new ProgressBar(originalFiles, name, destDir);
                System.Windows.Forms.MessageBox.Show(" Save finished we move on the next one ");
            }
        }
        // Here this is the code to open the state file
        private void OpenStateFile_Click(object sender, RoutedEventArgs e)
        {
            Process stateFile = new Process();
            stateFile.StartInfo = new ProcessStartInfo()
            {
                FileName = "Notepad.exe",
                Arguments = "..\\..\\..\\..\\StateFile.json"
            };
            stateFile.Start();
        }

        private void BtnLCF(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog BtnSF = new FolderBrowserDialog();
            if (BtnSF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string LastCompleteDirectory = BtnSF.SelectedPath;
                LCFTextBox.Text = LastCompleteDirectory;
            }
        }

        private void BtnThreadResume_Click(object sender, RoutedEventArgs e)
        {
            bool state = false;
            CompleteSave.isPaused(state);
            DifferentialSave.isPaused(state);
        }

        private void BtnPauseThread_Click(object sender, RoutedEventArgs e)
        {
            bool state = true;
            CompleteSave.isPaused(state);
            DifferentialSave.isPaused(state);
        }

        private void Btn_AddProcess_Click(object sender, RoutedEventArgs e)
        {
            bool _checkName = ProcessList.SearchNameExist(TextBoxProcessName.Text);
            if (_checkName == false)
            {

                ProcessList.addProcess(TextBoxProcessName.Text);
                ProcessListDisplay.ItemsSource = ProcessList.ImportProcessList();

            }
            else if (_checkName == true)
            {
                System.Windows.MessageBox.Show("Name is already Exist");
            }
        }

        private void Btn_DeleteProcess_Click(object sender, RoutedEventArgs e)
        {

            MyProcess processSelected = (MyProcess)ProcessListDisplay.SelectedItem;
            ProcessList.DeleteProcess(processSelected);
            System.Windows.MessageBox.Show(processSelected.Name + " is dead");
            ProcessListDisplay.ItemsSource = ProcessList.ImportProcessList();
        }

        private void Btn_AbortProcess_Click(object sender, RoutedEventArgs e)
        {
            bool state = true;
            CompleteSave.isStopped(state);
            DifferentialSave.isStopped(state);
        }

        private void Btn_AddFileType_Click(object sender, RoutedEventArgs e)
        {
            bool _checkName = FileTypeList.SearchNameExist(FileType.Text);
            if (_checkName == false)
            {

                FileTypeList.addType(FileType.Text);
                TypeListDisplay.ItemsSource = FileTypeList.ImportTypeList();

            }
            else if (_checkName == true)
            {
                System.Windows.MessageBox.Show("Name is already Exist");
            }
        }

        private void Btn_DeleteType_Click(object sender, RoutedEventArgs e)
        {
            FileType fileType = (FileType)TypeListDisplay.SelectedItem;
            FileTypeList.DeleteType(fileType);
            System.Windows.MessageBox.Show(fileType.Type + " is dead");
            TypeListDisplay.ItemsSource = FileTypeList.ImportTypeList();
        }

        private void Button_Click_Server(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("hello");
            Server serveur_socket = new Server();
            Trace.WriteLine(serveur_socket.Activate());
        }

        public static void Play_Socket(string _name, string _namesave)
        {
            //System.Windows.MessageBox.Show(_name);
            System.Windows.MessageBox.Show(_namesave);
            int index = SaveList.IndexByName(_namesave);
            Save save = SaveList.SaveByIndex(index);
            Trace.WriteLine(index);
            if (_name == "Play")
            {
                System.Windows.MessageBox.Show("Play");
                bool state = false;
                CompleteSave.isPaused(state);
                DifferentialSave.isPaused(state);
            }
            else if (_name == "Pause")
            {
                System.Windows.MessageBox.Show("Pause");
                bool state = true;
                CompleteSave.isPaused(state);
                DifferentialSave.isPaused(state);
            }
            else if (_name == "Stop")
            {
                System.Windows.MessageBox.Show("Stop");
                bool state = true;
                CompleteSave.isStopped(state);
                DifferentialSave.isStopped(state);
            }
        }
    }
}
