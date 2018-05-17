using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.ModelClasses
{
    class Letter : Interfaces.ILetter, Interfaces.ISymbol 
    {
        public bool Vowel { get; }
        public char Value { get; }
        public Letter()
        {
            Vowel = false;
        }
        public Letter (char value)
        {
            this.Value = value;
            this.Vowel = ControlClasses.SymbolsDictionary.SymbolsVowel.Contains(value.ToString());
        }
    }
}
