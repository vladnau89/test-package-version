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

		public ICommand<object> SelectSelfCommand { get; }

		public SelectableViewModel()
		{
			SelectSelfCommand = new DefaultCommand<object>(ExecuteSelectSelfCommand);
		}

		private void ExecuteSelectSelfCommand(object parameter)
		{
			Selected = true;
		}
	}
}