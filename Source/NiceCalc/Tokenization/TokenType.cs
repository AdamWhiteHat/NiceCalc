using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Tokenization
{
	public enum TokenType
	{
		Number,
		Operation,
		Function,
		Variable,
		OpenParentheses,
		CloseParentheses,
		ArgumentDelimiter,
		Literal
	}
}
