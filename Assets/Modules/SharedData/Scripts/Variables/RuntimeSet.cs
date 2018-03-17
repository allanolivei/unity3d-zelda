using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharedData
{
	public abstract class RuntimeSet<T> : ScriptableObject
	{
		public List<T> items = new List<T>();

		public void Add(T thing)
		{
			if (!items.Contains(thing))
				items.Add(thing);
		}

		public void Remove(T thing)
		{
			if (items.Contains(thing))
				items.Remove(thing);
		}
	}
}