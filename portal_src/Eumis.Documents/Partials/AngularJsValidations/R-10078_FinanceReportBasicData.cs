using System;
using System.Xml.Serialization;

namespace R_10078
{
    public partial class FinanceReportBasicData
    {
        private bool _isStartDateValid = true;
        private bool _isEndDateValid = true;

        [XmlIgnore]
        public bool IsStartDateValid { get { return _isStartDateValid; } set { _isStartDateValid = value; } }
        [XmlIgnore]
        public bool IsEndDateValid { get { return _isEndDateValid; } set { _isEndDateValid = value; } }
    }
}
