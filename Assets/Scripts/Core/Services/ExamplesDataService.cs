using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Services
{
    public class ExamplesDataService
    {
        //private const string ExamplesFolderName = @".\Work\Examples";
        
        private const string ExamplesFolderName = @".\Work\Examples";
        private const string ExamplesFileName = "examples.json";

        private List<Example> _examples = new List<Example>();

        private DirectoryInfo _examplesDirectory;
        
        public ExamplesDataService()
        {
            _examplesDirectory = new DirectoryInfo(ExamplesFolderName);
            
            if (!_examplesDirectory.Exists)
            {
                _examplesDirectory.Create();
            }
            
            
            
            /*_examples.Add(new Example(
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
            
            File.WriteAllText(ExamplesFolderName + "examples.json", JsonConvert.SerializeObject(_examples, Formatting.Indented));*/
        }

        public async Task<List<Example>> ReadExamplesAsync()
        {
            string fullExamplesFileName = Path.Combine(_examplesDirectory.FullName, ExamplesFileName);
            
            if (!File.Exists(fullExamplesFileName))
            {
                Debug.LogException(new Exception("Examples file not found"));
            }
            
            string jsonNotation = await File.ReadAllTextAsync(fullExamplesFileName);

            _examples = JsonConvert.DeserializeObject<List<Example>>(jsonNotation);

            return _examples;
        }
        
        /*private string GetFullFileName(string fileName)
        {
            return Path.Combine(_examplesDirectory.FullName, fileName);
        }*/

    }
}