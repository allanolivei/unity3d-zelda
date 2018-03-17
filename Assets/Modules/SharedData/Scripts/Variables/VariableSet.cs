using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SharedData
{
	[CreateAssetMenu(fileName = "VariableSet", menuName = "SharedData/Variable/VariableSet", order = 1)]
	public class VariableSet : ScriptableObject, IEnumerable
	{
		[SerializeField]
		private SharedVariable[] variables;

		public object GetValue( string name ) 
		{
			var variable = GetVariable (name);
			return variable ? variable.GetObjectValue() : null;
		}

		public T GetValue<T>( string name ) 
		{
			var variable = GetVariable (name);
			return variable ? (T)System.Convert.ChangeType (variable.GetObjectValue (), typeof(T)) : default(T);
		}

		public void SetValue( string name, object value ) 
		{
			var variable = GetVariable (name);
			if (variable) variable.SetValue (value);
		}

		public void SetValue<T>( string name, T value ) 
		{
			var variable = GetVariable (name);
			if (variable) variable.SetValue<T> (value);
		}

		public SharedVariable GetVariable( string name )
		{
			for (int i = 0; i < variables.Length; i++) 
				if (variables [i].name == name)
					return variables [i];
			return null;
		}

		public T GetVariable<T>( string name ) where T : SharedVariable
		{
			return GetVariable(name) as T;
		}

		public IEnumerator GetEnumerator()
		{
			return this.variables.GetEnumerator();
		}

		public int Count()
		{
			return this.variables.Length;
		}

		public SharedVariable this[int i]
		{
			get { return this.variables[i]; }
		}
	}
}