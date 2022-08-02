using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10002
{
    public partial class ProjectBasicData
    {
        [XmlIgnore]
        public List<Tuple<R_09991.EnumNomenclature, string>> Locations { get; set; }

        [XmlIgnore]
        public int MaxDuration { get; set; }

        [XmlIgnore]
        public bool IsFinalRecipients { get; set; }

        [XmlIgnore]
        public bool IsFinancialIntermediaries { get; set; }

        [XmlIgnore]
        public string displayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(Name, NameEN);
            }
        }

        [XmlIgnore]
        public bool FillMainData { get; set; }
    }
}
