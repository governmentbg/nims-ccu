using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10028
{
    public partial class PreliminaryPartner
    {
        [XmlIgnore]
        public int SectionNumber { get; set; }
    }
}
