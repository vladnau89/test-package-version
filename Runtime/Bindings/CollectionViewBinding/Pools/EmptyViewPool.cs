// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class EmptyViewPool: IViewPool
	{
		private IViewFactory Factory { get; }

		public EmptyViewPool([MaybeNull] IViewFactory factory = null)
		{
			Factory = factory ?? new DefaultViewFactory();
		}

		public ViewBinding Spawn(ViewBinding prefab)
		{
			return Factory.Create(prefab);
		}

		public void Despawn(ViewBinding view)
		{
			Object.Destroy(view.gameObject);
		}
	}
}
