using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextParser.ModelClasses
{
    public class TextLine
    {
        public string Value { get; }
        public ICollection<Interfaces.ISentenceItem> Items { get; }
        
        public TextLine()
        {
            Value = String.Empty;
            Items = new List<Interfaces.ISentenceItem>();
        }
        public TextLine(string value, ICollection<Interfaces.ISentenceItem> items)
        {
            Value = value;
            Items = items;
            Parse();
        }
        private void Parse()
        {
          //  ICollection<Interfaces.ISentenceItem> result=new List<Interfaces.ISentenceItem>();

            Regex regex = new Regex(@"\s+");
            string valuefixed = regex.Replace(Value, " ");
            Word word = new Word();
            for (int i=0;i< valuefixed.Length;i++)
            {
                if (ControlClasses.SymbolsDictionary.SymbolsInSent.Contains(valuefixed[i].ToString() ))
                {
                    AddWord(word);
                    word = new Word();
                    Items.Add(new PunctuationMark(valuefixed[i].ToString()));
                    continue;
                }
                if (ControlClasses.SymbolsDictionary.SymbolsEndOfSent.Contains(valuefixed[i].ToString()))
                {
                    if (i < valuefixed.Length - 1)
                    {
                        if (ControlClasses.SymbolsDictionary.SymbolsEndOfSent.Contains(valuefixed[i + 1].ToString()))
                        {
                            if (i < valuefixed.Length - 2)
                            {
                                if (ControlClasses.SymbolsDictionary.SymbolsEndOfSent.Contains(valuefixed[i + 2].ToString()))
                                {
                                    AddWord(word);
                                    word = new Word();
                                    Items.Add(new PunctuationMark(valuefixed[i].ToString() + valuefixed[i + 1].ToString() + valuefixed[i + 2].ToString()));
                                    i += 2;
                                }
                                else
                                {
                                    AddWord(word);
                                    word = new Word();
                                    Items.Add(new PunctuationMark(valuefixed[i].ToString() + valuefixed[i + 1].ToString()));
                                    i++;
                                }
                            }
                            else
                            {
                                AddWord(word);
                                word = new Word();
                                Items.Add(new PunctuationMark(valuefixed[i].ToString() + valuefixed[i + 1].ToString()));
                                i++;
                            }
                        }
                        else
                        {
                            AddWord(word);
                            word = new Word();
                            Items.Add(new PunctuationMark(valuefixed[i].ToString()));
                        }
                    }
                    else
                    {
                        AddWord(word);
                        word = new Word();
                        Items.Add(new PunctuationMark(valuefixed[i].ToString()));
                    }
                }
                else
                {
                    word.Letters.Add(new Letter(valuefixed[i]));
                    if(i== valuefixed.Length -1) AddWord(word);

                }
            }
           // return result;
        }
        public static  List<Interfaces.IWord > Parse(List<TextLine> textLines)
        {
            List<Interfaces.IWord> wordsInText = new List<Interfaces.IWord>();
            for (int i = 0; i < textLines.Count; i++)
            {
                foreach (var item in textLines.ElementAt(i).Items)
                {
                    if (item is Interfaces.IWord)
                    {
                        if (!wordsInText.Contains((item as Interfaces.IWord), new EqualsWord()))
                        {
                            (item as Word).numbersOfLines.Add(i + 1);
                            (item as Word).CountInText++;
                            wordsInText.Add(item as Word);
                        }
                        else
                        {
                            Interfaces.IWord word = wordsInText.Find(x => (x as Word).Value.ToUpper() == (item as Word).Value.ToUpper());
                            if (!(word as Word).numbersOfLines.Contains(i+1)) (word as Word).numbersOfLines.Add(i+1);
                            (word as Word).CountInText++;
                        }
                    }
                }
            }
            return wordsInText;
        }
            public void AddWord(Interfaces.IWord word)
        {
            if (word.Length > 0) Items.Add((word as Word));

        }
    }
}
