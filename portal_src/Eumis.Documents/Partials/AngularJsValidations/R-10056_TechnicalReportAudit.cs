using System;
using System.Xml.Serialization;

namespace R_10056
{
    public partial class TechnicalReportAudit
    {
        private bool _isAuditeeTypeValid = true;
        private bool _isFinalReportDateValid = true;
        private bool _isFinalReportNumberValid = true;
        private bool _isInspectionEndDateValid = true;
        private bool _isInspectionStartDateValid = true;
        private bool _isInstitutionValid = true;
        private bool _isKindValid = true;
        private bool _isPreviousReportDateValid = true;
        private bool _isPreviousReportNumberValid = true;
        private bool _isRangeValid = true;
        private bool _isTypeValid = true;
        private bool _hasValidCount = true;


        [XmlIgnore]
        public bool IsAuditeeTypeValid { get { return _isAuditeeTypeValid; } set { _isAuditeeTypeValid = value; } }
        [XmlIgnore]
        public bool IsFinalReportDateValid { get { return _isFinalReportDateValid; } set { _isFinalReportDateValid = value; } }
        [XmlIgnore]
        public bool IsFinalReportNumberValid { get { return _isFinalReportNumberValid; } set { _isFinalReportNumberValid = value; } }
        [XmlIgnore]
        public bool IsInspectionEndDateValid { get { return _isInspectionEndDateValid; } set { _isInspectionEndDateValid = value; } }
        [XmlIgnore]
        public bool IsInspectionStartDateValid { get { return _isInspectionStartDateValid; } set { _isInspectionStartDateValid = value; } }
        [XmlIgnore]
        public bool IsInstitutionValid { get { return _isInstitutionValid; } set { _isInstitutionValid = value; } }
        [XmlIgnore]
        public bool IsKindValid { get { return _isKindValid; } set { _isKindValid = value; } }
        [XmlIgnore]
        public bool IsPreviousReportDateValid { get { return _isPreviousReportDateValid; } set { _isPreviousReportDateValid = value; } }
        [XmlIgnore]
        public bool IsPreviousReportNumberValid { get { return _isPreviousReportNumberValid; } set { _isPreviousReportNumberValid = value; } }
        [XmlIgnore]
        public bool IsRangeValid { get { return _isRangeValid; } set { _isRangeValid = value; } }
        [XmlIgnore]
        public bool IsTypeValid { get { return _isTypeValid; } set { _isTypeValid = value; } }
        [XmlIgnore]
        public bool HasValidCount { get { return _hasValidCount; } set { _hasValidCount = value; } }
    }
}
