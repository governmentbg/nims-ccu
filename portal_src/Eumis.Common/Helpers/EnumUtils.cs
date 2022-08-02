using Eumis.Common.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Helpers
{
    public static class EnumUtils
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<Enum, Func<string>>> Cache = new ConcurrentDictionary<Type, Dictionary<Enum, Func<string>>>();

        public static string GetEnumDescription(this Enum element)
        {
            if (element == null)
            {
                return null;
            }

            Type type = element.GetType();

            if (!Cache.TryGetValue(type, out var cachedDescriptionGetters))
            {
                cachedDescriptionGetters = new Dictionary<Enum, Func<string>>();
                foreach (Enum enumValue in Enum.GetValues(type))
                {
                    FieldInfo fi = type.GetField(enumValue.ToString());
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    Func<string> descriptionGetter;
                    if (attributes != null && attributes.Length > 0)
                    {
                        var attr = attributes[0];
                        descriptionGetter = () =>
                        {
                            return attr.GetDescription();
                        };
                    }
                    else
                    {
                        string descr = element.ToString();
                        descriptionGetter = () => descr;
                    }

                    cachedDescriptionGetters.Add(enumValue, descriptionGetter);
                }

                Cache.TryAdd(type, cachedDescriptionGetters);
            }

            return cachedDescriptionGetters[element]();
        }
    }
}
