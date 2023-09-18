﻿using System;
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
		public static readonly string Functions = "⎷|Əⅇ[⎿⎾±σγτ!ℙꓑꟼＦＤ⍻⌥⋂⋃∑∏πe";
		public static readonly char AssignmentOperator = '=';
		public static readonly char Sum = '∑';
		public static readonly char Product = '∏';
		public static readonly char Pi = 'π';
		public static readonly char E = 'e';

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
				throw new ParsingException($"Precedence dictionary does not contain an entry for token: '{c}'", token: c);
			}
		}

		static Syntax()
		{
			List<string> reserved = new List<string>();
			reserved.AddRange(NiceCalc.Execution.Functions.FunctionTokenDictionary.Keys);
			reserved.AddRange(Functions.ToCharArray().Select(c => c.ToString()));
			reserved.AddRange(PrecedenceDictionary.Keys.Select(c => c.ToString()));
			ReservedIdentifiers = reserved;
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
