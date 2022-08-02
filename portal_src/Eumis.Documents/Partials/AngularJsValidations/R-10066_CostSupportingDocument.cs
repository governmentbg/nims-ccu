using System;
using System.Xml.Serialization;

namespace R_10066
{
    public partial class CostSupportingDocument
    {
        private bool _isTypeValid = true;
        private bool _isDescriptionValid = true;
        private bool _isNumberValid = true;
        private bool _isDateValid = true;
        private bool _isPaymentDateValid = true;
        private bool _isPartnerValid = true;
        private bool _isContractorValid = true;
        private bool _isContractContractorValid = true;

        [XmlIgnore]
        public bool IsTypeValid { get { return _isTypeValid; } set { _isTypeValid = value; } }
        [XmlIgnore]
        public bool IsDescriptionValid { get { return _isDescriptionValid; } set { _isDescriptionValid = value; } }
        [XmlIgnore]
        public bool IsNumberValid { get { return _isNumberValid; } set { _isNumberValid = value; } }
        [XmlIgnore]
        public bool IsDateValid { get { return _isDateValid; } set { _isDateValid = value; } }
        [XmlIgnore]
        public bool IsPaymentDateValid { get { return _isPaymentDateValid; } set { _isPaymentDateValid = value; } }
        [XmlIgnore]
        public bool IsPartnerValid { get { return _isPartnerValid; } set { _isPartnerValid = value; } }
        [XmlIgnore]
        public bool IsContractorValid { get { return _isContractorValid; } set { _isContractorValid = value; } }
        [XmlIgnore]
        public bool IsContractContractorValid { get { return _isContractContractorValid; } set { _isContractContractorValid = value; } }

        [XmlIgnore]
        public bool IsOpen { get; set; }
    }
}
