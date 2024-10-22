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
        public static Func<Fraction, Fraction> GetUnaryRationalFunction(char functionToken)
        {
            if (!TokenUnaryRationalFunctionDictionary.ContainsKey(functionToken))
            {
                throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenUnaryRationalFunctionDictionary)}.", functionToken);
            }
            return TokenUnaryRationalFunctionDictionary[functionToken];
        }

        public static Func<BigInteger, BigInteger, Fraction> GetBinaryRationalFunction(char functionToken)
        {
            if (!TokenBinaryRationalFunctionDictionary.ContainsKey(functionToken))
            {
                throw new ParsingException($"Unrecognized function token '{functionToken}' in dictionary {nameof(TokenBinaryRationalFunctionDictionary)}.", functionToken);
            }
            return TokenBinaryRationalFunctionDictionary[functionToken];
        }

        public static Func<Fraction, Fraction, Fraction> GetBinaryRationalOperation(char operationToken)
        {
            if (!TokenBinaryRationalOperationDictionary.ContainsKey(operationToken))
            {
                throw new ParsingException($"Unrecognized operation token '{operationToken}' in dictionary {nameof(TokenBinaryRationalOperationDictionary)}.", operationToken);
            }
            return TokenBinaryRationalOperationDictionary[operationToken];
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

        private static readonly Dictionary<char, Func<Fraction, Fraction>> TokenUnaryRationalFunctionDictionary = new Dictionary<char, Func<Fraction, Fraction>>()
        {
            { '⎷', new Func<Fraction, Fraction>((Fraction i) => Fraction.Sqrt(i,BigDecimal.Precision)) }, // sqrt
			{ '|', new Func<Fraction, Fraction>((Fraction i) => Fraction.Abs(i)) },  // abs	
			{ '±', new Func<Fraction, Fraction>((Fraction i) => i.Sign) },     // sign
		};

        private static readonly Dictionary<char, Func<BigInteger, BigInteger, Fraction>> TokenBinaryRationalFunctionDictionary = new Dictionary<char, Func<BigInteger, BigInteger, Fraction>>()
        {
            { '⍻', new Func<BigInteger, BigInteger, Fraction>((BigInteger a, BigInteger b) => Fraction.NthRoot(b, a, BigDecimal.Precision)) } // nthroot
		};

        private static readonly Dictionary<char, Func<Fraction, Fraction, Fraction>> TokenBinaryRationalOperationDictionary = new Dictionary<char, Func<Fraction, Fraction, Fraction>>()
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
