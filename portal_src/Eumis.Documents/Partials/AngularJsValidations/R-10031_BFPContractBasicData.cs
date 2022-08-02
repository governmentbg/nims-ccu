using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10031
{
    public partial class BFPContractBasicData
    {
        private bool _isNameValid = true;
        private bool _isDurationValid = true;
        private bool _isNameEnValid = true;
        private bool _isDescriptionValid = true;
        private bool _isDescriptionEnValid = true;
        private bool _isPurposeValid = true;

        private bool _isCompletionStatusValid = true;

        private bool _isStartDateValid = true;
        private bool _isStartConditionValid = true;
        private bool _isTemporarySuspensionDateValid = true;
        private bool _isTemporarySuspensionReasonValid = true;
        private bool _isCompletionDateValid = true;
        private bool _isTerminationDateValid = true;
        private bool _isTerminationReasonValid = true;

        private bool _isContractDateValid = true;
        private bool _isOtherRegistrationValid = true;
        private bool _isStoragePlaceValid = true;

        private bool _isBeneficiaryRegistrationVATValid = true;
        private bool _isBankAccountValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsDurationValid 
        {
            get { return _isDurationValid; }
            set { _isDurationValid = value; }
        }

        [XmlIgnore]
        public bool IsNameEnValid
        {
            get { return _isNameEnValid; }
            set { _isNameEnValid = value; }
        }

        [XmlIgnore]
        public bool IsDescriptionValid
        {
            get { return _isDescriptionValid; }
            set { _isDescriptionValid = value; }
        }

        [XmlIgnore]
        public bool IsDescriptionEnValid
        {
            get { return _isDescriptionEnValid; }
            set { _isDescriptionEnValid = value; }
        }

        [XmlIgnore]
        public bool IsPurposeValid
        {
            get { return _isPurposeValid; }
            set { _isPurposeValid = value; }
        }

        [XmlIgnore]
        public bool IsCompletionStatusValid
        {
            get { return _isCompletionStatusValid; }
            set { _isCompletionStatusValid = value; }
        }

        [XmlIgnore]
        public bool IsStartDateValid
        {
            get { return _isStartDateValid; }
            set { _isStartDateValid = value; }
        }

        [XmlIgnore]
        public bool IsStartConditionValid
        {
            get { return _isStartConditionValid; }
            set { _isStartConditionValid = value; }
        }

        [XmlIgnore]
        public bool IsTemporarySuspensionDateValid
        {
            get { return _isTemporarySuspensionDateValid; }
            set { _isTemporarySuspensionDateValid = value; }
        }

        [XmlIgnore]
        public bool IsTemporarySuspensionReasonValid
        {
            get { return _isTemporarySuspensionReasonValid; }
            set { _isTemporarySuspensionReasonValid = value; }
        }

        [XmlIgnore]
        public bool IsCompletionDateValid
        {
            get { return _isCompletionDateValid; }
            set { _isCompletionDateValid = value; }
        }

        [XmlIgnore]
        public bool IsTerminationDateValid
        {
            get { return _isTerminationDateValid; }
            set { _isTerminationDateValid = value; }
        }

        [XmlIgnore]
        public bool IsTerminationReasonValid
        {
            get { return _isTerminationReasonValid; }
            set { _isTerminationReasonValid = value; }
        }

        [XmlIgnore]
        public bool IsContractDateValid
        {
            get { return _isContractDateValid; }
            set { _isContractDateValid = value; }
        }

        [XmlIgnore]
        public bool IsOtherRegistrationValid
        {
            get { return _isOtherRegistrationValid; }
            set { _isOtherRegistrationValid = value; }
        }

        [XmlIgnore]
        public bool IsStoragePlaceValid
        {
            get { return _isStoragePlaceValid; }
            set { _isStoragePlaceValid = value; }
        }

        [XmlIgnore]
        public bool IsBeneficiaryRegistrationVATValid
        {
            get { return _isBeneficiaryRegistrationVATValid; }
            set { _isBeneficiaryRegistrationVATValid = value; }
        }

        [XmlIgnore]
        public bool IsBankAccountValid
        {
            get { return _isBankAccountValid; }
            set { _isBankAccountValid = value; }
        }
    }
}
