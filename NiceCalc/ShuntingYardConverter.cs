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
		private static readonly Dictionary<char, int> PrecedenceDictionary = new ()
		{
			{'(', 0}, {')', 0},
			{'+', 1}, {'-', 1},
			{'*', 2}, {'/', 2},
			{'^', 3}
		};
		private static readonly Dictionary<char, Associativity> AssociativityDictionary = new ()
		{
			{'+', Associativity.Left}, {'-', Associativity.Left}, {'*', Associativity.Left},{'/', Associativity.Left},
			{'^', Associativity.Right}
		};

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

		public static string Convert(string infixNotationString)
		{
			if (string.IsNullOrWhiteSpace(infixNotationString))
			{
				throw new ParsingException("Argument infixNotationString must not be null, empty or whitespace.");
			}

			List<char> output = new List<char>();
			Stack<char> operatorStack = new Stack<char>();
			//Stack<string> functionStack = new Stack<string>();

			List<string> enumerableInfixTokens = new List<string>();



			string sanitizedString = new string(infixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());

			string number = string.Empty;
			string parameter = string.Empty;

			bool isInFunction = false;

			//
			// Parse the raw input into a list of tokens.
			// Collect runs of digits into a single token (number)
			//
			foreach (char c in sanitizedString)
			{
				if (isInFunction)
				{
					if (c == '(')
					{
						enumerableInfixTokens.Add(c.ToString());
					}
					else if (c == ',')
					{
						enumerableInfixTokens.Add(parameter);
						parameter = string.Empty;
						enumerableInfixTokens.Add(c.ToString());
					}
					else if (c == ')')
					{
						enumerableInfixTokens.Add(parameter);
						parameter = string.Empty;
						enumerableInfixTokens.Add(c.ToString());
						isInFunction = false;
					}
					else
					{
						parameter += c.ToString();
					}
				}
				else if (InfixNotation.Functions.Contains(c))
				{
					if (number.Length > 0)
					{
						enumerableInfixTokens.Add(number);
						number = string.Empty;
					}

					isInFunction = true;

					//functionStack.Push(c.ToString());
					enumerableInfixTokens.Add(c.ToString());
				}
				else if (InfixNotation.Operators.Contains(c) ||
							"()".Contains(c))
				{
					if (number.Length > 0)
					{
						enumerableInfixTokens.Add(number);
						number = string.Empty;
					}
					enumerableInfixTokens.Add(c.ToString());
				}
				else if (InfixNotation.Numbers.Contains(c))
				{
					number += c.ToString();
				}
				else
				{
					throw new ParsingException($"Unexpected character: '{c}'.", token: c);
				}
			}

			if (number.Length > 0)
			{
				enumerableInfixTokens.Add(number);
				number = string.Empty;
			}


			//
			// Do actual shunting yard algorithm parsing
			//
			foreach (string token in enumerableInfixTokens)
			{
				if (InfixNotation.IsNumeric(token))
				{
					AddToOutput(output, token.ToArray());
				}
				else if (token.Length == 1)
				{
					char c = token[0];

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
							char o = operatorStack.Peek();
							if (o != '(')
							{
								AddToOutput(output, operatorStack.Pop());
							}
							else
							{
								operatorStack.Pop();
								leftParenthesisFound = true;
								break;
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
					throw new ParsingException($"String '{token}' is not numeric and has a length greater than 1.", token: token, stack: operatorStack);
				}
			} // foreach


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
