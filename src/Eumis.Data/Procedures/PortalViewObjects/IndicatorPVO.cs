using System;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.Indicators;
using Eumis.Domain.Procedures.Json;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class IndicatorPVO
    {
        public IndicatorPVO(IndicatorJson indicator)
        {
            this.Gid = indicator.Gid;
            this.Name = indicator.Name;
            this.NameAlt = indicator.NameAlt;
            this.MeasureName = indicator.MeasureName;
            this.MeasureNameAlt = indicator.MeasureNameAlt;
            this.HasGenderDivision = indicator.HasGenderDivision;
            this.IsActive = indicator.IsActive;
        }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string MeasureName { get; set; }

        public string MeasureNameAlt { get; set; }

        public bool HasGenderDivision { get; set; }

        public bool IsActive { get; set; }
    }
}
