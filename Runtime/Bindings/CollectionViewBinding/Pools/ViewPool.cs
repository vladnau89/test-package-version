// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class ViewPool: MonoBehaviour, IViewPool
	{
		[field: SerializeField]
		protected ViewFactory Factory { get; private set; }

		public abstract ViewBinding Spawn(ViewBinding prefab);

		public abstract void Despawn(ViewBinding view);
	}
}
