using System;
using UnityEngine;
using UnityEngine.UI;

namespace SM.Core.Unity.UI.MVVM
{
	internal class ColorViewTransition: ViewTransition
	{
		[Serializable]
		private struct GraphicColor
		{
			[field: SerializeField]
			public Graphic Graphic { get; private set; }

			[field: SerializeField]
			public Color Color { get; private set; }
		}

		[field: SerializeField]
		private GraphicColor[] Items { get; set; }

		public override void Apply()
		{
			foreach (var item in Items)
			{
				item.Graphic.color = item.Color;
			}
		}
	}
}