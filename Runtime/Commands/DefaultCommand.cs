using System;

namespace SM.Core.Unity.UI.MVVM
{
	public class DefaultCommand<TFrontParameter, TBackParameter>: ICommand<TBackParameter>
		where TFrontParameter: TBackParameter
	{
		private bool _available;

		private Action<TFrontParameter> ExecuteHandler { get; }

		public bool Available
		{
			get => _available;
			set
			{
				if (_available != value)
				{
					_available = value;
					CanExecuteChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public event EventHandler CanExecuteChanged;

		public DefaultCommand(Action<TFrontParameter> execute, bool available = true)
		{
			ExecuteHandler = execute;
			Available = available;
		}

		public bool CanExecute(TBackParameter parameter)
		{
			return Available;
		}

		public void Execute(TBackParameter parameter)
		{
			ExecuteHandler((TFrontParameter)parameter);
		}
	}

	public class DefaultCommand<TParameter>: DefaultCommand<TParameter, object>
	{
		public DefaultCommand(Action<TParameter> execute, bool available = true): base(execute, available)
		{
		}
	}
}