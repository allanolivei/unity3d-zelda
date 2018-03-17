using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SharedData
{
	
	public abstract class SharedVariableEditor<T, V> : Editor where T : SharedVariableTemplate<V>
	{
		private SerializedProperty description;
		private SerializedProperty value;
		private T variable;

		private void OnEnable()
		{
			this.description = serializedObject.FindProperty ("description");
			this.value = serializedObject.FindProperty ("_value");
			this.variable = target as T;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField (this.description);
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (this.value);
			if ( EditorGUI.EndChangeCheck() ) this.variable.Dispatch ();
			serializedObject.ApplyModifiedProperties();

			EditorGUI.BeginDisabledGroup (!Application.isPlaying);
			if (GUILayout.Button ("Invoke")) this.variable.Dispatch ();
			EditorGUI.EndDisabledGroup ();
		}
	}

	[CustomEditor(typeof(IntVariable))]
	public class IntVariableEditor : SharedVariableEditor <IntVariable, int> { }

	[CustomEditor(typeof(FloatVariable))]
	public class FloatVariableEditor : SharedVariableEditor <FloatVariable, float> { }

	[CustomEditor(typeof(StringVariable))]
	public class StringVariableEditor : SharedVariableEditor <StringVariable, string> { }

}