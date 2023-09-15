using System.Collections.Generic;
using UnityEngine;

//todo:
// implement some sample
// add sample of validation
// add mocks for meta
// improve editor
// handle several property names in one view (CollectionViewBinding).
//   add attribute for property name field and select it from view editor
// support of navigation
// add SetField for INotifyPropertyChanged in ViewModel
// add ViewBinding<IEnumerable> for readonly collections
// activate/deactivate view?
// add GenericObjectClass to store common stuff for GenericObject
// handle errors when ViewBinding reference nonexistent property (on property renaming)
// implement pool
// show detailed errors if name or type of a property was changed in a view model (runtime and editor)
// add event args caching inside ViewModel
// move GenericObject functionality from ViewModel to ViewBinding
// add ability to select state of StateViewBinding in editor

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class View<TViewModel>: TypedViewBinding<TViewModel>
		where TViewModel: class, IViewModel
	{
		[field: SerializeField]
		private List<ViewBinding> Bindings { get; set; }

		protected virtual void OnValidate()
		{
			if (Value != null)
			{
				SetViewModelSettings();
			}
		}

		public override void SetValue(TViewModel value)
		{
			base.SetValue(value);

			foreach (var binding in Bindings)
			{
				binding.ViewModel = value;
			}

			if (value != null)
			{
				SetViewModelSettings();
			}
		}

		protected virtual void SetViewModelSettings()
		{
		}
	}
}