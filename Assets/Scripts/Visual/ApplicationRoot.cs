using System;
using System.Collections.Generic;
using Controls;
using Core.Models;
using Core.Services;
using TMPro;
using UnityEngine;
using Logger = Core.Services.Logger;

namespace Visual
{
    public class ApplicationRoot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _exampleTextMesh;
        [SerializeField] private Drawer _drawer;
        [SerializeField] private InputManager _inputManager;
        
        private ExamplesReader _examplesReader;
        private Logger _logger;
        private List<Blueprint> _blueprints;
        private int _currentBlueprintIndex = 0;
        
        private async void Awake()
        {
            _logger = new Logger(@".\Work\app.log");
            _examplesReader = new ExamplesReader();
            _blueprints = await _examplesReader.ReadExamplesAsync();
            
            _exampleTextMesh.SetText($"Example: {_currentBlueprintIndex}");
            _drawer.DrawBlueprint(_blueprints[_currentBlueprintIndex]);
        }
        
        private void OnEnable()
        {
            _inputManager.Resolve += Resolve;
            _inputManager.SwitchBlueprint += SwitchBlueprint;
        }

        private void Resolve()
        {
            Blueprint solution = _blueprints[_currentBlueprintIndex].Resolve();
            _drawer.ClearAreaFor(solution.Type);
            _drawer.DrawBlueprint(solution);
        }

        private void SwitchBlueprint(int direction)
        {
            int previousBlueprintIndex = _currentBlueprintIndex; 
            _currentBlueprintIndex = Math.Clamp(_currentBlueprintIndex + direction, 0, _blueprints.Count - 1);

            if (_currentBlueprintIndex != previousBlueprintIndex)
            {
                _exampleTextMesh.SetText($"Example: {_currentBlueprintIndex}");
                _drawer.ClearAllAreas();
                _drawer.DrawBlueprint(_blueprints[_currentBlueprintIndex]);
            }
        }

        private void OnDisable()
        {
            _inputManager.Resolve -= Resolve;
            _inputManager.SwitchBlueprint -= SwitchBlueprint;
        }

        private void OnDestroy()
        {
            _logger.Dispose();
        }
    }
}