using System;
using Core.Services;
using UnityEngine;

namespace Visual
{
    public class SceneRoot : MonoBehaviour
    {
        private ExamplesDataService _examplesDataService = new ExamplesDataService(); 
        
        private void Awake()
        {
            _examplesDataService = new ExamplesDataService();
        }
        
         
    }
}