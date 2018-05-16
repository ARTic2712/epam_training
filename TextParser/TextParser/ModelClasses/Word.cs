using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Interfaces;

namespace TextParser.ModelClasses
{
    public class Word : Interfaces.ISentenceItem, Interfaces.IWord
    {
        public string Value { get { return Letters.ToString(); } }

        public ICollection<ILetter> Letters { get; }
    }
}
