using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Enums;
using TextParser.Interfaces;

namespace TextParser.ModelClasses
{
    public class Sentence : Interfaces.ISentence
    {
        public ICollection<ISentenceItem> Items { get; }

        public SentenceType SentType { get; }
    }
}
