using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NiceCalc.Math;

namespace NiceCalc.Execution
{
	public static class Functions
	{
		public static int GetParameterCount(char functionToken)
		{
			if (!TokenParameterCountDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenParameterCountDictionary)}.", functionToken);
			}
			return TokenParameterCountDictionary[functionToken];
		}

		public static bool IsFunctionIntegers(char functionToken)
		{
			if (!TokenParameterIntegerDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenParameterIntegerDictionary)}.", functionToken);
			}
			return TokenParameterIntegerDictionary[functionToken];
		}

		public static Func<BigInteger, BigInteger> GetUnaryIntegerFunction(char functionToken)
		{
			if (!TokenUnaryIntegerFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenUnaryIntegerFunctionDictionary)}.", functionToken);
			}
			return TokenUnaryIntegerFunctionDictionary[functionToken];
		}

		public static Func<BigInteger, BigInteger, BigInteger> GetBinaryIntegerFunction(char functionToken)
		{
			if (!TokenBinaryIntegerFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenBinaryIntegerFunctionDictionary)}.", functionToken);
			}
			return TokenBinaryIntegerFunctionDictionary[functionToken];
		}

		public static readonly Dictionary<string, string> FunctionTokenDictionary = new()
		{
			{ "sqrt", "⎷" },
			{ "abs", "|" },
			{ "ln", "ⅇ" },
			{ "round", "[" },
			{ "floor", "⎿" },
			{ "truncate", "⎿" },
			{ "trunc", "⎿" },
			{ "ceiling", "⎾" },
			{ "ceil", "⎾" },
			{ "sign", "±" },
			{ "sin", "σ" },
			{ "cos", "γ" },
			{ "tan", "τ" },
			{ "factorial", "!" },

			{ "isprime", "ℙ" },
			{ "nextprime", "ꓑ" },
			{ "previousprime", "ꟼ" },

			{ "factor", "Ｆ" },
			{ "divisors", "Ｄ" },

			{ "#", "⍻" },
			{ "nthroot", "⍻" },
			{ "logn", "⌥" },
			{ "gcd", "⋂" },
			{ "lcm", "⋃" }
		};

		private static readonly Dictionary<char, int> TokenParameterCountDictionary = new()
		{
			{ '⎷', 1 },
			{ '|', 1 },
			{ 'ⅇ', 1 },
			{ '[', 1 },
			{ '⎿', 1 },
			{ '⎾', 1 },
			{ '±', 1 },
			{ 'σ', 1 },
			{ 'γ', 1 },
			{ 'τ', 1 },
			{ '!', 1 },
			{ 'ℙ', 1 },
			{ 'ꓑ', 1 },
			{ 'ꟼ', 1 },
			{ 'Ｆ', 1 },
			{ 'Ｄ', 1 },

			{ '⍻', 2 },
			{ '⌥', 2 },
			{ '⋂', 2 },
			{ '⋃', 2 }
		};

		private static readonly Dictionary<char, bool> TokenParameterIntegerDictionary = new()
		{
			// === Unary ===

			// Real
			{ '⎷', false },
			{ '|', false },
			{ '±', false },
			{ '!', false },
			{ 'ⅇ', false },
			{ '[', false },
			{ '⎿', false },
			{ '⎾', false },
			{ 'σ', false },
			{ 'γ', false },
			{ 'τ', false },

			// BigInteger

			{ 'ℙ', true },
			{ 'ꓑ', true },
			{ 'ꟼ', true },

			// === Binary ===
			// Real
			{ '⍻', false },
			{ '⌥', false },

			// BigInteger
			{ '⋂', true },
			{ '⋃', true },

			// BigInteger, but Returns a string
			{ 'Ｆ', true },
			{ 'Ｄ', true },
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger>> TokenUnaryIntegerFunctionDictionary = new()
		{
			{ 'ℙ', new Func<BigInteger, BigInteger>((BigInteger i) => Factorization.IsProbablePrime(i) ? BigInteger.One : BigInteger.Zero) },
			{ 'ꓑ', new Func<BigInteger, BigInteger>((BigInteger i) => Factorization.GetNextPrime(i)) },
			{ 'ꟼ', new Func<BigInteger, BigInteger>((BigInteger i) => Factorization.GetPreviousPrime(i)) }

		};

		private static readonly Dictionary<char, Func<BigInteger, string>> TokenUnaryStringFunctionDictionary = new()
		{
			{ 'Ｆ', new Func<BigInteger, string>((BigInteger i) => Factorization.GetPrimeFactorizationString(i)) },
			{ 'Ｄ', new Func<BigInteger, string>((BigInteger i) => $"({string.Join(", ", Factorization.GetDivisors(i))}") }
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger, BigInteger>> TokenBinaryIntegerFunctionDictionary = new()
		{
			{ '⋂', new Func<BigInteger, BigInteger, BigInteger>((BigInteger a, BigInteger b) => BigInteger.GreatestCommonDivisor(a, b)) }, // gcd
			{ '⋃', new Func<BigInteger, BigInteger, BigInteger>((BigInteger a, BigInteger b) => BigIntegerMaths.LCM(a, b)) } // lcm
		};
	}
}
