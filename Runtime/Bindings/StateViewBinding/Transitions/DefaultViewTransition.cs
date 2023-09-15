// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class DefaultViewTransition<TTarget, TValue>: ViewTransition
	{
		[Serializable]
		private struct Item
		{
			[field: SerializeField]
			public TTarget Target { get; private set; }

			[field: SerializeField]
			public TValue Value { get; private set; }
		}

		[field: SerializeField]
		private Item[] Items { get; set; }

		public sealed override void Apply()
		{
			foreach (var item in Items)
			{
				Apply(item.Target, item.Value);
			}
		}

		protected abstract void Apply(TTarget target, TValue value);
	}
}
