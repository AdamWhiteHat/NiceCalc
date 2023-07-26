using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceCalc
{
	public class ArrayComparer<T> : IEqualityComparer<T[]>
	{
		public bool Equals(T[] x, T[] y)
		{
			if (x == null)
			{
				if (y == null)
				{
					return true;
				}
				return false;
			}

			return (GetUniqueString(x) == GetUniqueString(y));
		}

		public int GetHashCode(T[] obj)
		{
			return GetUniqueString(obj).GetHashCode();
		}

		private static string GetUniqueString(T[] arr)
		{
			return string.Join(", ", arr.OrderBy(t => t).Select(t => t.ToString()));
		}
	}
}
