using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractIndicator
    {
        public string gid { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public string typeName { get; set; }
        public string typeNameAlt { get; set; }
        public string trendName { get; set; }
        public string trendNameAlt { get; set; }
        public string kindName { get; set; }
        public string kindNameAlt { get; set; }
        public string measureName { get; set; }
        public string measureNameAlt { get; set; }
        public bool isActive { get; set; }

        public string aggregatedReport { get; set; }
        public string aggregatedReportAlt { get; set; }
        public string aggregatedTarget { get; set; }
        public string aggregatedTargetAlt { get; set; }
        public bool hasGenderDivision { get; set; }
    }

    public enum IndicatorTrend
    {
        [Description("Намаление")]
        Reduction = 1,

        [Description("Увеличение")]
        Increase = 2,

        [Description("Неприложимо")]
        Inapplicable = 3,
    }
}
