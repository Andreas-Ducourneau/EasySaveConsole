namespace Core.Model.Business
{
    public class Save : Isave
    {
        // Here we define the property for the complete backup
        public string TypeOfSave { get; set; }
        public string name { get; set; }
        public string SourceDirectory { get; set; }
        public string DestinationDirectory { get; set; }
        public string LastCompleteDirectory { get; set; }
    }
}
