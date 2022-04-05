using System.IO;
using System.Diagnostics;
using System;

namespace CryptoSoft
{
    public class CryptoSoft
    {
        // We encrypt the files being backed up
        public static int Crypto(string destDirectory)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string path = destDirectory;
            string key = "..\\..\\..\\key.txt";
            if (!Directory.Exists(path))
            {
                stopwatch.Stop();
                int timeExe = -1;
                return timeExe;
            }
            else
            {
                try
                {
                    string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    foreach (string a in files)
                    {
                        byte[] data = File.ReadAllBytes(a);
                        byte[] keys = File.ReadAllBytes(key);
                        long key64 = keys.LongLength;

                        for (int i = 0; i < data.Length; i++)
                        {
                            byte x = keys[(byte)(i % key64)];
                            data[i] = (byte)(data[i] ^ x);
                        }

                        File.WriteAllBytes(a, data);
                        stopwatch.Stop();
                        Console.WriteLine(a);
                    }

                    int timeExe = (int)(stopwatch.Elapsed.TotalMilliseconds);
                    Console.WriteLine(timeExe);

                    return timeExe;
                }
                catch
                {
                    return -1;
                }
            }
        }
    }
}