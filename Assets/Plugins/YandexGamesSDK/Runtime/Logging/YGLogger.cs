using System;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Logging
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        None
    }

    public static class YGLogger
    {
        public static LogLevel CurrentLogLevel { get; set; } = LogLevel.Debug;

        public static bool IsLoggingEnabled => YandexGamesSDKConfig.Instance.verboseLogging;

        private static string FormatLogMessage(LogLevel level, string message)
        {
            return $"[YandexGamesSDK] [{DateTime.Now:HH:mm:ss}] [{level}] {message}";
        }

        public static void Debug(string message)
        {
            if (IsLoggingEnabled && CurrentLogLevel <= LogLevel.Debug)
                LogToConsole(LogLevel.Debug, message);
        }

        public static void Info(string message)
        {
            if (IsLoggingEnabled && CurrentLogLevel <= LogLevel.Info)
                LogToConsole(LogLevel.Info, message);
        }

        public static void Warning(string message)
        {
            if (IsLoggingEnabled && CurrentLogLevel <= LogLevel.Warning)
                LogToConsole(LogLevel.Warning, message);
        }

        public static void Error(string message)
        {
            if (IsLoggingEnabled && CurrentLogLevel <= LogLevel.Error)
                LogToConsole(LogLevel.Error, message);
        }

        private static void LogToConsole(LogLevel level, string message)
        {
            string formattedMessage = FormatLogMessage(level, message);

            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    UnityEngine.Debug.Log(formattedMessage);
                    break;
                case LogLevel.Warning:
                    UnityEngine.Debug.LogWarning(formattedMessage);
                    break;
                case LogLevel.Error:
                    UnityEngine.Debug.LogError(formattedMessage);
                    break;
            }
        }
    }
}