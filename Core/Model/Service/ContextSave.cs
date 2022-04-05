using Core.Model.Business;
using Core.Model.Service.SaveStrategy;

namespace Core.Model.Service.StrategySave
{
    // Here this is the code for the strategy
     class ContextSave
    {
        private Strategy _strategy;
        public ContextSave(Strategy strategy)
        {
            this._strategy = strategy;
        }
        public void ContextInterface(string name, string sourceDirectory, string destinationDirectory, string lastCompleteDirectory, Business.Save save, Nko obj, string Type)
        {
            _strategy.SaveAlgorithm(name, sourceDirectory,  destinationDirectory, lastCompleteDirectory, obj, Type);
        }
    }
}
