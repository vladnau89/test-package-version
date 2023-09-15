using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class CollectionViewBinding: TypedViewBinding<INotifyCollectionChanged>
	{
		[field: SerializeField]
		private ViewBinding ItemPrefab { get; set; }

		[MaybeNull]
		[field: SerializeField]
		private string SelectedPropertyName { get; set; }

		[MaybeNull]
		[field: SerializeField]
		private ViewPool ViewPool { get; set; }

		private IViewPool _pool;

		private IViewPool Pool => _pool ??= ViewPool ? ViewPool : new EmptyViewPool();

		[MaybeNull]
		private object SelectedValue
		{
			get => GetViewModelProperty<object>(SelectedPropertyName, false);
			set => SetViewModelProperty(SelectedPropertyName, value, false);
		}

		[MaybeNull]
		public object SelectedItem { get; private set; }

		private List<(ViewBinding, SelectableViewModel)> Items { get; } = new();

		public event EventHandler SelectedItemChanged;

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Value = null;
		}

		protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			base.OnViewModelPropertyChanged(sender, args);
			if (SelectedPropertyName == args.PropertyName)
			{
				var selectedValue = SelectedValue;
				var newSelectedItem = default(ViewBinding);
				foreach (var (viewBinding, selectableViewModel) in Items)
				{
					if (selectableViewModel.Value == selectedValue)
					{
						newSelectedItem = viewBinding;
						selectableViewModel.Selected = true;
					}
					else
					{
						selectableViewModel.Selected = false;
					}
				}

				SelectedItem = newSelectedItem;
				SelectedItemChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public override void SetValue(INotifyCollectionChanged value)
		{
			// todo: add editor time validation
			if (value != null && value is not IList)
			{
				throw new ArgumentException($"Parameter must implement {nameof(IList)}.", nameof(value));
			}

			if (Value != null)
			{
				Value.CollectionChanged -= OnCollectionChanged;
				RemoveAll();
			}

			base.SetValue(value);

			if (Value != null)
			{
				Value.CollectionChanged += OnCollectionChanged;
				AddRange((IList)value);
			}
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					Add(args.NewStartingIndex, args.NewItems[0]);
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Remove:
					Remove(args.OldStartingIndex);
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Add(int index, object value)
		{
			var item = Pool.Spawn(ItemPrefab);
			item.transform.SetParent(transform, false);
			item.SetValue(value);

			var selectableViewModel = default(SelectableViewModel);
			if (item.TryGetComponent<SelectableView>(out var selectableItem))
			{
				selectableViewModel = new SelectableViewModel(value, value == SelectedValue, x => SelectedValue = x);
				selectableItem.Value = selectableViewModel;
			}

			Items.Insert(index, (item, selectableViewModel));
		}

		private void AddRange(IList values)
		{
			for (var i = 0; i < values.Count; i++)
			{
				Add(i, values[i]);
			}
		}

		private void Remove(int index)
		{
			var item = Items[index].Item1;
			Items.RemoveAt(index);
			Pool.Despawn(item);
		}

		private void RemoveAll()
		{
			foreach (var (item, _) in Items)
			{
				Pool.Despawn(item);
			}
			Items.Clear();
		}
	}
}