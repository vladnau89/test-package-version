using System;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class ActivationViewTransition: ViewTransition
	{
		[Serializable]
		private struct ObjectActive
		{
			[field: SerializeField]
			public GameObject Object { get; private set; }

			[field: SerializeField]
			public bool Active { get; private set; }
		}

		[field: SerializeField]
		private ObjectActive[] Items { get; set; }

		public override void Apply()
		{
			foreach (var item in Items)
			{
				item.Object.SetActive(item.Active);
			}
		}
	}
}