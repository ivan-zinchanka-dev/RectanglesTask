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

        private int _currentExampleIndex = 0;
        
        private async void Awake()
        {
            _examplesDataService = new ExamplesDataService();

            List<Example> examples = await _examplesDataService.ReadExamplesAsync();
            
            _drawer.DrawExample(examples[_currentExampleIndex]);
            _drawer.DrawExample(examples[_currentExampleIndex].Resolve(), true);
        }
        
         
    }
}