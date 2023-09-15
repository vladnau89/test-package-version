// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System;

namespace SM.Core.Unity.UI.MVVM
{
	public class GenericCommand<TParameter>: DefaultCommand<TParameter, TParameter>
	{
		public GenericCommand(Action<TParameter> execute, bool available = true): base(execute, available)
		{
		}
	}
}
