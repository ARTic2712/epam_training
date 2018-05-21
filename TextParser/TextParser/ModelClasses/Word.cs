using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TextParser.Interfaces;

namespace TextParser.ModelClasses
{
    public class Word : ISentenceItem, IWord
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
            numbersOfLines = new List<int>();
        }
        public Word(ICollection<ILetter> letters)
        {
            Letters = letters;
            numbersOfLines = new List<int>();
        }
        public int Length
        {
            get { return Letters.Count(); }
        }
        public ICollection<int> numbersOfLines { get;  }
        public int CountInText {get; set; }
    }

    public class CompareWord  : IEqualityComparer<IWord >
    {
        public bool Equals(IWord x, IWord y)
        {
            if ( (x as Word).Value == (y as Word).Value)
            {
                return true ;
            }
            else
            {
                return false;
            }
        }
        
        int IEqualityComparer<IWord>.GetHashCode(IWord obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
