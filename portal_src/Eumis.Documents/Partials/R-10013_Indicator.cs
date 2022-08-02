using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10013
{
    public partial class Indicator
    {
        [XmlIgnore]
        public bool IsDeactivated { get; set; }

        [XmlIgnore]
        public string displayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Name, this.NameEN);
            }
        }

        [XmlIgnore]
        public string displayTypeName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.TypeName, this.TypeNameEN);
            }
        }

        [XmlIgnore]
        public string displayKindName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.KindName, this.KindNameEN);
            }
        }

        [XmlIgnore]
        public string displayTrendName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.TrendName, this.TrendNameEN);
            }
        }

        [XmlIgnore]
        public string displayAggregatedReport
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.AggregatedReport, this.AggregatedReportEN);
            }
        }

        [XmlIgnore]
        public string displayAggregatedTarget
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.AggregatedTarget, this.AggregatedTargetEN);
            }
        }

        [XmlIgnore]
        public string displayMeasureName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.MeasureName, this.MeasureNameEN);
            }
        }

    }
}
