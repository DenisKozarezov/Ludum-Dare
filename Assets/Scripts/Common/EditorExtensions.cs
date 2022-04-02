using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class EditorExtensions
    {
        private static Dictionary<LogType, string> LogPrefix = new Dictionary<LogType, string>
        {
            { LogType.Default, "<b><color=white>[LOG]:</color></b> " },
            { LogType.Warning, "<b><color=yellow>[WARNING]:</color></b> " },
            { LogType.Critical, "<b><color=red>[CRITICAL]:</color></b> " },
            { LogType.Assert, "<b><color=white>[ASSERT]:</color></b> " },
        };

        public enum LogType : byte
        {
            Default = 0,
            Warning = 1,
            Critical = 2,
            Assert = 4,
        }

        public static void Log(string message, LogType type = LogType.Default)
        {
            if (string.IsNullOrEmpty(message)) return;
            Debug.Log(LogPrefix[type] + message);
        }
    }
}