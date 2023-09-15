using System.ComponentModel;

namespace SM.Core.Unity.UI.MVVM
{
	public interface IViewModel: INotifyPropertyChanged
	{
		public T GetProperty<T>(string propertyName);

		public void SetProperty<T>(string propertyName, T value);
	}
}