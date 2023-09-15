// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class LocalRotationViewTransition: ViewTransition
	{
		[Serializable]
		private struct TransformRotation
		{
			[field: SerializeField]
			public Transform Transform { get; private set; }

			[field: SerializeField]
			public Vector3 Rotation { get; private set; }
		}

		[field: SerializeField]
		private TransformRotation[] Items { get; set; }

		public override void Apply()
		{
			foreach (var item in Items)
			{
				item.Transform.localRotation = Quaternion.Euler(item.Rotation);
			}
		}
	}
}
