using UnityEngine;

namespace SharedData
{

	[CreateAssetMenu(fileName = "IntVariable", menuName = "SharedData/Variable/Integer", order = 1)]
	public class IntVariable : SharedVariableTemplate<int> 
	{
		public static IntVariable operator ++(IntVariable a)
		{
			a.value++;
			return a;
		}

		public static IntVariable operator --(IntVariable a)
		{
			a.value--;
			return a;
		}

		public static IntVariable operator +(IntVariable a, IntVariable b)
		{
			a.value += b.value;
			return a;
		}

		public static IntVariable operator -(IntVariable a, IntVariable b)
		{
			a.value -= b.value;
			return a;
		}

		public static IntVariable operator *(IntVariable a, IntVariable b)
		{
			a.value *= b.value;
			return a;
		}

		public static IntVariable operator /(IntVariable a, IntVariable b)
		{
			a.value /= b.value;
			return a;
		}

		public static implicit operator float(IntVariable v)
		{
			return v.value;
		}

		public static implicit operator int(IntVariable v)
		{
			return v.value;
		}
	}

}