using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class ViewTransition: MonoBehaviour
	{
		public abstract void Apply();

#if UNITY_EDITOR
		private void Reset()
		{
			if (TryGetComponent<ViewState>(out var viewState))
			{
				viewState.CollectTransitions();
			}
		}
#endif
	}
}