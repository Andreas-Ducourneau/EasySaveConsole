using System.IO;

namespace Core.Model.Service
{
    public class SaveState
    {
        private static StreamWriter sw;

        // We open the State file
        public static void OpenFile()
        {
            sw = new StreamWriter("..\\..\\..\\..\\StateFile.json");
        }

        // We close the State file
        public static void CloseFile()
        {
            sw.WriteLine("################");
            sw.Close();
        }

        // We get the name of the backup
        public static void SaveName(string DestinationDirectory)
        {
            string name = (Path.GetFileName(DestinationDirectory));
            sw.WriteLine(name);
        }

        // We get the time of the backup
        public static void SaveTime(string date)
        {
            sw.WriteLine(date);
        }

        // We get the status of the backup, if it's running or not
        public static void SaveStatus(bool status)
        {
            sw.WriteLine("L'état du travail de sauvegarde est {0}", status);
        }

        // We count the numbers of files in the backup
        public static void FileCount(string[] originalFiles)
        {
            int nbFichiers = originalFiles.Length;
            sw.WriteLine("Nombre de fichiers : {0}", nbFichiers);
        }

        // We count the size of the backup
        public static void SaveSize(long size)
        {
            sw.WriteLine("Taille des fichiers : {0} octets", size);
        }

        // We calculate the progress of the backup
        public static void SaveProgress(int j, string[] originalFiles)
        {
            float progress = (float)((float)j / originalFiles.Length * 100.0);
            sw.WriteLine("Progression : {0}", progress);
        }

        // We count the number of remaining files
        public static void FileCountLeft(int j, string[] orginalFiles)
        {
            long nbFichiersLeft = orginalFiles.Length - j;
            sw.WriteLine("Nombre de fichiers restants : {0}", nbFichiersLeft);
        }

        // We count the remaining size of the backup
        public static void SaveSizeLeft(long size, long currentSize)
        {
            long sizeLeft = currentSize - size;
            sw.WriteLine("Taille des fichiers restants : {0}", sizeLeft);
        }

        // We get the source folder of the backup
        public static void SaveSourceFile(int j, string[] originalFiles)
        {
            string srcFile = originalFiles[j];
            sw.WriteLine("Fichier source en cours de sauvegarde : {0}", srcFile);
        }

        // We get the destination folder of the backup
        public static void SaveDestination(string DestinationDirectory)
        {
            sw.WriteLine("Adresse de destination : {0}", DestinationDirectory);
        }
    }
}
