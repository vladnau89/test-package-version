// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace SM.Core.Unity.UI.MVVM
{
	public class GridSelectorViewModel: ViewModel, IDisposable
	{
		private int SelectedRowIndex { get; set; }
		private int SelectedColumnIndex { get; set; }

		[MaybeNull]
		private SelectableViewModel SelectedItem
		{
			get
			{
				if (SelectedRowIndex != -1 && SelectedColumnIndex != -1)
				{
					return SelectableItems[SelectedRowIndex, SelectedColumnIndex];
				}
				else
				{
					return null;
				}
			}
			set
			{
				var selectedItem = SelectedItem;
				if (selectedItem == value)
				{
					return;
				}

				if (value != null)
				{
					for (var i = 0; i < RowCapacity; i++)
					{
						for (var j = 0; j < ColumnCapacity; j++)
						{
							if (this[i, j] == value)
							{
								SelectItem(i, j);
								return;
							}
						}
					}
				}
				else
				{
					SelectItem(-1, -1);
				}
			}
		}

		private SelectableViewModel[,] SelectableItems { get; }

		private int RowCapacity => SelectableItems.GetLength(0);
		private int ColumnCapacity => SelectableItems.GetLength(1);

		public GenericCommand<float> HorizontalNavigateCommand { get; }
		public GenericCommand<float> VerticalNavigateCommand { get; }

		public GridSelectorViewModel(int rowCapacity, int columnCapacity)
		{
			SelectableItems = new SelectableViewModel[rowCapacity, columnCapacity];
			SelectedRowIndex = -1;
			SelectedColumnIndex = -1;

			HorizontalNavigateCommand = new GenericCommand<float>(ExecuteHorizontalNavigateCommand);
			VerticalNavigateCommand = new GenericCommand<float>(ExecuteVerticalNavigateCommand);
		}

		public void Dispose()
		{
			Clear();
		}

		public SelectableViewModel this[int i, int j]
		{
			get => SelectableItems[i, j];
			set
			{
				var oldValue = this[i, j];
				if (oldValue != null)
				{
					oldValue.PropertyChanged -= OnSelectablePropertyChanged;
				}

				SelectableItems[i, j] = value;

				if (value != null)
				{
					value.PropertyChanged += OnSelectablePropertyChanged;
				}
			}
		}

		public void SetRow(int rowIndex, IEnumerable<SelectableViewModel> selectables)
		{
			var columnIndex = 0;
			foreach (var selectable in selectables)
			{
				this[rowIndex, columnIndex] = selectable;
				columnIndex++;
			}
		}

		public void SetColumn(int columnIndex, IEnumerable<SelectableViewModel> selectables)
		{
			var rowIndex = 0;
			foreach (var selectable in selectables)
			{
				this[rowIndex, columnIndex] = selectable;
				rowIndex++;
			}
		}

		public void Clear()
		{
			for (var i = 0; i < RowCapacity; i++)
			{
				for (var j = 0; j < ColumnCapacity; j++)
				{
					this[i, j] = null;
				}
			}
		}

		public void SelectFirstItem()
		{
			SelectItem(0, 0);
		}

		public void SelectItem(int rowIndex, int columnIndex)
		{
			if (SelectedRowIndex == rowIndex && SelectedColumnIndex == columnIndex)
			{
				return;
			}

			SelectedRowIndex = rowIndex;
			SelectedColumnIndex = columnIndex;

			var selectedItem = SelectedItem;
			foreach (var selectable in SelectableItems)
			{
				selectable.Selected = (selectable == selectedItem);
			}
		}

		private void OnSelectablePropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			var selectable = (SelectableViewModel)sender;
			if (args.PropertyName == nameof(SelectableViewModel.Selected) && selectable.Selected)
			{
				SelectedItem = selectable;
			}
		}

		private void ExecuteHorizontalNavigateCommand(float input)
		{
			if (SelectedItem == null)
			{
				SelectFirstItem();
				return;
			}

			var selectedColumnIndex = SelectedColumnIndex;
			if (input > 0)
			{
				selectedColumnIndex = (selectedColumnIndex + 1) % ColumnCapacity;
			}
			else if (input < 0)
			{
				selectedColumnIndex = (selectedColumnIndex + ColumnCapacity - 1) % ColumnCapacity;
			}

			SelectItem(SelectedRowIndex, selectedColumnIndex);
		}

		private void ExecuteVerticalNavigateCommand(float input)
		{
			if (SelectedItem == null)
			{
				SelectFirstItem();
				return;
			}

			var selectedRowIndex = SelectedRowIndex;
			if (input > 0)
			{
				selectedRowIndex = (selectedRowIndex + RowCapacity - 1) % RowCapacity;
			}
			else if (input < 0)
			{
				selectedRowIndex = (selectedRowIndex + 1) % RowCapacity;
			}

			SelectItem(selectedRowIndex, SelectedColumnIndex);
		}
	}
}
