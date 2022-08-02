using System;
using System.Xml.Serialization;

namespace R_10044
{
    public partial class TechnicalReportAttachedDocuments
    {
        private bool _hasValidCount = true;

        [XmlIgnore]
        public bool HasValidCount
        {
            get { return _hasValidCount; }
            set { _hasValidCount = value; }
        }
    }
}
