using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceCalc.Execution;

namespace NiceCalc.Tokenization
{
	public class FunctionToken : Token
	{
		public int ParameterCount;
		public override char Symbol { get { return _symbol; } }
		public override TokenType TokenType { get { return TokenType.Function; } }

		private char _symbol;

		public FunctionToken(char symbol)
		{
			_symbol = symbol;
			ParameterCount = Functions.GetParameterCount(_symbol);
		}
	}
}
