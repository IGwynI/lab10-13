using System;
using System.Collections.Generic;
using System.IO;

namespace Компилятор
{
    class Program
    {
        static void Main()
        {
            string filePath = @"C:\Users\andr1\OneDrive\Рабочий стол\ЯП\lab10-13\test4.txt";
            string outputPath = @"C:\Users\andr1\OneDrive\Рабочий стол\ЯП\lab10-13\codes.txt";

            InputOutput.Initialize(filePath);
            if (InputOutput.EndOfFile)
            {
                Console.WriteLine("Файл не найден или пуст.");
                return;
            }

            LexicalAnalyzer lex = new LexicalAnalyzer();
            List<byte> tokens = new List<byte>();

            byte code;

            while (!InputOutput.EndOfFile)
            {
                code = lex.NextSym();
                if (code != 0)
                {
                    tokens.Add(code);
                }
            }

            File.WriteAllText(outputPath, string.Join(" ", tokens));
        }
    }
}