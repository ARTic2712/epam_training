using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextParser.ControlClasses
{
    public class TextReader : Interfaces.IFileReader
    {
        private string _fileName;

        public TextReader(string fileName)
        {
            this._fileName = fileName;
        }
        public ICollection<ModelClasses.TextLine > Read()
        {
            FileStream stream=null;
            StreamReader reader=null;
            try
            {
                 stream = new FileStream(_fileName, FileMode.Open);
                 reader = new StreamReader(stream, Encoding.Default);
                ICollection<ModelClasses.TextLine > result = new List<ModelClasses.TextLine>();
                
                while (!reader.EndOfStream)
                {
                    result.Add( new ModelClasses.TextLine( reader.ReadLine(), new List<Interfaces.ISentenceItem >()));
                }
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (stream !=null)
                {
                    stream.Close();
                    stream.Dispose();
                }
                if (reader  != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
            
        }
    }
}
