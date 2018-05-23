using System;
using System.Collections.Generic;
using System.Text;
using TextParser.Interfaces;
using TextParser.ModelClasses;

namespace TextParser.ControlClasses
{
    public class Parser
    {
        public static ICollection<Interfaces.ISentence> LinesToSentence(ICollection<ModelClasses.TextLine> lines)
        {
            if (lines ==null)
            {
                throw new Exception("lines are Empty");
            }
            ICollection<Interfaces.ISentence> resultSentences = new List<Interfaces.ISentence>();
            Interfaces.ISentence sentence = new ModelClasses.Sentence();
            foreach (ModelClasses.TextLine line in lines)
            {
                foreach (Interfaces.ISentenceItem item in line.Items )
                {
                    sentence.Items.Add(item);
                    if (item is Interfaces.IPunctuationMark)
                    {
                        if((item as ModelClasses.PunctuationMark).EndOfSentence)
                        {
                            switch((item as ModelClasses.PunctuationMark).Value)
                            {
                                case "!": { sentence.SentType = Enums.SentenceType.Exclamation; break; }
                                case "?": { sentence.SentType = Enums.SentenceType.Interrogative; break; }
                                case ".": { sentence.SentType = Enums.SentenceType.Narrative; break; }
                                case "...": { sentence.SentType = Enums.SentenceType.Unfinished ; break; }
                                case "!?": { sentence.SentType = Enums.SentenceType.Narrative; break; }

                            }
                            resultSentences.Add(sentence);
                            sentence = new ModelClasses.Sentence();
                        }
                    }
                }
            }
            return resultSentences;
        }
        public static string GetCorcodance(List<Word> wordsInText)
        {
            StringBuilder resultStr = new StringBuilder();
            wordsInText.Sort(new ModelClasses.CompareWord());
            Char currentLetter= new char();
            foreach(IWord word in wordsInText )
            {
                if (currentLetter.Equals('\0'))
                {
                    currentLetter = Char.ToUpper((((word as Word).Letters as List<ILetter>)[0] as Letter).Value);
                    resultStr.Append(currentLetter.ToString().ToUpper());
                    resultStr.Append(Environment.NewLine);
                }
                if(!currentLetter.Equals(Char.ToUpper( (((word as Word).Letters as List<ILetter>)[0] as Letter).Value)))
                {
                    currentLetter = Char.ToUpper( (((word as Word).Letters as List<ILetter>)[0] as Letter).Value);
                    resultStr.Append(currentLetter.ToString().ToUpper());
                    resultStr.Append(Environment.NewLine);
                }
                ((word as Word).numbersOfLines as List<int>).Sort();
                resultStr.AppendFormat("{0}...............................{1}: {2}{3}", (word as Word).Value, (word as Word).CountInText, string.Join(" ", (((word as Word).numbersOfLines as List<int>)).ToArray()),Environment.NewLine );

            }
            return resultStr.ToString();
        }
    }
}
