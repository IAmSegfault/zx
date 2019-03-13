using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Godot;
namespace Zona
{
    namespace Engine
    {
            public abstract class EnumClass : IComparable
        {
            private readonly int _value;
            private readonly string _displayName;

            protected EnumClass()
            {
            }

            protected EnumClass(int value, string displayName)
            {
                _value = value;
                _displayName = displayName;
            }

            public int Value
            {
                get { return _value; }
            }

            public string DisplayName
            {
                get { return _displayName; }
            }

            public override string ToString()
            {
                return DisplayName;
            }

            public static IEnumerable<T> GetAll<T>() where T : EnumClass, new()
            {
                var type = typeof(T);
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

                foreach (var info in fields)
                {
                    var instance = new T();
                    var locatedValue = info.GetValue(instance) as T;

                    if (locatedValue != null)
                    {
                        yield return locatedValue;
                    }
                }
            }

            public override bool Equals(object obj)
            {
                var otherValue = obj as EnumClass;

                if (otherValue == null)
                {
                    return false;
                }

                var typeMatches = GetType().Equals(obj.GetType());
                var valueMatches = _value.Equals(otherValue.Value);

                return typeMatches && valueMatches;
            }

            public override int GetHashCode()
            {
                return _value.GetHashCode();
            }

            public static int AbsoluteDifference(EnumClass firstValue, EnumClass secondValue)
            {
                var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
                return absoluteDifference;
            }

            public static T FromValue<T>(int value) where T : EnumClass, new()
            {
                var matchingItem = parse<T, int>(value, "value", item => item.Value == value);
                return matchingItem;
            }

            public static T FromDisplayName<T>(string displayName) where T : EnumClass, new()
            {
                var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
                return matchingItem;
            }

            private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : EnumClass, new()
            {
                var matchingItem = GetAll<T>().FirstOrDefault(predicate);

                if (matchingItem == null)
                {
                    var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                    throw new ApplicationException(message);
                }

                return matchingItem;
            }

            public int CompareTo(object other)
            {
                return Value.CompareTo(((EnumClass)other).Value);
            }
        }
    }
}