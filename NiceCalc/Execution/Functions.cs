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
			if (!IsKnownFunctionToken(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenParameterCountDictionary)}.", functionToken);
			}
			return TokenParameterCountDictionary[functionToken];
		}

		public static bool IsKnownFunctionToken(char functionToken)
		{
			return TokenParameterCountDictionary.ContainsKey(functionToken);
		}

		public static bool IsStringFunction(char functionToken)
		{
			return TokenUnaryStringFunctionDictionary.ContainsKey(functionToken);
		}

		public static bool IsIntegerFunction(char functionToken)
		{
			if (TokenUnaryIntegerFunctionDictionary.ContainsKey(functionToken))
			{
				return true;
			}
			if (TokenBinaryIntegerFunctionDictionary.ContainsKey(functionToken))
			{
				return true;
			}
			return false;
		}

		public static Func<BigInteger, string> GetUnaryStringFunction(char functionToken)
		{
			if (!TokenUnaryStringFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenUnaryStringFunctionDictionary)}.", functionToken);
			}
			return TokenUnaryStringFunctionDictionary[functionToken];
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

		public static readonly Dictionary<string, string> FunctionTokenDictionary = new Dictionary<string, string>()
		{
			{ "sqrt", "⎷" },
			{ "abs", "|" },
			{ "ln", "Ə" },
			{ "exp", "ⅇ" },
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
			{ "lcm", "⋃" },
			{ "sum", "∑" },
			{ "product", "∏" },
			{ "pi", "π" },
			//{ "", "e" }
		};

		private static readonly Dictionary<char, int> TokenParameterCountDictionary = new Dictionary<char, int>()
		{
			{ '⎷', 1 },
			{ '|', 1 },
			{ 'Ə', 1 },
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

			// 0 parameters means just that. Generally its the pi symbol or e, and expects a constant value.
			{ 'π', 0 },
			{ 'e', 0 },

			// Meaning 2 or more parameters
			{ '⋂', -1 },
			{ '⋃', -1 },
			{ '∑', -1 },
			{ '∏', -1 }
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger>> TokenUnaryIntegerFunctionDictionary = new Dictionary<char, Func<BigInteger, BigInteger>>()
		{
			{ 'ℙ', new Func<BigInteger, BigInteger>((BigInteger i) => Factorization.IsProbablePrime(i) ? BigInteger.One : BigInteger.Zero) },
			{ 'ꓑ', new Func<BigInteger, BigInteger>((BigInteger i) => Factorization.GetNextPrime(i)) },
			{ 'ꟼ', new Func<BigInteger, BigInteger>((BigInteger i) => Factorization.GetPreviousPrime(i)) }
		};

		private static readonly Dictionary<char, Func<BigInteger, string>> TokenUnaryStringFunctionDictionary = new Dictionary<char, Func<BigInteger, string>>()
		{
			{ 'Ｆ', new Func<BigInteger, string>((BigInteger i) => Factorization.GetPrimeFactorizationString(i)) },
			{ 'Ｄ', new Func<BigInteger, string>((BigInteger i) => $"({string.Join(", ", Factorization.GetDivisors(i))})") }
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger, BigInteger>> TokenBinaryIntegerFunctionDictionary = new Dictionary<char, Func<BigInteger, BigInteger, BigInteger>>()
		{
			{ '⋂', new Func<BigInteger, BigInteger, BigInteger>((BigInteger a, BigInteger b) => BigInteger.GreatestCommonDivisor(a, b)) }, // gcd
			{ '⋃', new Func<BigInteger, BigInteger, BigInteger>((BigInteger a, BigInteger b) => BigIntegerMaths.LCM(a, b)) } // lcm
		};
	}
}
