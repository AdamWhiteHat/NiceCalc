using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using ExtendedNumerics;

namespace NiceCalc
{

	public static class BigDecimalExtensionMethods
	{
		public static BigDecimal Mod(this BigDecimal source, BigDecimal mod, bool preferPositive = true)
		{
			if (source < mod)
			{
				return source;
			}
			BigDecimal r = source % mod;
			BigDecimal result = (r < 0) ? BigDecimal.Add(r, mod) : r;
			if (!preferPositive)
			{
				BigDecimal other = result - mod;
				if (BigDecimal.Abs(other) < BigDecimal.Abs(result))
				{
					result = other;
				}
			}
			return result;
		}

		public static int GetLength(this BigDecimal source)
		{
			BigDecimal n = BigDecimal.Abs(source);
			var digits = 0;
			while (n > 0)
			{
				digits++;
				n /= 10;
			}
			return digits;
		}

		public static BigDecimal Product(this IEnumerable<BigDecimal> source)
		{
			return source.Aggregate((accumulator, current) => accumulator * current);
		}

		public static BigDecimal Sum(this IEnumerable<BigDecimal> source)
		{
			return source.Aggregate((accumulator, current) => accumulator + current);
		}

		/// <summary>Returns the square root of a BigDecimal.</summary>
		public static BigDecimal SquareRoot(this BigDecimal source)
		{
			return NthRoot(source, 2);
		}

		/// <summary> Returns the Nth root of a BigDecimal. The the value must be a positive integer and the parameter root must be greater than or equal to 1.</summary>
		public static BigDecimal NthRoot(this BigDecimal source, int root)
		{
			BigDecimal remainder = new BigDecimal();
			return source.NthRoot(root, out remainder);
		}

		/// <summary> Returns the Nth root of a BigDecimal with remainder. The value must be a positive integer and the parameter root must be greater than or equal to 1.</summary>
		public static BigDecimal NthRoot(this BigDecimal source, int root, out BigDecimal remainder)
		{
			if (root < 1) throw new Exception("Root must be greater than or equal to 1");
			if (source.Sign == -1) throw new Exception("Value must be a positive integer");

			remainder = 0;
			if (source == BigDecimal.One) { return BigDecimal.One; }
			if (source == BigDecimal.Zero) { return BigDecimal.Zero; }
			if (root == 1) { return source; }

			BigDecimal upperbound = source;
			BigDecimal lowerbound = BigDecimal.Zero;

			while (true)
			{
				BigDecimal nval = (upperbound + lowerbound) / 2;
				BigDecimal testPow = BigDecimal.Pow(nval, root);

				if (testPow > source) upperbound = nval;
				if (testPow < source) lowerbound = nval;
				if (testPow == source)
				{
					lowerbound = nval;
					break;
				}
				if (lowerbound == upperbound - 1) break;
			}
			remainder = source - BigDecimal.Pow(lowerbound, root);
			return lowerbound;
		}
	}
}
