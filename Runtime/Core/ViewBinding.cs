using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class ViewBinding: MonoBehaviour
	{
		[field: SerializeField, ReadOnlyField]
		protected string PropertyName { get; private set; }

		[MaybeNull]
		private IViewModel _viewModel;

		[MaybeNull]
		protected internal IViewModel ViewModel
		{
			get => _viewModel;
			set
			{
				if (_viewModel != null)
				{
					_viewModel.PropertyChanged -= OnViewModelPropertyChanged;
				}

				_viewModel = value;

				if (_viewModel != null)
				{
					_viewModel.PropertyChanged += OnViewModelPropertyChanged;
					UpdateValueFromViewModel();
				}
			}
		}

		protected virtual void OnDestroy()
		{
			ViewModel = null;
		}

		public abstract void SetValue(object value);

		protected abstract void UpdateValueFromViewModel();

		protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (ViewModel == null || PropertyName == null)
			{
				throw new InvalidOperationException($"{nameof(SM.Core.Unity.UI.MVVM.ViewModel)} isn't assigned.");
			}

			if (PropertyName == args.PropertyName)
			{
				UpdateValueFromViewModel();
			}
		}
	}
}