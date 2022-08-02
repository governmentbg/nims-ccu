using Eumis.Common.Localization;
using Eumis.Data.Core.Nomenclatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.Core.Nomenclatures
{
    public class EntityGidNomVOJsonConverter : JsonConverter
    {
        public override bool CanRead
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var entityGidNomVO = (EntityGidNomVO)value;
            bool isDefaultCulture = new CultureInfo(SystemLocalization.DefaultCulture).Equals(Thread.CurrentThread.CurrentCulture);

            JObject.FromObject(new
            {
                entityGidNomVO.NomValueId,
                entityGidNomVO.Gid,
                Name =
                    isDefaultCulture ? entityGidNomVO.Name :
                    !string.IsNullOrWhiteSpace(entityGidNomVO.NameAlt) ? entityGidNomVO.NameAlt :
                    entityGidNomVO.Name,
            }).WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Should never be called as CanRead is false.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EntityGidNomVO);
        }
    }
}
