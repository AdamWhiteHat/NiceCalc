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
using NiceCalc.Execution;
using NiceCalc.Interpreter.Language;

namespace NiceCalc.Interpreter
{
	public static class InfixNotation
	{
		/// <summary>
		/// Tokenizes, Parses and then Evaluates a (mostly) infix numerical expression.
		/// "Mostly" means it also handles other syntactical constructions
		/// that are not strictly infix, such as function calls, e.g. "sqrt(42)"
		/// and the factorial notation, e.g. "42!"
		/// </summary>
		public static string Evaluate(string infixNotationString, NumericType type)
		{
			string functionTokenizedString = TokenizeFunctions(infixNotationString);
			Queue<string> postFixNotationString = ShuntingYardConverter.Convert(functionTokenizedString);
			string result = PostfixNotation.Evaluate(postFixNotationString, type);
			return result.Replace("/", " / ");
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

			foreach (var kvp in Functions.FunctionTokenDictionary)
			{
				result = result.Replace(kvp.Key, kvp.Value);
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
					while (index - 1 >= 0 && Syntax.Numbers.Contains(result[index - 1]))
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
	}
}
