using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceCalc.Execution;

namespace NiceCalc.Tokenization
{
    public class OperatorToken : Token
    {
        public int ParameterCount;
        public override char Symbol { get { return _symbol; } }
        public override TokenType TokenType { get { return TokenType.Operation; } }

        private char _symbol;

        public OperatorToken(char operation)
        {
            _symbol = operation;
            ParameterCount = 2;
        }
    }
}
