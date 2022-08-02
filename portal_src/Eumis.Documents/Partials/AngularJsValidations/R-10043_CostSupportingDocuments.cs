using System;
using System.Xml.Serialization;

namespace R_10043
{
    public partial class CostSupportingDocuments
    {
        private bool _isValid = true;

        [XmlIgnore]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }
    }
}
