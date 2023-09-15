using System;

namespace SM.Core.Unity.UI.MVVM
{
	public class SelectableViewModel: ViewModel
	{
		private bool _selected;

		public bool Selected
		{
			get => _selected;
			set => SetField(ref _selected, value);
		}

		public ICommand<object> SelectCommand { get; }

		public object Value { get; }

		public SelectableViewModel(object value, bool selected, Action<object> select)
		{
			Value = value;
			Selected = selected;
			SelectCommand = new DefaultCommand<object>((_) => select(Value));
		}
	}
}