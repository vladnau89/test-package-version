using UnityEngine;

namespace SM.Core.Unity.UI.MVVM
{
	public static class TransformUtils
	{
		public static bool IsChildOf(this Transform transform, Transform potentialParent)
		{
			while (transform != null && transform.parent != potentialParent)
			{
				transform = transform.parent;
			}

			return transform != null;
		}
		
		public static bool IsEqualsOrChildOf(this Transform transform, Transform potentialParent)
		{
			return transform == potentialParent || transform.IsChildOf(potentialParent);
		}
	}
}