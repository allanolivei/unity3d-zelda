using UnityEngine;
using System;

namespace SharedData
{
	
	[Serializable]
	public class StringReference : VariableReference<StringVariable, string>
	{

		public static implicit operator string(StringReference reference)
		{
			return reference.value.ToString();
		}

	}

}