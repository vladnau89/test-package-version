using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class DynamicCollectionViewBinding: CollectionViewBinding<INotifyCollectionChanged>
	{
		[field: SerializeField]
		private ViewBinding ItemPrefab { get; set; }

		[MaybeNull]
		[field: SerializeField]
		private ViewPool ViewPool { get; set; }

		private IViewPool _pool;

		private IViewPool Pool => _pool ??= ViewPool ? ViewPool : new EmptyViewPool();

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

		protected override ViewBinding CreateViewBinding(int index)
		{
			var viewBinding = Pool.Spawn(ItemPrefab);
			viewBinding.transform.SetParent(transform, false);
			return viewBinding;
		}

		protected override void DestroyViewBinding(ViewBinding viewBinding)
		{
			Pool.Despawn(viewBinding);
		}
	}
}