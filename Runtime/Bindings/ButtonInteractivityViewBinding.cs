using UnityEngine;
using UnityEngine.UI;

namespace SM.Core.Unity.UI.MVVM
{
	[RequireComponent(typeof(Button))]
	public class ButtonInteractivityViewBinding: BoolViewBinding
	{
		[field: SerializeField, HideInInspector]
		private Button Button { get; set; }

		private void OnValidate()
		{
			Button = GetComponent<Button>();
		}
		
		public override void SetValue(bool value)
		{
			base.SetValue(value);
			Button.interactable = value;
		}
	}
}