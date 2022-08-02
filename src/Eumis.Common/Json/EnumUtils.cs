using Eumis.Common.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace Eumis.Common.Json
{
    public static class EnumUtils
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<Enum, Func<CultureInfo, string>>> Cache = new ConcurrentDictionary<Type, Dictionary<Enum, Func<CultureInfo, string>>>();
        private static readonly ConcurrentDictionary<Type, Action<CultureInfo>> CultureSetterCache = new ConcurrentDictionary<Type, Action<CultureInfo>>();
        private static readonly ConcurrentDictionary<Type, Dictionary<Enum, int>> CacheOrder = new ConcurrentDictionary<Type, Dictionary<Enum, int>>();

        public static string GetCamelCaseEnumValue(Enum e)
        {
            return Enum.GetName(e.GetType(), e).ToCamelCase();
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetEnumDescription(this Enum element, CultureInfo culture = null)
        {
            if (element == null)
            {
                return null;
            }

            Type type = element.GetType();

            if (!Cache.TryGetValue(type, out var cachedDescriptionGetters))
            {
                cachedDescriptionGetters = new Dictionary<Enum, Func<CultureInfo, string>>();
                foreach (Enum enumValue in Enum.GetValues(type))
                {
                    FieldInfo fi = type.GetField(enumValue.ToString());
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    Func<CultureInfo, string> descriptionGetter;
                    if (attributes != null && attributes.Length > 0)
                    {
                        var attr = attributes[0];
                        descriptionGetter = (c) =>
                        {
                            if (c != null)
                            {
                                var cultureSetter = GetCultureSetter(attr.ResourceType);

                                cultureSetter(c);

                                try
                                {
                                    return attr.GetDescription();
                                }
                                finally
                                {
                                    cultureSetter(null);
                                }
                            }

                            return attr.GetDescription();
                        };
                    }
                    else
                    {
                        string descr = element.ToString();
                        descriptionGetter = (c) => descr;
                    }

                    cachedDescriptionGetters.Add(enumValue, descriptionGetter);
                }

                Cache.TryAdd(type, cachedDescriptionGetters);
            }

            return cachedDescriptionGetters[element](culture);
        }

        private static Action<CultureInfo> GetCultureSetter(Type resourceType)
        {
            if (!CultureSetterCache.TryGetValue(resourceType, out var cultureSetter))
            {
                PropertyInfo property = resourceType.GetProperty("Culture");

                bool badlyConfigured = false;
                if (!resourceType.IsVisible || property == null || property.PropertyType != typeof(CultureInfo))
                {
                    badlyConfigured = true;
                }
                else
                {
                    // Ensure the getter for the property is available as public static
                    MethodInfo setter = property.GetSetMethod();

                    if (setter == null || !(setter.IsPublic && setter.IsStatic))
                    {
                        badlyConfigured = true;
                    }
                }

                if (badlyConfigured)
                {
                    cultureSetter = (c) => { throw new InvalidOperationException($"The resource type {resourceType.Name} is not visible or it does not have a \"Culture\" property that is \"public static CultureInfo\""); };
                }
                else
                {
                    cultureSetter = (c) => { property.SetValue(resourceType, c); };
                }

                CultureSetterCache.TryAdd(resourceType, cultureSetter);
            }

            return cultureSetter;
        }

        public static T GetEnumByDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            // change the current culture as we are checking the default descriptions only
            // when trying to find the corresponding enum
            var currentUICulture = Thread.CurrentThread.CurrentUICulture;
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SystemLocalization.DefaultCulture);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(SystemLocalization.DefaultCulture);

            try
            {
                foreach (var field in type.GetFields())
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                    {
                        if (attribute.GetDescription().ToUpperInvariant().Trim() == description.ToUpperInvariant().Trim())
                        {
                            return (T)field.GetValue(null);
                        }
                    }
                }

                throw new ArgumentException("Enum not found.");
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = currentUICulture;
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }
        }

        public static int GetOrderNum(this Enum element)
        {
            var type = element.GetType();

            if (!CacheOrder.TryGetValue(type, out var cachedOrderNumbers))
            {
                cachedOrderNumbers = new Dictionary<Enum, int>();
                foreach (Enum enumValue in Enum.GetValues(type))
                {
                    FieldInfo fi = type.GetField(enumValue.ToString());
                    OrderNumberAttribute[] attributes = (OrderNumberAttribute[])fi.GetCustomAttributes(typeof(OrderNumberAttribute), false);
                    int value;

                    if (attributes != null && attributes.Length > 0)
                    {
                        value = attributes[0].OrderNum;
                    }
                    else
                    {
                        value = Convert.ToInt32(element);
                    }

                    cachedOrderNumbers.Add(enumValue, value);
                }

                CacheOrder.TryAdd(type, cachedOrderNumbers);
            }

            return cachedOrderNumbers[element];
        }
    }
}
