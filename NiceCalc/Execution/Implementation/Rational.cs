using ExtendedNumerics;
using NiceCalc.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Execution.Implementation
{
	public static class Rational
	{
		public static Func<Fraction, Fraction> GetUnaryRealFunction(char functionToken)
		{
			if (!TokenUnaryRealFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenUnaryRealFunctionDictionary)}.", functionToken);
			}
			return TokenUnaryRealFunctionDictionary[functionToken];
		}

		public static Func<BigInteger, BigInteger, Fraction> GetBinaryRealFunction(char functionToken)
		{
			if (!TokenBinaryRealFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenBinaryRealFunctionDictionary)}.", functionToken);
			}
			return TokenBinaryRealFunctionDictionary[functionToken];
		}

		public static Func<Fraction, Fraction, Fraction> GetBinaryOperation(char operationToken)
		{
			if (!TokenBinaryOperationDictionary.ContainsKey(operationToken))
			{
				throw new ParsingException($"Unrecognized operation token '{operationToken}' in dictionary {nameof(TokenBinaryOperationDictionary)}.", operationToken);
			}
			return TokenBinaryOperationDictionary[operationToken];
		}

		public static bool IsFunctionTokenSupported(char token)
		{
			return SupportedFunctions.Contains(token);
		}

		private static char[] SupportedFunctions = new char[]
		{
			'⎷',
			'|',
			'±',
			'⍻',
			'+',
			'-',
			'*',
			'/',
			'%',
			'^'
		};

		private static readonly Dictionary<char, Func<Fraction, Fraction>> TokenUnaryRealFunctionDictionary = new()
		{
			{ '⎷', new Func<Fraction, Fraction>((Fraction i) => Fraction.Sqrt(i)) }, // sqrt
			{ '|', new Func<Fraction, Fraction>((Fraction i) => Fraction.Abs(i)) },  // abs	
			{ '±', new Func<Fraction, Fraction>((Fraction i) => i.Sign) },     // sign
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger, Fraction>> TokenBinaryRealFunctionDictionary = new()
		{
			{ '⍻', new Func<BigInteger, BigInteger, Fraction>((BigInteger a, BigInteger b) => Fraction.NthRoot(b, a, BigDecimal.Precision)) } // nthroot
		};

		private static readonly Dictionary<char, Func<Fraction, Fraction, Fraction>> TokenBinaryOperationDictionary = new Dictionary<char, Func<Fraction, Fraction, Fraction>>()
		{
			{'+', Fraction.Add },
			{'-', Fraction.Subtract },
			{'*', Fraction.Multiply },
			{'/', Fraction.Divide},
			{'%', FractionAdapter.Mod },
			{'^', Fraction.Pow }
		};
	}
}
