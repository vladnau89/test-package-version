// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace SM.Core.Unity.UI.MVVM
{
	public sealed class ImageSpriteViewTransition: DefaultViewTransition<Image, Sprite>
	{
		protected override void Apply(Image target, Sprite value)
		{
			target.sprite = value;
		}
	}
}
