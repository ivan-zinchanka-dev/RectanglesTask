using System.Collections.Generic;
using Core.Models;
using Core.Services;
using UnityEngine;

namespace Visual
{
    public class ApplicationRoot : MonoBehaviour
    {
        [SerializeField] private Drawer _drawer;
        
        private ExamplesDataService _examplesDataService = new ExamplesDataService(); 
        
        private async void Awake()
        {
            _examplesDataService = new ExamplesDataService();

            IEnumerable<Example> examples = await _examplesDataService.ReadExamplesAsync();

            foreach (var example in examples)
            {
                _drawer.DrawExample(example);
            }
            
            
            
        }
        
         
    }
}