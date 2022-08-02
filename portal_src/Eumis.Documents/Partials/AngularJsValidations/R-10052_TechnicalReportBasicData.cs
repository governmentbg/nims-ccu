using System;
using System.Xml.Serialization;

namespace R_10052
{
    public partial class TechnicalReportBasicData
    {
        private bool _isStartDateValid = true;
        private bool _isEndDateValid = true;
        private bool _isReportTypeValid = true;
        private bool _isPreparerNameValid = true;
        private bool _isPreparerPositionValid = true;
        private bool _isPreparerPhoneValid = true;
        private bool _isPreparerEmailValid = true;

        [XmlIgnore]
        public bool IsStartDateValid { get { return _isStartDateValid; } set { _isStartDateValid = value; } }
        [XmlIgnore]
        public bool IsEndDateValid { get { return _isEndDateValid; } set { _isEndDateValid = value; } }
        [XmlIgnore]
        public bool IsReportTypeValid { get { return _isReportTypeValid; } set { _isReportTypeValid = value; } }
        [XmlIgnore]
        public bool IsPreparerNameValid { get { return _isPreparerNameValid; } set { _isPreparerNameValid = value; } }
        [XmlIgnore]
        public bool IsPreparerPositionValid { get { return _isPreparerPositionValid; } set { _isPreparerPositionValid = value; } }
        [XmlIgnore]
        public bool IsPreparerPhoneValid { get { return _isPreparerPhoneValid; } set { _isPreparerPhoneValid = value; } }
        [XmlIgnore]
        public bool IsPreparerEmailValid { get { return _isPreparerEmailValid; } set { _isPreparerEmailValid = value; } }
    }
}
