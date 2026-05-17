using System;

namespace Компилятор
{
    class Program
    {
        static void Main()
        {
            string filePath = @"C:\Users\andr1\OneDrive\Рабочий стол\ЯП\lab10-13\test1.txt";

            InputOutput.Initialize(filePath);

            while (!InputOutput.EndOfFile)
            {
                InputOutput.NextCh();

                if (InputOutput.PositionNow.LineNumber == 4 &&
                    InputOutput.PositionNow.CharNumber == 3)
                {
                    InputOutput.Error(100, InputOutput.PositionNow);
                }

                if (InputOutput.PositionNow.LineNumber == 7 &&
                    InputOutput.PositionNow.CharNumber == 5)
                {
                    InputOutput.Error(147, InputOutput.PositionNow);
                }

                if (InputOutput.PositionNow.LineNumber == 10 &&
                    InputOutput.PositionNow.CharNumber == 0)
                {
                    InputOutput.Error(203, InputOutput.PositionNow);
                }

                if (InputOutput.PositionNow.LineNumber == 10 &&
                    InputOutput.PositionNow.CharNumber == 4)
                {
                    InputOutput.Error(147, InputOutput.PositionNow);
                }
            }

        }
    }
}