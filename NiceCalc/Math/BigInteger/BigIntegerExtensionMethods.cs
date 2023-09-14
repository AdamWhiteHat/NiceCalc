using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace NiceCalc.Math
{
	public static class BigIntegerExtensionMethods
	{
		public static BigInteger Mod(this BigInteger source, BigInteger mod, bool preferPositive = true)
		{
			if (source < mod)
			{
				return source;
			}
			BigInteger r = source % mod;
			BigInteger result = (r < 0) ? BigInteger.Add(r, mod) : r;
			if (!preferPositive)
			{
				BigInteger other = result - mod;
				if (BigInteger.Abs(other) < BigInteger.Abs(result))
				{
					result = other;
				}
			}
			return result;
		}

		public static int GetLength(this BigInteger source)
		{
			BigInteger n = BigInteger.Abs(source);
			var digits = 0;
			while (n > 0)
			{
				digits++;
				n /= 10;
			}
			return digits;
		}

		public static BigInteger Product(this IEnumerable<BigInteger> source)
		{
			return source.Aggregate((accumulator, current) => accumulator * current);
		}

		public static BigInteger Sum(this IEnumerable<BigInteger> source)
		{
			return source.Aggregate((accumulator, current) => accumulator + current);
		}

		/// <summary>Returns the square root of a BigInteger.</summary>
		public static BigInteger SquareRoot(this BigInteger source)
		{
			return NthRoot(source, 2);
		}

		/// <summary> Returns the Nth root of a BigInteger. The the value must be a positive integer and the parameter root must be greater than or equal to 1.</summary>
		public static BigInteger NthRoot(this BigInteger source, int root)
		{
			BigInteger remainder = new BigInteger();
			return source.NthRoot(root, out remainder);
		}

		/// <summary> Returns the Nth root of a BigInteger with remainder. The value must be a positive integer and the parameter root must be greater than or equal to 1.</summary>
		public static BigInteger NthRoot(this BigInteger source, int root, out BigInteger remainder)
		{
			if (root < 1) throw new Exception("Root must be greater than or equal to 1");
			if (source.Sign == -1) throw new Exception("Value must be a positive integer");

			remainder = 0;
			if (source == BigInteger.One) { return BigInteger.One; }
			if (source == BigInteger.Zero) { return BigInteger.Zero; }
			if (root == 1) { return source; }

			BigInteger upperbound = source;
			BigInteger lowerbound = BigInteger.Zero;

			while (true)
			{
				BigInteger nval = (upperbound + lowerbound) >> 1;
				BigInteger testPow = BigInteger.Pow(nval, root);

				if (testPow > source) upperbound = nval;
				if (testPow < source) lowerbound = nval;
				if (testPow == source)
				{
					lowerbound = nval;
					break;
				}
				if (lowerbound == upperbound - 1) break;
			}
			remainder = source - BigInteger.Pow(lowerbound, root);
			return lowerbound;
		}
	}
}
