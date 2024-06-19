using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Services
{
    public class BaseDataService
    {
        private const string BaseFolderName = "Data";
       
        
        
        protected DirectoryInfo BaseDirectoryInfo { get; private set; }

        public BaseDataService()
        {
            CheckDirectory();
        }
        
        private void CheckDirectory()
        {
            string fullPath = Path.Combine(Application.persistentDataPath, BaseFolderName);
            BaseDirectoryInfo = new DirectoryInfo(fullPath);
        
            if (!BaseDirectoryInfo.Exists)
            {
                BaseDirectoryInfo.Create();
            }
        }
        
        private string GetFullFileName(string fileName)
        {
            return Path.Combine(BaseDirectoryInfo.FullName, fileName);
        }

        protected TModel GetData<TModel>(string fileName)
        {
            string fullFileName = GetFullFileName(fileName);
            
            if (!File.Exists(fullFileName))
            {
                return default;
            }

            try
            {
                string jsonNotation = File.ReadAllText(fullFileName);

                if (string.IsNullOrEmpty(jsonNotation) || string.IsNullOrWhiteSpace(jsonNotation))
                {
                    return default;
                }
                
                return JsonConvert.DeserializeObject<TModel>(jsonNotation);

            }
            catch (Exception ex)
            {
                Exception loggedException = 
                    new Exception("[DataService] Deserialization of " + fileName + " is failed. Default model created");
                Debug.LogException(loggedException);

                return default;
            }
        }

        protected void SaveData<TModel>(TModel data, string fileName)
        {
            Task.Run(() => { SaveInternal(data, fileName); });
        }
        
        private void SaveInternal<TModel>(TModel data, string fileName)
        {
            try
            {
                string fullFileName = GetFullFileName(fileName);
                string jsonNotation = JsonConvert.SerializeObject(data);
                
                lock (data)
                {
                    File.WriteAllText(fullFileName, jsonNotation);
                }

            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}