using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10046
{
    public partial class Contractor
    {
        private bool _isUinTypeValid = true;
        private bool _isUinValid = true;
        private bool _isNameValid = true;
        private bool _isNameEnValid = true;
        private bool _isAddressValid = true;

        private bool _isRepresentativeNamesValid = true;
        private bool _isRepresentativeIDNumberValid = true;
        private bool _isVATRegistrationValid = true;

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
        public bool IsAddressValid
        {
            get { return _isAddressValid; }
            set { _isAddressValid = value; }
        }

        [XmlIgnore]
        public bool IsRepresentativeNamesValid { get { return _isRepresentativeNamesValid; } set { _isRepresentativeNamesValid = value; } }

        [XmlIgnore]
        public bool IsRepresentativeIDNumberValid { get { return _isRepresentativeIDNumberValid; } set { _isRepresentativeIDNumberValid = value; } }

        [XmlIgnore]
        public bool IsVATRegistrationValid { get { return _isVATRegistrationValid; } set { _isVATRegistrationValid = value; } }
    }
}
