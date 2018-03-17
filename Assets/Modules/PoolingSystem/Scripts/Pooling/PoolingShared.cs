using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolingSystem
{

	[CreateAssetMenu(fileName = "PoolingShared", menuName = "PoolingSystem/PoolingShared", order = 1)]
	public class PoolingShared : ScriptableObject 
	{

		public enum RETURN_TYPE
		{
			QUEUE,
			STACK,
			RANDOM,
			PING_PONG
		}

		public RETURN_TYPE returnType;

		[SerializeField]
		private Object[] references;

//		private List<PoolingRuntime<Object>> pool = new List<PoolingRuntime<Object>> ();
//
//		public int Count { get { return pool.Count; } }
//
//		public T Get<T>()
//		{
//			if (pool.Count > 0)
//				return queue.Dequeue();
//			return this.Create(references[0]);
//		}
//
//		public Object Get()
//		{
//			return this.Create (references[0]);
//		}
//
//		public void Recycle( Object element )
//		{
//		}
//
//		public Object Create( Object objectReference )
//		{
//			Object inst = UnityEngine.Object.Instantiate(objectReference);
//			inst.name = objectReference.name;
//			return inst;
//		}
//
//		private int NextIndex()
//		{
//			switch (returnType) 
//			{
//			case
//			}
//		}
		
	}
	 
}
