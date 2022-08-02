using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10040
{
    public partial class BFPContractIndicators
    {
        [XmlIgnore]
        public IEnumerable<R_10038.SelectedIndicator> Items { get; set; }

        public static List<R_10038.SelectedIndicator> Load(List<Eumis.Documents.Contracts.ContractIndicator> indicators)
        {
            List<R_10038.SelectedIndicator> result = new List<R_10038.SelectedIndicator>();

            if (indicators != null)
            {
                result = indicators.Where(e => e.isActive).Select(e => new R_10038.SelectedIndicator()
                {
                    Id = e.gid,
                    Name = e.name,
                    TypeName = e.typeName,
                    TrendName = e.trendName,
                    KindName = e.kindName,
                    MeasureName = e.measureName,
                    AggregatedReport = e.aggregatedReport,
                    AggregatedTarget = e.aggregatedTarget,
                    HasGenderDivision = e.hasGenderDivision
                }).ToList();
            }

            return result;
        }
    }
}
