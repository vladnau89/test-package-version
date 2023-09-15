using System.Globalization;

namespace SM.Core.Unity.UI.MVVM.TextMeshPro
{
	public class TextIntViewBinding: TextViewBinding<int>
	{
		protected override string ConvertToStringIfNoFormat(int value)
		{
			return value.ToString(CultureInfo.CurrentUICulture);
		}
	}
}
