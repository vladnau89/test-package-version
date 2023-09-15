using System;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class ScaleViewTransition: ViewTransition
	{
		[Serializable]
		private struct TransformScale
		{
			[field: SerializeField]
			public Transform Transform { get; private set; }

			[field: SerializeField]
			public Vector3 Scale { get; private set; }
		}

		[field: SerializeField]
		private TransformScale[] Items { get; set; }

		public override void Apply()
		{
			foreach (var item in Items)
			{
				item.Transform.localScale = item.Scale;
			}
		}
	}
}
