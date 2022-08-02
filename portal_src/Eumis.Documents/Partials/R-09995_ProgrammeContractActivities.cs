using Eumis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_09995
{
    public partial class ProgrammeContractActivities
    {
        [XmlIgnore]
        public IEnumerable<SerializableSelectListItem> Items { get; set; }

        [XmlIgnore]
        public int Index { get; set; }

        [XmlIgnore]
        public int SectionNumber { get; set; }

        [XmlIgnore]
        public bool IsActive { get; set; }

        [XmlIgnore]
        public string ProgrammeNameFormatted
        {
            get
            {
                return String.IsNullOrWhiteSpace(this.programmeName) ? "" : string.Format(Eumis.Common.Resources.Global.ProgrammeNameTemplate,
                                                this.programmeName);
            }
        }

        [XmlIgnore]
        public bool IsFinalRecipients { get; set; }

        [XmlIgnore]
        public bool IsFinancialIntermediaries { get; set; }
    }
}
