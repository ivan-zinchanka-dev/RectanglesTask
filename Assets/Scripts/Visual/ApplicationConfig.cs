using UnityEngine;

namespace Visual
{
    [CreateAssetMenu(fileName = "application_config", menuName = "Configs/ApplicationConfig", order = 0)]
    public class ApplicationConfig : ScriptableObject
    {
        [field: SerializeField] 
        public string LogFilePath { get; private set; }
        [field: SerializeField] 
        public string ExamplesFolderPath { get; private set; }
    }
}