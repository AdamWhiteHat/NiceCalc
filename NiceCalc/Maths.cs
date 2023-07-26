using System.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace NiceCalc
{
	public static class Maths
	{
		/// <summary>
		/// Exponentiation by squaring, using arbitrarily large signed integers
		/// </summary>
		public static BigInteger Pow(BigInteger @base, BigInteger exponent)
		{
			BigInteger b = BigInteger.Abs(@base);
			BigInteger exp = BigInteger.Abs(exponent);
			BigInteger result = BigInteger.One;
			while (exp > 0)
			{
				if ((exp & 1) == 1) // If exponent is odd (&1 == %2)
				{
					result = (result * b);
					exp -= 1;
					if (exp == 0) { break; }
				}

				b = (b * b);
				exp >>= 1; // exp /= 2;
			}
			return result;
		}

		/// <summary>
		/// Exponentiation by squaring, modulus some number (as needed)
		/// </summary>
		public static BigInteger PowerMod(BigInteger @base, BigInteger exponent, BigInteger modulus)
		{
			BigInteger result = BigInteger.One;
			while (exponent > 0)
			{
				if ((exponent & 1) == 1) // If exponent is odd
				{
					result = (result * @base).Mod(modulus);
					exponent -= 1;
					if (exponent == 0) { break; }
				}

				@base = (@base * @base).Mod(modulus);
				exponent >>= 1; // exponent /= 2;
			}
			return result.Mod(modulus);
		}

		/// <summary>Least common multiple</summary>
		public static BigInteger LCM(BigInteger num1, BigInteger num2)
		{
			BigInteger absValue1 = BigInteger.Abs(num1);
			BigInteger absValue2 = BigInteger.Abs(num2);
			return (absValue1 * absValue2) / BigInteger.GreatestCommonDivisor(absValue1, absValue2);
		}

		/// <summary>
		/// Factorial function: n! = 1 * 2 * ... * n-1 * n
		/// </summary>
		public static BigInteger Factorial(BigInteger n)
		{
			return MultiplyRange(2, n); //Enumerable.Range(2, n-1).Product();
		}

		private static BigInteger MultiplyRange(BigInteger from, BigInteger to)
		{
			var diff = to - from;
			if (diff == 1) { return from * to; }
			if (diff == 0) { return from; }

			BigInteger half = (from + to) / 2;
			return BigInteger.Multiply(MultiplyRange(from, half), MultiplyRange(half + 1, to));
		}
	}
}
