using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
	public abstract class Token
	{
		public abstract char Symbol { get; }
		public abstract TokenType TokenType { get; }

		public override string ToString()
		{
			return Symbol.ToString();
		}
	}
}
