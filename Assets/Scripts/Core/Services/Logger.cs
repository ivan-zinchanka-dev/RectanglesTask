using System;
using System.IO;
using UnityEngine;

namespace Core.Services
{
    public class Logger : IDisposable
    {
        private readonly string _fullFileName;
        private readonly object _threadLock;
        
        public Logger(string fullFileName)
        {
            _fullFileName = fullFileName;
            _threadLock = new object();
            Application.logMessageReceived += OnLogMessageReceived;
        }

        private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
        {
            lock (_threadLock)
            {
                if (!File.Exists(_fullFileName))
                {
                    File.Create(_fullFileName);
                }
                
                File.AppendAllTextAsync(_fullFileName,$"[{DateTime.Now}] {GetLogPrefix(type)}: {condition}\n");
            }
        }

        private static string GetLogPrefix(LogType logType)
        {
            switch (logType)
            {
                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    return "Error";
                
                case LogType.Warning:
                    return "Warning";
                
                case LogType.Log:
                default:
                    return "Information";
            }
        }

        public void Dispose()
        {
            Application.logMessageReceived -= OnLogMessageReceived;
        }
    }
}