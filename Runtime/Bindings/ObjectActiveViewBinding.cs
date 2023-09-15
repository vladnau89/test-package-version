namespace SM.Core.Unity.UI.MVVM
{
	public class ObjectActiveViewBinding: BoolViewBinding
	{
		public override void SetValue(bool value)
		{
			base.SetValue(value);
			gameObject.SetActive(value);
		}
	}
}