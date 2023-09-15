using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class ViewModel: IViewModel
	{
		private bool _active;

		public bool Active
		{
			get => _active;
			set
			{
				if (SetField(ref _active, value))
				{
					OnActiveChanged();
				}
			}
		}

		private GenericObject GenericObject { get; }

		public event PropertyChangedEventHandler PropertyChanged;

		protected ViewModel(bool active = true)
		{
			GenericObject = new GenericObject(this);
			_active = active;
		}

		public T GetProperty<T>(string propertyName)
		{
			return GenericObject.GetProperty<T>(propertyName);
		}

		public void SetProperty<T>(string propertyName, T value)
		{
			GenericObject.SetProperty(propertyName, value);
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
			{
				return false;
			}

			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		protected bool SetField(ref float field, float value, [CallerMemberName] string propertyName = null)
		{
			if (Mathf.Approximately(field, value))
			{
				return false;
			}

			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		protected bool SetField(ref float field, float value, int digits, [CallerMemberName] string propertyName = null)
		{
			value = MathF.Round(value, digits);
			return SetField(ref field, value, propertyName);
		}

		protected virtual void OnActiveChanged()
		{
		}
	}
}