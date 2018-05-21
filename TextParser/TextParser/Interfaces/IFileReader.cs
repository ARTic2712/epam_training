using System.Collections.Generic;

namespace TextParser.Interfaces
{
    public interface IFileReader
    {
        ICollection<ModelClasses.TextLine> Read();
    }
}
