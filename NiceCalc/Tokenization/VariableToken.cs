using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
    public class VariableToken : IToken
    {
        public string Name;
        public char Symbol { get { return '@'; } }
        public TokenType TokenType { get { return TokenType.Variable; } }

        public VariableToken(string identifier)
        {
            Name = identifier;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
