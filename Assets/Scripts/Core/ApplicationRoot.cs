using System;
using System.Collections.Generic;
using Controls;
using Models;
using Services;
using Services.Render;
using TMPro;
using UnityEngine;
using Logger = Services.Logger;

namespace Core
{
    public class ApplicationRoot : MonoBehaviour
    {
        [SerializeField] 
        private ApplicationConfig _appConfig;
        [SerializeField] 
        private InputManager _inputManager;
        [SerializeField] 
        private Drawer _drawer;
        [SerializeField] 
        private TextMeshProUGUI _exampleTextMesh;
        
        private ExamplesReader _examplesReader;
        private Logger _logger;
        private List<Blueprint> _blueprints;
        private int _currentBlueprintIndex = 0;
        
        private async void Awake()
        {
            _logger = new Logger(_appConfig.LogFilePath);
            Debug.Log("Application started");
            
            _examplesReader = new ExamplesReader(_appConfig.ExamplesFolderPath);
            _blueprints = await _examplesReader.ReadExamplesAsync();
            
            Debug.Log("Drawing of the example started");
            
            _exampleTextMesh.SetText($"Example: {_currentBlueprintIndex}");
            _drawer.DrawBlueprint(_blueprints[_currentBlueprintIndex]);
            
            Debug.Log("Drawing of the example completed");
        }
        
        private void OnEnable()
        {
            _inputManager.Resolve += Resolve;
            _inputManager.SwitchBlueprint += SwitchBlueprint;
        }

        private void Resolve()
        {
            Blueprint solution = _blueprints[_currentBlueprintIndex].Resolve();
            
            Debug.Log("Drawing of the solution started");
            
            _drawer.ClearAreaFor(solution.Type);
            _drawer.DrawBlueprint(solution);
            
            Debug.Log("Drawing of the solution completed");
        }

        private void SwitchBlueprint(int direction)
        {
            Debug.Log("Switching of example started");
            
            int previousBlueprintIndex = _currentBlueprintIndex; 
            _currentBlueprintIndex = Math.Clamp(_currentBlueprintIndex + direction, 0, _blueprints.Count - 1);

            if (_currentBlueprintIndex != previousBlueprintIndex)
            {
                Debug.Log("Drawing of the example started");
                
                _exampleTextMesh.SetText($"Example: {_currentBlueprintIndex}");
                _drawer.ClearAllAreas();
                _drawer.DrawBlueprint(_blueprints[_currentBlueprintIndex]);
                
                Debug.Log("Drawing of the example completed");
            }
            
            Debug.Log("Switching of example completed");
        }

        private void OnDisable()
        {
            _inputManager.Resolve -= Resolve;
            _inputManager.SwitchBlueprint -= SwitchBlueprint;
        }

        private void OnDestroy()
        {
            Debug.Log("Application is shutting down");
            _logger.Dispose();
        }
    }
}