using UnityEditor;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	public static class SerializedObjectUtils
	{
		public static SerializedProperty FindAutoProperty(this SerializedObject obj, string propertyPath)
		{
			return obj.FindProperty($"<{propertyPath}>k__BackingField");
		}

		public static SerializedProperty FindAutoPropertyRelative(
			this SerializedProperty property,
			string relativePropertyPath)
		{
			return property.FindPropertyRelative($"<{relativePropertyPath}>k__BackingField");
		}
	}
}