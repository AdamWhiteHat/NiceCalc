using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Math
{
	public static class Factorization
	{
		public static readonly BigInteger PrimeCache_MaxValue;

		private static BigInteger _cacheCeiling;
		private static List<BigInteger> _primeCache;
		private static BigInteger _cacheLargestPrimeCurrently;
		private static BigInteger[] _probablePrimeCheckBases;

		static Factorization()
		{
			_cacheCeiling = BigInteger.Pow(11, 8);
			PrimeCache_MaxValue = _cacheCeiling;
			_primeCache = new List<BigInteger> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };
			_probablePrimeCheckBases = _primeCache.ToArray();
			_cacheLargestPrimeCurrently = _primeCache.Last();
		}

		public static void EnsurePrimeCacheSize(BigInteger maxPrime)
		{
			BigInteger boundedPrimeRequest = BigInteger.Min(maxPrime, _cacheCeiling);
			if (_cacheLargestPrimeCurrently < boundedPrimeRequest)
			{
				_primeCache = PrimeFactory.GetPrimes(boundedPrimeRequest).ToList();
				_cacheLargestPrimeCurrently = boundedPrimeRequest;
			}
		}

		public static IEnumerable<BigInteger> GetPrimeFactorization(BigInteger value)
		{
			return GetPrimeFactorization(value, value.SquareRoot());
		}

		public static IEnumerable<BigInteger> GetPrimeFactorization(BigInteger value, BigInteger maxValue)
		{
			value = BigInteger.Abs(value);

			if (maxValue < 1 || (maxValue * maxValue) > value)
			{
				maxValue = BigInteger.Min(maxValue, value.SquareRoot() + 1);
			}

			if (value == 0) { return new BigInteger[] { 0 }; }
			if (value < 10) { if (value == 0 || value == 1 || value == 2 || value == 3 || value == 5 || value == 7) { return new List<BigInteger>() { value }; } }

			BigInteger primeListSize = maxValue + 5;

			EnsurePrimeCacheSize(primeListSize);

			if (_primeCache.Contains(value)) { return new List<BigInteger>() { value }; }

			List<BigInteger> factors = new List<BigInteger>();
			foreach (BigInteger prime in _primeCache)
			{
				while (value % prime == 0)
				{
					value /= prime;
					factors.Add(prime);
				}

				if (value == 1) break;
			}

			if (value != 1) { factors.Add(value); }

			return factors;
		}

		public static List<BigInteger> GetDivisors(BigInteger n)
		{
			if (n.IsOne) { return new List<BigInteger> { 1 }; }
			var factors = Factorization.GetPrimeFactorization(n);
			var combinations = Combinatorics.GetAllPossibleCombinations<BigInteger>(factors);

			return new BigInteger[] { 1 }.Concat(combinations.Select(arr => arr.Product())).OrderBy(val => val).ToList();
		}

		public static bool IsProbablePrime(BigInteger input)
		{
			if (input == 2 || input == 3)
			{
				return true;
			}
			if (input < 2 || (input & 1) == 0) // &1 == %2
			{
				return false;
			}

			EnsurePrimeCacheSize(input + 1);

			if (input < _cacheLargestPrimeCurrently)
			{
				return _primeCache.Contains(input);
			}

			BigInteger d = input - 1;
			int s = 0;

			while ((d & 1) == 0) // &1 == %2
			{
				d >>= 1;
				s += 1;
			}

			foreach (BigInteger a in _probablePrimeCheckBases)
			{
				BigInteger x = BigInteger.ModPow(a, d, input);
				if (x == 1 || x == input - 1)
				{
					continue;
				}

				for (int r = 1; r < s; r++)
				{
					x = BigInteger.ModPow(x, 2, input);
					if (x == 1)
					{
						return false;
					}
					if (x == input - 1)
					{
						break;
					}
				}

				if (x != input - 1)
				{
					return false;
				}
			}
			return true;
		}

		public static BigInteger GetNextPrime(BigInteger fromValue)
		{
			BigInteger result = fromValue + 1;
			if (result.IsEven)
			{
				result += 1;
			}

			while (!Factorization.IsProbablePrime(result))
			{
				result += 2;
			}

			return result;
		}

		public static BigInteger GetPreviousPrime(BigInteger fromValue)
		{
			BigInteger result = fromValue.IsEven ? fromValue - 1 : fromValue - 2;

			while (result > 0)
			{
				if (Factorization.IsProbablePrime(result))
				{
					return result;
				}
				result -= 2;
			}

			throw new Exception($"No primes exist between {fromValue} and zero.");
		}

		public static IEnumerable<BigInteger> GetDistinctPrimeFactors(BigInteger value)
		{
			return GetDistinctPrimeFactors(value, value.SquareRoot());
		}

		public static IEnumerable<BigInteger> GetDistinctPrimeFactors(BigInteger value, BigInteger maxValue)
		{
			return GetPrimeFactorization(value, maxValue).Distinct();
		}

		public static string GetPrimeFactorizationString(BigInteger value)
		{
			var primeFactors = GetPrimeFactorization(value, value.SquareRoot() + 1);
			var groups = primeFactors.GroupBy(bi => bi);
			return string.Join(" * ", groups.Select(g => $"{g.Key}^{g.Count()}"));
		}		
	}
}
