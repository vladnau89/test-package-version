using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	[RequireComponent(typeof(Canvas))]
	public class CanvasActiveViewBinding: BoolViewBinding
	{
		[field: SerializeField, HideInInspector]
		private Canvas Canvas { get; set; }

		private void OnValidate()
		{
			Canvas = GetComponent<Canvas>();
		}
		
		public override void SetValue(bool value)
		{
			base.SetValue(value);
			Canvas.enabled = value;
		}
	}
}