using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;

namespace NiceCalc.Math
{
    public static class BigDecimalAdapter
    {
        public static BigDecimal Pow(BigDecimal @base, BigDecimal exponent)
        {
            if (!exponent.GetFractionalPart().IsZero())
            {
                throw new ArgumentException("The Pow operation does not support exponents that are not positive whole numbers.");
            }

            BigInteger exp = exponent.WholeValue;
            return BigDecimal.Pow(@base, exp, BigDecimal.Precision);
        }

        /// <summary>
        /// Factorial function: n! = 1 * 2 * ... * n-1 * n
        /// Only supports integer arguments
        /// </summary>
        public static BigDecimal Factorial(BigDecimal value)
        {
            if (!value.GetFractionalPart().IsZero() || value.Sign == -1)
            {
                throw new ArgumentException("The Factorial function does only supports positive whole number arguments.");
            }

            BigInteger parameter = value.WholeValue;
            return new BigDecimal(numerator: BigIntegerMaths.Factorial(parameter), denominator: 1);

        }
    }

}
