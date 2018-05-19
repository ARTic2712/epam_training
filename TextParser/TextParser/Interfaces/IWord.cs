using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.Interfaces
{
    public interface IWord
    {
        ICollection<ILetter> Letters { get;}
        int Length { get; }
    }
}
