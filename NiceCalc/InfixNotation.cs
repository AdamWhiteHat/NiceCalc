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
		public static readonly string Functions = "⎷|ⅇ[⎿⎾±σγτ!ℙꓑꟼＦＤ⍻⌥⋂⋃";

		/// <summary>
		/// Tests if a string consists of only digit (numeric) characters.
		/// Decimal place characters are allowed, as they are part of the number.
		/// A string that is null or empty fails this test (returns false).
		/// </summary>		
		public static bool IsNumeric(string text)
		{
			return !string.IsNullOrWhiteSpace(text) && text.All(c => Numbers.Contains(c));
		}

		/// <summary>
		/// Tokenizes, Parses and then Evaluates a (mostly) infix numerical expression.
		/// "Mostly" means it also handles other syntactical constructions
		/// that are not strictly infix, such as function calls, e.g. "sqrt(42)"
		/// and the factorial notation, e.g. "42!"
		/// </summary>
		public static BigDecimal Evaluate(string infixNotationString)
		{
			string functionTokenizedString = TokenizeFunctions(infixNotationString);
			string postFixNotationString = ShuntingYardConverter.Convert(functionTokenizedString);
			return PostfixNotation.Evaluate(postFixNotationString);
		}

		/// <summary>
		/// Replaces spelled-out function names, e.g. "sqrt(42)"
		/// into single-character function symbols, e.g. "⎷(42)"
		/// as well as handle special syntax constructs (factorials).
		/// </summary>		
		private static string TokenizeFunctions(string input)
		{
			string result = input;

			result = RewriteFactorials(result);

			foreach (var kvp in FunctionTokenDictionary)
			{
				result = result.Replace(kvp.Key, kvp.Value, true, CultureInfo.InvariantCulture);
			}
			return result;
		}

		/// <summary>
		/// Turns factorials: "(2 * 12!) - 12"
		/// Into function form: "(2 * factorial(12)) - 12"
		/// </summary>
		private static string RewriteFactorials(string input)
		{
			string result = input;
			while (result.Contains('!'))
			{
				int symbolIndex = result.LastIndexOf('!');
				int index = symbolIndex;

				if (result[symbolIndex - 1] == ')')
				{
					index = result.LastIndexOf('(', symbolIndex - 1);
					if (index == -1)
					{
						throw new Exception(
												$"Found closed parenthesis ')' next to factorial symbol '!' at index {symbolIndex}, but cannot find the open parenthesis: '('.");
					}

					result = result.Remove(symbolIndex, 1);

				}
				else
				{
					while (index - 1 >= 0 && Numbers.Contains(result[index - 1]))
					{
						index--;
					}

					result = result.Remove(symbolIndex, 1);

					result = result.Insert(symbolIndex, ")");
					result = result.Insert(index, "(");
				}

				result = result.Insert(index, "factorial");
			}
			return result;
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
			// === Unary ===

			// Real
			{ '⎷',      false    },
			{ '|',      false    },
			{ '±',      false   },
			{ '!',      false   },
			{ 'ⅇ',      false   },
			{ '[',      false   },
			{ '⎿',      false   },
			{ '⎾',      false   },
			{ 'σ',      false   },
			{ 'γ',      false   },
			{ 'τ',      false   },

			// BigInteger
			
			{ 'ℙ',      true    },
			{ 'ꓑ',      true    },
			{ 'ꟼ',      true    },

			// === Binary ===
			// Real
			{ '⍻',      false   },
			{ '⌥',      false   },

			// BigInteger
			{ '⋂',      true    },
			{ '⋃',      true    },

			// BigInteger, but Returns a string
			{ 'Ｆ',      true    },
			{ 'Ｄ',      true    },
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger>> TokenUnaryIntegerFunctionDictionary = new()
		{
			//{ '⎷',  new Func<BigInteger, BigInteger>((i) => i.SquareRoot()) }, // sqrt
			//{ '|',  new Func<BigInteger, BigInteger>((BigInteger i) => BigInteger.Abs(i))},	 // abs	
			//{ '±',  new Func<BigInteger, BigInteger>((i) => i.Sign)},	 // sign
			//{ '!',  new Func<BigInteger, BigInteger>((i) => Maths.Factorial(i))},	 // factorial
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
			{ '⎷',  new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.SquareRoot(i,BigDecimal.Precision)) }, // sqrt
			{ '|',  new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Abs(i))},	 // abs	
			{ '±',  new Func<BigDecimal, BigDecimal>((i) =>  i.Sign)},	 // sign
			{ '!',  new Func<BigDecimal, BigDecimal>((i) => BigDecimalMaths.Factorial(i))},	 // factorial
			{ 'ⅇ',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Exp(i,BigDecimal.Precision))},	 // ln
			{ '[',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Round(i))},	 // round
			{ '⎿',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Truncate(i))}, // truncate
			{ '⎾',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Ceiling(i))}, // ceiling
			{ 'σ',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Sin(i,BigDecimal.Precision))},	 // sin
			{ 'γ',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Cos(i,BigDecimal.Precision))},	 // cos
			{ 'τ',  new Func<BigDecimal,BigDecimal>((i) => BigDecimal.Tan(i,BigDecimal.Precision))},	 // tan
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger, BigInteger>> TokenBinaryIntegerFunctionDictionary = new()
		{
			{ '⋂',  new Func<BigInteger, BigInteger, BigInteger>((a, b) => BigInteger.GreatestCommonDivisor( a,b) )}, // gcd
			{ '⋃',  new Func<BigInteger, BigInteger, BigInteger>((a, b) => Maths.LCM( a,b)) } // lcm
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger, BigDecimal>> TokenBinaryRealFunctionDictionary = new()
		{
			//{ '⋂',  new Func<BigInteger, BigInteger, BigDecimal>((a, b) => BigInteger.GreatestCommonDivisor( a,b) )}, // gcd
			//{ '⋃',  new Func<BigInteger, BigInteger, BigDecimal>((a, b) => Maths.LCM( a,b)) } // lcm
			{ '⌥',  new Func<BigInteger, BigInteger, BigDecimal>((a, b) => BigDecimal.Exp(a) / BigDecimal.Exp(b) ) }, // logn
			{ '⍻',  new Func<BigInteger, BigInteger, BigDecimal>((a, b) => BigDecimal.NthRoot(new BigDecimal(mantissa: b,exponent:0),(int) a,decimalPlaces:  BigDecimal.Precision) ) } // nthroot
		};
	}
}
