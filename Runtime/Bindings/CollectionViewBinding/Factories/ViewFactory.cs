// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class ViewFactory: MonoBehaviour, IViewFactory
	{
		public abstract ViewBinding Create(ViewBinding prefab);
	}
}
