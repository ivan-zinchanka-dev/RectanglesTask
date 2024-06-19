using System.IO;

namespace Core.Services
{
    public class ExamplesDataService
    {
        //private const string ExamplesFolderName = @".\Work\Examples";
        
        private const string ExamplesFolderName = @".\Work\Examples";
        
        public ExamplesDataService()
        {
            File.WriteAllText(ExamplesFolderName + "foo.txt", "Foo file");
        }
        
    }
}