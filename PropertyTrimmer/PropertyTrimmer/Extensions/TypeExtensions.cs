using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PropertyTrimmer.Helpers;

namespace PropertyTrimmer.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetStringProperties(this Type type)
        {
            return type.GetProperties().Where(property => property.PropertyType == typeof(string));
        }

        public static bool IsTypeOfString(this Type type)
        {
            return type == typeof(string);
        }

        public static PropertyInfo GetPropertyByName(this Type type, string propertyName)
        {
            Guard.ThrowIfStringIsNullOrWhiteSpace(propertyName);
            return type.GetProperties().FirstOrDefault(property => property.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
