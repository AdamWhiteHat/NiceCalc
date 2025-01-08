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

        public static IToken Evaluate(Queue<IToken> postfixTokenQueue, NumericType numericType)
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
            private Stack<IToken> Stack;
            private Queue<IToken> InputQueue;

            public Evaluation(Queue<IToken> inputQueue, NumericType numericType)
            {
                InputQueue = inputQueue;
                NumericType = numericType;
                Stack = new Stack<IToken>();
            }

            public IToken Eval()
            {
                IToken token;

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

                                Queue<IToken> newQueue
                                    = new Queue<IToken>(
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
                    IToken lastToken = Stack.Pop();
                    if (lastToken.TokenType == TokenType.Number)
                    {
                       return (NumberToken)lastToken;
                    }
                    else if(lastToken .TokenType == TokenType.Literal)
                    {
                        return lastToken;
                    }
                    throw new ParsingException($"The last token on the stack was not of type {nameof(NumberToken)}.", token: lastToken, stack: Stack);

                }
                else
                {
                    throw new ParsingException("The input has too many values for the number of operators.", token: null, stack: Stack);
                }

            } // method





            private void ProcessFunction(int paramCount, IToken tokenChar)
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
                    IToken poppedToken = Stack.Pop();
                    if (!(poppedToken is NumberToken))
                    {
                        throw new ParsingException($"The last token on the stack was not of type {nameof(NumberToken)}.", token: poppedToken, stack: Stack);
                    }

                    NumberToken param1 = (NumberToken)poppedToken;

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
                        IToken resultString;

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
                    IToken poppedToken1 = Stack.Pop();
                    if (!(poppedToken1 is NumberToken))
                    {
                        throw new ParsingException($"The last token on the stack was not of type {nameof(NumberToken)}.", token: poppedToken1, stack: Stack);
                    }

                    IToken poppedToken2 = Stack.Pop();
                    if (!(poppedToken2 is NumberToken))
                    {
                        throw new ParsingException($"The last token on the stack was not of type {nameof(NumberToken)}.", token: poppedToken2, stack: Stack);
                    }

                    NumberToken param1 = (NumberToken)poppedToken1;
                    NumberToken param2 = (NumberToken)poppedToken2;

                    if (isIntegerFunction)
                    {
                        Func<BigInteger, BigInteger, BigInteger> binaryIntFunc = Functions.GetBinaryIntegerFunction(tokenChar.Symbol);

                        BigInteger result = binaryIntFunc(param1.IntegerValue, param2.IntegerValue);
                        Stack.Push(new NumberToken(result));
                    }
                    else
                    {
                        IToken resultString;
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


            private void ProcessOperation(IToken tokenChar)
            {
                if (Stack.Count < 2)
                {
                    if (tokenChar.Symbol == Syntax.UnaryNegation)
                    {
                        IToken temp = Stack.Pop();
                        if (!(temp is NumberToken))
                        {
                            throw new ParsingException($"The last token on the stack was not of type {nameof(NumberToken)}.", token: temp, stack: Stack);
                        }

                        NumberToken val = (NumberToken)temp;

                        IToken resultVal;
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

                IToken rightToken = Stack.Pop();
                if (!(rightToken is NumberToken))
                {
                    throw new ParsingException($"The last token on the stack was not of type {nameof(NumberToken)}.", token: rightToken, stack: Stack);
                }
                IToken leftToken = Stack.Pop();
                if (!(leftToken is NumberToken))
                {
                    throw new ParsingException($"The last token on the stack was not of type {nameof(NumberToken)}.", token: leftToken, stack: Stack);
                }

                NumberToken right = (NumberToken)rightToken;
                NumberToken left = (NumberToken)leftToken;

                IToken resultString;
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

            private static NumberToken Function_UnaryReal(IToken token, NumberToken parameter)
            {
                BigDecimal param1 = parameter.RealValue;

                Func<BigDecimal, BigDecimal> unaryRealFunc = NiceCalc.Execution.Implementation.Decimal.GetUnaryRealFunction(token.Symbol);

                BigDecimal result = unaryRealFunc(param1);

                return new NumberToken(result);
            }

            private static NumberToken Function_BinaryReal(IToken token, NumberToken left, NumberToken right)
            {
                Func<BigDecimal, BigDecimal, BigDecimal> binaryRealFunc = NiceCalc.Execution.Implementation.Decimal.GetBinaryRealFunction(token.Symbol);
                BigDecimal result = binaryRealFunc(left.RealValue, right.RealValue);
                return new NumberToken(result);
            }

            private static NumberToken Operation_BinaryReal(IToken token, NumberToken left, NumberToken right)
            {
                BigDecimal rhs = right.RealValue;
                BigDecimal lhs = left.RealValue;

                Func<BigDecimal, BigDecimal, BigDecimal> operation = NiceCalc.Execution.Implementation.Decimal.GetBinaryRealOperation(token.Symbol);

                BigDecimal result = operation(lhs, rhs);

                return new NumberToken(result);
            }

            #endregion

            #region Rational

            private static NumberToken Function_UnaryFraction(IToken token, NumberToken parameter)
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

            private static NumberToken Function_BinaryFraction(IToken token, NumberToken left, NumberToken right)
            {
                if (!NiceCalc.Execution.Implementation.Rational.IsFunctionTokenSupported(token.Symbol))
                {
                    return Function_BinaryReal(token, left, right);
                }

                Func<BigInteger, BigInteger, Fraction> binaryRealFunc = NiceCalc.Execution.Implementation.Rational.GetBinaryRationalFunction(token.Symbol);
                Fraction result = binaryRealFunc(left.IntegerValue, right.IntegerValue);
                return new NumberToken(result);
            }

            private static NumberToken Operation_BinaryFraction(IToken token, NumberToken left, NumberToken right)
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
