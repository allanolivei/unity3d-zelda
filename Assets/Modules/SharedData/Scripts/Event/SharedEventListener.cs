using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SharedData
{
	public class SharedEventListener : MonoBehaviour, ISharedEventListener
	{

		[Tooltip("Event to register with.")]
		public SharedEvent Event;

		[Tooltip("Response to invoke when Event is raised.")]
		public UnityEvent Response;

		private void OnEnable()
		{
			Event.RegisterListener(this);
		}

		private void OnDisable()
		{
			Event.UnregisterListener(this);
		}

		public void DispatchHandler(SharedEvent e)
		{
			Response.Invoke();
		}

	}
}