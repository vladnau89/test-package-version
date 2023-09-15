// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

namespace SM.Core.Unity.UI.MVVM
{
	public interface IViewPool
	{
		public ViewBinding Spawn(ViewBinding prefab);

		public void Despawn(ViewBinding view);
	}
}
