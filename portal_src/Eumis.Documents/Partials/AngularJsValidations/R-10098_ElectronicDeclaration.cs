using System.Xml.Serialization;

namespace R_10098
{
    public partial class ElectronicDeclaration
    {
        private bool _isFieldValueValid = true;

        [XmlIgnore]
        public bool IsFieldValueValid
        {
            get { return _isFieldValueValid; }
            set { _isFieldValueValid = value; }
        }
    }
}
