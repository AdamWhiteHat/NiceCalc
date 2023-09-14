using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc.Math
{
	public static class Combinatorics
	{
		public static BigInteger Choose(int n, int k)
		{
			if (n == k)
			{
				return BigIntegerMaths.Factorial(n);
			}

			//      n!
			//  ----------
			//  k!(n − k)!
			return BigInteger.Divide(
						BigIntegerMaths.Factorial(n),
						BigInteger.Multiply(BigIntegerMaths.Factorial(k), BigIntegerMaths.Factorial(n - k))
					);
		}

		public static IEnumerable<T[]> GetAllPossibleCombinations<T>(IEnumerable<T> input)
		{
			List<T[]> results = new List<T[]>();

			int counter = 1;
			while (counter <= input.Count())
			{
				var combinations = Combinatorics.GetCombinations<T>(input, counter);
				results.AddRange(combinations);
				counter++;
			}

			return results.Distinct(new ArrayComparer<T>());
		}

		public static IEnumerable<T[]> GetCombinations<T>(IEnumerable<T> input, int take)
		{
			List<T> copy = input.ToList();
			int n = copy.Count();
			var result = new int[take];
			var stack = new Stack<int>();
			stack.Push(0);

			while (stack.Count > 0)
			{
				int index = stack.Count - 1;
				int value = stack.Pop();
				while (value < n)
				{
					result[index++] = value++;
					stack.Push(value);
					if (index == take)
					{
						yield return result.Select(i => copy[i]).ToArray();
						break;
					}
				}
			}
			yield break;
		}
	}
}
