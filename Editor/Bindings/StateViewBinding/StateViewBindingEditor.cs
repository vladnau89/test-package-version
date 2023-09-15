using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	[CustomEditor(typeof(StateViewBinding<>), true)]
	public class StateViewBindingEditor: UnityEditor.Editor
	{
		private SerializedProperty States { get; set; }
		private SerializedProperty StateTransitions { get; set; }

		private void OnEnable()
		{
			States = serializedObject.FindAutoProperty("States");
			StateTransitions = serializedObject.FindAutoProperty("StateTransitions");
		}

		public override void OnInspectorGUI()
		{
			if (Application.isPlaying)
			{
				GUI.enabled = false;
				EditorGUILayout.TextField("Current State", ((ViewBinding)target).ObjectValue.ToString());
				GUI.enabled = true;
			}

			GUILayout.Space(10);
			foreach (var stateProperty in States.EnumerateArray())
			{
				var valueProperty = stateProperty.FindAutoPropertyRelative("Value");
				var viewStateProperty = stateProperty.FindAutoPropertyRelative("ViewState");

				var labelText = valueProperty.enumDisplayNames[valueProperty.enumValueIndex];
				EditorGUILayout.PropertyField(viewStateProperty, new GUIContent(labelText));
			}

			GUILayout.Space(10);
			EditorGUILayout.PropertyField(StateTransitions);

			GUILayout.Space(10);
			if (!Application.isPlaying && GUILayout.Button("Ensure States"))
			{
				EnsureStateGameObjects();
			}

			serializedObject.ApplyModifiedProperties();

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