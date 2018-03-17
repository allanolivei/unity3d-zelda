using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SharedData
{
	
	public abstract class VariableReferenceDrawer : PropertyDrawer
	{

		private readonly string[] popupOptions = 
		{ "Use Constant", "Use Variable" };
		private GUIStyle popupStyle;


		public override void OnGUI (Rect p, SerializedProperty property, GUIContent label)
		{
			if (popupStyle == null)
			{
				popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
				popupStyle.imagePosition = ImagePosition.ImageOnly;
			}

			EditorGUI.BeginChangeCheck();

			// initialize properties
			label = EditorGUI.BeginProperty(p, label, property);

			// constant ou variable option
			SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
			int result = EditorGUI.Popup(
				new Rect(p.x + EditorGUIUtility.labelWidth, p.y, 20, p.height), 
				useConstant.boolValue ? 0 : 1, 
				popupOptions, 
				popupStyle);
			useConstant.boolValue = result == 0;
			EditorGUIUtility.labelWidth += 20;

			if (useConstant.boolValue) 
				// constant value
				EditorGUI.PropertyField(p, property.FindPropertyRelative("constantValue"), label);
			else
				// SharedVariable
				EditorGUI.PropertyField (p, property.FindPropertyRelative("variable"), label);

			EditorGUIUtility.labelWidth -= 20;

			EditorGUI.EndProperty();

			if (EditorGUI.EndChangeCheck())
				property.serializedObject.ApplyModifiedProperties();
		}

	}


	[CustomPropertyDrawer(typeof(FloatReference))]
	public class FloatReferenceDrawer : VariableReferenceDrawer{}

	[CustomPropertyDrawer(typeof(IntReference))]
	public class IntReferenceDrawer : VariableReferenceDrawer{}

	[CustomPropertyDrawer(typeof(StringReference))]
	public class StringReferenceDrawer : VariableReferenceDrawer{}
}