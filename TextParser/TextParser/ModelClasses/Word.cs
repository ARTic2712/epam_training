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
        public string Value
        {
            get
            {
                return new string((from el in Letters select (el as Letter).Value).ToArray());
            }
        }

        public ICollection<ILetter> Letters { get; }
        public Word()
        {
            Letters = new List<ILetter>();
        }
        public Word(ICollection<ILetter > letters)
        {
            this.Letters = letters;
        }
        public int Length
        {
            get { return this.Letters.Count(); }
        }
    }
}
