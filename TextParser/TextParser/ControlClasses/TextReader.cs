using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TextParser.ControlClasses
{
    public class TextReader : Interfaces.IFileReader
    {
        private string _fileName;

        public TextReader(string fileName)
        {
            _fileName = fileName;
        }
        public ICollection<ModelClasses.TextLine > Read()
        {
            FileStream stream=null;
            StreamReader reader=null;
            ICollection<ModelClasses.TextLine> result = new List<ModelClasses.TextLine>();

            try
            {
                 stream = new FileStream(_fileName, FileMode.Open);
                 reader = new StreamReader(stream, Encoding.Default);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
             
            }
            while (!reader.EndOfStream)
            {
                    result.Add( new ModelClasses.TextLine( reader.ReadLine(), new List<Interfaces.ISentenceItem >()));
            }
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            return result;
           
            
        }
    }
}
