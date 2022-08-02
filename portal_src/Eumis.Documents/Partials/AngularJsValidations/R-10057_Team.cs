using System;
using System.Xml.Serialization;

namespace R_10057
{
    public partial class TechnicalReportTeamMember
    {
        private bool _isNameValid = true;
        private bool _isPositionValid = true;
        private bool _isUinTypeValid = true;
        private bool _isUinValid = true;
        private bool _isCommitmentTypeValid = true;
        private bool _isDateValid = true;
        private bool _isHoursValid = true;
        private bool _isActivityValid = true;
        private bool _isResultValid = true;

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
        public bool IsCommitmentTypeValid
        {
            get { return _isCommitmentTypeValid; }
            set { _isCommitmentTypeValid = value; }
        }

        [XmlIgnore]
        public bool IsDateValid
        {
            get { return _isDateValid; }
            set { _isDateValid = value; }
        }

        [XmlIgnore]
        public bool IsHoursValid
        {
            get { return _isHoursValid; }
            set { _isHoursValid = value; }
        }

        [XmlIgnore]
        public bool IsActivityValid
        {
            get { return _isActivityValid; }
            set { _isActivityValid = value; }
        }

        [XmlIgnore]
        public bool IsResultValid
        {
            get { return _isResultValid; }
            set { _isResultValid = value; }
        }

        [XmlIgnore]
        public bool IsOpen { get; set; }
    }
}
