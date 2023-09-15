using System.Globalization;

namespace SM.Core.Unity.UI.MVVM.TextMeshPro
{
	public class TextDoubleViewBinding: TextViewBinding<double>
	{
		protected override string ConvertToStringIfNoFormat(double value)
		{
			return value.ToString(CultureInfo.CurrentUICulture);
		}
	}
}
