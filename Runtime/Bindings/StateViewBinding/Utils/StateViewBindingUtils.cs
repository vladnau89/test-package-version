// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	internal static class StateViewBindingUtils
	{
		[return: NotNull]
		public static ViewState EnsureStateObject(string gameObjectName, Transform parent)
		{
			var result = parent
				.GetComponentsInChildren<ViewState>(true)
				.FirstOrDefault(x => x.name == gameObjectName);

			if (result == null)
			{
				var stateObject = new GameObject(gameObjectName, typeof(RectTransform), typeof(ViewState))
				{
					transform =
					{
						parent = parent,
						localPosition = Vector3.zero,
						localScale = Vector3.zero,
						localRotation = Quaternion.identity,
					}
				};
				stateObject.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

				result = stateObject.GetComponent<ViewState>();
			}

			return result;
		}
	}
}
