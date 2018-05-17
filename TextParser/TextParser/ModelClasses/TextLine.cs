using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextParser.ModelClasses
{
    public class TextLine
    {
        public string Value { get; }
        public ICollection<Interfaces.ISentenceItem> Items { get; }
        public TextLine()
        {
            this.Value = String.Empty;
            this.Items = new List<Interfaces.ISentenceItem>();
        }
        public TextLine(string value, ICollection<Interfaces.ISentenceItem> items)
        {
            this.Value = value;
            this.Items = items;
            this.Items = Parse();
        }
        private ICollection<Interfaces.ISentenceItem> Parse()
        {
            ICollection<Interfaces.ISentenceItem> result=new List<Interfaces.ISentenceItem>();

            Regex regex = new Regex(@"\s+");
            string valuefixed = regex.Replace(Value, " ");

            for (int i=0;i< valuefixed.Length;i++)
            {

            }
            return result;
        }
    }
}
