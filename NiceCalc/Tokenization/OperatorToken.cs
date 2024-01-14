using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
	public class OperatorToken : Token
	{
		public char Symbol;
		public OperatorToken(char operation)
		{
			Symbol = operation;
		}

		public override string ToString()
		{
			return Symbol.ToString();
		}
	}
}
