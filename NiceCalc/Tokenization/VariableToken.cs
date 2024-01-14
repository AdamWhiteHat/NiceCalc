using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
	public class VariableToken : Token
	{
		public string Name;
		public override char Symbol { get { return '@'; } }
		public override TokenType TokenType { get { return TokenType.Variable; } }

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
