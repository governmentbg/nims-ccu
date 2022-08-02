using Eumis.Common;
using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10001
{
    public partial class PublicNomenclature
    {
        [XmlIgnore]
        public IEnumerable<SerializableSelectListItem> Items { get; set; }

        [XmlIgnore]
        public String FunctionName { get; set; }

        [XmlIgnore]
        public string id { get { return this.Code; } }

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
    }
}
