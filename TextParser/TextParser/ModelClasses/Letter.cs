using System.Linq;

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
            Value = value;
            Vowel = ControlClasses.SymbolsDictionary.SymbolsVowel.Contains(value.ToString());
        }
    }
}
