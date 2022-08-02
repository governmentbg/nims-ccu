using System;
using System.Xml.Serialization;

namespace R_10045
{
    public partial class PaymentRequestAttachedDocuments
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
