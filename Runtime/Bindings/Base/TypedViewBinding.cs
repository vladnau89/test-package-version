using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class TypedViewBinding<TValue>: ViewBinding
	{
		private TValue _value;

		public TValue Value
		{
			get => _value;
			set => SetValue(value);
		}

		public override object ObjectValue
		{
			get => _value;
			set => SetValue((TValue)value);
		}

		public virtual void SetValue(TValue value)
		{
			_value = value;
		}

		/// <summary>
		/// Clear value on view model unbinding.
		/// It's important to clear the Value to unsubscribe Value events or to clear some stuff.
		/// In cases where this is not necessary you shouldn't clear the Value and may override this method.
		/// It's useful to skip clearing in cases when you want to save state of ViewBinding (e.g. StateViewBinding).
		/// </summary>
		protected override void ClearValue()
		{
			Value = default;
		}

		protected sealed override void UpdateValueFromViewModel()
		{
			var value = GetViewModelProperty<TValue>(PropertyName);
			SetValue(value);
		}

		protected void UpdateValueInViewModel(TValue value)
		{
			SetViewModelProperty(PropertyName, value);
		}

		[return: MaybeNull]
		protected T GetViewModelProperty<T>(string propertyName, bool required = true)
		{
			if (!required && string.IsNullOrWhiteSpace(propertyName))
			{
				return default;
			}

			try
			{
				return ViewModel != null ? ViewModel.GetProperty<T>(propertyName) : default;
			}
			catch (Exception e) when (e is ArgumentException or InvalidCastException)
			{
				Debug.LogError(e, this);
				return default;
			}
		}

		protected void SetViewModelProperty<T>(string propertyName, T value, bool required = true)
		{
			if (!required && string.IsNullOrWhiteSpace(propertyName))
			{
				return;
			}

			try
			{
				ViewModel?.SetProperty(propertyName, value);
			}
			catch (Exception e) when (e is ArgumentException or InvalidCastException or InvalidOperationException)
			{
				Debug.LogError(e, this);
			}
		}
	}
}