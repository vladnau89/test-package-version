// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace SM.Core.Unity.UI.MVVM
{
	[RequireComponent(typeof(Image))]
	public class ImageColorViewBinding: TypedViewBinding<Color>
	{
		[field: SerializeField, HideInInspector]
		private Image Image { get; set; }

		private void OnValidate()
		{
			Image = GetComponent<Image>();
		}

		public override void SetValue(Color value)
		{
			base.SetValue(value);
			Image.color = value;
		}
	}
}
