using System.Collections.Generic;
using System.Reflection;
using PropertyTrimmer.Extensions;
using PropertyTrimmer.Helpers;

namespace PropertyTrimmer
{
    public static class PropertyTrimmer
    {
        /// <summary>
        /// Removes all leading and trailing white-space characters from specified class property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="propertyName"></param>
        public static void TrimProperty<T>(T item, string propertyName) where T : class
        {
            Guard.ThrowIfNull(item);
            Guard.ThrowIfStringIsNullOrWhiteSpace(propertyName);

            var property = typeof(T).GetPropertyByName(propertyName);
            if (PropertyExists(property))
            {
                Trim(item, property);
            }
        }

        /// <summary>
        /// Removes all leading and trailing white-space characters from all string properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public static void TrimProperties<T>(T item) where T : class
        {
            Guard.ThrowIfNull(item);

            var properties = typeof(T).GetStringProperties();
            properties?.Do(property => Trim(item, property));
        }

        /// <summary>
        /// Removes all leading and trailing white-space characters from specified class property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="propertyName"></param>
        public static void TrimProperty<T>(List<T> items, string propertyName) where T : class
        {
            Guard.ThrowIfCollectionNullOrEmpty(items);
            Guard.ThrowIfStringIsNullOrWhiteSpace(propertyName);

            var property = typeof(T).GetPropertyByName(propertyName);
            if (PropertyExists(property))
            {
                items.Do(item => Trim(item, property));
            }
        }

        /// <summary>
        /// Removes all leading and trailing white-space characters from all string properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public static void TrimProperties<T>(List<T> items) where T : class
        {
            Guard.ThrowIfCollectionNullOrEmpty(items);

            var properties = typeof(T).GetStringProperties();
            if (properties != null)
            {
                foreach (var item in items)
                {
                    properties.Do(property => Trim(item, property));
                }
            }
        }

        private static bool PropertyExists(PropertyInfo property)
        {
            return property != null && property.PropertyType == typeof(string);
        }

        private static void Trim<T>(T item, PropertyInfo property)
        {
            var currentValue = property.GetValue(item, null).ToString();
            if (!string.IsNullOrWhiteSpace(currentValue))
            {
                property.SetValue(item, currentValue.Trim(), null);
            }
        }
    }
}
