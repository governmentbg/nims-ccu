using System;
using System.Xml.Serialization;

namespace R_10053
{
    public partial class TechnicalReportActivity
    {
        private bool _isExecutionDescriptionValid = true;
        private bool _isStatusValid = true;
        private bool _isMonthsDurationValid = true;
        private bool _isActualStartDateValid = true;
        private bool _isActualEndDateValid = true;
        private bool _isDelayReasonValid = true;
        private bool _isPeriodResultValid = true;
        private bool _isCumulativeResultValid = true;

        [XmlIgnore]
        public bool IsExecutionDescriptionValid { get { return _isExecutionDescriptionValid; } set { _isExecutionDescriptionValid = value; } }
        [XmlIgnore]
        public bool IsStatusValid { get { return _isStatusValid; } set { _isStatusValid = value; } }
        [XmlIgnore]
        public bool IsMonthsDurationValid { get { return _isMonthsDurationValid; } set { _isMonthsDurationValid = value; } }
        [XmlIgnore]
        public bool IsActualStartDateValid { get { return _isActualStartDateValid; } set { _isActualStartDateValid = value; } }
        [XmlIgnore]
        public bool IsActualEndDateValid { get { return _isActualEndDateValid; } set { _isActualEndDateValid = value; } }
        [XmlIgnore]
        public bool IsDelayReasonValid { get { return _isDelayReasonValid; } set { _isDelayReasonValid = value; } }
        [XmlIgnore]
        public bool IsPeriodResultValid { get { return _isPeriodResultValid; } set { _isPeriodResultValid = value; } }
        [XmlIgnore]
        public bool IsCumulativeResultValid { get { return _isCumulativeResultValid; } set { _isCumulativeResultValid = value; } }
    }
}
