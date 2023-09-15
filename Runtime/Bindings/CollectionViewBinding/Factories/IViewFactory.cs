// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

namespace SM.Core.Unity.UI.MVVM
{
	public interface IViewFactory
	{
		public ViewBinding Create(ViewBinding prefab);
	}
}
