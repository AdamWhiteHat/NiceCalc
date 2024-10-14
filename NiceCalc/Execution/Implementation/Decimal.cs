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
	public static class Decimal
	{
		public static Func<BigDecimal, BigDecimal> GetUnaryRealFunction(char functionToken)
		{
			if (!TokenUnaryRealFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenUnaryRealFunctionDictionary)}.", functionToken);
			}
			return TokenUnaryRealFunctionDictionary[functionToken];
		}

		public static Func<BigInteger, BigInteger, BigDecimal> GetBinaryRealFunction(char functionToken)
		{
			if (!TokenBinaryRealFunctionDictionary.ContainsKey(functionToken))
			{
				throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenBinaryRealFunctionDictionary)}.", functionToken);
			}
			return TokenBinaryRealFunctionDictionary[functionToken];
		}

		public static Func<BigDecimal, BigDecimal, BigDecimal> GetBinaryOperation(char operationToken)
		{
			if (!TokenBinaryOperationDictionary.ContainsKey(operationToken))
			{
				throw new ParsingException($"Unrecognized operation token '{operationToken}' in dictionary {nameof(TokenBinaryOperationDictionary)}.", operationToken);
			}
			return TokenBinaryOperationDictionary[operationToken];
		}

		private static readonly Dictionary<char, Func<BigDecimal, BigDecimal>> TokenUnaryRealFunctionDictionary = new Dictionary<char, Func<BigDecimal, BigDecimal>>()
		{
			{ '⎷', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.SquareRoot(i, BigDecimal.Precision)) }, // sqrt
			{ '|', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Abs(i)) },  // abs	
			{ '±', new Func<BigDecimal, BigDecimal>((BigDecimal i) => i.Sign) },     // sign
			{ '-', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Negate(i)) },  // -	
			{ '!', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimalAdapter.Factorial(i)) },   // factorial
			{ 'Ə', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Ln(i, BigDecimal.Precision)) },    // ln
			{ 'ⅇ', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Exp(i, BigDecimal.Precision)) },    // Exp
			{ '[', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Round(i)) },    // round
			{ '⎿', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Truncate(i)) }, // truncate
			{ '⎾', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Ceiling(i)) }, // ceiling
			{ 'σ', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Sin(i, BigDecimal.Precision)) },    // sin
			{ 'γ', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Cos(i, BigDecimal.Precision)) },    // cos
			{ 'τ', new Func<BigDecimal, BigDecimal>((BigDecimal i) => BigDecimal.Tan(i, BigDecimal.Precision)) },    // tan
		};

		private static readonly Dictionary<char, Func<BigInteger, BigInteger, BigDecimal>> TokenBinaryRealFunctionDictionary = new Dictionary<char, Func<BigInteger, BigInteger, BigDecimal>>()
		{
			{ '⌥', new Func<BigInteger, BigInteger, BigDecimal>((BigInteger a, BigInteger b) => BigDecimal.Exp(a) / BigDecimal.Exp(b)) }, // logn
			{ '⍻', new Func<BigInteger, BigInteger, BigDecimal>((BigInteger a, BigInteger b) => BigDecimal.NthRoot(new BigDecimal(mantissa: b, exponent: 0), (int)a, decimalPlaces: BigDecimal.Precision)) } // nthroot
		};

		private static readonly Dictionary<char, Func<BigDecimal, BigDecimal, BigDecimal>> TokenBinaryOperationDictionary = new Dictionary<char, Func<BigDecimal, BigDecimal, BigDecimal>>()
		{
			{'+', BigDecimal.Add },
			{'-', BigDecimal.Subtract },
			{'*', BigDecimal.Multiply },
			{'/', BigDecimal.Divide},
			{'%', BigDecimal.Mod},
			{'^', BigDecimalAdapter.Pow }
		};
	}
}
