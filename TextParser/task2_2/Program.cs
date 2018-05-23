using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Interfaces;
using TextParser.ControlClasses;
using TextParser.ModelClasses;
using System.Xml.Serialization;

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
            
            List<Word> wordsInText =(TextLine.Parse(textLines));
            Console.WriteLine( Parser.GetCorcodance(wordsInText));
            try
            {
                Serializator.SerializeConcordance(wordsInText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            wordsInText.Clear();
            try
            {
                wordsInText = Serializator.DeserializeConcordance();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
