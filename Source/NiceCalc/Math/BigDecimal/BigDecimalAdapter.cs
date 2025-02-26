using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;
using ExtendedNumerics.Helpers;

namespace NiceCalc.Math
{
    public static class BigDecimalAdapter
    {
        public static BigDecimal Pow(BigDecimal @base, BigDecimal exponent)
        {
            int precision = BigDecimal.Precision;
            BigDecimal.Precision = precision + 10;
            try
            {
                if (!exponent.GetFractionalPart().IsZero())
                {
                    return BigDecimal.Pow(@base, exponent, precision);
                    //throw new ArgumentException("The Pow operation does not support exponents that are not positive whole numbers.");
                }
                else
                {
                    if (@base.GetFractionalPart().IsZero())
                    {
                        return new BigDecimal(BigIntegerMaths.Pow(@base.WholeValue, exponent.WholeValue));
                    }
                    else
                    {
                        return BigDecimal.Pow(@base, exponent.WholeValue, precision);
                    }
                }
            }
            finally
            {
                BigDecimal.Precision = precision;
            }
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
