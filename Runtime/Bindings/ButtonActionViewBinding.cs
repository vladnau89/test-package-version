using UnityEngine;
using UnityEngine.UI;

namespace SM.Core.Unity.UI.MVVM
{
	[RequireComponent(typeof(Button))]
	public class ButtonActionViewBinding: CommandActionViewBinding<object>
	{
		[field: SerializeField]
		private string ParameterPropertyName { get; set; }

		[field: SerializeField, HideInInspector]
		private Button Button { get; set; }

		protected override object Parameter => GetViewModelProperty<object>(ParameterPropertyName, false);

		private void OnValidate()
		{
			Button = GetComponent<Button>();
		}

		private void Awake()
		{
			Button.onClick.AddListener(OnActionPerformed);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Value = null;
			Button.onClick.RemoveListener(OnActionPerformed);
		}
	}
}