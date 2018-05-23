using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TextParser.ControlClasses
{
    public class Serializator
    {
        public static void SerializeConcordance(List<ModelClasses. Word > words) 
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ModelClasses.Word>));
            using (FileStream fs = new FileStream(Properties.Settings.Default.pathConcordanceSerialize, FileMode.OpenOrCreate))
            {
                try
                {
                    serializer.Serialize(fs, words);
                    Console.WriteLine("Object serializated");
                }
                catch
                {
                    throw new Exception("Error serialization");
                }
            }
        }
        public static List<ModelClasses.Word > DeserializeConcordance()
        {
            using (FileStream fs = new FileStream(Properties.Settings.Default.pathConcordanceSerialize, FileMode.OpenOrCreate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof (List<ModelClasses.Word>));
                try
                {
                    List<ModelClasses.Word> concordance = (List<ModelClasses.Word>) serializer.Deserialize(fs);
                    Console.WriteLine("Object deserializated");
                    return concordance;
                }
                catch
                {
                    throw new Exception("Error deserialization");
                }
            }
        }
        //[Serializable ]
        //public class SerizClass
        //{
        //    [XmlArray("Collection"), XmlArrayItem("Item")]
        //    public List<ModelClasses.Word> Collection { get; set; }
        //    public SerizClass()
        //    { }
        //    public SerizClass(List<ModelClasses.Word> word)
        //    {
        //        Collection = word;
        //    }
        //}
    }
}
