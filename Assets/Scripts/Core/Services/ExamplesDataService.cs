using System.Collections.Generic;
using System.IO;
using Core.Models;
using Newtonsoft.Json;

namespace Core.Services
{
    public class ExamplesDataService
    {
        //private const string ExamplesFolderName = @".\Work\Examples";
        
        private const string ExamplesFolderName = @".\Work\Examples\";

        private readonly List<Example> _examples = new List<Example>();
        
        public ExamplesDataService()
        {
            _examples.Add(new Example(
                new Rectangle(new Point(12.4, 67.2), new Point(-2.5, -56.4)),
                new List<Rectangle>()
                {
                    new Rectangle(new Point(-2.5, -56.4), new Point(12.4, 67.2)),
                    new Rectangle(new Point(12.4, 67.2), new Point(-2.5, -56.4)),
                }));
            
            _examples.Add(new Example(
                new Rectangle(new Point(1, 1), new Point(1, 1)),
                new List<Rectangle>()
                {
                    new Rectangle(new Point(1000, 1000), new Point(1000, 1000)),
                    new Rectangle(new Point(1000, 1000), new Point(1000, 1000)),
                }));
            
            File.WriteAllText(ExamplesFolderName + "examples.json", JsonConvert.SerializeObject(_examples, Formatting.Indented));
        }
        
    }
}