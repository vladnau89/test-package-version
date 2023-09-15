// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	[CustomEditor(typeof(BoolStateViewBinding), true)]
	public class BoolViewBindingEditor: UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			GUILayout.Space(10);

			if (!Application.isPlaying && GUILayout.Button("Ensure States"))
			{
				var boolStateViewBinding = (BoolStateViewBinding)target;
				boolStateViewBinding.EnsureStateGameObjects();
			}
		}
	}
}
