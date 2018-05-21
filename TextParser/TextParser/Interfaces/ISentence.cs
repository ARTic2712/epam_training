using System.Collections.Generic;

namespace TextParser.Interfaces
{
    public interface ISentence
    {
        ICollection<ISentenceItem > Items { get; }
        Enums.SentenceType SentType { get; set; }
    }
}
