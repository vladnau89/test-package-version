// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace SM.Core.Unity.UI.MVVM
{
	[RequireComponent(typeof(Image))]
	public class ImageSpriteViewBinding: TypedViewBinding<Sprite>
	{
		[field: SerializeField, HideInInspector]
		private Image Image { get; set; }

		private void OnValidate()
		{
			Image = GetComponent<Image>();
		}

		public override void SetValue(Sprite value)
		{
			base.SetValue(value);
			Image.sprite = value;
		}
	}
}
