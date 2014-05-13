using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CarmelClasses
{
    public static class Utility
    {
        public static string GetLettersOnly(string str)
        {
            return new String(str.Where(Char.IsLetter).ToArray());
        }
    }
}
