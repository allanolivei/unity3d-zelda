using UnityEngine;
using System;

namespace SharedData
{
	
	[Serializable]
	public class FloatReference : VariableReference<FloatVariable, float>
	{
		public static implicit operator int(FloatReference reference)
		{
			return (int)reference.value;
		}

		public static implicit operator float(FloatReference reference)
		{
			return reference.value;
		}

		public static implicit operator string(FloatReference reference)
		{
			return reference.value.ToString();
		}
	}

}