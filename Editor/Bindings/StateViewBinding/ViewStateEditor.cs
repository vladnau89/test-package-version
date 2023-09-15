using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	[CustomEditor(typeof(ViewState), true)]
	[CanEditMultipleObjects]
	public class ViewStateEditor: UnityEditor.Editor
	{
		private IEnumerable<ViewState> ViewStates => targets.Select(t => (ViewState)t);

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			GUILayout.Space(10);

			if (!Application.isPlaying && GUILayout.Button("Collect Transitions"))
			{
				foreach (var viewState in ViewStates)
				{
					viewState.CollectTransitions();
				}
			}
		}
	}
}