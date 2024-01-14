using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
	public class LiteralToken : Token
	{
		public string Value;
		public override char Symbol { get { return '"'; } }
		public override TokenType TokenType { get { return TokenType.Literal; } }

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
