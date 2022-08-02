using System;
using System.Xml.Serialization;

namespace R_10080
{
    public partial class OfferAttachedDocuments
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
