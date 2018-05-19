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

        public SentenceType SentType { get; set; }
        public Sentence()
        {
            Items = new List<ISentenceItem>() ;
        }
        public Sentence(ICollection<ISentenceItem > items)
        {
            this.Items = items;
        }
        public Sentence(ICollection<ISentenceItem> items, SentenceType sentType)
        {
            this.Items = items;
            this.SentType = sentType;
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
    }
}
