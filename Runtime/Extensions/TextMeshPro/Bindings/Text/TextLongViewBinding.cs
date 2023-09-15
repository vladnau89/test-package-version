// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System.Globalization;

namespace SM.Core.Unity.UI.MVVM.TextMeshPro
{
	public class TextLongViewBinding: TextViewBinding<long>
	{
		protected override string ConvertToStringIfNoFormat(long value)
		{
			return value.ToString(CultureInfo.CurrentUICulture);
		}
	}
}
