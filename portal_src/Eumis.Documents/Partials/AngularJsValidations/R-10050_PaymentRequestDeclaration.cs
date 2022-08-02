using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10050
{
    public partial class PaymentRequestDeclaration
    {
        private bool _isRepresentingBeneficiaryEGNValid = true;
        private bool _isRepresentingFirstNameValid = true;
        private bool _isRepresentingMiddleNameValid = true;
        private bool _isRepresentingLastNameValid = true;
        private bool _isTextPt3Valid = true;
        private bool _isTextPt25Valid = true;

        [XmlIgnore]
        public bool IsRepresentingBeneficiaryEGNValid
        {
            get { return _isRepresentingBeneficiaryEGNValid; }
            set { _isRepresentingBeneficiaryEGNValid = value; }
        }

        [XmlIgnore]
        public bool IsRepresentingFirstNameValid
        {
            get { return _isRepresentingFirstNameValid; }
            set { _isRepresentingFirstNameValid = value; }
        }

        [XmlIgnore]
        public bool IsRepresentingMiddleNameValid
        {
            get { return _isRepresentingMiddleNameValid; }
            set { _isRepresentingMiddleNameValid = value; }
        }

        [XmlIgnore]
        public bool IsRepresentingLastNameValid
        {
            get { return _isRepresentingLastNameValid; }
            set { _isRepresentingLastNameValid = value; }
        }

        [XmlIgnore]
        public bool IsTextPt3Valid
        {
            get { return _isTextPt3Valid; }
            set { _isTextPt3Valid = value; }
        }

        [XmlIgnore]
        public bool IsTextPt25Valid
        {
            get { return _isTextPt25Valid; }
            set { _isTextPt25Valid = value; }
        }
    }
}
