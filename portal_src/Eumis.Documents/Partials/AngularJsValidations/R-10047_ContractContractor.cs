using System;
using System.Xml.Serialization;

namespace R_10047
{
    public partial class ContractContractor
    {
        private bool _isSignDateValid = true;
        private bool _isNumberValid = true;
        private bool _isTotalAmountExcludingVATValid = true;
        private bool _isContractAmountWithoutVATValid = true;
        private bool _isVATAmountIfEligibleValid = true;
        private bool _isTotalFundedValueValid = true;
        private bool _isBudgetDifferenceValueValid = true;
        private bool _isNumberAnnexesValid = true;
        private bool _isCurrentAnnexTotalAmountValid = true;
        private bool _isCommentValid = true;
        private bool _isStartDateValid = true;
        private bool _isEndDateValid = true;
        private bool _isContractorValid = true;

        private bool _isUniquePairValid = true;

        [XmlIgnore]
        public bool IsSignDateValid
        {
            get { return _isSignDateValid; }
            set { _isSignDateValid = value; }
        }

        [XmlIgnore]
        public bool IsNumberValid
        {
            get { return _isNumberValid; }
            set { _isNumberValid = value; }
        }

        [XmlIgnore]
        public bool IsTotalAmountExcludingVATValid { get { return _isTotalAmountExcludingVATValid; } set { _isTotalAmountExcludingVATValid = value; } }

        [XmlIgnore]
        public bool IsContractAmountWithoutVATValid { get { return _isContractAmountWithoutVATValid; } set { _isContractAmountWithoutVATValid = value; } }

        [XmlIgnore]
        public bool IsVATAmountIfEligibleValid { get { return _isVATAmountIfEligibleValid; } set { _isVATAmountIfEligibleValid = value; } }

        [XmlIgnore]
        public bool IsTotalFundedValueValid { get { return _isTotalFundedValueValid; } set { _isTotalFundedValueValid = value; } }

        [XmlIgnore]
        public bool IsBudgetDifferenceValueValid
        {
            get { return _isBudgetDifferenceValueValid; }
            set { _isBudgetDifferenceValueValid = value; }
        }

        [XmlIgnore]
        public bool IsNumberAnnexesValid
        {
            get { return _isNumberAnnexesValid; }
            set { _isNumberAnnexesValid = value; }
        }

        [XmlIgnore]
        public bool IsCurrentAnnexTotalAmountValid
        {
            get { return _isCurrentAnnexTotalAmountValid; }
            set { _isCurrentAnnexTotalAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsCommentValid
        {
            get { return _isCommentValid; }
            set { _isCommentValid = value; }
        }

        [XmlIgnore]
        public bool IsStartDateValid
        {
            get { return _isStartDateValid; }
            set { _isStartDateValid = value; }
        }

        [XmlIgnore]
        public bool IsEndDateValid
        {
            get { return _isEndDateValid; }
            set { _isEndDateValid = value; }
        }

        [XmlIgnore]
        public bool IsContractorValid
        {
            get { return _isContractorValid; }
            set { _isContractorValid = value; }
        }

        [XmlIgnore]
        public bool IsUniquePairValid
        {
            get { return _isUniquePairValid; }
            set { _isUniquePairValid = value; }
        }
    }
}
