using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc
{
	public class CalculatorException : Exception
	{
		public CalculatorException(string message)
			: base(message)
		{
		}

		public override string ToString()
		{
			return "Error: " + this.Message;
		}
	}
}
