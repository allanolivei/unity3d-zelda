using UnityEngine;
using UnityEditor;



namespace SharedData
{
	public abstract class SharedVariableTemplateDrawer : PropertyDrawer
	{

		public override void OnGUI (Rect p, SerializedProperty property, GUIContent label)
		{

			EditorGUI.BeginChangeCheck();

			// initialize properties
			label = EditorGUI.BeginProperty(p, label, property);


			// has SharedVariable?
			if (property.objectReferenceValue) 
			{
				SerializedObject so = new SerializedObject (property.objectReferenceValue);
				SerializedProperty prop = so.FindProperty ("_value");

				// SharedVariable.value field
				so.Update ();
				EditorGUI.PropertyField (
					new Rect(p.x, p.y, p.width*0.6f, p.height), 
					prop, 
					label);
				so.ApplyModifiedProperties ();

				// SharedVariable field
				EditorGUI.PropertyField (
					new Rect(p.x+p.width*0.6f, p.y, p.width*0.4f, p.height), 
					property, 
					GUIContent.none);
			}
			else 
			{
				// Fill SharedVariable... is empty
				EditorGUI.PropertyField (p, property, label);
			}

			EditorGUI.EndProperty();

			if (EditorGUI.EndChangeCheck())
				property.serializedObject.ApplyModifiedProperties();
		}

	}


	[CustomPropertyDrawer(typeof(FloatVariable))]
	public class FloatVariableDrawer : SharedVariableTemplateDrawer { }

	[CustomPropertyDrawer(typeof(IntVariable))]
	public class IntVariableDrawer : SharedVariableTemplateDrawer { }

	[CustomPropertyDrawer(typeof(StringVariable))]
	public class StringVariableDrawer : SharedVariableTemplateDrawer { }




	[CustomPropertyDrawer(typeof(SharedVariableAttribute))]
	public class SharedVariableDrawer : PropertyDrawer
	{
		private SerializedObject so;
		private SerializedProperty prop;
		
		public override void OnGUI(Rect p, SerializedProperty property, GUIContent label)
		{
			SharedVariableAttribute attrib = attribute as SharedVariableAttribute;

			// create object by name
			if ( !string.IsNullOrEmpty (attrib.name) && 
				 (!property.objectReferenceValue || (property.objectReferenceValue as SharedVariable).name != attrib.name)) {

				property.objectReferenceValue = 
					SharedDataEditorUtilities.FindOrCreateSharedVariableBySerializedProperty (property, attrib.name);
			}

			// init property
			EditorGUI.BeginChangeCheck();

			// initialize properties
			label = EditorGUI.BeginProperty(p, label, property);

			// has SharedVariable?
			if (property.objectReferenceValue) 
			{
				SerializedObject so = new SerializedObject (property.objectReferenceValue);
				SerializedProperty prop = so.FindProperty ("_value");

				// SharedVariable.value field
				so.Update ();
				EditorGUI.PropertyField (
					new Rect(p.x, p.y, p.width*0.6f, p.height), 
					prop, 
					label);
				so.ApplyModifiedProperties ();

				// SharedVariable field
				EditorGUI.PropertyField (
					new Rect(p.x+p.width*0.6f, p.y, p.width*0.4f, p.height), 
					property, 
					GUIContent.none);
			}
			else 
			{
				// Fill SharedVariable... is empty
				EditorGUI.PropertyField (p, property, label);
			}

			// end
			EditorGUI.EndProperty();

			// apply
			if (EditorGUI.EndChangeCheck())
				property.serializedObject.ApplyModifiedProperties();
		}
	}

}