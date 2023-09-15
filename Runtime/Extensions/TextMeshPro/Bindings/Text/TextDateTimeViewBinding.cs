using System;
using System.Globalization;

namespace SM.Core.Unity.UI.MVVM.TextMeshPro
{
	public class TextDateTimeViewBinding: TextViewBinding<DateTime>
	{
		protected override string ConvertToStringIfNoFormat(DateTime value)
		{
			return value.ToString(CultureInfo.CurrentUICulture);
		}
	}
}
