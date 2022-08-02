using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10039
{
    public partial class BFPContractContractTeam
    {
        private bool _isNameValid = true;
        private bool _isPositionValid = true;
        private bool _isResponsibilitiesValid = true;
        private bool _isPhoneValid = true;
        private bool _isEmailValid = true;
        private bool _isFaxValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsPositionValid
        {
            get { return _isPositionValid; }
            set { _isPositionValid = value; }
        }

        [XmlIgnore]
        public bool IsResponsibilitiesValid
        {
            get { return _isResponsibilitiesValid; }
            set { _isResponsibilitiesValid = value; }
        }

        [XmlIgnore]
        public bool IsPhoneValid
        {
            get { return _isPhoneValid; }
            set { _isPhoneValid = value; }
        }

        [XmlIgnore]
        public bool IsEmailValid
        {
            get { return _isEmailValid; }
            set { _isEmailValid = value; }
        }

        [XmlIgnore]
        public bool IsFaxValid
        {
            get { return _isFaxValid; }
            set { _isFaxValid = value; }
        }
    }
}
