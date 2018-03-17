using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

using System;
using System.Reflection;

namespace SharedData
{

	public static class SharedDataEditorUtilities
	{

		public static string DIRECTORY = "Data/Variables";

		public static SharedVariable CreateSharedVariable( string type, string name = "NewVariable" )
		{
			// generate path
			System.IO.Directory.CreateDirectory ("Assets/"+DIRECTORY);
			string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/"+DIRECTORY+"/"+name+".asset");

			// create and save assets
			var newVariable = Editor.CreateInstance(type) as SharedVariable;
				newVariable.name = name;
			AssetDatabase.CreateAsset(newVariable, path);
			AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newVariable));
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			return newVariable;
		}

		public static SharedVariable CreateSharedVariable( System.Type type, string name = "NewVariable" )
		{
			return CreateSharedVariable(type.FullName, name);
		}

		public static T CreateSharedVariable<T>( string name = "NewVariable" ) where T : SharedVariable
		{
			return CreateSharedVariable(typeof(T), name) as T;
		}

		public static T FindOrCreateSharedVariable<T>(string name) where T : SharedVariable
		{
			return (T)FindOrCreateSharedVariable(typeof(T).FullName, name);
		}

		public static SharedVariable FindOrCreateSharedVariable(string type, string name)
		{
			SharedVariable[] sv = FindVariables ();

			for (int i = 0; i < sv.Length; i++)
				if (sv [i].GetType().FullName == type && sv [i].name == name)
					return sv [i];

			return CreateSharedVariable (type, name);
		}

		public static SharedVariable FindOrCreateSharedVariableBySerializedProperty(SerializedProperty prop, string name)
		{
			string[] types = FindSharedVariablesTypes ();

			foreach (string tname in types)
				if ( prop.type.Contains (tname.Replace("SharedData.", string.Empty)) )
					return FindOrCreateSharedVariable (tname, name);

			return null;
		}

		public static SharedVariable[] FindVariables()
		{
			string[] paths = AssetDatabase.FindAssets ("t:sharedVariable");
			return (
				from string path in paths
				select AssetDatabase.LoadAssetAtPath<SharedVariable> (AssetDatabase.GUIDToAssetPath(path))
			).ToArray<SharedVariable> ();
		}

		public static string[] FindSharedVariablesTypes()
		{
			Type[] types = Assembly.GetAssembly(typeof(SharedVariable)).GetTypes();
			return (
				from Type type in types 
				where (type.IsSubclassOf(typeof(SharedVariable)) && !type.FullName.Contains("Template"))
				select type.FullName
			).ToArray();
		}
	}

}