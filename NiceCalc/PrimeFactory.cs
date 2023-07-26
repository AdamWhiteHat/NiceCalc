using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc
{
	public static class PrimeFactory
	{
		private static BigInteger _cacheLargestPrimeCurrently;
		private static BigInteger _cacheCeiling;
		internal static BigInteger[] _primeCache;

		static PrimeFactory()
		{
			_cacheCeiling = BigInteger.Pow(11, 7);
			_primeCache = new BigInteger[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };
			_cacheLargestPrimeCurrently = _primeCache.Last();
		}

		public static void EnsurePrimeCacheSize(BigInteger maxPrime)
		{
			BigInteger boundedPrimeRequest = BigInteger.Min(maxPrime, _cacheCeiling);
			if (_cacheLargestPrimeCurrently < boundedPrimeRequest)
			{
				_primeCache = PrimeFactory.GetPrimes(boundedPrimeRequest).ToArray();
				_cacheLargestPrimeCurrently = _primeCache.Last();
			}
		}

		public static bool IsPrime(BigInteger p)
		{
			var absP = BigInteger.Abs(p);
			EnsurePrimeCacheSize(absP + 1);
			return _primeCache.Contains(absP);
		}

		public static int GetIndexFromValue(int value)
		{
			if (value == -1)
			{
				return -1;
			}

			EnsurePrimeCacheSize(value + 1);

			BigInteger primeValue = _primeCache.First(p => p >= value);

			int index = Array.IndexOf(_primeCache, primeValue);
			return index;
		}

		public static BigInteger GetValueFromIndex(int index)
		{
			while (!_primeCache.Any() || (_primeCache.Length - 1) < index)
			{
				EnsurePrimeCacheSize(GetApproximateNthPrime(index) + 1);
			}
			BigInteger value = _primeCache[index];
			return value;
		}

		public static int GetApproximateNthPrime(int n)
		{
			// n*ln( n*e*ln(ln(n)) )
			double approx = n * Math.Log(n * Math.E * Math.Log(Math.Log(n)));
			double ceil = Math.Ceiling(approx);
			return (int)ceil;
		}

		public static IEnumerable<BigInteger> GetPrimes(BigInteger ceiling)
		{
			return FastPrimeSieve.GetRange(2, ceiling);
		}
	}
}
