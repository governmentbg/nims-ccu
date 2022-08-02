using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_09989
{
    public partial class Location
    {
        [XmlIgnore]
        public string id { get { return this.Code ?? string.Empty; } }

        [XmlIgnore]
        public string text { get { return this.displayName; } }

        [XmlIgnore]
        public string displayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Name, this.NameEN);
            }
        }

        [XmlIgnore]
        public string displayFullPathName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.FullPathName, this.FullPathNameEN);
            }
        }
    }
}
