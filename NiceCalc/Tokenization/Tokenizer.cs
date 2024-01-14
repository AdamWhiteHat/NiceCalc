using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceCalc.Execution;
using NiceCalc.Interpreter.Language;

namespace NiceCalc.Tokenization
{
	public static class Tokenizer
	{
		private static readonly string NonIdentifierTokens = Syntax.Operators + "() ";



		public static List<string> Tokenize(string expression)
		{
			List<string> tokens = new List<string>();

			List<char> number = new List<char>();
			List<char> identifier = new List<char>();

			char[] whitespaceStripped = expression.Where(c => !char.IsWhiteSpace(c)).ToArray();
			foreach (char c in whitespaceStripped)
			{
				if (NonIdentifierTokens.Contains(c))
				{
					if (number.Any())
					{
						tokens.Add(new string(number.ToArray()));
						number.Clear();
					}
					if (identifier.Any())
					{
						tokens.Add(new string(identifier.ToArray()));
						identifier.Clear();
					}
					tokens.Add(c.ToString());
				}
				else if (Syntax.Numbers.Contains(c))
				{
					if (identifier.Any())
					{
						identifier.Add(c);
					}
					else
					{
						number.Add(c);
					}
				}
				else
				{
					if (number.Any())
					{
						tokens.Add(new string(number.ToArray()));
						number.Clear();
					}
					identifier.Add(c);
				}
			}

			if (number.Any())
			{
				tokens.Add(new string(number.ToArray()));
				number.Clear();
			}
			if (identifier.Any())
			{
				tokens.Add(new string(identifier.ToArray()));
				identifier.Clear();
			}

			return tokens;
		}


		public static class Preprocess
		{
			/// <summary>
			/// Replaces spelled-out function names, e.g. "sqrt(42)"
			/// into single-character function symbols, e.g. "⎷(42)"
			/// as well as handle special syntax constructs (factorials).
			/// </summary>		
			public static List<string> TokenizeFunctions(List<string> input)
			{
				List<string> tokens = input.ToList();

				int index;
				foreach (var kvp in Functions.FunctionTokenDictionary)
				{
					index = tokens.IndexOf(kvp.Key);
					while (index != -1)
					{
						tokens[index] = kvp.Value;
						index = tokens.IndexOf(kvp.Key);
					}
				}

				// Deal with special syntax of the factorial function;
				// Replace: 42! With: !(42)
				index = tokens.IndexOf("!");
				while (index != -1)
				{
					if (index == 0) // Don't access index - 1
					{
						break;
					}

					tokens.RemoveAt(index);
					tokens.Insert(index, ")");
					tokens.Insert(index - 1, "(");
					tokens.Insert(index - 1, "!");

					index = tokens.IndexOf("!", index); // Must restart the search as last offset, or get stuck in a loop.
				}

				return tokens;
			}

			/// <summary>
			/// Turns factorials: "(2 * 12!) - 12"
			/// Into function form: "(2 * factorial(12)) - 12"
			/// </summary>
			public static string RewriteFactorials(string input)
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
							throw new Exception($"Found closed parenthesis ')' next to factorial symbol '!' at index {symbolIndex}, but cannot find the open parenthesis: '('.");
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
}
