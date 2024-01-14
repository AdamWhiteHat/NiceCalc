using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;
using NiceCalc.Interpreter.Language;

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

			if (Text.Contains('/'))
			{
				SetRationalValue(Fraction.Parse(Text));
			}
			else if (Text.Contains(Syntax.NumberDecimalSeparator))
			{
				SetRealValue(BigDecimal.Parse(Text));
			}
			else
			{
				SetIntegerValue(BigInteger.Parse(Text));
			}
		}

		private void SetIntegerValue(BigInteger value)
		{
			IntegerValue = value;
			IsNegative = (IntegerValue.Sign == -1);

			RationalValue = new Fraction(IntegerValue);
			RealValue = new BigDecimal(mantissa: IntegerValue, exponent: 0);
			IsInteger = true;
		}

		private void SetRealValue(BigDecimal value)
		{
			IsInteger = false;
			RealValue = value;
			IsNegative = (RealValue.Sign == -1);

			if (RealValue.Equals(new BigDecimal(mantissa: RealValue.WholeValue, exponent: 0)))
			{
				IntegerValue = RealValue.WholeValue;
				RationalValue = new Fraction(IntegerValue, BigInteger.One);
				IsInteger = true;
			}
		}

		private void SetRationalValue(Fraction value)
		{
			IsInteger = false;
			RationalValue = value;
			IsNegative = (RationalValue.Sign == -1);

			RealValue = new BigDecimal(numerator: RationalValue.Numerator, denominator: RationalValue.Denominator);
			if (RationalValue.Denominator == BigInteger.One)
			{
				IntegerValue = RationalValue.Numerator;
				IsInteger = true;
			}
		}



		public override string ToString()
		{
			return Text;
		}
	}
}
