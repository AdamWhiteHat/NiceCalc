using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using ExtendedNumerics;

namespace NiceCalc.Math
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

		public static BigDecimal Product(this IEnumerable<BigDecimal> source)
		{
			return source.Aggregate((accumulator, current) => accumulator * current);
		}

		public static BigDecimal Sum(this IEnumerable<BigDecimal> source)
		{
			return source.Aggregate((accumulator, current) => accumulator + current);
		}
	}
}
