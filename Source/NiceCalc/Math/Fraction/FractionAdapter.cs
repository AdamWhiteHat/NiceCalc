using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;

namespace NiceCalc.Math
{
	public static class FractionAdapter
	{
		public static Fraction Mod(Fraction dividend, Fraction divisor)
		{
			Fraction remainder;
			Fraction.DivRem(dividend, divisor, out remainder);
			return remainder;
		}
	}
}
