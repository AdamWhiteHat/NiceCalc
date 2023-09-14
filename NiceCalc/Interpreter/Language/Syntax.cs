using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Interpreter.Language
{
	public enum NumericType
	{
		Real,
		Rational
	}

	public enum Associativity
	{
		Left, Right
	}

	public static class Syntax
	{
		public static readonly string Numbers = "0123456789.";
		public static readonly string Operators = "+-*/%^";
		public static readonly string Functions = "⎷|ⅇ[⎿⎾±σγτ!ℙꓑꟼＦＤ⍻⌥⋂⋃";

		public static readonly Dictionary<char, int> PrecedenceDictionary = new()
		{
			{ '(', 0 },
			{ ')', 0 },
			{ '+', 1 },
			{ '-', 1 },
			{ '*', 2 },
			{ '/', 2 },
			{ '^', 3 }
		};

		public static readonly Dictionary<char, Associativity> AssociativityDictionary = new()
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
				throw new ParsingException($"Precedence dictionary does not contain an entry for token: '{c}'", token: c);
			}
		}

		/// <summary>
		/// Tests if a string consists of only digit (numeric) characters.
		/// Decimal place characters are allowed, as they are part of the number.
		/// A string that is null or empty fails this test (returns false).
		/// </summary>		
		public static bool IsNumeric(string text)
		{
			return !string.IsNullOrWhiteSpace(text) && text.All(c => Numbers.Contains(c));
		}
	}
}
