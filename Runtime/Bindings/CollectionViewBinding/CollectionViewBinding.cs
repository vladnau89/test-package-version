// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class CollectionViewBinding<TCollection>: TypedViewBinding<TCollection>
	{
		private readonly struct Item
		{
			public ViewBinding ViewBinding { get; }

			[MaybeNull]
			public SelectableViewModel Selectable { get; }

			public Item(ViewBinding viewBinding, [MaybeNull] SelectableViewModel selectable)
			{
				ViewBinding = viewBinding;
				Selectable = selectable;
			}
		}

		[MaybeNull]
		[field: SerializeField]
		private string SelectedPropertyName { get; set; }

		[MaybeNull]
		private object SelectedValue
		{
			get => GetViewModelProperty<object>(SelectedPropertyName, false);
			set => SetViewModelProperty(SelectedPropertyName, value, false);
		}

		[MaybeNull]
		public object SelectedItem { get; private set; }

		private List<Item> Items { get; } = new();

		public event EventHandler SelectedItemChanged;

		protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			base.OnViewModelPropertyChanged(sender, args);
			if (SelectedPropertyName == args.PropertyName)
			{
				var selectedValue = SelectedValue;
				foreach (var item in Items)
				{
					if (item.Selectable != null)
					{
						if (selectedValue == null)
						{
							item.Selectable.Selected = false;
						}
						else if (item.ViewBinding.ObjectValue == selectedValue)
						{
							item.Selectable.Selected = true;
							return;
						}
					}
				}
			}
		}

		protected abstract ViewBinding CreateViewBinding(int index);

		protected abstract void DestroyViewBinding(ViewBinding viewBinding);

		protected void Add(int index, object value)
		{
			var viewBinding = CreateViewBinding(index);
			viewBinding.ObjectValue = value;

			var selectable = default(SelectableViewModel);
			if (value is IHavingSelectable havingSelectable)
			{
				selectable = havingSelectable.Selectable;
				selectable.PropertyChanged += OnSelectablePropertyChanged;
			}

			Items.Insert(index, new Item(viewBinding, selectable));
		}

		protected void AddRange(IList values)
		{
			for (var i = 0; i < values.Count; i++)
			{
				Add(i, values[i]);
			}
		}

		protected void Remove(int index)
		{
			var item = Items[index];

			item.ViewBinding.ObjectValue = null;
			DestroyViewBinding(item.ViewBinding);

			if (item.Selectable != null)
			{
				item.Selectable.PropertyChanged -= OnSelectablePropertyChanged;
			}

			Items.RemoveAt(index);
		}

		protected void RemoveAll()
		{
			foreach (var item in Items)
			{
				if (item.Selectable != null)
				{
					item.Selectable.PropertyChanged -= OnSelectablePropertyChanged;
				}

				item.ViewBinding.ObjectValue = null;
				DestroyViewBinding(item.ViewBinding);
			}
			Items.Clear();
		}

		private void OnSelectablePropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			var selectable = (SelectableViewModel)sender;
			if (args.PropertyName == nameof(SelectableViewModel.Selected) && selectable.Selected)
			{
				var newViewBinding = default(ViewBinding);
				foreach (var item in Items)
				{
					if (item.Selectable != null)
					{
						if (item.Selectable == selectable)
						{
							newViewBinding = item.ViewBinding;
						}
						else
						{
							item.Selectable.Selected = false;
						}
					}
				}

				SelectedValue = newViewBinding ? newViewBinding.ObjectValue : null;
				SelectedItem = newViewBinding;
				SelectedItemChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
