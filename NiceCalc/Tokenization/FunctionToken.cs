using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
	public class FunctionToken : Token
	{
		public char Symbol;

		public FunctionToken(char symbol)
		{
			Symbol = symbol;
		}

		public override string ToString()
		{
			return Symbol.ToString();
		}
	}
}
