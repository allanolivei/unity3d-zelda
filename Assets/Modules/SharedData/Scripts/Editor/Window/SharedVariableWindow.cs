using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SharedData
{

	public class SharedVariableWindow : EditorWindow
	{
		
		private Editor[] editors = new Editor[0];
		private string[] attribNames = new string[0];
		private int attribIndex;
		private Vector2 scrollPosition;

		[MenuItem("Window/Shared Variables")]
		static void Init()
		{
			SharedVariableWindow window = (SharedVariableWindow)EditorWindow.GetWindow(typeof(SharedVariableWindow));
			window.titleContent = new GUIContent("SharedVariables");
			window.Show();
		}

		private void OnEnable()
		{
			this.Refresh ();
		}

		private void OnGUI()
		{
			this.DrawHeader ();
			this.DrawList ();
			this.DrawCreate ();
		}

		private void DrawHeader()
		{
			GUILayout.BeginHorizontal(EditorStyles.toolbar);
			GUILayout.Label ("List of SharedVariables", EditorStyles.miniLabel);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button ("Refresh", EditorStyles.toolbarButton, GUILayout.Width (60)))
				Refresh ();
			GUILayout.EndHorizontal();
		}

		private void DrawList()
		{
			scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition);

			for (int i = 0; i < editors.Length; i++) 
			{
				if ( !editors [i] || !editors[i].target ) continue;

				EditorGUILayout.BeginVertical (EditorStyles.helpBox);
					this.editors[i].OnInspectorGUI();
					EditorGUI.BeginDisabledGroup(true);
					EditorGUILayout.ObjectField (editors[i].target, typeof(SharedVariable));
					EditorGUI.EndDisabledGroup();
				EditorGUILayout.EndVertical ();

				EditorGUILayout.Space ();
			}

			EditorGUILayout.EndScrollView();
		}

		private void DrawCreate()
		{
			// create new SharedVariable
			EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
			GUILayout.FlexibleSpace();
			attribIndex = EditorGUILayout.Popup(attribIndex, attribNames, EditorStyles.toolbarDropDown);
			if (GUILayout.Button ("Add", EditorStyles.toolbarButton, GUILayout.Width (60))) 
			{
				SharedDataEditorUtilities.CreateSharedVariable (attribNames [attribIndex]);
				Refresh ();
			}
			EditorGUILayout.EndHorizontal();
		}
			
		private void Refresh()
		{
			// find SharedVariable Subclasses
			attribNames = SharedDataEditorUtilities.FindSharedVariablesTypes ();
			// create editors
			this.FindSharedVariableAndCreateEditor ();
		}

		private void FindSharedVariableAndCreateEditor()
		{
			// fill SharedVariable list
			SharedVariable[] variables = SharedDataEditorUtilities.FindVariables ();
			// destroy editors
			for (int i = 0; i < this.editors.Length; i++)
				Object.DestroyImmediate (this.editors [i]);
			// create editors
			this.editors = new Editor[variables.Length];
			for (int i = 0; i < this.editors.Length; i++) 
				this.editors [i] = Editor.CreateEditor (variables [i]);
		}
	}
}