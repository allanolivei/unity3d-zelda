using UnityEngine;


namespace SharedData
{
	
	public class SharedVariableAttribute : PropertyAttribute
	{
		public string name;

		public SharedVariableAttribute(string name)
		{
			this.name = name;
		}
	}

}