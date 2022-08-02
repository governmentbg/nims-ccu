using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10049
{
    public partial class PaymentRequestBasicData
    {
        private bool _isNameValid = true;
        private bool _isTypeValid = true;
        private bool _isStartDateValid = true;
        private bool _isEndDateValid = true;
        private bool _isFinanceReportAmountValid = true;
        private bool _isAdditionalIncomeValid = true;
        private bool _isTotalAmountValid = true;
        private bool _isFinanceReportAmountWithoutIncomeValid = true;
        private bool _isOtherRegistrationValid = true;
        private bool _isBeneficiaryRegistrationVATValid = true;
        private bool _isBankAccountValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsTypeValid
        {
            get { return _isTypeValid; }
            set { _isTypeValid = value; }
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
        public bool IsFinanceReportAmountValid
        {
            get { return _isFinanceReportAmountValid; }
            set { _isFinanceReportAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsAdditionalIncomeValid
        {
            get { return _isAdditionalIncomeValid; }
            set { _isAdditionalIncomeValid = value; }
        }

        [XmlIgnore]
        public bool IsTotalAmountValid
        {
            get { return _isTotalAmountValid; }
            set { _isTotalAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsFinanceReportAmountWithoutIncomeValid
        {
            get { return _isFinanceReportAmountWithoutIncomeValid; }
            set { _isFinanceReportAmountWithoutIncomeValid = value; }
        }

        [XmlIgnore]
        public bool IsOtherRegistrationValid
        {
            get { return _isOtherRegistrationValid; }
            set { _isOtherRegistrationValid = value; }
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
