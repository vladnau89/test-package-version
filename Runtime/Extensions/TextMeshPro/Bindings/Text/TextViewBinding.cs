using TMPro;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM.TextMeshPro
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public abstract class TextViewBinding<TValue>: TypedViewBinding<TValue>
	{
		[field: SerializeField]
		private string Format { get; set; }
		
		[field: SerializeField, HideInInspector]
		private TextMeshProUGUI Text { get; set; }

		private void OnValidate()
		{
			Text = GetComponent<TextMeshProUGUI>();
		}

		public override void SetValue(TValue value)
		{
			base.SetValue(value);
			Text.text = string.IsNullOrWhiteSpace(Format) ? ConvertToStringIfNoFormat(value) : string.Format(Format, value);
		}

		protected abstract string ConvertToStringIfNoFormat(TValue value);
	}
}
