using System;
using System.Xml.Serialization;

namespace R_10040
{
    public partial class BFPContractAttachedDocuments
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
