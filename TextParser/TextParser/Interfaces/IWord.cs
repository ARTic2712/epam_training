using System.Collections.Generic;

namespace TextParser.Interfaces
{
    public interface IWord
    {
        ICollection<ILetter> Letters { get;}
        int Length { get; }
    }
}
