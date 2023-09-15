using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	public class ObjectPickerDropdown<T>: AdvancedDropdown
		where T: Component
	{
		private Action<T> SelectItem { get; }
		
		[MaybeNull]
		private Func<T, bool> Filter { get; }

		public ObjectPickerDropdown(Action<T> selectItem, Func<T, bool> filter = null):
			base(new AdvancedDropdownState())
		{
			SelectItem = selectItem;
			Filter = filter;
		}

		protected override AdvancedDropdownItem BuildRoot()
		{
			var root = new AdvancedDropdownItem(typeof(T).Name);
			
			foreach (var item in FindObjects())
			{
				root.AddChild(new KeyValueDropdownItem<T>($"{item.name} ({item.GetType().Name})", item));
			}

			return root;
		}

		protected override void ItemSelected(AdvancedDropdownItem item)
		{
			base.ItemSelected(item);

			var selectedItem = ((KeyValueDropdownItem<T>)item).Value;
			SelectItem(selectedItem);
		}

		private IEnumerable<T> FindObjects()
		{
			var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
			var allObjects = (prefabStage != null)
				? prefabStage.FindComponentsOfType<T>()
				: Object.FindObjectsOfType<T>(true);
			return (Filter != null) ? allObjects.Where(Filter) : allObjects;
		}
	}
}