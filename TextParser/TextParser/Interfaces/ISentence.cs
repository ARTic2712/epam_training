using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.Interfaces
{
    interface ISentence
    {
        ICollection<ISentenceItem > Items { get; }
        Enums.SentenceType SentType { get; }
    }
}
