using System;
using System.Collections.Generic;
using System.Linq;
using TextParser.Interfaces;
using TextParser.ControlClasses;
using TextParser.ModelClasses;


namespace task2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileReader textReader = new TextReader(Properties.Settings.Default.FilePath);
            List<TextLine > textLines =(List<TextLine>) textReader.Read();
            if (textLines==null)
            {
                return;
            }
            List<ISentence> sentences = (List<ISentence>)Parser.LinesToSentence(textLines);

            //// 1) Sort by count words
            IEnumerable <ISentence> sentSorted = from el in sentences orderby (el as Sentence).CountWords  select el;
            foreach (ISentence sent in sentSorted)
            {
                Console.WriteLine((sent as Sentence).CountWords );
                if(sent.SentType== TextParser.Enums.SentenceType.Interrogative)
                {
                    Console.WriteLine("----------");
                    ///// 2) write words with 5 letters wothout repeat
                    (sent as Sentence).WriteWords(5);
                    Console.WriteLine("----------");
                }
                /// 3) delete words with 3 letters thats begin with consonant
                (sent as Sentence).DeleteOnConsonant(3);

                /// 4) replace words with 53 letters by a substring
                (sent as Sentence).Replace(" REPLACED WORDS WITH 5 LETTERS ", 5);
                Console.WriteLine(sent.ToString());

            }

        }
    }
}
