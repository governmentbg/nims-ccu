using System;
using Eumis.Common.Json;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularityVersionVO
    {
        public int VersionId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public IrregularityVersionStatus Status { get; set; }

        public DateTime ModifyDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Year? ReportYear { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Quarter? ReportQuarter { get; set; }

        public int OrderNum { get; set; }
    }
}
