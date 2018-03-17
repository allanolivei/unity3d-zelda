using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharedData
{

	public abstract class SharedVariable : SharedEvent
	{
		public abstract object GetObjectValue ();
		public abstract S GetValue<S> ();
		public abstract void SetValue<S> (S value);


		public static implicit operator string(SharedVariable v)
		{
			return v ? v.ToString() : string.Empty;
		}
	}

	public abstract class SharedVariableTemplate<T> : SharedVariable
	{
		[SerializeField]
		protected T _value;

		public virtual T value
		{
			get { return _value; }
			set {
				_value = value;
				this.Dispatch ();
			}
		}

		public override S GetValue<S> ()
		{
			return (S)System.Convert.ChangeType(value, typeof(S));
		}

		public override void SetValue<S> ( S value )
		{
			this.value = (T)System.Convert.ChangeType(value, typeof(T));
		}

		public override object GetObjectValue ()
		{
			return value;
		}

		public T GetValue()
		{
			return value;
		}

		public void SetValue( T value )
		{
			this.value = value;
		}

		public static implicit operator T(SharedVariableTemplate<T> v)
		{
			return v.value;
		}

		public override string ToString() 
		{
			return value.ToString();
		}

	}

}
