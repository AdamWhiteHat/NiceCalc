using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;

namespace NiceCalc.Math
{
    public static class BigDecimalMaths
    {
        /// <summary>
        /// Exponentiation by squaring, modulus some number (as needed)
        /// </summary>
        public static BigDecimal PowerMod(BigDecimal @base, BigInteger exponent, BigDecimal modulus)
        {
            BigDecimal result = BigDecimal.One;
            while (exponent > 0)
            {
                if ((exponent % 2) == 1) // If exponent is odd
                {
                    result = (result * @base).Mod(modulus);
                    exponent -= 1;
                    if (exponent == 0) { break; }
                }

                @base = (@base * @base).Mod(modulus);
                exponent /= 2; // exponent /= 2;
            }
            return result.Mod(modulus);
        }

        /// <summary>
        /// Exponentiation by squaring
        /// </summary>
        public static BigDecimal Pow(BigDecimal @base, BigInteger exponent)
        {
            BigDecimal result = BigDecimal.One;
            while (exponent > 0)
            {
                if ((exponent % 2) == 1) // If exponent is odd
                {
                    result = (result * @base);
                    exponent -= 1;
                    if (exponent == 0) { break; }
                }

                @base = (@base * @base);
                exponent /= 2; // exponent /= 2;
            }
            return result;
        }
    }
}
