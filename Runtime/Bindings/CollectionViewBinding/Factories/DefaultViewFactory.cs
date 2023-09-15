// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class DefaultViewFactory: IViewFactory
	{
		public ViewBinding Create(ViewBinding prefab)
		{
			return Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
		}
	}
}
