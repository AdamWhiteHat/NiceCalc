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
using NiceCalc.Interpreter.Language;
using System.Reflection.Metadata;
using System.Data.Common;

namespace NiceCalc.Execution
{
	public static class PostfixNotation
	{
		private static readonly string AllowedCharacters = Syntax.Numbers + Syntax.Operators + Syntax.Functions + " /";

		public static string Evaluate(string postfixNotationString, NumericType numericType)
		{
			if (string.IsNullOrWhiteSpace(postfixNotationString))
			{
				throw new ParsingException($"Argument {nameof(postfixNotationString)} must not be null, empty or whitespace.");
			}

			var unknownCharacters = postfixNotationString.Where(c => !AllowedCharacters.Contains(c));
			if (unknownCharacters.Any())
			{
				throw new ParsingException($"Argument {nameof(postfixNotationString)} contains some unknown tokens: {{ {string.Join(", ", unknownCharacters)} }}.");
			}

			string sanitizedString = new string(postfixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());

			List<string> enumerablePostfixTokens = sanitizedString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			Stack<string> stack = new Stack<string>();
			foreach (string token in enumerablePostfixTokens)
			{
				if (token.Length > 0)
				{
					if (token.Length > 1)
					{
						if (Syntax.IsNumeric(token))
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

						if (Syntax.Numbers.Contains(tokenChar))
						{
							stack.Push(tokenChar.ToString());
						}
						else if (Syntax.Functions.Contains(tokenChar))
						{
							int paramCount = Functions.GetParameterCount(tokenChar);

							if (stack.Count < paramCount)
							{
								throw new ParsingException($"Insufficient number of parameters on the stack for the function token '{tokenChar}'; Was expecting {paramCount}, but only {stack.Count} items on the stack.", token: tokenChar, stack: stack);
							}

							bool useIntegers = Functions.IsFunctionIntegers(tokenChar);

							if (paramCount == 1)
							{
								string param1String = stack.Pop();

								if (useIntegers)
								{
									BigInteger param1 = BigInteger.Parse(param1String);
									Func<BigInteger, BigInteger> unaryIntFunc = Functions.GetUnaryIntegerFunction(tokenChar);

									BigInteger result = unaryIntFunc(param1);
									stack.Push(result.ToString());
								}
								else
								{
									string resultString = string.Empty;

									if (numericType == NumericType.Real)
									{
										resultString = Function_UnaryReal(tokenChar, param1String);
									}
									else
									{
										resultString = Function_UnaryFraction(tokenChar, param1String);
									}

									stack.Push(resultString);
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
									Func<BigInteger, BigInteger, BigInteger> binaryIntFunc = Functions.GetBinaryIntegerFunction(tokenChar);

									BigInteger result = binaryIntFunc(param1, param2);
									stack.Push(result.ToString());
								}
								else
								{
									string resultString = string.Empty;

									if (numericType == NumericType.Real)
									{
										resultString = Function_BinaryReal(tokenChar, param1, param2);
									}
									else
									{
										resultString = Function_BinaryFraction(tokenChar, param1, param2);
									}

									stack.Push(resultString);
								}
							}
							else
							{
								throw new ParsingException($"Functions with {paramCount} parameters not implemented yet. Did you add a new function to parser but not the evaluator?", token: tokenChar, stack: stack);
							}
						}
						else if (Syntax.Operators.Contains(tokenChar))
						{
							if (stack.Count < 2)
							{
								throw new ParsingException("The algebraic string has not sufficient values in the expression for the number of operators.", token: tokenChar, stack: stack);
							}

							string right = stack.Pop();
							string left = stack.Pop();

							string resultString = string.Empty;

							if (numericType == NumericType.Real)
							{
								resultString = Operation_BinaryReal(tokenChar, left, right);
							}
							else
							{
								resultString = Operation_BinaryFraction(tokenChar, left, right);
							}

							stack.Push(resultString);
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
				return stack.Pop();
			}
			else
			{
				throw new ParsingException("The input has too many values for the number of operators.", token: "(none)", stack: stack);
			}

		} // method


		#region Real

		private static string Function_UnaryReal(char token, string parameter)
		{
			BigDecimal param1 = ConvertToRealIfNeeded(parameter);

			Func<BigDecimal, BigDecimal> unaryRealFunc = NiceCalc.Execution.Implementation.Decimal.GetUnaryRealFunction(token);

			BigDecimal result = unaryRealFunc(param1);

			return result.ToString();
		}

		private static string Function_BinaryReal(char token, BigInteger left, BigInteger right)
		{
			Func<BigInteger, BigInteger, BigDecimal> binaryRealFunc = NiceCalc.Execution.Implementation.Decimal.GetBinaryRealFunction(token);
			BigDecimal result = binaryRealFunc(left, right);
			return result.ToString();
		}

		private static string Operation_BinaryReal(char token, string left, string right)
		{
			BigDecimal rhs = ConvertToRealIfNeeded(right);
			BigDecimal lhs = ConvertToRealIfNeeded(left);

			Func<BigDecimal, BigDecimal, BigDecimal> operation = NiceCalc.Execution.Implementation.Decimal.GetBinaryOperation(token);

			BigDecimal result = operation(lhs, rhs);

			return result.ToString();
		}

		#endregion

		#region Rational

		private static string Function_UnaryFraction(char token, string parameter)
		{
			if (!NiceCalc.Execution.Implementation.Rational.IsFunctionTokenSupported(token))
			{
				return Function_UnaryReal(token, parameter);
			}

			Fraction param1 = ConvertToRationalIfNeeded(parameter);

			Func<Fraction, Fraction> unaryRealFunc = NiceCalc.Execution.Implementation.Rational.GetUnaryRealFunction(token);

			Fraction result = unaryRealFunc(param1);

			return result.ToString();
		}

		private static string Function_BinaryFraction(char token, BigInteger left, BigInteger right)
		{
			if (!NiceCalc.Execution.Implementation.Rational.IsFunctionTokenSupported(token))
			{
				return Function_BinaryReal(token, left, right);
			}

			Func<BigInteger, BigInteger, Fraction> binaryRealFunc = NiceCalc.Execution.Implementation.Rational.GetBinaryRealFunction(token);
			Fraction result = binaryRealFunc(left, right);
			return result.ToString();
		}

		private static string Operation_BinaryFraction(char token, string left, string right)
		{
			Fraction rhs = ConvertToRationalIfNeeded(right);
			Fraction lhs = ConvertToRationalIfNeeded(left);

			Func<Fraction, Fraction, Fraction> operation = NiceCalc.Execution.Implementation.Rational.GetBinaryOperation(token);

			Fraction result = operation(lhs, rhs);

			return result.ToString();
		}

		#endregion

		#region Conversion

		private static BigDecimal ConvertToRealIfNeeded(string stringValue)
		{
			string type = "Unknown";

			try
			{
				if (stringValue.Contains('.'))
				{
					type = nameof(BigDecimal);
					return BigDecimal.Parse(stringValue);
				}
				else if (stringValue.Contains('/'))
				{
					type = nameof(Fraction);
					Fraction toConvert = Fraction.Parse(stringValue);
					return new BigDecimal(numerator: toConvert.Numerator, denominator: toConvert.Denominator);
				}
				else
				{
					type = nameof(BigInteger);
					BigInteger toConvert = BigInteger.Parse(stringValue);
					return new BigDecimal(mantissa: toConvert, exponent: 0);
				}
			}
			catch
			{
				throw new ParsingException($"Unable to parse the parameter into a {type}: \"{stringValue}\".", token: stringValue);
			}
		}

		private static Fraction ConvertToRationalIfNeeded(string stringValue)
		{
			string type = "Unknown";

			try
			{
				if (stringValue.Contains('.'))
				{
					type = nameof(BigDecimal);

					BigDecimal toConvert = BigDecimal.Parse(stringValue);

					BigInteger denominator = BigInteger.Pow(10, toConvert.Exponent);

					return new Fraction(toConvert.Mantissa, denominator);

				}
				else if (stringValue.Contains('/'))
				{
					type = nameof(Fraction);
					return Fraction.Parse(stringValue);
				}
				else
				{
					type = nameof(BigInteger);
					return new Fraction(BigInteger.Parse(stringValue));
				}
			}
			catch
			{
				throw new ParsingException($"Unable to parse the parameter into a {type}: \"{stringValue}\".", token: stringValue);
			}
		}

		#endregion

	} // class
} // namespace
