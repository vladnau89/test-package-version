using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace SM.Core.Unity.UI.MVVM.Samples.PhoneList
{
	internal class PhoneListViewModel: ViewModel
	{
		private readonly ObservableCollection<PhoneViewModel> _phones;

		private PhoneViewModel _selectedPhone;

		private PhoneViewModel _newPhone;

		private SizeViewState _itemsCountSize;

		private int MaxListCount { get; set; }

		public PhoneViewModel NewPhone
		{
			get => _newPhone;
			set => SetField(ref _newPhone, value);
		}

		public PhoneViewModel SelectedPhone
		{
			get => _selectedPhone;
			set => SetField(ref _selectedPhone, value);
		}

		public INotifyCollectionChanged Phones => _phones;

		public int ItemsCount => _phones.Count;

		public SizeViewState ItemsCountSize
		{
			get => _itemsCountSize;
			set => SetField(ref _itemsCountSize, value);
		}

		public DefaultCommand<PhoneViewModel> AddCommand { get; }
		public DefaultCommand<object> RemoveCommand { get; }

		public PhoneListViewModel()
		{
			_phones = new ObservableCollection<PhoneViewModel>
			{
				new() { Title = "iPhone 7", Company = "Apple" },
				new() { Title = "Galaxy S7 Edge", Company = "Samsung" },
			};

			SelectedPhone = _phones.FirstOrDefault();
			NewPhone = new PhoneViewModel() { Title = "iPhone 7", Company = "Apple" };

			AddCommand = new DefaultCommand<PhoneViewModel>(Add);
			RemoveCommand = new DefaultCommand<object>(Remove);
		}

		public void SetSettings(int maxListCount)
		{
			MaxListCount = maxListCount;

			OnPhoneCountChanged();
		}

		private void Add(PhoneViewModel newPhone)
		{
			_phones.Add(newPhone);
			NewPhone = new PhoneViewModel() { Title = "new", Company = "new" };
			OnPropertyChanged(nameof(ItemsCount));

			OnPhoneCountChanged();
		}

		private void Remove(object _)
		{
			_phones.RemoveAt(_phones.Count - 1);
			OnPropertyChanged(nameof(ItemsCount));
			OnPhoneCountChanged();
		}

		private void OnPhoneCountChanged()
		{
			NewPhone.Active = (_phones.Count < MaxListCount);
			AddCommand.Available = (_phones.Count < MaxListCount);
			RemoveCommand.Available = (_phones.Count > 0);

			if (_phones.Count <= 0)
			{
				ItemsCountSize = SizeViewState.Small;
			}
			else if (_phones.Count >= MaxListCount)
			{
				ItemsCountSize = SizeViewState.Big;
			}
			else
			{
				ItemsCountSize = SizeViewState.Medium;
			}
		}
	}
}