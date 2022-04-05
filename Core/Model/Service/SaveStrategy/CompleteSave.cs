using System.IO;
using System;
using System.Diagnostics;
using System.Threading;
using Core.Model.Business;
using System.Collections.Generic;

namespace Core.Model.Service.SaveStrategy
{
    public class CompleteSave : Strategy
    {
        private const String logFilePath = "..\\..\\..\\log.json";
        private const String StatePausePath = "ThreadState.txt";
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

            // Start the backup
            // We get the today date for the name of our backups  
            String date = NowDate.ToString("yyyy-MM-dd,HH.mm.ss");
            // Here we get the informations about the source directory and the Destination Directory and he we get all the source file from the source directory
            string SourceDirectory = sourceDirectory;
            string DestinationDirectory = destinationDirectory + name + " Le." + date;

            foreach (var type in list)
            {
                string[] originalFiles = Directory.GetFiles(SourceDirectory, "*" + type.Type, SearchOption.AllDirectories);
                // We check if the source directory is empty to prevent the creation of empty backups

                if (originalFiles.Length != 0)
                {
                    System.IO.Directory.CreateDirectory(DestinationDirectory);
                    Nko.limit = 1000;

                    // Here we copy the file in the directory that we created just above
                    Array.ForEach(originalFiles, (originalFileLocation) =>
                    {
                        Thread.Sleep(250);
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

                        FileInfo sourceFile = new FileInfo(originalFileLocation);
                        FileInfo destFile = new FileInfo(originalFileLocation.Replace(SourceDirectory, DestinationDirectory));
                            // Here we copy everything and we create directory if needed
                            if (destFile.Exists && sourceFile.Length < Nko.limit)
                        {
                            sourceFile.CopyTo(destFile.FullName, true);
                        }
                        else if (!destFile.Exists && sourceFile.Length < Nko.limit)
                        {
                            Directory.CreateDirectory(destFile.DirectoryName);
                            sourceFile.CopyTo(destFile.FullName, false);
                        }
                        if (sourceFile.Length > Nko.limit)
                        {
                            Monitor.Enter(obj);
                            try
                            {
                                Trace.WriteLine("Monitor");
                                if (destFile.Exists)
                                {
                                    sourceFile.CopyTo(destFile.FullName, true);
                                }
                                else if (!destFile.Exists)
                                {
                                    Directory.CreateDirectory(destFile.DirectoryName);
                                    sourceFile.CopyTo(destFile.FullName, false);
                                }
                            }
                                // catch blocks go here.
                                finally
                            {
                                Monitor.Exit(obj);
                            }
                        }
                    });
                }
                else if (originalFiles.Length == 0)
                // We check if the source directory is empty
                {
                    Trace.WriteLine("Source directory empty");
                }
                else
                {
                    Trace.WriteLine("error");
                }

                long size = 0;
                long currentSize = 0;
                int i = -1;
                int j = 0;

                // Counts the number of files in the source folder
                foreach (string srcFile in originalFiles)
                {
                    size = new FileInfo(srcFile).Length;
                }

                bool status = true;

                // Write Info for StateFile when Job is On
                while (i < j)
                {
                    for (j = 0; j < originalFiles.Length; j++)
                    {
                        string srcFile = originalFiles[j];
                        currentSize = new FileInfo(srcFile).Length;

                        SaveState.OpenFile();
                        SaveState.SaveTime(date);
                        SaveState.SaveName(DestinationDirectory);
                        SaveState.SaveStatus(status);
                        SaveState.FileCount(originalFiles);
                        SaveState.SaveSize(size);
                        SaveState.SaveProgress(j, originalFiles);
                        SaveState.FileCountLeft(j, originalFiles);
                        SaveState.SaveSizeLeft(size, currentSize);
                        SaveState.SaveSourceFile(j, originalFiles);
                        SaveState.SaveDestination(DestinationDirectory);
                        SaveState.CloseFile();
                        i++;
                    }
                }

                // Write Info for StateFile when Job is Off
                status = false;

                SaveState.OpenFile();
                SaveState.SaveTime(date);
                SaveState.SaveName(DestinationDirectory);
                SaveState.SaveStatus(status);
                SaveState.CloseFile();

                for (int x = 0; x < originalFiles.Length; x++)
                {
                    string srcFile = originalFiles[x];
                    currentSize = new FileInfo(srcFile).Length;
                    long msbefore = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    using (StreamWriter w = File.AppendText(logFilePath))
                    {
                        Log.Logger(name, srcFile, DestinationDirectory, currentSize, DateTimeOffset.Now.ToUnixTimeMilliseconds() - msbefore, w);
                    }
                }
            }
            CryptoSoft.CryptoSoft.Crypto(DestinationDirectory);
        }
    }
}