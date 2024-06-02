using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ProxyPatternExample
{
    // Клас SmartTextReader, який читає файл і перетворює його на двомірний масив
    public class SmartTextReader
    {
        private readonly string _filePath;

        public SmartTextReader(string filePath)
        {
            _filePath = filePath;
        }

        public virtual char[][] ReadFile()
        {
            string[] lines = File.ReadAllLines(_filePath);
            char[][] result = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }
            return result;
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }

    // Проксі з логуванням SmartTextChecker
    public class SmartTextChecker : SmartTextReader
    {
        private readonly SmartTextReader _reader;

        public SmartTextChecker(SmartTextReader reader) : base(reader.GetFilePath())
        {
            _reader = reader;
        }

        public override char[][] ReadFile()
        {
            Console.WriteLine("Opening file...");
            char[][] content = _reader.ReadFile();
            Console.WriteLine("File read successfully!");
            Console.WriteLine($"Total lines: {content.Length}");
            int totalChars = 0;
            foreach (var line in content)
            {
                totalChars += line.Length;
            }
            Console.WriteLine($"Total characters: {totalChars}");
            Console.WriteLine("Closing file...");
            return content;
        }
    }

    // Проксі з обмеженням доступу SmartTextReaderLocker
    public class SmartTextReaderLocker : SmartTextReader
    {
        private readonly SmartTextReader _reader;
        private readonly Regex _regex;

        public SmartTextReaderLocker(SmartTextReader reader, string pattern) : base(reader.GetFilePath())
        {
            _reader = reader;
            _regex = new Regex(pattern, RegexOptions.IgnoreCase);
        }

        public override char[][] ReadFile()
        {
            if (_regex.IsMatch(_reader.GetFilePath()))
            {
                Console.WriteLine("Access denied!");
                return null;
            }
            return _reader.ReadFile();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "sample.txt";
            // Створення реального читача файлів
            SmartTextReader reader = new SmartTextReader(filePath);

            // Створення проксі з логуванням
            SmartTextChecker checker = new SmartTextChecker(reader);
            checker.ReadFile();

            // Створення проксі з обмеженням доступу
            string restrictedPattern = @"^restricted.*";
            SmartTextReaderLocker locker = new SmartTextReaderLocker(reader, restrictedPattern);

            Console.WriteLine("\nTrying to read a non-restricted file:");
            locker.ReadFile();

            // Зміна шляху файлу для перевірки обмеження доступу
            SmartTextReader restrictedReader = new SmartTextReader("restricted_sample.txt");
            SmartTextReaderLocker restrictedLocker = new SmartTextReaderLocker(restrictedReader, restrictedPattern);

            Console.WriteLine("\nTrying to read a restricted file:");
            restrictedLocker.ReadFile();
        }
    }
}
