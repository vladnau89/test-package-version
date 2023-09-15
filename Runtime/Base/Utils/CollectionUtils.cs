// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SM.Core.Unity.UI.MVVM
{
	public static class CollectionUtils
	{
		public static T Next<T>(this IList<T> collection, T selectedItem, bool loop = true)
		{
			var index = collection.IndexOf(selectedItem);
			if (index == -1)
			{
				throw new ArgumentException("Collection doesn't contain selected item.", nameof(selectedItem));
			}

			index++;
			index = loop ? index % collection.Count : Math.Min(index, collection.Count - 1);
			return collection[index];
		}

		public static T Prev<T>(this IList<T> collection, T selectedItem, bool loop = true)
		{
			var index = collection.IndexOf(selectedItem);
			if (index == -1)
			{
				throw new ArgumentException("Collection doesn't contain selected item.", nameof(selectedItem));
			}

			index--;
			index = loop ? (index + collection.Count) % collection.Count : Math.Max(index, 0);
			return collection[index];
		}
	}
}
