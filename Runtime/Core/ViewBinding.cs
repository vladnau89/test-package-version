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
				if (_viewModel == value)
				{
					return;
				}

				if (_viewModel != null)
				{
					_viewModel.PropertyChanged -= OnViewModelPropertyChanged;
					_viewModel = null; // set _viewModel to null before value clearing to prevent clearing of _viewModel
					ClearValue();
				}

				_viewModel = value;

				if (_viewModel != null)
				{
					_viewModel.PropertyChanged += OnViewModelPropertyChanged;
					UpdateValueFromViewModel();
				}
			}
		}

		public abstract object ObjectValue { get; set; }

		protected abstract void ClearValue();

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

		protected virtual void OnDestroy()
		{
			ViewModel = null;
		}
	}
}