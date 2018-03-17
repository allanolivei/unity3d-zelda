using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SharedData
{

	[CustomEditor(typeof(SharedEvent))]
	public class SharedEventEditor : Editor
	{

		private SerializedProperty description;
		private SharedEvent sharedEvent;

		private void OnEnable()
		{
			this.description = serializedObject.FindProperty ("description");
			this.sharedEvent = target as SharedEvent;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField (this.description);
			serializedObject.ApplyModifiedProperties();

			EditorGUI.BeginDisabledGroup (!Application.isPlaying);
			if (GUILayout.Button("Invoke")) this.sharedEvent.Dispatch();
			EditorGUI.EndDisabledGroup ();
		}

	}
}