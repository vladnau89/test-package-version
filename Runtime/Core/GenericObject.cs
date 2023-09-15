using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SM.Core.Unity.UI.MVVM
{
	public readonly struct GenericObject
	{
		private readonly struct PropertyAccessor
		{
			internal Delegate Getter { get; }

			[MaybeNull]
			internal Delegate Setter { get; }

			internal Type PropertyType => Getter.GetType().GenericTypeArguments[0];

			internal PropertyAccessor(Delegate getter, [MaybeNull] Delegate setter)
			{
				Getter = getter;
				Setter = setter;
			}
		}

		private Dictionary<string, PropertyAccessor> PropertyAccessors { get; }

		public GenericObject(object @object)
		{
			PropertyAccessors = CreatePropertyAccessors();

			Dictionary<string, PropertyAccessor> CreatePropertyAccessors()
			{
				var propertyAccessors = new Dictionary<string, PropertyAccessor>();

				var genericTypeArguments = new Type[1];
				foreach (var property in GetProperties())
				{
					genericTypeArguments[0] = property.PropertyType;
					var getterType = typeof(Func<>).MakeGenericType(genericTypeArguments);
					var setterType = typeof(Action<>).MakeGenericType(genericTypeArguments);

					var getter = property.GetMethod.CreateDelegate(getterType, @object);
					var setter = property.CanWrite ? property.SetMethod.CreateDelegate(setterType, @object) : null;

					propertyAccessors[property.Name] = new PropertyAccessor(getter, setter);
				}

				return propertyAccessors;

				PropertyInfo[] GetProperties()
				{
					return @object.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
				}
			}
		}

		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="InvalidCastException"></exception>
		public T GetProperty<T>(string propertyName)
		{
			if (!PropertyAccessors.TryGetValue(propertyName, out var accessor))
			{
				throw CreateInvalidPropertyNameException(propertyName, nameof(propertyName));
			}

			if (typeof(T) != typeof(object))
			{
				return accessor.Getter is Func<T> typedGetter
					? typedGetter()
					: throw CreateInvalidPropertyTypeException(propertyName, accessor.PropertyType, typeof(T));
			}
			else
			{
				return (T)accessor.Getter.DynamicInvoke();
			}
		}

		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="InvalidCastException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public void SetProperty<T>(string propertyName, T value)
		{
			if (!PropertyAccessors.TryGetValue(propertyName, out var accessor))
			{
				throw CreateInvalidPropertyNameException(propertyName, nameof(propertyName));
			}

			if (accessor.Setter != null)
			{
				if (typeof(T) != typeof(object))
				{
					if (accessor.Setter is Action<T> typedSetter)
					{
						typedSetter(value);
					}
					else
					{
						throw CreateInvalidPropertyTypeException(propertyName, accessor.PropertyType, typeof(T));
					}
				}
				else
				{
					accessor.Setter.DynamicInvoke(value);
				}
			}
			else
			{
				throw new InvalidOperationException($"{propertyName} doesn't has setter.");
			}
		}

		private ArgumentException CreateInvalidPropertyNameException(string propertyName, string paramName)
		{
			return new ArgumentException(
				$"Object doesn't have '{propertyName}' property.",
				paramName);
		}

		private InvalidCastException CreateInvalidPropertyTypeException(
			string propertyName,
			Type propertyType,
			Type castType)
		{
			return new InvalidCastException(
				$"Can't cast property '{propertyName}' of type '{propertyType}' to type '{castType}'.");
		}
	}
}