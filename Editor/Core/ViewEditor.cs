using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Component = UnityEngine.Component;

namespace SM.Core.Unity.UI.MVVM.Editor
{
	[CustomEditor(typeof(View<>), true)]
	public class ViewEditor: UnityEditor.Editor
	{
		private SerializedProperty PropertyName { get; set; }
		private SerializedProperty Bindings { get; set; }

		private void OnEnable() 
		{
			PropertyName = serializedObject.FindAutoProperty("PropertyName");
			Bindings = serializedObject.FindAutoProperty("Bindings");
		}
		
		public override void OnInspectorGUI()
		{
			EditorGUILayout.PropertyField(PropertyName);

			RemoveEmptyBindings();

			GUI.enabled = false;
			EditorGUILayout.IntField("Bindings Count", Bindings.arraySize);
			GUI.enabled = true;

			var gropedBindings = Bindings
				.EnumerateArray()
				.Select(b => new { Property = b, Object = new SerializedObject(b.objectReferenceValue) })
				.GroupBy(b => b.Object.FindAutoProperty("PropertyName").stringValue)
				.Where(g => g.Key != null)
				.ToDictionary(g => g.Key, g => g.ToArray());
			
			var flags = BindingFlags.Instance | BindingFlags.Public;
			var viewModelType = target.GetType().BaseType.GetGenericArguments()[0];
			var properties = viewModelType.GetProperties(flags).Where(p => p.GetIndexParameters().Length == 0);
			foreach (var property in properties)
			{
				gropedBindings.TryGetValue(property.Name, out var propertyBindings);
				
				GUILayout.BeginHorizontal();
			
				EditorGUILayout.LabelField(property.Name);
				if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
				{
					var objectPicker = new ObjectPickerDropdown<ViewBinding>(
						(selectedValue) =>
						{
							var newElementIndex = Bindings.arraySize;
							Bindings.InsertArrayElementAtIndex(newElementIndex);
							var newBinding = Bindings.GetArrayElementAtIndex(newElementIndex);
							newBinding.objectReferenceValue = selectedValue;
							var so = new SerializedObject(selectedValue);
							so.FindAutoProperty("PropertyName").stringValue = property.Name;
							so.ApplyModifiedProperties();
							Bindings.serializedObject.ApplyModifiedProperties();
						},
						(binding) =>
						{
							var propertyType = property.PropertyType;
							var bindingType = binding.GetType().GetProperty("Value").PropertyType;
							return bindingType.IsAssignableFrom(propertyType);
						});
					objectPicker.Show(new Rect());
				}
				if (GUILayout.Button("-", GUILayout.ExpandWidth(false)))
				{
					if (propertyBindings != null)
					{
						var propertyBinding = propertyBindings[^1];
						
						propertyBinding.Object.FindAutoProperty("PropertyName").stringValue = "";
						propertyBinding.Property.DeleteCommand();

						propertyBinding.Object.ApplyModifiedProperties();
						propertyBinding.Property.serializedObject.ApplyModifiedProperties();
						return;
					}
				}
			
				GUILayout.EndHorizontal();
			
				EditorGUI.indentLevel++;
				if (propertyBindings != null)
				{
					for (var i = 0; i < propertyBindings.Length; i++)
					{
						var propertyBinding = propertyBindings[i];
						EditorGUILayout.LabelField(propertyBinding.Property.objectReferenceValue.name);
						// EditorGUILayout.PropertyField(propertyBinding, new GUIContent($"Binding {i}"));
					}
				}
				EditorGUI.indentLevel--;
			}

			var nextProperty = serializedObject.GetIterator();
			nextProperty.Next(true);
			do
			{
				if (nextProperty.name != PropertyName.name &&
					nextProperty.name != Bindings.name &&
					nextProperty.name != "m_ObjectHideFlags" &&
					nextProperty.name != "m_Script")
				{
					EditorGUILayout.PropertyField(nextProperty);
				}
			} while (nextProperty.NextVisible(false));
			
			serializedObject.ApplyModifiedProperties();

			void RemoveEmptyBindings()
			{
				for (var i = 0; i < Bindings.arraySize; i++)
				{
					var binding = Bindings.GetArrayElementAtIndex(i);
					if (binding.objectReferenceValue == null)
					{
						binding.DeleteCommand();
					}
					else
					{
						var bindingValue = new SerializedObject(binding.objectReferenceValue);
						var propertyName = bindingValue.FindAutoProperty("PropertyName").stringValue;
						if (string.IsNullOrWhiteSpace(propertyName))
						{
							binding.DeleteCommand();
						}
					}
				}
				
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}