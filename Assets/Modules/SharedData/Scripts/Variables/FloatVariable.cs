using UnityEngine;

namespace SharedData
{

	[CreateAssetMenu(fileName = "FloatVariable", menuName = "SharedData/Variable/Float", order = 1)]
	public class FloatVariable : SharedVariableTemplate<float> 
	{
		public static FloatVariable operator ++(FloatVariable a)
		{
			a.value++;
			return a;
		}

		public static FloatVariable operator --(FloatVariable a)
		{
			a.value--;
			return a;
		}

		public static FloatVariable operator +(FloatVariable a, FloatVariable b)
		{
			a.value += b.value;
			return a;
		}

		public static FloatVariable operator -(FloatVariable a, FloatVariable b)
		{
			a.value -= b.value;
			return a;
		}

		public static FloatVariable operator *(FloatVariable a, FloatVariable b)
		{
			a.value *= b.value;
			return a;
		}

		public static FloatVariable operator /(FloatVariable a, FloatVariable b)
		{
			a.value /= b.value;
			return a;
		}

		public static implicit operator float(FloatVariable v)
		{
			return v.value;
		}

		public static implicit operator int(FloatVariable v)
		{
			return (int)v.value;
		}
	}

}