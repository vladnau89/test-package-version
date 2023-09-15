// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

namespace SM.Core.Unity.UI.MVVM
{
	public class EmptyViewPoolBehavior: ViewPool
	{
		private IViewPool _pool;

		private IViewPool Pool => _pool ??= new EmptyViewPool(Factory);

		public override ViewBinding Spawn(ViewBinding prefab)
		{
			return Pool.Spawn(prefab);
		}

		public override void Despawn(ViewBinding view)
		{
			Pool.Despawn(view);
		}
	}
}
