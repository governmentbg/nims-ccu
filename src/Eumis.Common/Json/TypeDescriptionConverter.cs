using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Eumis.Common.Json
{
    public class TypeDescriptionConverter : JsonConverter
    {
        private static readonly ConcurrentDictionary<Type, Func<string>> Cache = new ConcurrentDictionary<Type, Func<string>>();

        public TypeDescriptionConverter()
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (!typeof(Type).IsInstanceOfType(value))
            {
                writer.WriteNull();
                return;
            }

            Type t = (Type)value;

            if (!Cache.TryGetValue(t, out Func<string> cachedDescrGetter))
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])t.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    cachedDescrGetter = () => attributes[0].GetDescription();
                }
                else
                {
                    string descr = t.Name;
                    cachedDescrGetter = () => descr;
                }

                Cache.TryAdd(t, cachedDescrGetter);
            }

            writer.WriteValue(cachedDescrGetter());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Type);
        }
    }
}
