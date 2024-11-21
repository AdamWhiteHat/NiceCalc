using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
	public class LiteralToken : IToken
	{
		public string Value;
		public char Symbol { get { return '"'; } }
		public TokenType TokenType { get { return TokenType.Literal; } }

		public LiteralToken(string value)
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
