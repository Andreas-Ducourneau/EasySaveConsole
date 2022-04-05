using Core.Model.Business;
using Core.Model.Service.SaveStrategy;
using Core.Model.Service.StrategySave;
using System;
using System.Diagnostics;

// Here in function of the parameters, we execute the right backup algorithms
namespace Core.Model.Service
{
    public class SaveService
    {
        public static void ServiceSave(string name, string sourceDirectory, string destinationDirectory, string typeOfSave, string lastCompleteDirectory, Save save, Nko obj, string Type)
        {
            ContextSave SaveWork;

            // If typeOfSave if equal to full we execute the full backups, same if typeOfSave equal to diff
            if (typeOfSave == "Full")
            {
                Trace.WriteLine("Full");
                SaveWork = new ContextSave(new CompleteSave());
                SaveWork.ContextInterface(name, sourceDirectory, destinationDirectory, lastCompleteDirectory, save, obj, Type);
            }
            else if (typeOfSave == "Diff")
            {
                Trace.WriteLine("Diff");
                SaveWork = new ContextSave(new DifferentialSave());
                SaveWork.ContextInterface(name, sourceDirectory, destinationDirectory, lastCompleteDirectory, save, obj, Type);
            }
            else
            {
                Console.WriteLine("error");
            }
        }
    }
}
