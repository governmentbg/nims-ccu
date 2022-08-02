using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10019
{
    public partial class PaperAttachedDocumentCollection
    {
        private bool _hasUniqueIds = true;
        private bool _hasValidCount = true;

        [XmlIgnore]
        public bool HasUniqueIds
        {
            get { return _hasUniqueIds; }
            set { _hasUniqueIds = value; }
        }

        [XmlIgnore]
        public bool HasValidCount
        {
            get { return _hasValidCount; }
            set { _hasValidCount = value; }
        }
    }
}
