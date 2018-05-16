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
    }
}
