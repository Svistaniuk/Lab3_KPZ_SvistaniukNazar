using System;
using System.IO;

namespace LoggerAdapterExample
{
    // Клас Logger з методами Log(), Error(), Warn()
    public class Logger
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LOG: " + message);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + message);
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("WARN: " + message);
            Console.ResetColor();
        }
    }

    // Клас FileWriter з методами Write(), WriteLine()
    public class FileWriter
    {
        private readonly string _filePath;

        public FileWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string message)
        {
            File.AppendAllText(_filePath, message);
        }

        public void WriteLine(string message)
        {
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }
    }

    // Інтерфейс IFileLogger
    public interface IFileLogger
    {
        void Log(string message);
        void Error(string message);
        void Warn(string message);
    }

    // Адаптер FileLoggerAdapter
    public class FileLoggerAdapter : IFileLogger
    {
        private readonly FileWriter _fileWriter;

        public FileLoggerAdapter(FileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        public void Log(string message)
        {
            _fileWriter.WriteLine("LOG: " + message);
        }

        public void Error(string message)
        {
            _fileWriter.WriteLine("ERROR: " + message);
        }

        public void Warn(string message)
        {
            _fileWriter.WriteLine("WARN: " + message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення об'єкту Logger
            var logger = new Logger();
            logger.Log("This is a log message.");
            logger.Error("This is an error message.");
            logger.Warn("This is a warning message.");

            // Створення об'єкту FileWriter
            string filePath = "log.txt";
            var fileWriter = new FileWriter(filePath);

            // Створення об'єкту FileLoggerAdapter
            IFileLogger fileLogger = new FileLoggerAdapter(fileWriter);
            fileLogger.Log("This is a log message.");
            fileLogger.Error("This is an error message.");
            fileLogger.Warn("This is a warning message.");

            Console.WriteLine("Messages logged to console and file.");
        }
    }
}
