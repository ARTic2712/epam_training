using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
