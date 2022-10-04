namespace FoxUtilsLib
{
    namespace Logging
    {
        /// <summary>
        /// Базовый логгер для вывода в консоль и файл
        /// </summary>
        public class FLogger : IEquatable<FLogger?>
        {
            /// <summary>
            /// Имя приложения, используемого для вывода в логах
            /// </summary>
            public string AppName { get; }
            /// <summary>
            /// Минимальный уровень вывода логов
            /// </summary>
            public LogLevel MinimalLogLevel { get; set; }
            /// <summary>
            /// Строка формата для даты и времени.
            /// </summary>
            public string DateTimeFormat { get; }
            internal static object _MessageLock = new();

            /// <summary>
            /// Инициализирование стандартного логгера с определённым именем приложения
            /// </summary>
            /// <param name="appName">Имя приложения, используемого для вывода в лог</param>
            /// <param name="minimalLogLevel">Минимальный уровень логов для отображения</param>
            /// <param name="dateTimeFormat">Строка формата для даты и времени</param>
            public FLogger(string appName, LogLevel minimalLogLevel = LogLevel.Information, string dateTimeFormat = "dd.MM.yyyy HH:mm:ss.fff")
            {
                if (string.IsNullOrWhiteSpace(appName))
                    throw new ArgumentNullException(nameof(appName));
                if(string.IsNullOrWhiteSpace(dateTimeFormat))
                    throw new ArgumentNullException(nameof(dateTimeFormat));
                DateTime.Now.ToString(dateTimeFormat);
                AppName = appName;
                MinimalLogLevel = minimalLogLevel;
                DateTimeFormat = dateTimeFormat;
            }

            /// <summary>
            /// Ототбразить критическое сообщение в консоли
            /// </summary>
            /// <param name="message">Текст лога</param>
            /// <param name="ex">Исключение для отображения в консоли, если имеется</param>
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
            /// Ототбразить сообщение об ошибке в консоли
            /// </summary>
            /// <param name="message">Текст лога</param>
            /// <param name="ex">Исключение для отображения в консоли, если имеется</param>
            public void Error(string message, Exception? ex = null)
            {
                if (MinimalLogLevel < LogLevel.Error)
                    return;
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
            /// Ототбразить сообщение с предупреждением в консоли
            /// </summary>
            /// <param name="message">Текст лога</param>
            /// <param name="ex">Исключение для отображения в консоли, если имеется</param>
            public void Warning(string message, Exception? ex = null)
            {
                if (MinimalLogLevel < LogLevel.Warning)
                    return;
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
            /// Отправить информационное сообщение в консоль
            /// </summary>
            /// <param name="message">Текст лога</param>
            public void Info(string message)
            {
                if (MinimalLogLevel < LogLevel.Information)
                    return;
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
            /// Отправить отладочное сообщение в консоль
            /// </summary>
            /// <param name="message">Текст лога</param>
            public void Debug(string message)
            {
                if (MinimalLogLevel < LogLevel.Debug)
                    return;
                lock (_MessageLock)
                {
                    Console.Write($"[{DateTime.Now.ToString(DateTimeFormat)}] [{AppName}] ");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write($"[Debug] ");
                    Console.ResetColor();
                    Console.WriteLine($"{message}");
                }
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as FLogger);
            }

            public bool Equals(FLogger? other)
            {
                return other is not null &&
                       AppName == other.AppName &&
                       MinimalLogLevel == other.MinimalLogLevel &&
                       DateTimeFormat == other.DateTimeFormat;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(AppName, MinimalLogLevel, DateTimeFormat);
            }
        }

        /// <summary>
        /// Уровни логов
        /// </summary>
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
