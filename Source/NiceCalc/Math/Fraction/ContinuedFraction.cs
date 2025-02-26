using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Math
{
    public static class ContinuedFraction
    {
        public static Fraction ConvertDecimalToFraction(BigDecimal value, int precision = -1)
        {
            int decimalDigitCount = value.DecimalPlaces;
            if (decimalDigitCount < 4)
            {
                var muliplier = BigInteger.Pow(10, decimalDigitCount);
                var numerator = value * muliplier;
                return new Fraction((BigInteger)numerator, muliplier);
            }

            BigInteger[] array = FromDecimal(value.GetWholePart(), value.GetFractionalPart(), precision);

            Tuple<BigInteger, BigInteger> result = ToRational(array);

            return new Fraction(result.Item1, result.Item2);
        }

        private static BigInteger[] FromDecimal(BigInteger wholePart, BigDecimal fractionalPart, int precision = -1)
        {
            List<BigInteger> result = new List<BigInteger>();

            if (fractionalPart.ToString().Count() < 3)
            {
                return result.ToArray();
            }
            if (BigInteger.Abs(wholePart) > -1)
            {
                result.Add(wholePart);
            }

            int prec = precision;
            string len = fractionalPart.ToString().Substring(2);
            if (precision <= 0)
            {
                prec = len.Count() + 1;
                if (prec % 2 == 1)
                {
                    prec -= 1;
                }
            }

            BigDecimal next = 0;
            BigDecimal last = 0;
            BigDecimal currentValue = fractionalPart;

            int currentValue_TensColumn = 1;
            int counter = prec;
            do
            {
                currentValue = BigDecimal.One / currentValue;
                next = new BigDecimal(currentValue.WholeValue);

                currentValue = BigDecimal.Subtract(currentValue, next);
                if (next > 0 && currentValue > 0)
                {
                    BigInteger leftOfDecimalPoint = (BigInteger)next;
                    result.Add(leftOfDecimalPoint);

                    int b = next.ToString().Count();
                    counter -= b;
                }
                else
                {
                    break;
                }

                currentValue_TensColumn = int.Parse(currentValue.ToString().Substring(2, 1));
                last = new BigDecimal(next.Mantissa, next.Exponent);
            }
            while (next > 0 && currentValue > 0 && counter > 0);

            if (result.Count > 1 && last == 1 && currentValue_TensColumn != 0)
            {
                BigInteger final = result.Last();
                result.RemoveAt(result.Count - 1);
                BigInteger temp = (BigInteger)last;
                result.Add(final + temp);
            }

            return result.ToArray();
        }


        private static Tuple<BigInteger, BigInteger> ToRational(BigInteger[] continuedFraction)
        {
            BigInteger numerator = 0;
            BigInteger denominator = 1;

            List<BigInteger> fraction = continuedFraction.ToList();

            foreach (BigInteger digit in fraction)
            {
                if (numerator == 0)
                {
                    numerator = digit;
                }
                else
                {
                    Swap(ref numerator, ref denominator);
                    numerator += digit * denominator;
                }
            }
            return new Tuple<BigInteger, BigInteger>(numerator, denominator);
        }

        private static void Swap(ref BigInteger left, ref BigInteger right)
        {
            BigInteger swap = left;
            left = right;
            right = swap;
        }

        public static string FormatString(BigInteger[] continuedFraction)
        {
            if (!continuedFraction.Any()) return "(empty)";
            return $"[{continuedFraction.First()};{string.Join(",", continuedFraction.Skip(1).Select(n => n.ToString()))}]";
        }
    }
}
