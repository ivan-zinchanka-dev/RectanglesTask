using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Services
{
    public class ExamplesReader
    {
        private const string ExamplesFolderName = @".\Work\Examples";
        private const string ExamplesFileName = "examples.json";
        
        private readonly DirectoryInfo _examplesDirectory;
        
        public ExamplesReader()
        {
            _examplesDirectory = new DirectoryInfo(ExamplesFolderName);

            if (!_examplesDirectory.Exists)
            {
                _examplesDirectory.Create();
            }
        }

        public async Task<List<Blueprint>> ReadExamplesAsync()
        {
            string fullExamplesFileName = Path.Combine(_examplesDirectory.FullName, ExamplesFileName);
            
            if (!File.Exists(fullExamplesFileName))
            {
                Debug.LogException(new Exception("Examples file not found"));
            }

            List<Blueprint> examples = new List<Blueprint>();
            
            try
            {
                string jsonNotation = await File.ReadAllTextAsync(fullExamplesFileName);
                examples = JsonConvert.DeserializeObject<List<Blueprint>>(jsonNotation);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogException(new Exception("examples.json is incorrect or corrupted"));
            }

            return examples;
        }
    }
}