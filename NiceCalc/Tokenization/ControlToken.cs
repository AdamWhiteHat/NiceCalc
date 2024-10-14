using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NiceCalc.Execution;
using NiceCalc.Interpreter.Language;

namespace NiceCalc.Tokenization
{
	public class ControlToken : Token
	{
		public override char Symbol { get { return _symbol; } }
		private char _symbol;

		public override TokenType TokenType { get { return _tokenType; } }
		private TokenType _tokenType;

		public ControlToken(char token)
		{
			_symbol = token;
			if (!Syntax.ControlTokens.Contains(_symbol))
			{
				throw new ParsingException($"The symbol '{_symbol}' is not supported by this token type ({nameof(ControlToken)}) yet!");
			}
			if (_symbol == ',') { _tokenType = TokenType.ArgumentDelimiter; }
			else if (_symbol == '(') { _tokenType = TokenType.OpenParentheses; }
			else if (_symbol == ')') { _tokenType = TokenType.CloseParentheses; }
		}
	}
}
