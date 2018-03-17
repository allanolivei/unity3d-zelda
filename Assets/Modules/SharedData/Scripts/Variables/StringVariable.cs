using UnityEngine;

namespace SharedData
{

	[CreateAssetMenu(fileName = "StringVariable", menuName = "SharedData/Variable/String", order = 1)]
	public class StringVariable : SharedVariableTemplate<string> 
	{
		public static StringVariable operator +(StringVariable a, StringVariable b)
		{
			a.value += b.value;
			return a;
		}
	}

}