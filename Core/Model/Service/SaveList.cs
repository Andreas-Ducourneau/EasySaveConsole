using Core.Model.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Core.Model.Service
{
    public class SaveList
    {
        // This function add a save to the save list 
        public static void addSave(string Name, string sourceDirectory, string destinationDirectory, string typeOfSave, string lastCompleteDirectory)
        {
            List<Save> saveList = new List<Save>();
            Save saves = new Save
            {
                TypeOfSave = typeOfSave,
                name = Name,
                SourceDirectory = sourceDirectory,
                DestinationDirectory = destinationDirectory,
                LastCompleteDirectory = lastCompleteDirectory
            };
            saveList = ImportSaveList();
            saveList.Add(saves);
            ExportList(saveList);
        }

        // This function export the list to a json file
        public static void ExportList(List<Save> listBackup)
        {
            string JsonString = JsonConvert.SerializeObject(listBackup, Formatting.Indented);

            using (var streamWriter = new StreamWriter("data.json"))
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
        public static List<Save> ImportSaveList()
        {
            using (var streamReader = new StreamReader("data.json"))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<List<Save>>(jsonReader);
                }
            }
        }

        // This is the code to delete a backup
        public static void DeleteSave(Save save)
        {
            //Create new list
            List<Save> saveList = new List<Save>();
            // Gets the contents of the data file and push on the list
            saveList = SaveList.ImportSaveList();

            int position = SeachIndex(saveList, save);
            saveList.RemoveAt(position);
            ExportList(saveList);
        }

        // We get the index of the backup which is running
        public static int SeachIndex(List<Save> liste, Save save)
        {
            int index = 0;
            for (int i = 0; save.name != liste[i].name; i++)
            {
                index++;
            }
            return index;
        }

        public static bool SearchNameExist(string name)
        {
            bool IsExist = false;
            List<Save> saveList = new List<Save>();
            // Gets the contents of the data file and push on the list
            saveList = SaveList.ImportSaveList();

            for (int i = 0; i < saveList.Count; i++)
            {
                if (saveList[i].name != name)
                {
                    IsExist = false;
                }
                else if (saveList[i].name == name)
                {
                    IsExist = true;
                }
            }
            return IsExist;
        }
        public static int IndexByName(string name)
        {
            List<Save> saveList = new List<Save>();
            saveList = SaveList.ImportSaveList();
            int index = 0;
            for (int i = 0; name != saveList[i].name; i++)
            {
                index++;
            }
            return index;
        }
        public static Save SaveByIndex(int index)
        {
            List<Save> saveList = new List<Save>();
            saveList = SaveList.ImportSaveList();
            return saveList[index];
        }
    }
}
