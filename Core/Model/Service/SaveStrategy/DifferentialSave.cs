using Core.Model.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;

namespace Core.Model.Service.SaveStrategy
{
    public class DifferentialSave : Strategy
    {
        private const String logFilePath = "..\\..\\..\\log.json";
        private DateTime NowDate = DateTime.Now;
        public static bool IsPaused = false;
        public static bool IsStopped = false;
        static List<FileType> list = new List<FileType>();
        public static void isPaused(bool check)
        {
            IsPaused = check;
        }

        public static void isStopped(bool check)
        {
            IsStopped = check;
        }
        public override void SaveAlgorithm(string name, string sourceDirectory, string destinationDirectory, string lastCompleteDirectory, Nko obj, string Type)
        {
            FileTypeList.finishList(sourceDirectory);
            list = FileTypeList.ImportTypeList();
            String date = NowDate.ToString("yyyy-MM-dd,HH.mm.ss");
            string DestinationDirectory = destinationDirectory + name + "Le." + date + ".zip";
            if (Directory.Exists("..\\..\\..\\tempSource\\"))
            {
                string pathTemp = "..\\..\\..\\tempSource\\";
                Directory.Delete(pathTemp, true);
            }
            foreach (var type in list)
            {
                // We get the informations of the backup that the user write
                string SourceDirectory = sourceDirectory;
                string completeDirectory = lastCompleteDirectory;
                string TempSourcedDirectory = "..\\..\\..\\tempSource\\";
                string sourceFileName;
                string savedFileName;

                // We create the temp directory and we get all the file in the source and we also get every file from the last complete backups
                DirectoryInfo di = Directory.CreateDirectory(TempSourcedDirectory);
                var source = new DirectoryInfo(SourceDirectory);
                var directory = new DirectoryInfo(completeDirectory);

                var lastSave = directory.GetDirectories().OrderByDescending(f => f.LastWriteTime).First();
                Trace.WriteLine(lastSave.ToString());
                string LastCompleteDirectory = lastSave.ToString();

                Nko.limit = 1000;

                string[] sourcesFiles = Directory.GetFiles(SourceDirectory, "*" + type.Type, SearchOption.AllDirectories);
                string[] savedfile = Directory.GetFiles(LastCompleteDirectory, "*" + type.Type, SearchOption.AllDirectories);

                foreach (string s in sourcesFiles)
                {
                    foreach (string d in savedfile)
                    {
                        while (IsPaused == true)
                        {
                            Trace.WriteLine("PAUSE");
                            Thread.Sleep(2000);
                        }
                        if (IsStopped == true)
                        {
                            Thread.Sleep(2000);
                            Thread.CurrentThread.Interrupt();
                        }
                        bool isProcessAlive = ProcessList.IsProcessExist();

                        while (isProcessAlive == true)
                        {
                            Thread.Sleep(2000);
                            isProcessAlive = ProcessList.IsProcessExist();
                            if (isProcessAlive == false)
                            {
                                Trace.WriteLine("ON PEUT CONTINUER");
                            }
                        }

                        // We use this to manipulate the information of the file
                        sourceFileName = System.IO.Path.GetFileName(s);
                        savedFileName = System.IO.Path.GetFileName(d);

                        FileInfo SourceFile = new System.IO.FileInfo(sourceFileName);
                        FileInfo SavedFile = new System.IO.FileInfo(savedFileName);
                        Console.WriteLine(SavedFile);
                        FileInfo SourceFilePath = new System.IO.FileInfo(System.IO.Path.Combine(SourceDirectory, SourceFile.Name));
                        FileInfo SavedFilePath = new System.IO.FileInfo(System.IO.Path.Combine(LastCompleteDirectory, SavedFile.Name));
                        FileInfo SemiPath = new System.IO.FileInfo(System.IO.Path.Combine(LastCompleteDirectory, SourceFile.Name));
                        // If the name of the source file and the file from the last is the name, we compare the last write time
                        if (SourceFile.Name.Equals(SavedFile.Name))
                        {
                            if (SourceFilePath.LastWriteTime > SavedFilePath.LastWriteTime)
                            {
                                System.IO.File.Copy(s, Path.Combine(TempSourcedDirectory, SourceFile.Name), true);
                            }
                            else
                            {
                                Trace.WriteLine(" Any file to copy");
                            }
                        }
                        else if (SemiPath.Exists)
                        {
                        }
                        else if (!SavedFile.Exists)
                        {
                            // If the file don't exit we create it because it mean that its a new save
                            System.IO.File.Copy(s, Path.Combine(TempSourcedDirectory, SourceFile.Name), true);
                        }
                    }
                }
                if (Directory.GetDirectories(TempSourcedDirectory).Length == 0 && Directory.GetFiles(TempSourcedDirectory).Length == 0)
                {
                    Console.WriteLine("Any file changed since the last complete Backup, impossible to create empty backup");
                }
                else
                {
                    // At the end we create a zip from the temp directory to the destination directory and we delete our temp directory
                    CryptoSoft.CryptoSoft.Crypto(TempSourcedDirectory);
                    ZipFile.CreateFromDirectory(TempSourcedDirectory, DestinationDirectory);
                }
                Console.ReadLine();
                string PathSaved = "..\\..\\..\\tempSource\\";
                Directory.Delete(PathSaved, true);

                long size = 0;
                long currentSize = 0;
                int i = 0;
                int j = 0;

                foreach (string srcFile in sourcesFiles)
                {
                    size = new FileInfo(srcFile).Length;
                }
                bool status = true;
                // Write Info for StateFile when Job is On
                while (i < j)
                {
                    for (j = 0; j < sourcesFiles.Length; j++)
                    {
                        string srcFile = sourcesFiles[i];
                        currentSize = new FileInfo(srcFile).Length;

                        SaveState.OpenFile();
                        SaveState.SaveTime(date);
                        SaveState.SaveName(destinationDirectory);
                        SaveState.SaveStatus(status);
                        SaveState.FileCount(sourcesFiles);
                        SaveState.SaveSize(size);
                        SaveState.SaveProgress(j, sourcesFiles);
                        SaveState.FileCountLeft(j, sourcesFiles);
                        SaveState.SaveSizeLeft(size, currentSize);
                        SaveState.SaveSourceFile(j, sourcesFiles);
                        SaveState.SaveDestination(destinationDirectory);
                        SaveState.CloseFile();
                        i++;

                        long msbefore = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        using (StreamWriter w = File.AppendText(logFilePath))
                        {
                            Log.Logger(name, srcFile, destinationDirectory, currentSize, DateTimeOffset.Now.ToUnixTimeMilliseconds() - msbefore, w);
                        }
                    }
                }

                // Write Info for StateFile when Job is Off
                status = false;

                SaveState.OpenFile();
                SaveState.SaveTime(date);
                SaveState.SaveName(destinationDirectory);
                SaveState.SaveStatus(status);
                SaveState.CloseFile();
            }
        }
    }
}

