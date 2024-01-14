using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
	public class NumberToken<T> : Token
	{
		public string Digits;

		public NumberToken(string digits)
		{
			Digits = digits;
		}

		public override string ToString()
		{
			return Digits;
		}
	}
}
