using System;
using System.Collections.Generic;
using Controls;
using Core.Models;
using Core.Services;
using UnityEngine;

namespace Visual
{
    public class ApplicationRoot : MonoBehaviour
    {
        [SerializeField] private Drawer _drawer;
        [SerializeField] private InputManager _inputManager;
        
        private ExamplesDataService _examplesDataService = new ExamplesDataService();

        
        private int _currentExampleIndex = 0;
        
        private async void Awake()
        {
            _examplesDataService = new ExamplesDataService();

            List<Example> examples = await _examplesDataService.ReadExamplesAsync();
            
            _drawer.DrawExample(examples[_currentExampleIndex]);
            
        }
        
        private void OnEnable()
        {
            _inputManager.Resolve += Resolve;
        }

        private void Resolve()
        {
            _drawer.DrawExample(_examplesDataService.Examples[_currentExampleIndex].Resolve(), true);
            // TODO Clear solution before draw new
            
        }
        
        private void OnDisable()
        {
            _inputManager.Resolve -= Resolve;
        }
    }
}