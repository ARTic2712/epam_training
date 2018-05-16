using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.ModelClasses
{
    public class PunctuationMark : Interfaces.ISentenceItem, Interfaces.IPunctuationMark
    {
        public string Value { get;}

        public bool EndOfSentence {get;}
    }
}
