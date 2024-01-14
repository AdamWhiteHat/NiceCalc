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
using NiceCalc.Interpreter.Language;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace NiceCalc.Interpreter
{
	public static class ShuntingYardConverter
	{
		private static readonly string[] AllowedTokens = (Syntax.Numbers + Syntax.Operators + Syntax.Functions + "(,)/").ToCharArray().Select(c => c.ToString()).ToArray();

		private static void AddToOutput(Queue<string> output, char value)
		{
			AddToOutput(output, value.ToString());
		}

		private static void AddToOutput(Queue<string> output, string value)
		{
			if (value != null && value.Length > 0)
			{
				output.Enqueue(value);
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
				if (Syntax.Numbers.Contains(c))
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

			if (number.Any())
			{
				string value = new string(number.ToArray());
				result.Enqueue(value);
				number.Clear();
			}

			return result;
		}

		public static Queue<string> Convert(List<string> tokens)
		{
			if (!tokens.Any())
			{
				return new Queue<string>(); // No-op
			}

			var unknownTokens = tokens.Where(str => !str.All(c => Syntax.Numbers.Contains(c)) && !AllowedTokens.Contains(str));
			if (unknownTokens.Any())
			{
				throw new ParsingException($"Expression contains unknown tokens: {{ {string.Join(", ", unknownTokens)} }}.");
			}

			string expr = string.Join("", tokens);

			var dumbTokens = DumbTokenizer(expr);



			Queue<string> output = new Queue<string>();
			Stack<char> operatorStack = new Stack<char>();
			Queue<string> inputQueue = new Queue<string>(tokens);

			string current = null;
			while (inputQueue.Any())
			{
				current = inputQueue.Dequeue();

				//if (Syntax.IsNumeric(current))
				if(current.All(c => Syntax.Numbers.Contains(c)))
				{
					AddToOutput(output, current);
				}
				else if (current.Length == 1)
				{
					char c = current[0];

					if (Syntax.Numbers.Contains(c))
					{
						AddToOutput(output, c);
					}
					else if (Syntax.Functions.Contains(c))
					{
						operatorStack.Push(c);
					}
					else if (Syntax.Operators.Contains(c))
					{
						if (operatorStack.Count > 0)
						{
							char o = operatorStack.Peek();

							while (
								o != '('
								&&
								(
									(Syntax.AssociativityDictionary[c] == Associativity.Left &&
									Syntax.GetPrecedence(c) <= Syntax.GetPrecedence(o))
										||
									(Syntax.AssociativityDictionary[c] == Associativity.Right &&
									Syntax.GetPrecedence(c) < Syntax.GetPrecedence(o))
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
							char op = operatorStack.Pop();
							if (op == '(')
							{
								leftParenthesisFound = true;
								break;
							}
							else
							{
								AddToOutput(output, op);
							}
						}

						if (!leftParenthesisFound)
						{
							throw new ParsingException("The expression contains mismatched parentheses: Missing a left parenthesis.", c, operatorStack);
						}
					}
					else
					{
						throw new ParsingException($"Unrecognized character: '{c}'.", token: c, stack: operatorStack);
					}
				}
				else
				{
					throw new ParsingException($"At this stage in the parsing, all tokens that are not a number value are expected to be a single character long, but a {current.Length} length token was encountered that failed to parse into a number: '{current}' (maybe because it contains a non-numeric value?)", token: current, stack: operatorStack);
				}
			} // while


			//
			// Pop off last items.
			//
			while (operatorStack.Count > 0)
			{
				char o = operatorStack.Pop();
				if (o == '(')
				{
					throw new ParsingException("The expression contains an extraneous left parenthesis.", token: o, stack: operatorStack);
				}
				else if (o == ')')
				{
					throw new ParsingException("The expression contains an extraneous right parenthesis.", token: o, stack: operatorStack);
				}
				else
				{
					AddToOutput(output, o);
				}
			}

			return output;
		}


	}
}
