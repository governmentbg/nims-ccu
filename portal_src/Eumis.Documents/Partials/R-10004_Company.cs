using Eumis.Common.Localization;
using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10004
{
    public partial class Company
    {
        [XmlIgnore]
        public bool IsOpen { get; set; }

        [XmlIgnore]
        public string displayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Name, this.NameEN);
            }
        }

        [XmlIgnore]
        public bool IsPartialReadOnly { get; set; }
    }
}
