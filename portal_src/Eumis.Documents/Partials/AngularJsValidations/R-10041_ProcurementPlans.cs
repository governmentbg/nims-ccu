using System;
using System.Xml.Serialization;

namespace R_10041
{
    public partial class ProcurementPlans
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
