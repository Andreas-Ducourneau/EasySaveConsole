using Core.Model.Business;
using Core.Model.Service.SaveStrategy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Core.Model.Service
{
    public class FileTypeList
    {
        public static void addType(string type)
        {
            List<FileType> FileTypeList = new List<FileType>();
            FileType fileType = new FileType
            {

                Type = type,

            };
            FileTypeList = ImportTypeList();
            FileTypeList.Add(fileType);
            ExportList(FileTypeList);
        }

        // This function export the list to a json file
        public static void ExportList(List<FileType> TypeList)
        {
            string JsonString = JsonConvert.SerializeObject(TypeList, Formatting.Indented);

            using (var streamWriter = new StreamWriter("FileType.json"))
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
        public static List<FileType> ImportTypeList()
        {
            using (var streamReader = new StreamReader("FileType.json"))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<List<FileType>>(jsonReader);
                }
            }
        }

        // This is the code to delete a backup
        public static void DeleteType(FileType fileType)
        {
            //Create new list
            List<FileType> TypeList = new List<FileType>();
            // Gets the contents of the data file and push on the list
            TypeList = ImportTypeList();

            int position = SeachIndex(TypeList, fileType);
            TypeList.RemoveAt(position);
            ExportList(TypeList);
        }
        public static int SeachIndex(List<FileType> liste, FileType fileType)
        {
            int index = 0;
            for (int i = 0; fileType.Type != liste[i].Type; i++)
            {
                index++;
            }
            return index;
        }

        public static bool SearchNameExist(string Type)
        {
            bool IsExist = false;
            List<FileType> TypeList = new List<FileType>();
            // Gets the contents of the data file and push on the list
            TypeList = ImportTypeList();

            for (int i = 0; i < TypeList.Count; i++)
            {
                if (TypeList[i].Type != Type)
                {
                    IsExist = false;
                }
                else if (TypeList[i].Type == Type)
                {
                    IsExist = true;
                    i = TypeList.Count;
                }
            }
            return IsExist;
        }
        public static void finishList(string sourceDirectory)
        {
            string[] files = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories);
            foreach (string f in files)
            {
                var type = f.Split(".");
                string FileExtensions = type[1];

                bool _checkName = SearchNameExist(FileExtensions);

                if (_checkName == false)
                {
                    addType(FileExtensions);
                    Trace.WriteLine("we ad");
                }
                else if (_checkName == true)
                {
                    Trace.WriteLine("Name is already Exist");
                }
            }
        }
    }
}
