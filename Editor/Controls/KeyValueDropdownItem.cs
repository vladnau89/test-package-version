using UnityEditor.IMGUI.Controls;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	public class KeyValueDropdownItem<TValue>: AdvancedDropdownItem
	{
		public TValue Value { get; }

		public KeyValueDropdownItem(string name, TValue value): base(name)
		{
			Value = value;
		}
	}
}