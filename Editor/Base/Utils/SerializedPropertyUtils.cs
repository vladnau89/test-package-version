using System;
using System.Collections.Generic;
using UnityEditor;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	public static class SerializedPropertyUtils
	{
		public static IEnumerable<SerializedProperty> EnumerateArray(this SerializedProperty property)
		{
			if (!property.isArray)
			{
				throw new ArgumentException($"Property must be array.", nameof(property));
			}

			for (var i = 0; i < property.arraySize; i++)
			{
				yield return property.GetArrayElementAtIndex(i);
			}
		}
	}
}