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
using System.Data.Common;
using System.Windows.Forms;
using NiceCalc.Tokenization;

namespace NiceCalc.Execution
{
    public static class PostfixNotation
    {
        private static readonly string AllowedCharacters = Syntax.Numbers + Syntax.Operators + Syntax.Functions + " /";

        public static NumberToken Evaluate(Queue<Token> postfixTokenQueue, NumericType numericType)
        {
            if (postfixTokenQueue == null || postfixTokenQueue.Count < 1)
            {
                throw new ParsingException($"Argument {nameof(postfixTokenQueue)} must not be null or empty.");
            }

            Evaluation evaluation = new Evaluation(postfixTokenQueue, numericType);

            return evaluation.Eval();
        }




        private class Evaluation
        {
            private NumericType NumericType;
            private Stack<Token> Stack;
            private Queue<Token> InputQueue;

            public Evaluation(Queue<Token> inputQueue, NumericType numericType)
            {
                InputQueue = inputQueue;
                NumericType = numericType;
                Stack = new Stack<Token>();
            }

            public NumberToken Eval()
            {
                Token token;

                while (InputQueue.Any())
                {
                    token = InputQueue.Dequeue();

                    //char tokenChar = token[0];

                    if (token.TokenType == TokenType.Number)
                    {
                        Stack.Push(token);
                    }
                    else if (token.TokenType == TokenType.Function)
                    {
                        FunctionToken fToken = token as FunctionToken;

                        int paramCount = fToken.ParameterCount;

                        bool proceedToOperation = false;
                        if (paramCount == -1)
                        {
                            paramCount = 2;

                            if (fToken.Symbol == Syntax.Sum)
                            {
                                token = new OperatorToken('+');
                                proceedToOperation = true;
                            }
                            else if (token.Symbol == Syntax.Product)
                            {
                                token = new OperatorToken('*');
                                proceedToOperation = true;
                            }

                            if (InputQueue.Count < (Stack.Count - 1))
                            {
                                int quantity = Stack.Count - 2;

                                Queue<Token> newQueue
                                    = new Queue<Token>(
                                       Enumerable.Repeat(token, quantity)
                                       .Concat(InputQueue)
                                    );

                                InputQueue = newQueue;
                            }
                        }

                        if (proceedToOperation)
                        {
                            ProcessOperation(token);
                        }
                        else
                        {
                            ProcessFunction(paramCount, token);
                        }
                    }
                    else if (token.TokenType == TokenType.Operation)
                    {
                        ProcessOperation(token);
                    }
                    else
                    {
                        throw new ParsingException($"Unrecognized character '{token.ToString()}'.", token: token, stack: Stack);
                    }

                }

                if (Stack.Count == 1)
                {
                    Token lastToken = Stack.Pop();
                    NumberToken result = lastToken as NumberToken;
                    if (result == null)
                    {
                        throw new ParsingException($"The last token on the stack was not of type {nameof(NumberToken)}.", token: lastToken, stack: Stack);
                    }
                    return result;
                }
                else
                {
                    throw new ParsingException("The input has too many values for the number of operators.", token: null, stack: Stack);
                }

            } // method





            private void ProcessFunction(int paramCount, Token tokenChar)
            {
                if (Stack.Count < paramCount)
                {
                    throw new ParsingException($"Insufficient number of parameters on the stack for the function token '{tokenChar}'; Was expecting {paramCount}, but only {Stack.Count} items on the stack.", token: tokenChar, stack: Stack);
                }

                bool isStringFunction = Functions.IsStringFunction(tokenChar.Symbol);
                bool isIntegerFunction = Functions.IsIntegerFunction(tokenChar.Symbol);


                if (paramCount == 0)
                {
                    if (tokenChar.Symbol == Syntax.Pi)
                    {
                        Stack.Push(tokenChar);
                        return;
                    }
                    else if (tokenChar.Symbol == Syntax.E)
                    {
                        Stack.Push(tokenChar);
                        return;
                    }
                }
                else if (paramCount == 1)
                {
                    NumberToken param1 = Stack.Pop() as NumberToken;

                    if (isIntegerFunction)
                    {
                        Func<BigInteger, BigInteger> unaryIntFunc = Functions.GetUnaryIntegerFunction(tokenChar.Symbol);

                        BigInteger result = unaryIntFunc(param1.IntegerValue);
                        Stack.Push(new NumberToken(result));
                    }
                    else if (isStringFunction)
                    {
                        Func<BigInteger, string> stringFunction = Functions.GetUnaryStringFunction(tokenChar.Symbol);

                        string results = stringFunction(param1.IntegerValue);
                        Stack.Push(new LiteralToken(results));
                    }
                    else
                    {
                        Token resultString;

                        if (NumericType == NumericType.Real)
                        {
                            resultString = Function_UnaryReal(tokenChar, param1);
                        }
                        else
                        {
                            resultString = Function_UnaryFraction(tokenChar, param1);
                        }

                        Stack.Push(resultString);
                    }
                }
                else if (paramCount == 2)
                {
                    NumberToken param1 = Stack.Pop() as NumberToken;
                    NumberToken param2 = Stack.Pop() as NumberToken;

                    if (isIntegerFunction)
                    {
                        Func<BigInteger, BigInteger, BigInteger> binaryIntFunc = Functions.GetBinaryIntegerFunction(tokenChar.Symbol);

                        BigInteger result = binaryIntFunc(param1.IntegerValue, param2.IntegerValue);
                        Stack.Push(new NumberToken(result));
                    }
                    else
                    {
                        Token resultString;
                        if (NumericType == NumericType.Real)
                        {
                            resultString = Function_BinaryReal(tokenChar, param1, param2);
                        }
                        else
                        {
                            resultString = Function_BinaryFraction(tokenChar, param1, param2);
                        }

                        Stack.Push(resultString);
                    }
                }
                else
                {
                    throw new ParsingException($"Functions with {paramCount} parameters not implemented yet. Did you add a new function to parser but not the evaluator?", token: tokenChar, stack: Stack);
                }
            }


            private void ProcessOperation(Token tokenChar)
            {
                if (Stack.Count < 2)
                {
                    if (tokenChar.Symbol == Syntax.UnaryNegation)
                    {
                        Token temp = Stack.Pop();
                        NumberToken val = temp as NumberToken;

                        Token resultVal;
                        if (val.PreferredNumericType == NumericType.Rational)
                        {
                            resultVal = Function_UnaryFraction(tokenChar, val);
                        }
                        else
                        {
                            resultVal = Function_UnaryReal(tokenChar, val);
                        }

                        Stack.Push(resultVal);
                        return;
                    }

                    throw new ParsingException("The algebraic string has not sufficient values in the expression for the number of operators.", token: tokenChar, stack: Stack);
                }

                Token rightToken = Stack.Pop();
                Token leftToken = Stack.Pop();

                NumberToken right = rightToken as NumberToken;
                NumberToken left = leftToken as NumberToken;

                Token resultString;
                if (right.PreferredNumericType == NumericType.Rational && left.PreferredNumericType == NumericType.Rational)
                {
                    resultString = Operation_BinaryFraction(tokenChar, left, right);
                }
                else
                {
                    resultString = Operation_BinaryReal(tokenChar, left, right);
                }

                Stack.Push(resultString);
            }

            #region Real

            private static NumberToken Function_UnaryReal(Token token, NumberToken parameter)
            {
                BigDecimal param1 = parameter.RealValue;

                Func<BigDecimal, BigDecimal> unaryRealFunc = NiceCalc.Execution.Implementation.Decimal.GetUnaryRealFunction(token.Symbol);

                BigDecimal result = unaryRealFunc(param1);

                return new NumberToken(result);
            }

            private static NumberToken Function_BinaryReal(Token token, NumberToken left, NumberToken right)
            {
                Func<BigDecimal, BigDecimal, BigDecimal> binaryRealFunc = NiceCalc.Execution.Implementation.Decimal.GetBinaryRealFunction(token.Symbol);
                BigDecimal result = binaryRealFunc(left.RealValue, right.RealValue);
                return new NumberToken(result);
            }

            private static NumberToken Operation_BinaryReal(Token token, NumberToken left, NumberToken right)
            {
                BigDecimal rhs = right.RealValue;
                BigDecimal lhs = left.RealValue;

                Func<BigDecimal, BigDecimal, BigDecimal> operation = NiceCalc.Execution.Implementation.Decimal.GetBinaryRealOperation(token.Symbol);

                BigDecimal result = operation(lhs, rhs);

                return new NumberToken(result);
            }

            #endregion

            #region Rational

            private static NumberToken Function_UnaryFraction(Token token, NumberToken parameter)
            {
                if (!NiceCalc.Execution.Implementation.Rational.IsFunctionTokenSupported(token.Symbol))
                {
                    return Function_UnaryReal(token, parameter);
                }

                Fraction param1 = parameter.RationalValue;

                Func<Fraction, Fraction> unaryRealFunc = NiceCalc.Execution.Implementation.Rational.GetUnaryRationalFunction(token.Symbol);

                Fraction result = unaryRealFunc(param1);

                return new NumberToken(result);
            }

            private static NumberToken Function_BinaryFraction(Token token, NumberToken left, NumberToken right)
            {
                if (!NiceCalc.Execution.Implementation.Rational.IsFunctionTokenSupported(token.Symbol))
                {
                    return Function_BinaryReal(token, left, right);
                }

                Func<BigInteger, BigInteger, Fraction> binaryRealFunc = NiceCalc.Execution.Implementation.Rational.GetBinaryRationalFunction(token.Symbol);
                Fraction result = binaryRealFunc(left.IntegerValue, right.IntegerValue);
                return new NumberToken(result);
            }

            private static NumberToken Operation_BinaryFraction(Token token, NumberToken left, NumberToken right)
            {
                Fraction rhs = right.RationalValue;
                Fraction lhs = left.RationalValue;

                Func<Fraction, Fraction, Fraction> operation = NiceCalc.Execution.Implementation.Rational.GetBinaryRationalOperation(token.Symbol);

                Fraction result = operation(lhs, rhs);

                return new NumberToken(result);
            }

            #endregion

            /*
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
			*/
        }

    } // class
} // namespace
