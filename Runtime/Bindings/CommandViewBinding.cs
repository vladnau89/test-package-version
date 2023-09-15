using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public abstract class CommandActionViewBinding<TParameter>: TypedViewBinding<ICommand<TParameter>>
	{
		[MaybeNull]
		[field: SerializeField]
		private BoolViewBinding CanExecute { get; set; }

		[MaybeNull]
		protected abstract TParameter Parameter { get; }

		protected void OnActionPerformed()
		{
			Value.Execute(Parameter);
		}

		public override void SetValue(ICommand<TParameter> value)
		{
			if (Value != null)
			{
				Value.CanExecuteChanged -= OnCanExecuteChanged;
			}

			base.SetValue(value);

			if (Value != null)
			{
				Value.CanExecuteChanged += OnCanExecuteChanged;
				UpdateAvailability();
			}
		}

		private void OnCanExecuteChanged(object sender, EventArgs args)
		{
			UpdateAvailability();
		}

		private void UpdateAvailability()
		{
			if (CanExecute)
			{
				CanExecute.SetValue(Value.CanExecute(Parameter));
			}
		}
	}
}
