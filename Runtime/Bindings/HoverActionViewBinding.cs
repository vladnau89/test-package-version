// Copyright (c) Saber BGS 2023. All rights reserved.
// ---------------------------------------------------------------------------------------------

using UnityEngine.EventSystems;

namespace SM.Core.Unity.UI.MVVM
{
	public class HoverActionViewBinding: CommandActionViewBinding<object>, IPointerEnterHandler
	{
		protected override object Parameter => null;

		public void OnPointerEnter(PointerEventData eventData)
		{
			OnActionPerformed();
		}
	}
}
