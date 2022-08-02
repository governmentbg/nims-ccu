using System;
using Newtonsoft.Json;

namespace Eumis.Common.Json
{
    public class MoneyConverter : JsonConverter
    {
        public MoneyConverter()
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            Type objectType = value.GetType();
            if (!this.CanConvert(objectType))
            {
                throw new JsonSerializationException("Cannot convert to money from " + objectType.Name);
            }

            decimal dMoney = (decimal)value;
            long lMoney = (long)Math.Floor(dMoney * 100);

            writer.WriteValue(lMoney);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!this.CanConvert(objectType))
            {
                throw new JsonSerializationException("Cannot convert from money to " + objectType.Name);
            }

            long? money = serializer.Deserialize<long?>(reader);

            if (money == null)
            {
                if (objectType == typeof(decimal))
                {
                    return default(decimal);
                }
                else
                {
                    // Nullable<decimal>
                    return null;
                }
            }

            return (decimal)money.Value / 100;
        }

        public override bool CanConvert(Type objectType)
        {
            return this.GetActualType(objectType) == typeof(decimal);
        }

        private Type GetActualType(Type objectType)
        {
            return (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>))
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;
        }
    }
}
