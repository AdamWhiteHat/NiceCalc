using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceCalc.Execution;

namespace NiceCalc.Tokenization
{
    public class OperatorToken : IToken
    {
        public int ParameterCount;
        public char Symbol { get { return _symbol; } }
        public TokenType TokenType { get { return TokenType.Operation; } }

        private char _symbol;

        public OperatorToken(char operation)
        {
            _symbol = operation;
            ParameterCount = 2;
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}
