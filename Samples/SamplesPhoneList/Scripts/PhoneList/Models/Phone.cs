using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SM.Core.Unity.UI.MVVM.Samples.PhoneList
{
	internal class Phone: INotifyPropertyChanged
	{
		private string _title;
		private string _company;
		private int _price;

		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				OnPropertyChanged();
			}
		}

		public string Company
		{
			get => _company;
			set
			{
				_company = value;
				OnPropertyChanged();
			}
		}

		public int Price
		{
			get => _price;
			set
			{
				_price = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}