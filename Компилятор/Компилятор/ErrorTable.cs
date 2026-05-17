using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Компилятор
{
    static class ErrorTable
    {
        static readonly Dictionary<byte, string> errors = new Dictionary<byte, string>
        {
            { 100, "использование имени не соответствует описанию" },
            { 147, "тип метки не совпадает с типом выбирающего выражения" },
            { 203, "целая константа превышает допустимый диапазон" }
        };

        public static string GetDescription(byte code)
        {
            return errors.TryGetValue(code, out string desc) ? desc : null;
        }
    }
}
