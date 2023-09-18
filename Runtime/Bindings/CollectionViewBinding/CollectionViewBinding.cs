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
	public abstract class CollectionViewBinding<TCollection>
	{
		private readonly struct Item
		{
        }

		[MaybeNull]
		[field: SerializeField]
		private string SelectedPropertyName { get; set; }

		[MaybeNull]
		private object SelectedValue
		{
			get => null;
			set
			{
				
			}
		}

		[MaybeNull]
		public object SelectedItem { get; private set; }

		private List<Item> Items { get; } = new();

		public event EventHandler SelectedItemChanged;
        

	}
}
