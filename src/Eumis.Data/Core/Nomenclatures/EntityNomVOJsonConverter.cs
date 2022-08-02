using Eumis.Common.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Threading;

namespace Eumis.Data.Core.Nomenclatures
{
    public class EntityNomVOJsonConverter : JsonConverter
    {
        public override bool CanRead
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            bool isDefaultCulture = new CultureInfo(SystemLocalization.DefaultCulture).Equals(Thread.CurrentThread.CurrentCulture);

            JsonObjectContract contract = (JsonObjectContract)serializer.ContractResolver.ResolveContract(value.GetType());
            var entityNomVO = (EntityNomVO)value;

            writer.WriteStartObject();
            foreach (var property in contract.Properties)
            {
                if (property.PropertyName.Equals(nameof(entityNomVO.NameAlt), StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                writer.WritePropertyName(property.PropertyName);

                if (property.PropertyName.Equals(nameof(entityNomVO.Name), StringComparison.InvariantCultureIgnoreCase))
                {
                    string displayName =
                        isDefaultCulture ? entityNomVO.Name :
                        !string.IsNullOrEmpty(entityNomVO.NameAlt) ? entityNomVO.NameAlt :
                        entityNomVO.Name;

                    writer.WriteValue(displayName);
                }
                else
                {
                    writer.WriteValue(property.ValueProvider.GetValue(value));
                }
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Should never be called as CanRead is false.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EntityNomVO);
        }
    }
}
