// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public sealed class BoolStateViewBinding: BoolViewBinding
	{
		[field: SerializeField]
		private ViewState False { get; set; }

		[field: SerializeField]
		private ViewState True { get; set; }

		public override void SetValue(bool value)
		{
			base.SetValue(value);

			if (value && True)
			{
				True.Apply();
			}
			else if (!value && False)
			{
				False.Apply();
			}
		}
		
#if UNITY_EDITOR
		private void Reset()
		{
			EnsureStateGameObjects();
		}

		internal void EnsureStateGameObjects()
		{
			if (!False)
			{
				False = StateViewBindingUtils.EnsureStateObject("FalseState", transform);
				UnityEditor.EditorUtility.SetDirty(this);
			}

			if (!True)
			{
				True = StateViewBindingUtils.EnsureStateObject("TrueState", transform);
				UnityEditor.EditorUtility.SetDirty(this);
			}
		}
#endif
	}
}
