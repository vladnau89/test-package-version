using System;

namespace SM.Core.Unity.UI.MVVM
{
	public class SelectableViewModel
	{
		private bool _selected;

		public bool Selected
		{
			get => _selected;
			set
			{
				
			}
		}

		public object Value { get; }

		public SelectableViewModel(object value, bool selected, Action<object> select)
		{
			Value = value;
			Selected = selected;
		}
	}
}