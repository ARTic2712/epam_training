using System;
using System.Collections.Generic;

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
    }
}
