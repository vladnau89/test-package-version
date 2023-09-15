using System.Linq;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class ViewState: MonoBehaviour
	{
		[field: SerializeField]
		private ViewTransition[] Transitions { get; set; }

		public void Apply()
		{
			foreach (var transition in Transitions)
			{
				transition.Apply();
			}
		}

#if UNITY_EDITOR
		internal void CollectTransitions()
		{
			var newTransitions = GetComponents<ViewTransition>();

			if (Transitions == null)
			{
				Transitions = newTransitions;
				UnityEditor.EditorUtility.SetDirty(this);
				return;
			}

			newTransitions = newTransitions.Union(Transitions.Except(newTransitions)).Where(x => x != null).ToArray();
			if (!Transitions.SequenceEqual(newTransitions))
			{
				Transitions = newTransitions;
				UnityEditor.EditorUtility.SetDirty(this);
			}
		}
#endif
	}
}