using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	[CustomEditor(typeof(StateViewBinding<>), true)]
	public class StateViewBindingEditor: UnityEditor.Editor
	{
		private SerializedProperty States { get; set; }

		private void OnEnable()
		{
			States = serializedObject.FindAutoProperty("States");
		}

		public override void OnInspectorGUI()
		{
			foreach (var stateProperty in States.EnumerateArray())
			{
				var valueProperty = stateProperty.FindAutoPropertyRelative("Value");
				var viewStateProperty = stateProperty.FindAutoPropertyRelative("ViewState");

				var labelText = valueProperty.enumDisplayNames[valueProperty.enumValueIndex];
				EditorGUILayout.PropertyField(viewStateProperty, new GUIContent(labelText));
			}
			
			GUILayout.Space(10);
			
			if (!Application.isPlaying && GUILayout.Button("Ensure States"))
			{
				EnsureStateGameObjects();
			}

			void EnsureStateGameObjects()
			{
				target
					.GetType()
					.GetMethod("EnsureStateGameObjects", BindingFlags.Instance | BindingFlags.NonPublic)
					?.Invoke(target, null);
			}
		}
	}
}