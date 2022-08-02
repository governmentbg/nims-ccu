using System.Collections.Generic;
using Eumis.Domain.Indicators;
using System.Xml.Serialization;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class Anex3IndicatorVO
    {
        public string Name { get; set; }

        public string Measure { get; set; }

        public bool HasGenderDivision { get; set; }

        public decimal? BaseTotalValue { get; set; }

        public decimal? BaseMenValue { get; set; }

        public decimal? BaseWomenValue { get; set; }

        public decimal? TargetTotalValue { get; set; }

        public decimal? TargetMenValue { get; set; }

        public decimal? TargetWomenValue { get; set; }

        // ignore this property when serializing
        [XmlIgnore]
        public Dictionary<int, MonitoringIndicatorPeriodAmountVO> PeriodAmounts { get; set; }
    }
}
