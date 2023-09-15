using System.Globalization;

namespace SM.Core.Unity.UI.MVVM.TextMeshPro
{
	public class TextFloatViewBinding: TextViewBinding<float>
	{
		protected override string ConvertToStringIfNoFormat(float value)
		{
			return value.ToString(CultureInfo.CurrentUICulture);
		}
	}
}
