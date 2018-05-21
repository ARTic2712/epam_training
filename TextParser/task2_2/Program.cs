using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Interfaces;
using TextParser.ControlClasses;
using TextParser.ModelClasses;

namespace task2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileReader textReader = new TextReader(Properties.Settings.Default.FilePath);
            List<TextLine> textLines = (List<TextLine>)textReader.Read();
            if (textLines == null)
            {
                return;
            }
            List<IWord> wordsInText = TextLine.Parse(textLines);
            
        }
    }
}
