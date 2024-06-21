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
            var files = _examplesDirectory.GetFiles("*.json");

            List<Blueprint> examples = new List<Blueprint>(files.Length);
            
            foreach (var file in files)
            {
                try
                {
                    string jsonNotation = await File.ReadAllTextAsync(file.FullName);
                    examples.Add(JsonConvert.DeserializeObject<Blueprint>(jsonNotation));
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    Debug.LogException(new Exception($"File {file.FullName} is incorrect or corrupted"));
                    
                    examples.Add(new Blueprint(true));
                }
            }
            
            return examples;
        }
    }
}