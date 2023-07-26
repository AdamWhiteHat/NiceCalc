/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms.VisualStyles;
using ExtendedNumerics;
using System.ComponentModel;

namespace NiceCalc
{
	public static class InfixNotation
	{
		public static readonly string Numbers = "0123456789.";
		public static readonly string Operators = "+-*/^";
		public static readonly string Functions = "⎷⍻|⌥ⅇ[⎿⎾⋂⋃±σγτ!";

		public static bool IsNumeric(string text)
		{
			return !string.IsNullOrWhiteSpace(text) && text.All(c => Numbers.Contains(c));
		}

		public static BigDecimal Evaluate(string infixNotationString)
		{
			string functionTokenizedString = TokenizeFunctions(infixNotationString);
			string postFixNotationString = ShuntingYardConverter.Convert(functionTokenizedString);
			return PostfixNotation.Evaluate(postFixNotationString);
		}

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

		public static Func<BigDecimal, BigDecimal> GetUnaryRealFunction(char functionToken)
		{
			if (!TokenUnaryRealFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenUnaryRealFunctionDictionary)}.", functionToken);
			}
			return TokenUnaryRealFunctionDictionary[functionToken];
		}

		public static Func<BigInteger, BigInteger, BigInteger> GetBinaryIntegerFunction(char functionToken)
		{
			if (!TokenBinaryIntegerFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenBinaryIntegerFunctionDictionary)}.", functionToken);
			}
			return TokenBinaryIntegerFunctionDictionary[functionToken];
		}

		public static Func<BigInteger, BigInteger, BigDecimal> GetBinaryRealFunction(char functionToken)
		{
			if (!TokenBinaryRealFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenBinaryRealFunctionDictionary)}.", functionToken);
			}
			return TokenBinaryRealFunctionDictionary[functionToken];
		}

		private static string TokenizeFunctions(string input)
		{
			string result = input;
			foreach (var kvp in FunctionTokenDictionary)
			{
				result = result.Replace(kvp.Key, kvp.Value, true, CultureInfo.InvariantCulture);
			}
			return result;
		}

		private static readonly Dictionary<string, string> FunctionTokenDictionary = new()
		{
			{ "sqrt",          "⎷"   },
			{ "abs",           "|"   },
			{ "ln",            "ⅇ"   },
			{ "round",         "["   },
			{ "floor",         "⎿"  },
			{ "truncate",      "⎿"  },
			{ "trunc",         "⎿"  },
			{ "ceiling",       "⎾"  },
			{ "ceil",          "⎾"  },
			{ "sign",          "±"   },
			{ "sin",           "σ"   },
			{ "cos",           "γ"   },
			{ "tan",           "τ"   },
			{ "factorial",     "!"   },

			{ "isprime",       "ℙ"   },
			{ "nextprime",     "ꓑ"   },
			{ "previousprime", "ꟼ"   },

			{ "factor",        "Ｆ"   },
			{ "divisors",      "Ｄ"   },

			{ "#",             "⍻"  },
			{ "nthroot",       "⍻"  },
			{ "logn",          "⌥"  },
			{ "gcd",           "⋂"  },
			{ "lcm",           "⋃"  }
		};

		private static readonly Dictionary<char, int> TokenParameterCountDictionary = new()
		{
			{ '⎷',  1 },
			{ '|',  1 },
			{ 'ⅇ',  1 },
			{ '[',  1 },
			{ '⎿',  1 },
			{ '⎾',  1 },
			{ '±',  1 },
			{ 'σ',  1 },
			{ 'γ',  1 },
			{ 'τ',  1 },
			{ '!',  1 },
			{ 'ℙ',  1 },
			{ 'ꓑ',  1 },
			{ 'ꟼ',  1 },
			{ 'Ｆ',  1 },
			{ 'Ｄ',  1 },

			{ '⍻',    2 },
			{ '⌥',    2 },
			{ '⋂',    2 },
			{ '⋃',    2 }
		};

		private static readonly Dictionary<char, bool> TokenParameterIntegerDictionary = new()
		{
			{ '⎷',      true    },
			{ '|',      true    },
			{ 'ⅇ',      false   },
			{ '[',      false   },
			{ '⎿',      false   },
			{ '⎾',      false   },
			{ '±',      false   },
			{ 'σ',      false   },
			{ 'γ',      false   },
			{ 'τ',      false   },
			{ '!',      false   },
			{ 'ℙ',      true    },
			{ 'ꓑ',      true    },
			{ 'ꟼ',      true    },
			{ 'Ｆ',      true    },
			{ 'Ｄ',      true    },
			{ '⍻',      false   },
			{ '⌥',      false   },
			{ '⋂',      true    },
			{ '⋃',      true    }
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger>> TokenUnaryIntegerFunctionDictionary = new()
		{
			{ '⎷',  new Func<BigInteger, BigInteger>((i) => i.SquareRoot()) }, // sqrt
			{ '|',  new Func<BigInteger, BigInteger>((BigInteger i) => BigInteger.Abs(i))},	 // abs	
			{ '±',  new Func<BigInteger, BigInteger>((i) => i.Sign)},	 // sign
			{ '!',  new Func<BigInteger, BigInteger>((i) => Maths.Factorial(i))},	 // factorial
			{ 'ℙ',  new Func<BigInteger, BigInteger>((i) => Factorization.IsProbablePrime(i)?BigInteger.One:BigInteger.Zero)   },
			{ 'ꓑ',  new Func<BigInteger, BigInteger>((i) => Factorization.GetNextPrime(i))   },
			{ 'ꟼ',  new Func<BigInteger, BigInteger>((i) => Factorization.GetPreviousPrime(i))   }

		};

		private static readonly Dictionary<char, Func<BigInteger, string>> TokenUnaryStringFunctionDictionary = new()
		{
			{ 'Ｆ',  new Func<BigInteger, string>((i) => Factorization.GetPrimeFactorizationString( i))   },
			{ 'Ｄ',  new Func<BigInteger, string>((i) => $"({string.Join(", ",Factorization.GetDivisors( i))}")   }
		};

		private static readonly Dictionary<char, Func<BigDecimal, BigDecimal>> TokenUnaryRealFunctionDictionary = new()
		{
			{ 'ⅇ',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Exp((BigInteger)i))},	 // ln
			{ '[',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Round(i))},	 // round
			{ '⎿',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Truncate(i))}, // truncate
			{ '⎾',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Ceiling(i))}, // ceiling
			{ 'σ',  new Func<BigDecimal,BigDecimal>((i) => Math.Sin((double)i))},	 // sin
			{ 'γ',  new Func<BigDecimal,BigDecimal>((i) => Math.Cos((double)i))},	 // cos
			{ 'τ',  new Func<BigDecimal,BigDecimal>((i) => Math.Tan((double)i))},	 // tan
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger, BigInteger>> TokenBinaryIntegerFunctionDictionary = new()
		{
			{ '⋂',  new Func<BigInteger, BigInteger, BigInteger>((a, b) => BigInteger.GreatestCommonDivisor( a,b) )}, // gcd
			{ '⋃',  new Func<BigInteger, BigInteger, BigInteger>((a, b) => Maths.LCM( a,b)) } // lcm
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger, BigDecimal>> TokenBinaryRealFunctionDictionary = new()
		{
			{ '⌥',  new Func<BigInteger, BigInteger, BigDecimal>((a, b) => BigDecimal.Exp(a) / BigDecimal.Exp(b) ) }, // logn
			{ '⍻',  new Func<BigInteger, BigInteger, BigDecimal>((a, b) => BigDecimal.NthRoot(new BigDecimal(mantissa: b,exponent:0),(int) a, 15) ) } // nthroot
		};
	}
}
