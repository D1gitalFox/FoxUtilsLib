namespace FoxUtilsLib
{
    namespace Logging
    {
        /// <summary>
        /// Simpliest logger.
        /// </summary>
        public class FLogger
        {
            public string AppName { get; }
            public LogLevel MinimalLogLevel { get; set; }
            public string DateTimeFormat { get; }
            internal static object _MessageLock = new();

            /// <summary>
            /// Initialize a basic logger with given AppName and MinimalLogLevel
            /// </summary>
            /// <param name="appName">App label shown in every log message</param>
            /// <param name="minimalLogLevel">All logs below this level won't be shown</param>
            /// <param name="dateTimeFormat">Date and time format to be shown in logs</param>
            public FLogger(string appName, LogLevel minimalLogLevel = LogLevel.Information, string dateTimeFormat = "dd.MM.yyyy HH:mm:ss.fff")
            {
                if (string.IsNullOrWhiteSpace(appName))
                    throw new ArgumentNullException(nameof(appName));
                DateTime.Now.ToString(dateTimeFormat);
                AppName = appName;
                MinimalLogLevel = minimalLogLevel;
                DateTimeFormat = dateTimeFormat;
            }

            /// <summary>
            /// Send a critical message into console
            /// </summary>
            /// <param name="message">Message to be sent</param>
            /// <param name="ex">Exception to show, if any</param>
            public void Crit(string message, Exception? ex = null)
            {
                lock (_MessageLock)
                {
                    Console.Write($"[{DateTime.Now.ToString(DateTimeFormat)}] [{AppName}] ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"[CRIT ] ");
                    Console.ResetColor();
                    Console.WriteLine($"{message}");
                }
                if (ex is not null)
                {
                    Crit($"{ex.GetType().FullName}: {ex.Message}\n{ex.StackTrace}");
                }
            }

            /// <summary>
            /// Send an error message into console
            /// </summary>
            /// <param name="message">Message to be sent</param>
            /// <param name="ex">Exception to show, if any</param>
            public void Error(string message, Exception? ex = null)
            {
                lock (_MessageLock)
                {
                    Console.Write($"[{DateTime.Now.ToString(DateTimeFormat)}] [{AppName}] ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"[Error] ");
                    Console.ResetColor();
                    Console.WriteLine($"{message}");
                }
                if (ex is not null)
                {
                    Error($"{ex.GetType().FullName}: {ex.Message}\n{ex.StackTrace}");
                }
            }

            /// <summary>
            /// Send a warning message into console
            /// </summary>
            /// <param name="message">Message to be sent</param>
            /// <param name="ex">Exception to show, if any</param>
            public void Warning(string message, Exception? ex = null)
            {
                lock (_MessageLock)
                {
                    Console.Write($"[{DateTime.Now.ToString(DateTimeFormat)}] [{AppName}] ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[Warn ] ");
                    Console.ResetColor();
                    Console.WriteLine($"{message}");
                }
                if (ex is not null)
                {
                    Warning($"{ex.GetType().FullName}: {ex.Message}\n{ex.StackTrace}");
                }
            }

            /// <summary>
            /// Send an informational message into console
            /// </summary>
            /// <param name="message">Message to be sent</param>
            public void Info(string message)
            {
                lock (_MessageLock)
                {
                    Console.Write($"[{DateTime.Now.ToString(DateTimeFormat)}] [{AppName}] ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"[Info ] ");
                    Console.ResetColor();
                    Console.WriteLine($"{message}");
                }
            }

            /// <summary>
            /// Send a debug message into console
            /// </summary>
            /// <param name="message">Message to be sent</param>
            /// <param name="ex">Exception to show, if any</param>
            public void Debug(string message)
            {
                lock (_MessageLock)
                {
                    Console.Write($"[{DateTime.Now.ToString(DateTimeFormat)}] [{AppName}] ");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write($"[Debug] ");
                    Console.ResetColor();
                    Console.WriteLine($"{message}");
                }
            }
        }

        public enum LogLevel
        {
            Critical,
            Error,
            Warning,
            Information,
            Debug,
        }
    }
}
