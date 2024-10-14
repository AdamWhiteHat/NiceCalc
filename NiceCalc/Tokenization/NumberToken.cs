using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;
using NiceCalc.Interpreter.Language;
using NiceCalc.Math;

namespace NiceCalc.Tokenization
{
    public class NumberToken : Token
    {
        public bool IsNegative;
        public bool IsInteger;

        public BigDecimal RealValue;
        public Fraction RationalValue;
        public BigInteger IntegerValue;

        public string Text;
        public override char Symbol { get { return '#'; } }
        public override TokenType TokenType { get { return TokenType.Number; } }

        public NumericType PreferredNumericType { get; private set; }

        public NumberToken(BigInteger integerValue)
        {
            SetIntegerValue(integerValue);
        }

        public NumberToken(BigDecimal realValue)
        {
            SetRealValue(realValue);
        }

        public NumberToken(Fraction rationalValue)
        {
            SetRationalValue(rationalValue);
        }

        public NumberToken(string digits)
        {
            Text = digits;

            NumericType type = QueryNumericType(digits);

            if (type == NumericType.Rational)
            {
                SetRationalValue(Fraction.Parse(Text));
            }
            else if (type == NumericType.Real)
            {
                SetRealValue(BigDecimal.Parse(Text));
            }
            else if (type == NumericType.Integer)
            {
                SetIntegerValue(BigInteger.Parse(Text));
            }
            else
            {
                throw new Exception("Unknown Numeric type to NumberToken. Handling of this type needs to be added.");
            }
        }

        private void SetIntegerValue(BigInteger value)
        {
            PreferredNumericType = NumericType.Integer;
            IntegerValue = value;
            Text = IntegerValue.ToString();
            IsNegative = (IntegerValue.Sign == -1);

            RationalValue = new Fraction(IntegerValue);
            RealValue = new BigDecimal(mantissa: IntegerValue, exponent: 0);
            IsInteger = true;
        }

        private void SetRealValue(BigDecimal value)
        {
            PreferredNumericType = NumericType.Real;
            IsInteger = false;
            RealValue = value;
            Text = RealValue.ToString();
            IsNegative = (RealValue.Sign == -1);

            if (RealValue.Equals(new BigDecimal(mantissa: RealValue.WholeValue, exponent: 0)))
            {
                IntegerValue = RealValue.WholeValue;
                RationalValue = new Fraction(IntegerValue, BigInteger.One);
                IsInteger = true;
            }
            else
            {
                RationalValue = ContinuedFraction.ConvertDecimalToFraction(RealValue);
            }
        }

        private void SetRationalValue(Fraction value)
        {
            PreferredNumericType = NumericType.Rational;
            IsInteger = false;
            RationalValue = value;
            Text = RationalValue.ToString();
            IsNegative = (RationalValue.Sign == -1);

            RealValue = new BigDecimal(numerator: RationalValue.Numerator, denominator: RationalValue.Denominator);
            if (RationalValue.Denominator == BigInteger.One)
            {
                IntegerValue = RationalValue.Numerator;
                IsInteger = true;
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

        public override string ToString()
        {
            return Text;
        }
    }
}
