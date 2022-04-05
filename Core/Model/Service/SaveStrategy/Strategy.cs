using Core.Model.Business;

namespace Core.Model.Service.SaveStrategy
{
    public abstract class Strategy
    {
        public abstract void SaveAlgorithm(string name, string sourceDirectory, string destinationDirectory, string lastCompleteDirectory,Nko obj, string Type);
    }
}
