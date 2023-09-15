using TMPro;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM.TextMeshPro
{
	[RequireComponent(typeof(TMP_InputField))]
	public class InputTextViewBinding: TypedViewBinding<string>
	{
		[field: SerializeField, HideInInspector]
		private TMP_InputField InputText { get; set; }

		private void OnValidate()
		{
			InputText = GetComponent<TMP_InputField>();
		}

		private void Awake()
		{
			InputText.onValueChanged.AddListener(OnInputTextValueChanged);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			InputText.onValueChanged.RemoveListener(OnInputTextValueChanged);
		}

		public override void SetValue(string value)
		{
			base.SetValue(value);
			InputText.text = value;
		}

		private void OnInputTextValueChanged(string value)
		{
			UpdateValueInViewModel(value);
		}
	}
}