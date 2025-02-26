using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;
using Newtonsoft.Json.Linq;
using NiceCalc.Interpreter.Language;
using NiceCalc.Math;

namespace NiceCalc.Tokenization
{
    public struct NumberToken : IToken
    {
        public bool IsNegative;
        public bool IsInteger;

        public BigDecimal RealValue { get { return (_realValue.HasValue) ? _realValue.Value : (_realValue = _realEvaluationFunc()).Value; } }
        public Fraction RationalValue { get { return (_rationalValue.HasValue) ? _rationalValue.Value : (_rationalValue = _rationalEvaluationFunc()).Value; } }
        public BigInteger IntegerValue { get { return (_integerValue.HasValue) ? _integerValue.Value : (_integerValue = _integerEvaluationFunc()).Value; } }

        private BigDecimal? _realValue;
        private Fraction? _rationalValue;
        private BigInteger? _integerValue;

        private Func<BigDecimal> _realEvaluationFunc;
        private Func<Fraction> _rationalEvaluationFunc;
        private Func<BigInteger> _integerEvaluationFunc;

        private static string UnknownNumericType = "Unknown Numeric type to NumberToken. Handling of this type needs to be added.";
        private static string IntegerNoRepresentation_ExceptionMessage = $"The integer value of this token should not have been evaluated; There is no integer representation. Check the {nameof(IsInteger)} property first to avoid this error in the future.";


        public string Text;
        public char Symbol { get { return '#'; } }
        public TokenType TokenType { get { return TokenType.Number; } }

        public NumericType PreferredNumericType { get; private set; }

        public NumberToken(BigInteger integerValue)
        {
            PreferredNumericType = NumericType.Integer;
            IsInteger = true;
            BigInteger localValue = integerValue;
            Text = localValue.ToString();
            IsNegative = (localValue.Sign == -1);

            _integerValue = localValue;
            _realValue = null;
            _rationalValue = null;

            _integerEvaluationFunc = () => localValue;
            _rationalEvaluationFunc = () => new Fraction(localValue);
            _realEvaluationFunc = () => new BigDecimal(mantissa: localValue, exponent: 0);
        }

        public NumberToken(BigDecimal realValue)
        {
            PreferredNumericType = NumericType.Real;
            IsInteger = false;
            BigDecimal localValue = realValue;
            Text = localValue.ToString();
            IsNegative = (localValue.Sign == -1);
            _realValue = localValue;

            _realEvaluationFunc = () => localValue;
            _integerValue = null;
            _rationalValue = null;

            if (localValue.Equals(new BigDecimal(mantissa: localValue.WholeValue, exponent: 0)))
            {
                _rationalEvaluationFunc = () => new Fraction(localValue.WholeValue, BigInteger.One);
                _integerEvaluationFunc = () => localValue.WholeValue;
                IsInteger = true;
            }
            else
            {
                _rationalEvaluationFunc = () => ContinuedFraction.ConvertDecimalToFraction(localValue);
                _integerEvaluationFunc = () => throw new InvalidOperationException(IntegerNoRepresentation_ExceptionMessage);
            }
        }

        public NumberToken(Fraction rationalValue)
        {
            PreferredNumericType = NumericType.Rational;
            IsInteger = false;
            Fraction localValue = rationalValue;
            Text = localValue.ToString();
            IsNegative = (localValue.Sign == -1);
            _rationalValue = localValue;

            _rationalEvaluationFunc = () => localValue;
            _realValue = null;
            _integerValue = null;

            _realEvaluationFunc = () => new BigDecimal(numerator: localValue.Numerator, denominator: localValue.Denominator);
            if (localValue.Denominator == BigInteger.One)
            {
                _integerEvaluationFunc = () => localValue.Numerator;
                IsInteger = true;
            }
            else
            {
                _integerEvaluationFunc = () => throw new InvalidOperationException(IntegerNoRepresentation_ExceptionMessage);
            }
        }

        public static bool operator ==(NumberToken left, NumberToken right)
        {
            if (left.PreferredNumericType != right.PreferredNumericType)
            {
                return false;
            }
            if (left.PreferredNumericType == NumericType.Real)
            {
                return left.RealValue == right.RealValue;
            }
            else if (left.PreferredNumericType == NumericType.Rational)
            {
                return left.RationalValue == right.RationalValue;
            }
            else if (left.PreferredNumericType == NumericType.Integer)
            {
                return left.IntegerValue == right.IntegerValue;
            }
            else
            {
                throw new Exception(UnknownNumericType);
            }
        }
        public static bool operator !=(NumberToken left, NumberToken right) => !(left == right);

        public override string ToString()
        {
            return Text;
        }

        public static class Factory
        {
            public static NumberToken Parse(string digits)
            {
                NumericType type = QueryNumericType(digits);

                if (type == NumericType.Rational)
                {
                    return new NumberToken(Fraction.Parse(digits));
                }
                else if (type == NumericType.Real)
                {
                    return new NumberToken(BigDecimal.Parse(digits));
                }
                else if (type == NumericType.Integer)
                {
                    return new NumberToken(BigInteger.Parse(digits));
                }
                else
                {
                    throw new Exception(UnknownNumericType);
                }
            }
        }

        public static NumericType QueryNumericType(string value)
        {
            if (value.Contains('/'))
            {
                return NumericType.Rational;
            }
            else if (value.Contains(Syntax.NumberDecimalSeparator))
            {
                return NumericType.Real;
            }
            else
            {
                return NumericType.Integer;
            }
        }


    }
}
