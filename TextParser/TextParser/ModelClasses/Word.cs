using System;
using System.Collections.Generic;
using System.Linq;
using TextParser.Interfaces;

namespace TextParser.ModelClasses
{
    [Serializable ]
    public class Word : ISentenceItem, IWord
    {
        public string Value
        {
            get
            {
                return new string((from el in Letters select (el as Letter).Value).ToArray());
            }
            set { }
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
        
        public List<int> numbersOfLines { get; set; }
        
        public int CountInText {get; set; }
    }

    public class EqualsWord  : IEqualityComparer<IWord >
    {
        public bool Equals(IWord x, IWord y)
        {
            if ( (x as Word).Value.ToUpper()   == (y as Word).Value.ToUpper())
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
    public class CompareWord : Comparer<IWord>
    {
        public override int Compare(IWord x, IWord y)
        {
            return String.Compare((x as Word).Value, (y as Word).Value);
        }
    }
}
