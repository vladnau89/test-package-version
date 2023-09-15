using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SM.Core.Unity.UI.MVVM.Samples.PhoneList
{
	internal class PhoneViewModel: ViewModel, IHavingSelectable
	{
		private string _title;
		private string _company;
		private int _price;
		private readonly ReadOnlyObservableCollection<string> _tags;

		public string Title
		{
			get => _title;
			set => SetField(ref _title, value);
		}
		
		public string Company
		{
			get => _company;
			set => SetField(ref _company, value);
		}

		public INotifyCollectionChanged Tags => _tags;

		public SelectableViewModel Selectable { get; }

		public PhoneViewModel()
		{
			Selectable = new SelectableViewModel();
			_tags = new ReadOnlyObservableCollection<string>(new ObservableCollection<string>(new List<string>()
			{
				"tag1", "tag2", "tag3"
			}));
		}
	}
}