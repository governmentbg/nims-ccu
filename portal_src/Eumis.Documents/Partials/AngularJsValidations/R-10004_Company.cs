using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10004
{
    public partial class Company
    {
        private bool _isUinTypeValid = true;
        private bool _isUinValid = true;
        private bool _isNameValid = true;
        private bool _isNameEnValid = true;
        private bool _isCompanyTypeValid = true;
        private bool _isCompanyLegalTypeValid = true;
        private bool _isFinancialContributionValid = true;
        private bool _isEmailValid = true;
        private bool _isPhone1Valid = true;
        private bool _isPhone2Valid = true;
        private bool _isFaxValid = true;
        private bool _isCompanyRepresentativePersonValid = true;
        private bool _isCompanyContactPersonValid = true;
        private bool _isCompanyContactPersonPhoneValid = true;
        private bool _isCompanyContactPersonEmailValid = true;

        [XmlIgnore]
        public bool IsUinTypeValid
        {
            get { return _isUinTypeValid; }
            set { _isUinTypeValid = value; }
        }

        [XmlIgnore]
        public bool IsUinValid
        {
            get { return _isUinValid; }
            set { _isUinValid = value; }
        }

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsNameEnValid
        {
            get { return _isNameEnValid; }
            set { _isNameEnValid = value; }
        }

        [XmlIgnore]
        public bool IsCompanyTypeValid
        {
            get { return _isCompanyTypeValid; }
            set { _isCompanyTypeValid = value; }
        }

        [XmlIgnore]
        public bool IsCompanyLegalTypeValid
        {
            get { return _isCompanyLegalTypeValid; }
            set { _isCompanyLegalTypeValid = value; }
        }

        [XmlIgnore]
        public bool IsFinancialContributionValid
        {
            get { return _isFinancialContributionValid; }
            set { _isFinancialContributionValid = value; }
        }

        [XmlIgnore]
        public bool IsEmailValid
        {
            get { return _isEmailValid; }
            set { _isEmailValid = value; }
        }

        [XmlIgnore]
        public bool IsPhone1Valid
        {
            get { return _isPhone1Valid; }
            set { _isPhone1Valid = value; }
        }

        [XmlIgnore]
        public bool IsPhone2Valid
        {
            get { return _isPhone2Valid; }
            set { _isPhone2Valid = value; }
        }

        [XmlIgnore]
        public bool IsFaxValid
        {
            get { return _isFaxValid; }
            set { _isFaxValid = value; }
        }

        [XmlIgnore]
        public bool IsCompanyRepresentativePersonValid
        {
            get { return _isCompanyRepresentativePersonValid; }
            set { _isCompanyRepresentativePersonValid = value; }
        }

        [XmlIgnore]
        public bool IsCompanyContactPersonValid
        {
            get { return _isCompanyContactPersonValid; }
            set { _isCompanyContactPersonValid = value; }
        }

        [XmlIgnore]
        public bool IsCompanyContactPersonPhoneValid
        {
            get { return _isCompanyContactPersonPhoneValid; }
            set { _isCompanyContactPersonPhoneValid = value; }
        }

        [XmlIgnore]
        public bool IsCompanyContactPersonEmailValid
        {
            get { return _isCompanyContactPersonEmailValid; }
            set { _isCompanyContactPersonEmailValid = value; }
        }
    }
}
