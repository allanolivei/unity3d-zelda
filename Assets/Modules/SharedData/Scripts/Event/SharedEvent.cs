using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharedData
{
	[CreateAssetMenu(fileName = "SharedEvent", menuName = "SharedData/Event/SharedEvent", order = 1)]
	public class SharedEvent : ScriptableObject
	{

		private readonly List<ISharedEventListener> eventListeners = 
			new List<ISharedEventListener>();

		private readonly List<System.Action<SharedEvent>> eventCallback = 
			new List<System.Action<SharedEvent>>();

		[Multiline]
		public string description;

		public void Dispatch()
		{
			for(int i = eventListeners.Count -1; i >= 0; i--)
				eventListeners[i].DispatchHandler(this);

			for(int i = eventCallback.Count -1; i >= 0; i--)
				eventCallback[i].Invoke(this);
		}

		public void RegisterCallback(System.Action<SharedEvent> callBack)
		{
			if (!eventCallback.Contains (callBack))
				eventCallback.Add (callBack);
		}

		public void UnregisterCallback(System.Action<SharedEvent> callBack)
		{
			if (eventCallback.Contains (callBack))
				eventCallback.Remove (callBack);
		}

		public void RegisterListener(ISharedEventListener listener)
		{
			if (!eventListeners.Contains(listener))
				eventListeners.Add(listener);
		}

		public void UnregisterListener(ISharedEventListener listener)
		{
			if (eventListeners.Contains(listener))
				eventListeners.Remove(listener);
		}

	}
}
