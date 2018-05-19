using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.ControlClasses
{
    public class SymbolsDictionary
    {
        public static readonly string[] SymbolsInSent = { ",", ";", ":", " ", "-", "\'", "\"", "(", ")" };
        public static readonly string[] SymbolsEndOfSent = { "!", "?", ".", "!?", "..." };
        public static readonly string[] SymbolsVowel = { "a", "e", "i", "o", "y", "u" };
    }
}
