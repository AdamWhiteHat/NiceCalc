using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Interpreter.Language
{
    public enum NumericType
    {
        Real,
        Rational,
        Integer
    }

    public enum Associativity
    {
        Left, Right
    }

    public static class Syntax
    {
        public static readonly string Numbers = "0123456789.";
        public static readonly string Operators = "+-*/%^";
        public static readonly string Functions = "⎷|Əⅇ[⎿⎾±σγτ!ℙꓑꟼＦＤ⍻⌥⋂⋃∑∏πe";
        public static readonly string ControlTokens = "(,)";
        

        public static readonly char E = 'e';
        public static readonly char Pi = 'π';
        public static readonly char Product = '∏';
        public static readonly char Sum = '∑';
        public static readonly char AssignmentOperator = '=';
        public static readonly char NegativeSign;
        public static readonly char NumberDecimalSeparator;
        public static readonly char UnaryNegation = '-';

        public static NumberFormatInfo NumberFormattingInfo { get { return _numberFormatInfo; } }
        private static NumberFormatInfo _numberFormatInfo;

        public static readonly List<string> ReservedIdentifiers;

        public static readonly Dictionary<char, int> PrecedenceDictionary = new Dictionary<char, int>()
        {
            { '(', 0 },
            { ')', 0 },
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '%', 2 },
            { '^', 3 }
        };

        public static readonly Dictionary<char, Associativity> AssociativityDictionary = new Dictionary<char, Associativity>()
        {
            { '+', Associativity.Left },
            { '-', Associativity.Left },
            { '*', Associativity.Left },
            { '/', Associativity.Left },
            { '^', Associativity.Right }
        };

        public static int GetPrecedence(char c)
        {
            if (PrecedenceDictionary.ContainsKey(c))
            {
                return PrecedenceDictionary[c];
            }
            else if (Syntax.Functions.Contains(c))
            {
                return 4;
            }
            else
            {
                throw new ParsingException($"Precedence dictionary does not contain an entry for token: '{c}'", charToken: c);
            }
        }

        static Syntax()
        {
            _numberFormatInfo = NumberFormatInfo.CurrentInfo;
            NegativeSign = _numberFormatInfo.NegativeSign[0];
            NumberDecimalSeparator = _numberFormatInfo.NumberDecimalSeparator[0];

            List<string> reserved = new List<string>();
            reserved.AddRange(NiceCalc.Execution.Functions.FunctionTokenDictionary.Keys);
            reserved.AddRange(Functions.ToCharArray().Select(c => c.ToString()));
            reserved.AddRange((Operators + "()").Select(c => c.ToString()));
            ReservedIdentifiers = reserved;
        }

        public static void SetNumberFormatInfo(NumberFormatInfo numberFormatInfo)
        {
            _numberFormatInfo = numberFormatInfo;
        }

        /// <summary>
        /// Tests if a string consists of only digit (numeric) characters.
        /// The decimal separator character is allowed, as they are part of the number.
        /// The negation character, '-', is allowed, but only at the start of the string.
        /// A string that is null or empty fails this test (returns false).
        /// </summary>		
        public static bool IsNumeric(string text)
        {
            return !string.IsNullOrWhiteSpace(text) && text.TrimStart(new char[] { '-' }).All(c => Numbers.Contains(c));
        }

        /// <summary>
        /// Tests if a string consists of only alphabetical (letter) characters.
        /// </summary>		
        public static bool IsAlpha(string text)
        {
            return !string.IsNullOrWhiteSpace(text) && text.All(c => char.IsLetter(c));
        }
    }
}
