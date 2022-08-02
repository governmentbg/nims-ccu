using System.Xml.Serialization;

namespace R_10069
{
    public partial class SubcontractorMember
    {
        private bool _isContractorValid = true;
        private bool _isContractDateValid = true;
        private bool _isContractNumberValid = true;
        private bool _isContractAmountValid = true;

        [XmlIgnore]
        public bool IsContractorValid
        {
            get { return _isContractorValid; }
            set { _isContractorValid = value; }
        }

        [XmlIgnore]
        public bool IsContractDateValid
        {
            get { return _isContractDateValid; }
            set { _isContractDateValid = value; }
        }

        [XmlIgnore]
        public bool IsContractNumberValid
        {
            get { return _isContractNumberValid; }
            set { _isContractNumberValid = value; }
        }

        [XmlIgnore]
        public bool IsContractAmountValid
        {
            get { return _isContractAmountValid; }
            set { _isContractAmountValid = value; }
        }
    }
}
