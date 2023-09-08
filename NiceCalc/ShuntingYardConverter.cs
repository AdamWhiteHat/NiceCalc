/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Permissions;

namespace NiceCalc
{
	public static class ShuntingYardConverter
	{
		private static readonly string AllowedCharacters = InfixNotation.Numbers + InfixNotation.Operators + InfixNotation.Functions + "()";

		private enum Associativity
		{
			Left, Right
		}
		private static readonly Dictionary<char, int> PrecedenceDictionary = new()
		{
			{'(', 0}, {')', 0},
			{'+', 1}, {'-', 1},
			{'*', 2}, {'/', 2},
			{'^', 3}
		};
		private static readonly Dictionary<char, Associativity> AssociativityDictionary = new()
		{
			{'+', Associativity.Left}, {'-', Associativity.Left}, {'*', Associativity.Left},{'/', Associativity.Left},
			{'^', Associativity.Right}
		};

		private static readonly string EndToken = "{END}";

		private static void AddToOutput(List<char> output, params char[] chars)
		{
			if (chars != null && chars.Length > 0)
			{
				foreach (char c in chars)
				{
					output.Add(c);
				}
				output.Add(' ');
			}
		}

		private static int GetPrecedence(char c)
		{
			if (PrecedenceDictionary.ContainsKey(c))
			{
				return PrecedenceDictionary[c];
			}
			else if (InfixNotation.Functions.Contains(c))
			{
				return 4;
			}
			else
			{
				throw new ParsingException($"Unknown token '{c}' encountered while trying to determine its precedence.", token: c);
			}
		}

		/// <summary>
		/// A dumb (simple) tokenizer that turns characters into tokens and
		/// runs of digit characters (numbers) into a single token.
		/// Does not detect invalid/unknown characters, as this is handled elsewhere.
		/// </summary>
		/// <returns></returns>
		private static Queue<string> DumbTokenizer(string sanitizedInputString)
		{
			Queue<string> result = new Queue<string>();

			List<char> number = new List<char>();
			foreach (char c in sanitizedInputString)
			{
				if (InfixNotation.Numbers.Contains(c))
				{
					number.Add(c);
				}
				else
				{
					if (number.Any())
					{
						string value = new string(number.ToArray());
						result.Enqueue(value);
						number.Clear();
					}
					result.Enqueue(c.ToString());
				}
			}

			return result;
		}

		public static string Convert(string infixNotationString)
		{
			if (string.IsNullOrWhiteSpace(infixNotationString))
			{
				throw new ParsingException("Argument infixNotationString must not be null, empty or whitespace.");
			}

			var unknownCharacters = infixNotationString.Where(c => !AllowedCharacters.Contains(c));
			if (unknownCharacters.Any())
			{
				throw new ParsingException($"Argument {nameof(infixNotationString)} contains some unknown tokens: {{ {string.Join(", ", unknownCharacters)} }}.");
			}
			string sanitizedString = new string(infixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());

			Queue<string> inputQueue = DumbTokenizer(sanitizedString);

			string number = string.Empty;
			string parameter = string.Empty;
			List<char> output = new List<char>();
			Stack<char> operatorStack = new Stack<char>();

			string next = EndToken;
			string current = EndToken;

			while (inputQueue.TryDequeue(out current))
			{
				if (!inputQueue.TryPeek(out next)) { next = EndToken; }

				if (InfixNotation.IsNumeric(current))
				{
					AddToOutput(output, current.ToArray());
				}
				else if (current.Length == 1)
				{
					char c = current[0];

					if (InfixNotation.Numbers.Contains(c))
					{
						AddToOutput(output, c);
					}
					else if (InfixNotation.Functions.Contains(c))
					{
						operatorStack.Push(c);
					}
					else if (InfixNotation.Operators.Contains(c))
					{
						if (operatorStack.Count > 0)
						{
							char o = operatorStack.Peek();

							while (
								o != '('
								&&
								(
									(AssociativityDictionary[c] == Associativity.Left &&
									GetPrecedence(c) <= GetPrecedence(o))
										||
									(AssociativityDictionary[c] == Associativity.Right &&
									GetPrecedence(c) < GetPrecedence(o))
								)
							)
							{
								AddToOutput(output, operatorStack.Pop());
								if (operatorStack.Count <= 0)
								{
									break;
								}
								o = operatorStack.Peek();
							}

						}
						operatorStack.Push(c);
					}
					else if (c == ',')
					{
						if (operatorStack.Count > 0)
						{
							char o = operatorStack.Peek();

							while (o != '(')
							{
								AddToOutput(output, operatorStack.Pop());
								if (operatorStack.Count <= 0)
								{
									break;
								}
								o = operatorStack.Peek();
							}
						}
					}
					else if (c == '(')
					{
						operatorStack.Push(c);
					}
					else if (c == ')')
					{
						bool leftParenthesisFound = false;
						while (operatorStack.Count > 0)
						{
							char o = operatorStack.Pop();
							if (o == '(')
							{
								leftParenthesisFound = true;
								break;
							}
							else
							{
								AddToOutput(output, o);
							}
						}

						if (!leftParenthesisFound)
						{
							throw new ParsingException("The algebraic string contains mismatched parentheses (missing a left parenthesis).", c, operatorStack);
						}
					}
					else
					{
						throw new ParsingException($"Unrecognized character: '{c}'.", token: c, stack: operatorStack);
					}
				}
				else
				{
					throw new ParsingException($"String '{current}' is not numeric and has a length greater than 1.", token: current, stack: operatorStack);
				}
			} // while


			//
			// Syntax check
			//
			while (operatorStack.Count > 0)
			{
				char o = operatorStack.Pop();
				if (o == '(')
				{
					throw new ParsingException("Mismatched parentheses (extra left parenthesis).", token: o, stack: operatorStack);
				}
				else if (o == ')')
				{
					throw new ParsingException("Mismatched parentheses (extra right parenthesis).", token: o, stack: operatorStack);
				}
				else
				{
					AddToOutput(output, o);
				}
			}

			return new string(output.ToArray());
		}


	}
}
