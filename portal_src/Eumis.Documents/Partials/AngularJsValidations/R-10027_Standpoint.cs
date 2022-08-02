using System;
using System.Xml.Serialization;

namespace R_10027
{
    public partial class Standpoint
    {
        private bool _isSubjectValid = true;
        private bool _isContentValid = true;

        [XmlIgnore]
        public bool IsSubjectValid
        {
            get { return _isSubjectValid; }
            set { _isSubjectValid = value; }
        }

        [XmlIgnore]
        public bool IsContentValid
        {
            get { return _isContentValid; }
            set { _isContentValid = value; }
        }
    }
}
