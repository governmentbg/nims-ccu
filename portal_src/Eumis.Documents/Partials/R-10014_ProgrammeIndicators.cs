using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10014
{
    public partial class ProgrammeIndicators
    {
        [XmlIgnore]
        public IEnumerable<R_10013.Indicator> Items { get; set; }

        [XmlIgnore]
        public string ProgrammeNameFormatted
        {
            get
            {
                return String.IsNullOrWhiteSpace(this.programmeName) ? "" : string.Format(Eumis.Common.Resources.Global.ProgrammeNameTemplate,
                                                this.programmeName);
            }
        }

        public static List<R_10013.Indicator> Load(List<Eumis.Documents.Contracts.ContractIndicator> indicators)
        {
            List<R_10013.Indicator> result = new List<R_10013.Indicator>();

            if (indicators != null)
            {
                result = indicators.Where(e => e.isActive).Select(e => new R_10013.Indicator()
                {
                    Id = e.gid,
                    Name = e.name,
                    NameEN = e.nameAlt,
                    TypeName = e.typeName,
                    TypeNameEN = e.typeNameAlt,
                    TrendName = e.trendName,
                    TrendNameEN = e.trendNameAlt,
                    KindName = e.kindName,
                    KindNameEN = e.kindNameAlt,
                    MeasureName = e.measureName,
                    MeasureNameEN = e.measureNameAlt,
                    AggregatedReport = e.aggregatedReport,
                    AggregatedReportEN = e.aggregatedReportAlt,
                    AggregatedTarget = e.aggregatedTarget,
                    AggregatedTargetEN = e.aggregatedTargetAlt,
                    HasGenderDivision = e.hasGenderDivision
                }).ToList();
            }

            return result;
        }
    }
}
