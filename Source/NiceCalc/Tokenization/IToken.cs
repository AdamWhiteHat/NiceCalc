using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
    public interface IToken
    {
        char Symbol { get; }
        TokenType TokenType { get; }
        string ToString();
    }
}
