using UnityEngine;
using System;

namespace SharedData
{
	
	[Serializable]
	public class IntReference : VariableReference<IntVariable, int>
	{
		public static implicit operator int(IntReference reference)
		{
			return reference.value;
		}

		public static implicit operator float(IntReference reference)
		{
			return (float)reference.value;
		}

		public static implicit operator string(IntReference reference)
		{
			return reference.value.ToString();
		}
	}

}