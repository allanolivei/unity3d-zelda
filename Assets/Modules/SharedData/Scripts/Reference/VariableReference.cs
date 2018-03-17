using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharedData
{

	[Serializable]
	public abstract class VariableReference<T, V> where T : SharedVariableTemplate<V>
	{
		
		[SerializeField] public bool useConstant = true;
		[SerializeField] public T variable;
		[SerializeField] public V constantValue;

		public VariableReference() {}

		public VariableReference(V value)
		{
			useConstant = true;
			constantValue = value;
		}

		public V value
		{
			get { return useConstant ? constantValue : variable.value; }
		}

	}

}