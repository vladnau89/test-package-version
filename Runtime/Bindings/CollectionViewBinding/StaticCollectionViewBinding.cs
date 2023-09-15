// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class StaticCollectionViewBinding: CollectionViewBinding<IList>
	{
		[field: SerializeField]
		private ViewBinding[] ItemObjects { get; set; }

		public override void SetValue(IList value)
		{
			if (value != null && value.Count != ItemObjects.Length)
			{
				throw new ArgumentException(
					$"{nameof(ItemObjects)}.Count ({ItemObjects.Length}) must be equal to {nameof(value)}.Count ({value.Count}).",
					nameof(value));
			}

			if (Value != null)
			{
				RemoveAll();
			}

			base.SetValue(value);

			if (Value != null)
			{
				AddRange(value);
			}
		}

		protected override ViewBinding CreateViewBinding(int index)
		{
			return ItemObjects[index];
		}

		protected override void DestroyViewBinding(ViewBinding viewBinding)
		{
		}
	}
}
