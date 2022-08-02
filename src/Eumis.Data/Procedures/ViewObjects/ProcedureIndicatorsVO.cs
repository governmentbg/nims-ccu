using System;
using Eumis.Common.Json;
using Eumis.Domain.Indicators;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureIndicatorsVO
    {
        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public int IndicatorId { get; set; }

        public Guid Gid { get; set; }

        public string ProgrammeName { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool HasGenderDivision { get; set; }

        public string MeasureName { get; set; }

        public string MeasureNameAlt { get; set; }

        public decimal? BaseTotalValue { get; set; }

        public int? BaseYear { get; set; }

        public decimal? TargetTotalValue { get; set; }

        public decimal? MilestoneTargetTotalValue { get; set; }

        public string DataSource { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActiveStatus ActiveStatus { get; set; }
    }
}
