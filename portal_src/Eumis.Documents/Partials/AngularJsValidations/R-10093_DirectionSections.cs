using System;
using System.Xml.Serialization;

namespace R_10093
{
    public partial class DirectionSection
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
