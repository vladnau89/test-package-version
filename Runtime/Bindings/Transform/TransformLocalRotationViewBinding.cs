// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	[RequireComponent(typeof(Transform))]
	public class TransformLocalRotationViewBinding: TypedViewBinding<Quaternion>
	{
		[field: SerializeField, HideInInspector]
		private Transform Transform { get; set; }

		private void OnValidate()
		{
			Transform = GetComponent<Transform>();
		}

		public override void SetValue(Quaternion value)
		{
			base.SetValue(value);
			Transform.localRotation = value;
		}
	}
}
