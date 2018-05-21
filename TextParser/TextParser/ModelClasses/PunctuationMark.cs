using System.Linq;

namespace TextParser.ModelClasses
{
    public class PunctuationMark : Interfaces.ISentenceItem, Interfaces.IPunctuationMark
    {
        public string Value { get;}

        public bool EndOfSentence
        {
            get
            {
                if (Value != null) return ControlClasses.SymbolsDictionary.SymbolsEndOfSent.Contains(Value);
                else return false;
            }
            set { }
         }

        public PunctuationMark()
        {
            EndOfSentence = false;
        }
        public PunctuationMark(string value)
        {
            Value = value;
            EndOfSentence = ControlClasses.SymbolsDictionary.SymbolsEndOfSent.Contains(value);
        }
    }
}
