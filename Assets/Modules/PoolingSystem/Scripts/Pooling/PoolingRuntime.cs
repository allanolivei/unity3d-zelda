using System;
using System.Collections.Generic;

namespace PoolingSystem
{

	public class PoolingSimple<T>
	{
	    protected Queue<T> queue;

		public PoolingSimple()
	    {
	        this.queue = new Queue<T>();
	    }

	    public int Count
	    {
	        get { return queue.Count; }
	    }

	    public virtual T Get( params object[] args )
	    {
	        if (queue.Count > 0)
	            return queue.Dequeue();
	        return this.Create(args);
	    }

	    public virtual void Recycle( T element )
	    {
			if( !queue.Contains(element) )
	        	queue.Enqueue(element);
	    }

	    public virtual T Create( params object[] args )
	    {
	        return (T)Activator.CreateInstance(typeof(T), args);
	    }
	}

	public class PoolingRuntime<T> : PoolingSimple<T> where T : UnityEngine.Object
	{

	    public T objectReference { get; protected set; }

		public PoolingRuntime( T objectReference ) : base()
	    {
	        this.objectReference = objectReference;
	    }

	    public override T Create( params object[] args )
	    {
	        T inst = UnityEngine.Object.Instantiate<T>(objectReference);
	        inst.name = objectReference.name;
	        return inst;
	    }

	}

}