using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public class StateViewBinding<TEnum>: TypedViewBinding<TEnum>
		where TEnum: Enum
	{
		[Serializable]
		private struct State
		{
			[field: SerializeField]
			internal TEnum Value { get; private set; }

			[MaybeNull]
			[field: SerializeField]
			internal ViewState ViewState { get; set; }

			internal State(TEnum value, ViewState viewState)
			{
				Value = value;
				ViewState = viewState;
			}
		}

		[Serializable]
		private struct StateTransition
		{
			[field: SerializeField]
			internal TEnum FromValue { get; private set; }

			[field: SerializeField]
			internal TEnum ToValue { get; private set; }

			[MaybeNull]
			[field: SerializeField]
			internal ViewState ViewState { get; set; }
		}

		[field: SerializeField]
		private State[] States { get; set; }

		[field: SerializeField]
		private StateTransition[] StateTransitions { get; set; }

		protected override void ClearValue()
		{
		}

		public override void SetValue(TEnum value)
		{
			var fromValue = Value;
			base.SetValue(value);

			foreach (var stateTransition in StateTransitions)
			{
				if (EqualityComparer<TEnum>.Default.Equals(stateTransition.FromValue, fromValue) &&
					EqualityComparer<TEnum>.Default.Equals(stateTransition.ToValue, value))
				{
					if (stateTransition.ViewState)
					{
						stateTransition.ViewState.Apply();
					}
					break;
				}
			}

			foreach (var state in States)
			{
				if (EqualityComparer<TEnum>.Default.Equals(state.Value, value))
				{
					if (state.ViewState)
					{
						state.ViewState.Apply();
					}
					break;
				}
			}
		}

#if UNITY_EDITOR
		private void Reset()
		{
			States = Array.Empty<State>();
			EnsureStatesMatchEnum();
			EnsureStateGameObjects();
		}

		private void OnValidate()
		{
			EnsureStatesMatchEnum();
		}

		private void EnsureStatesMatchEnum()
		{
			var enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));

			if (States.Select(s => s.Value).SequenceEqual(enumValues))
			{
				return;
			}

			var oldStates = States;
			States = new State[enumValues.Length];
			for (var i = 0; i < enumValues.Length; i++)
			{
				var enumValue = enumValues[i];
				var viewState = oldStates
					.FirstOrDefault(s => EqualityComparer<TEnum>.Default.Equals(s.Value, enumValue))
					.ViewState;
				States[i] = new State(enumValue, viewState);
			}

			UnityEditor.EditorUtility.SetDirty(this);
		}

		internal void EnsureStateGameObjects()
		{
			var viewStates = transform.GetComponentsInChildren<ViewState>(true);

			// handle enum item addition or removal
			for (var i = 0; i < States.Length; i++)
			{
				var stateObjectName = $"{States[i].Value}State";

				if (States[i].ViewState == null)
				{
					States[i].ViewState = StateViewBindingUtils.EnsureStateObject(stateObjectName, transform);
					UnityEditor.EditorUtility.SetDirty(this);
				}

				if (States[i].ViewState.name != stateObjectName)
				{
					var viewState = viewStates.FirstOrDefault(x => x.name == stateObjectName);
					if (viewState != null)
					{
						States[i].ViewState = viewState;
						UnityEditor.EditorUtility.SetDirty(this);
					}
				}
			}

			// handle enum item renaming
			for (var i = 0; i < States.Length; i++)
			{
				var stateObjectName = $"{States[i].Value}State";
				var currentViewState = States[i].ViewState;

				if (currentViewState != null && currentViewState.name != stateObjectName)
				{
					if (States.Count(x => x.ViewState == currentViewState) > 1)
					{
						States[i].ViewState = StateViewBindingUtils.EnsureStateObject(stateObjectName, transform);
					}
					else
					{
						currentViewState.name = stateObjectName;
					}

					UnityEditor.EditorUtility.SetDirty(this);
				}
			}

			// delete unused state objects
			foreach (var viewState in viewStates)
			{
				if (States.All(x => x.ViewState != viewState))
				{
					DestroyImmediate(viewState.gameObject);
				}
			}
		}
#endif
	}
}