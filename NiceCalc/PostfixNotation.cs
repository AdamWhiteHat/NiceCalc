/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Numerics;
using System.Linq.Expressions;
using System.Collections.Generic;
using ExtendedNumerics;
using System.Collections;
using ExtendedNumerics.Helpers;

namespace NiceCalc
{
	public static class PostfixNotation
	{
		private static readonly string AllowedCharacters = InfixNotation.Numbers + InfixNotation.Operators + InfixNotation.Functions + " ";

		public static Expression<Func<BigInteger>> ExpressionTree(string postfixNotationString)
		{
			if (string.IsNullOrWhiteSpace(postfixNotationString))
			{
				throw new ArgumentException("Argument postfixNotationString must not be null, empty or whitespace.", "postfixNotationString");
			}

			Stack<Expression> stack = new Stack<Expression>();
			string sanitizedString = new string(postfixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());
			List<string> enumerablePostfixTokens = sanitizedString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			foreach (string token in enumerablePostfixTokens)
			{
				if (token.Length < 1)
				{
					throw new Exception("Token.Length is less than one.");
				}

				BigInteger tokenValue = 0;
				bool parseSuccess = BigInteger.TryParse(token, out tokenValue);


				if (token.Length > 1) // Numbers > 10 will have a token length > 1
				{
					if (InfixNotation.IsNumeric(token) && parseSuccess)
					{
						stack.Push(Expression.Constant(tokenValue));
					}
					else
					{
						throw new Exception("Operators and operands must be separated by a space.");
					}
				}
				else
				{
					char tokenChar = token[0];

					if (InfixNotation.Numbers.Contains(tokenChar) && parseSuccess)
					{
						stack.Push(Expression.Constant(tokenValue));
					}
					else if (InfixNotation.Operators.Contains(tokenChar))
					{
						if (stack.Count < 2) // There must be two operands for the operator to operate on
						{
							throw new FormatException("The algebraic string has not sufficient values in the expression for the number of operators; There must be two operands for the operator to operate on.");
						}

						Expression left = stack.Pop();
						Expression right = stack.Pop();
						Expression operation = null;

						/*
						// ^ token uses Math.Pow, which both gives and takes double, hence convert
						if (tokenChar == '^')
						{
							if (left.Type != typeof(double))
							{
								left = Expression.Convert(left, typeof(double));
							}
							if (right.Type != typeof(double))
							{
								right = Expression.Convert(right, typeof(double));
							}
						}
						else // Math.Pow returns a double, so we must check here for all other operators
						{
							if (left.Type != typeof(BigInteger))
							{
								left = Expression.Convert(left, typeof(BigInteger));
							}
							if (right.Type != typeof(BigInteger))
							{
								right = Expression.Convert(right, typeof(BigInteger));
							}
						}
						*/

						if (tokenChar == '+')
						{
							operation = Expression.AddChecked(left, right);
						}
						else if (tokenChar == '-')
						{
							operation = Expression.SubtractChecked(left, right);
						}
						else if (tokenChar == '*')
						{
							operation = Expression.MultiplyChecked(left, right);
						}
						else if (tokenChar == '/')
						{
							operation = Expression.Divide(left, right);
						}
						else if (tokenChar == '^')
						{
							operation = Expression.Power(left, right);
						}

						if (operation != null)
						{
							stack.Push(operation);
						}
						else
						{
							throw new Exception("Value never got set.");
						}
					}
					else
					{
						throw new Exception(string.Format("Unrecognized character '{0}'.", tokenChar));
					}
				}

			}

			if (stack.Count == 1)
			{
				return Expression.Lambda<Func<BigInteger>>(stack.Pop());
			}
			else
			{
				throw new Exception("The input has too many values for the number of operators.");
			}

		} // method


		public static BigDecimal Evaluate(string postfixNotationString)
		{
			if (string.IsNullOrWhiteSpace(postfixNotationString))
			{
				throw new ParsingException($"Argument {nameof(postfixNotationString)} must not be null, empty or whitespace.");
			}

			Stack<string> stack = new Stack<string>();
			string sanitizedString = new string(postfixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());
			List<string> enumerablePostfixTokens = sanitizedString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			foreach (string token in enumerablePostfixTokens)
			{
				if (token.Length > 0)
				{
					if (token.Length > 1)
					{
						if (InfixNotation.IsNumeric(token))
						{
							stack.Push(token);
						}
						else
						{
							throw new ParsingException("Operators and operands must be separated by a space.", token: token);
						}
					}
					else
					{
						char tokenChar = token[0];

						if (InfixNotation.Numbers.Contains(tokenChar))
						{
							stack.Push(tokenChar.ToString());
						}
						else if (InfixNotation.Functions.Contains(tokenChar))
						{
							int paramCount = InfixNotation.GetParameterCount(tokenChar);

							if (stack.Count < paramCount)
							{
								throw new ParsingException($"Insufficient number of parameters on the stack for the function token '{tokenChar}'; Was expecting {paramCount}, but only {stack.Count} items on the stack.", token: tokenChar, stack: stack);
							}

							bool useIntegers = InfixNotation.IsFunctionIntegers(tokenChar);

							if (paramCount == 1)
							{
								if (useIntegers)
								{
									Func<BigInteger, BigInteger> unaryIntFunc = InfixNotation.GetUnaryIntegerFunction(tokenChar);

									string param1String = stack.Pop();
									BigInteger param1 = BigInteger.Parse(param1String);

									BigInteger result = unaryIntFunc(param1);
									stack.Push(result.ToString());

								}
								else
								{
									Func<BigDecimal, BigDecimal> unaryRealFunc = InfixNotation.GetUnaryRealFunction(tokenChar);

									string param1String = stack.Pop();
									BigDecimal param1 = BigDecimal.Parse(param1String);

									BigDecimal result = unaryRealFunc(param1);
									stack.Push(result.ToString());
								}
							}
							else if (paramCount == 2)
							{
								string param1String = stack.Pop();
								string param2String = stack.Pop();
								BigInteger param1 = BigInteger.Parse(param1String);
								BigInteger param2 = BigInteger.Parse(param1String);

								if (useIntegers)
								{
									Func<BigInteger, BigInteger, BigInteger> binaryIntFunc = InfixNotation.GetBinaryIntegerFunction(tokenChar);

									BigInteger result = binaryIntFunc(param1, param2);
									stack.Push(result.ToString());
								}
								else
								{
									Func<BigInteger, BigInteger, BigDecimal> binaryRealFunc = InfixNotation.GetBinaryRealFunction(tokenChar);

									BigDecimal result = binaryRealFunc(param1, param2);
									stack.Push(result.ToString());
								}
							}
							else
							{
								throw new ParsingException($"Functions with {paramCount} parameters not implemented yet. Did you add a new function to parser but not the evaluator?", token: tokenChar, stack: stack);
							}
						}
						else if (InfixNotation.Operators.Contains(tokenChar))
						{
							if (stack.Count < 2)
							{
								throw new ParsingException("The algebraic string has not sufficient values in the expression for the number of operators.", token: tokenChar, stack: stack);
							}

							string r = stack.Pop();
							string l = stack.Pop();

							bool success = false;

							BigDecimal result = Operation_BigDecimal(tokenChar, r, l);

							if (result != BigDecimal.MinusOne)
							{
								stack.Push(result.ToString());
								success = true;
							}

							if (!success)
							{
								throw new ParsingException("Value never got set.", token: tokenChar, stack: stack);
							}
						}
						else
						{
							throw new ParsingException($"Unrecognized character '{tokenChar}'.", token: tokenChar, stack: stack);
						}
					}
				}
				else
				{
					throw new ParsingException("Token length is less than one.");
				}
			}

			if (stack.Count == 1)
			{
				string stackValue = stack.Pop();

				if (stackValue.Contains('.'))
				{
					return BigDecimal.Parse(stackValue);
				}
				else
				{
					BigInteger result = 0;
					if (!BigInteger.TryParse(stackValue, out result))
					{
						throw new ParsingException("Last value on stack could not be parsed into an integer.", token: stackValue, stack: stack);
					}
					else
					{
						return result;
					}
				}
			}
			else
			{
				throw new ParsingException("The input has too many values for the number of operators.", token: "(none)", stack: stack);
			}

		} // method

		private static BigInteger Operation_BigInteger(char token, string r, string l)
		{
			BigInteger rhs = BigInteger.MinusOne;
			BigInteger lhs = BigInteger.MinusOne;

			bool parseSuccess = BigInteger.TryParse(r, out rhs);
			parseSuccess &= (rhs != BigInteger.MinusOne);

			if (!parseSuccess)
			{
				throw new ParsingException("Unable to parse the right-hand-side parameter into a BigInteger.", token: r);
			}

			parseSuccess &= BigInteger.TryParse(l, out lhs);
			parseSuccess &= (lhs != BigInteger.MinusOne);

			if (!parseSuccess)
			{
				throw new ParsingException("Unable to parse the left-hand-side parameter into a BigInteger.", token: l);
			}

			BigInteger value = BigInteger.MinusOne;
			if (token == '+')
			{
				value = lhs + rhs;
			}
			else if (token == '-')
			{
				value = lhs - rhs;
			}
			else if (token == '*')
			{
				value = lhs * rhs;
			}
			else if (token == '/')
			{
				value = lhs / rhs;
			}
			else if (token == '^')
			{
				value = Maths.Pow(lhs, rhs);
			}

			return value;
		}

		private static BigDecimal Operation_BigDecimal(char token, string r, string l)
		{
			BigDecimal rhs = BigDecimal.MinusOne;
			BigDecimal lhs = BigDecimal.MinusOne;

			try
			{
				rhs = BigDecimal.Parse(r);
			}
			catch (Exception ex)
			{
				throw new ParsingException($"Unable to parse the right-hand-side parameter into a BigDecimal. InnerException.Message: \"{ex.Message}\".", token: r);
			}

			try
			{
				lhs = BigDecimal.Parse(l);
			}
			catch (Exception ex)
			{
				throw new ParsingException($"Unable to parse the left-hand-side parameter into a BigDecimal. InnerException.Message: \"{ex.Message}\".", token: l);
			}

			BigDecimal value = BigDecimal.MinusOne;
			if (token == '+')
			{
				value = lhs + rhs;
			}
			else if (token == '-')
			{
				value = lhs - rhs;
			}
			else if (token == '*')
			{
				value = lhs * rhs;
			}
			else if (token == '/')
			{
				value = lhs / rhs;
			}
			else if (token == '^')
			{
				BigDecimal frac = rhs.GetFractionalPart();

				if (frac != BigDecimal.Zero)
				{
					throw new ParsingException("Calculator does not currently support raising a number to a decimal value.", token: token);
				}

				BigInteger rhs_bi = (BigInteger)rhs.WholeValue;
				value = BigDecimal.Pow(lhs, rhs_bi);
			}

			return value;
		}
	} // class
} // namespace
