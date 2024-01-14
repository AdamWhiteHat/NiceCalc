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
using NiceCalc.Tokenization;
using Newtonsoft.Json.Linq;

namespace NiceCalc.Interpreter
{
	public static class ShuntingYardConverter
	{
		private static readonly string[] AllowedTokens = (Syntax.Numbers + Syntax.Operators + Syntax.Functions + "(,)/").ToCharArray().Select(c => c.ToString()).ToArray();

		private static void AddToOutput(Queue<Token> output, Token value)
		{
			output.Enqueue(value);
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

		public static Queue<Token> Convert(List<Token> tokens)
		{
			if (!tokens.Any())
			{
				return new Queue<Token>(); // No-op
			}



			Queue<Token> output = new Queue<Token>();
			Stack<Token> operatorStack = new Stack<Token>();
			Queue<Token> inputQueue = new Queue<Token>(tokens);

			Token current = null;
			while (inputQueue.Any())
			{
				current = inputQueue.Dequeue();

				if (current.TokenType == TokenType.Number)
				{
					output.Enqueue(current);
				}
				else if (current.TokenType == TokenType.Function)
				{
					operatorStack.Push(current);
				}
				else if (current.TokenType == TokenType.Operation)
				{
					if (operatorStack.Count > 0)
					{
						Token op = operatorStack.Peek();

						while (
							op.TokenType != TokenType.OpenParentheses
							&&
							(
								(Syntax.AssociativityDictionary[current.Symbol] == Associativity.Left &&
								Syntax.GetPrecedence(current.Symbol) <= Syntax.GetPrecedence(op.Symbol))
									||
								(Syntax.AssociativityDictionary[current.Symbol] == Associativity.Right &&
								Syntax.GetPrecedence(current.Symbol) < Syntax.GetPrecedence(op.Symbol))
							)
						)
						{
							output.Enqueue(operatorStack.Pop());
							if (operatorStack.Count <= 0)
							{
								break;
							}
							op = operatorStack.Peek();
						}

					}
					operatorStack.Push(current);
				}
				else if (current.TokenType == TokenType.ArgumentDelimiter)
				{
					if (operatorStack.Count > 0)
					{
						Token op = operatorStack.Peek();

						while (op.TokenType != TokenType.OpenParentheses)
						{
							output.Enqueue(operatorStack.Pop());
							if (operatorStack.Count <= 0)
							{
								break;
							}
							op = operatorStack.Peek();
						}
					}
				}
				else if (current.TokenType == TokenType.OpenParentheses)
				{
					operatorStack.Push(current);
				}
				else if (current.TokenType == TokenType.CloseParentheses)
				{
					bool leftParenthesisFound = false;
					while (operatorStack.Count > 0)
					{
						Token op = operatorStack.Pop();
						if (op.TokenType == TokenType.OpenParentheses)
						{
							leftParenthesisFound = true;
							break;
						}
						else
						{
							output.Enqueue(op);
						}
					}

					if (!leftParenthesisFound)
					{
						throw new ParsingException("The expression contains mismatched parentheses: Missing a left parenthesis.", token: current, stack: operatorStack);
					}
				}
				else
				{
					throw new ParsingException($"Unrecognized character: '{current.ToString()}'.", token: current, stack: operatorStack);
				}



			} // while

			//
			// Pop off last items.
			//
			while (operatorStack.Count > 0)
			{
				Token op = operatorStack.Pop();
				if (op.TokenType == TokenType.OpenParentheses)
				{
					throw new ParsingException("The expression contains an extraneous left parenthesis.", token: op, stack: operatorStack);
				}
				else if (op.TokenType == TokenType.CloseParentheses)
				{
					throw new ParsingException("The expression contains an extraneous right parenthesis.", token: op, stack: operatorStack);
				}
				else
				{
					output.Enqueue(op);
				}
			}

			return output;
		}


	}
}
