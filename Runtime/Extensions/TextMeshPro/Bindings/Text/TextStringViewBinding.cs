namespace SM.Core.Unity.UI.MVVM.TextMeshPro
{
	public class TextStringViewBinding: TextViewBinding<string>
	{
		protected override string ConvertToStringIfNoFormat(string value)
		{
			return value;
		}
	}
}