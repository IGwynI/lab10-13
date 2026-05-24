using System.Collections.Generic;

namespace Компилятор
{
    static class ErrorTable
    {
        static readonly Dictionary<byte, string> errors = new Dictionary<byte, string>
        {
            { 1, "недопустимый символ" },
            { 203, "целая константа превышает допустимый диапазон" }
        };

        public static string GetDescription(byte code)
        {
            return errors.TryGetValue(code, out string desc) ? desc : null;
        }
    }
}