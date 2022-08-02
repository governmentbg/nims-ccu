using System;
using System.Xml.Serialization;

namespace R_10054
{
    public partial class TechnicalReportIndicator
    {
        private bool _isPeriodAmountMenValid = true;
        private bool _isPeriodAmountWomenValid = true;
        private bool _isPeriodAmountTotalValid = true;

        private bool _isCumulativeAmountMenValid = true;
        private bool _isCumulativeAmountWomenValid = true;
        private bool _isCumulativeAmountTotalValid = true;

        private bool _isResidueAmountMenValid = true;
        private bool _isResidueAmountWomenValid = true;
        private bool _isResidueAmountTotalValid = true;

        private bool _isCommentValid = true;

        [XmlIgnore]
        public bool IsPeriodAmountMenValid { get { return _isPeriodAmountMenValid; } set { _isPeriodAmountMenValid = value; } }
        [XmlIgnore]
        public bool IsPeriodAmountWomenValid { get { return _isPeriodAmountWomenValid; } set { _isPeriodAmountWomenValid = value; } }
        [XmlIgnore]
        public bool IsPeriodAmountTotalValid { get { return _isPeriodAmountTotalValid; } set { _isPeriodAmountTotalValid = value; } }

        [XmlIgnore]
        public bool IsCumulativeAmountMenValid { get { return _isCumulativeAmountMenValid; } set { _isCumulativeAmountMenValid = value; } }
        [XmlIgnore]
        public bool IsCumulativeAmountWomenValid { get { return _isCumulativeAmountWomenValid; } set { _isCumulativeAmountWomenValid = value; } }
        [XmlIgnore]
        public bool IsCumulativeAmountTotalValid { get { return _isCumulativeAmountTotalValid; } set { _isCumulativeAmountTotalValid = value; } }

        [XmlIgnore]
        public bool IsResidueAmountMenValid { get { return _isResidueAmountMenValid; } set { _isResidueAmountMenValid = value; } }
        [XmlIgnore]
        public bool IsResidueAmountWomenValid { get { return _isResidueAmountWomenValid; } set { _isResidueAmountWomenValid = value; } }
        [XmlIgnore]
        public bool IsResidueAmountTotalValid { get { return _isResidueAmountTotalValid; } set { _isResidueAmountTotalValid = value; } }

        [XmlIgnore]
        public bool IsCommentValid { get { return _isCommentValid; } set { _isCommentValid = value; } }
    }
}
