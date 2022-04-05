using System;
using System.IO;

namespace Core.Model.Service
{
    public class Log
    {
        public static void Logger(string name, string srcFile, string destinationDirectory, long size, long ms, StreamWriter w)
        {
            w.Write("\r\nLog Entry :");
            w.WriteLine($"TransfertDate : {DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine($"EnvironmentName : {name} ");
            w.WriteLine($"SourceFile : {srcFile}");
            w.WriteLine($"DestinationFile : {destinationDirectory}");
            w.WriteLine($"FileSize : {size}");
            w.WriteLine($"TeansfertTime : {ms}");
            w.WriteLine("-------------------------------");
        }
    }
}
