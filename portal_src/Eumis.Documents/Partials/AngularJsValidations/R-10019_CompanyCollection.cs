using Eumis.Documents.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10019
{
    public partial class CompanyCollection
    {
        private bool _hasValidCount = true;

        [XmlIgnore]
        public bool HasValidCount
        {
            get { return _hasValidCount; }
            set { _hasValidCount = value; }
        }

        [XmlIgnore]
        public int SectionNumber { get; set; }

        [XmlIgnore]
        public bool IsActive { get; set; }
    }
}
