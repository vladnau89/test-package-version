using UnityEngine;

namespace SM.Core.Unity.UI.MVVM.Samples.PhoneList
{
	internal class PhoneListWindow: View<PhoneListViewModel>
	{
		[field: Header("Settings")]
		[field: SerializeField, Range(0, 20)]
		private int MaxListCount { get; set; }

		protected override void SetViewModelSettings()
		{
			Value.SetSettings(MaxListCount);
		}
	}
}