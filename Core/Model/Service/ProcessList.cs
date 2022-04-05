using Core.Model.Business;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Core.Model.Service
{
    public class ProcessList
    {
        public static List<MyProcess> GetProcessList()
        {
            return Process.GetProcesses().Select(p => new MyProcess
            {
                Id = p.Id,
                Name = p.ProcessName,
                Priority = p.BasePriority,
            }).ToList();
        }

        public static void addProcess(string Name)
        {
            List<MyProcess> ProcessList = new List<MyProcess>();
            MyProcess process = new MyProcess
            {

                Name = Name,

            };
            ProcessList = ImportProcessList();
            ProcessList.Add(process);
            ExportList(ProcessList);
        }

        // This function export the list to a json file
        public static void ExportList(List<MyProcess> listBackup)
        {
            string JsonString = JsonConvert.SerializeObject(listBackup, Formatting.Indented);

            using (var streamWriter = new StreamWriter("process.json"))
            {
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    var serializer = new JsonSerializer();
                    jsonWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonWriter, JsonConvert.DeserializeObject(JsonString));
                }
            }
        }

        // Here we import the json File 
        public static List<MyProcess> ImportProcessList()
        {
            using (var streamReader = new StreamReader("process.json"))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<List<MyProcess>>(jsonReader);
                }
            }
        }

        // This is the code to delete a backup
        public static void DeleteProcess(MyProcess process)
        {
            //Create new list
            List<MyProcess> processList = new List<MyProcess>();
            // Gets the contents of the data file and push on the list
            processList = ProcessList.ImportProcessList();

            int position = SeachIndex(processList, process);
            processList.RemoveAt(position);
            ExportList(processList);
        }
        public static int SeachIndex(List<MyProcess> liste, MyProcess process)
        {
            int index = 0;
            for (int i = 0; process.Name != liste[i].Name; i++)
            {
                index++;
            }
            return index;
        }
        public static bool SearchNameExist(string name)
        {
            bool IsExist = false;
            List<MyProcess> processList = new List<MyProcess>();
            // Gets the contents of the data file and push on the list
            processList = ImportProcessList();

            for (int i = 0; i < processList.Count; i++)
            {
                if (processList[i].Name != name)
                {
                    IsExist = false;
                }
                else if (processList[i].Name == name)
                {
                    IsExist = true;
                }
            }
            return IsExist;
        }

        public static bool IsProcessExist()
        {
            bool isAlive = false;
            foreach (var process in ProcessList.GetProcessList())
            {
                foreach (var myProcess in ProcessList.ImportProcessList())
                {
                    //Trace.WriteLine("test");
                    if (myProcess.Name.Equals(process.Name))
                    {
                        Trace.WriteLine("ON A PROCESS");
                        isAlive = true;
                    }
                }
            }
            return isAlive;
        }





    }
}
