﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceCalc.Execution;
using NiceCalc.Interpreter.Language;

namespace NiceCalc.Tokenization
{
    public class FunctionToken : IToken
    {
        public int ParameterCount;
        public char Symbol { get { return _symbol; } }
        public TokenType TokenType { get { return TokenType.Function; } }

        private char _symbol;

        public FunctionToken(char symbol)
        {
            if (!Syntax.Functions.Contains(symbol))
            {
                throw new InvalidOperationException(symbol.ToString());
            }
            _symbol = symbol;
            ParameterCount = Functions.GetParameterCount(_symbol);
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}
