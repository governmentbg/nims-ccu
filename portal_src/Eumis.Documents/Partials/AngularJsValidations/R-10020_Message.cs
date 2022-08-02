using System.Xml.Serialization;

namespace R_10020
{
    public partial class Message
    {
        private bool _isSubjectValid = true;

        [XmlIgnore]
        public bool IsSubjectValid
        {
            get { return _isSubjectValid; }
            set { _isSubjectValid = value; }
        }
    }
}
