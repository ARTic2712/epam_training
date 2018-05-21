using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Enums;
using TextParser.Interfaces;

namespace TextParser.ModelClasses
{
    public class Sentence : Interfaces.ISentence
    {
        public ICollection<ISentenceItem> Items { get; }

        public SentenceType SentType { get; set; }
        public Sentence()
        {
            Items = new List<ISentenceItem>() ;
        }
        public Sentence(ICollection<ISentenceItem > items)
        {
            Items = items;
        }
        public Sentence(ICollection<ISentenceItem> items, SentenceType sentType)
        {
            Items = items;
            SentType = sentType;
        }
        public override string ToString()
        {
            StringBuilder resultStr = new StringBuilder();
            if(Items.Count>0)
            {
                foreach(Interfaces.ISentenceItem item in Items )
                {
                    resultStr.Append(item.Value);
                }
                return resultStr.ToString();
            }
            return string.Empty;
        }
        public int CountWords
        {
            get
            {
                int result = 0;
                foreach (var item in Items )
                {
                    if (item is IWord) result++;
                }
                return result;
            }
        }
        public void WriteWords()
        {
            foreach(var item in Items )
            {
                if(item is IWord )
                {
                    Console.WriteLine(item.Value.ToString());
                }
            }
        }
        public void WriteWords(int length)
        {
            var words = from el in Items where (el is IWord) && ((el as IWord).Length == length) group (IWord)el by el.Value;  //select (IWord) el;
            foreach (var item in words )
            {
                    Console.WriteLine((item.ElementAt(0) as Word).Value.ToString() );
            }
        }
        public void DeleteOnConsonant()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if(Items.ElementAt(i) is IWord)
                {
                    Word word = Items.ElementAt(i) as Word;
                    if (!word.Letters.ElementAt(0).Vowel )
                    {
                        Items.Remove(word);
                    }
                }
            }
        }
        public void DeleteOnConsonant(int length)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items.ElementAt(i) is IWord)
                {
                    Word word = Items.ElementAt(i) as Word;
                    if (!word.Letters.ElementAt(0).Vowel && word.Length==length )
                    {
                        Items.Remove(word);
                    }
                }
            }
        }
        public void Replace(string substring, int length)
        {
            TextLine lineToInsert = new TextLine(substring, new List<ISentenceItem>());
           for(int i =0; i<Items.Count; i++)
            {
                if(Items.ElementAt(i) is IWord && (Items.ElementAt(i) as IWord).Length ==length )
                {
                    (Items as List<ISentenceItem>).RemoveAt(i);
                    (Items as List<ISentenceItem>).InsertRange(i, lineToInsert.Items);
                    i += lineToInsert.Items.Count;
                }
            }
        }
    }
}
