using UnityEngine;

namespace SM.Core.Unity.UI.MVVM.Samples.PhoneList
{
	internal class Launcher: MonoBehaviour
	{
		[field: SerializeField]
		private PhoneListWindow PhoneListWindow { get; set; }

		private void Awake()
		{
			PhoneListWindow.Value = new PhoneListViewModel();
		}

		private void OnDestroy()
		{
			PhoneListWindow.Value = null;
		}
	}
}