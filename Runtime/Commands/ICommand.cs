using System;

namespace SM.Core.Unity.UI.MVVM
{
	public interface ICommand<in TParameter>
	{
		public event EventHandler CanExecuteChanged;

		public void Execute(TParameter parameter);

		public bool CanExecute(TParameter parameter);
	}
}