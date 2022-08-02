using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10048
{
    public partial class ProcurementPlan
    {
        private bool _isNameValid = true;
        private bool _isAreaValid = true;
        private bool _isLegalActValid = true;
        private bool _isProcedureTypeValid = true;
        private bool _isMAPreliminaryControlValid = true;
        private bool _isPPAPreliminaryControlValid = true;
        private bool _isInternetAddressValid = true;
        private bool _isExpectedAmountValid = true;
        private bool _isNoticeDateValid = true;
        private bool _isOffersDeadlineDateValid = true;
        private bool _isDifferentiatedPositionCountValid = true;
        private bool _isPublicAttachedDocumentCountValid = true;
        private bool _isAdditionalAttachedDocumentCountValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsAreaValid
        {
            get { return _isAreaValid; }
            set { _isAreaValid = value; }
        }

        [XmlIgnore]
        public bool IsLegalActValid
        {
            get { return _isLegalActValid; }
            set { _isLegalActValid = value; }
        }

        [XmlIgnore]
        public bool IsProcedureTypeValid
        {
            get { return _isProcedureTypeValid; }
            set { _isProcedureTypeValid = value; }
        }

        [XmlIgnore]
        public bool IsMAPreliminaryControlValid
        {
            get { return _isMAPreliminaryControlValid; }
            set { _isMAPreliminaryControlValid = value; }
        }

        [XmlIgnore]
        public bool IsPPAPreliminaryControlValid
        {
            get { return _isPPAPreliminaryControlValid; }
            set { _isPPAPreliminaryControlValid = value; }
        }

        [XmlIgnore]
        public bool IsInternetAddressValid
        {
            get { return _isInternetAddressValid; }
            set { _isInternetAddressValid = value; }
        }

        [XmlIgnore]
        public bool IsExpectedAmountValid
        {
            get { return _isExpectedAmountValid; }
            set { _isExpectedAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsNoticeDateValid
        {
            get { return _isNoticeDateValid; }
            set { _isNoticeDateValid = value; }
        }

        [XmlIgnore]
        public bool IsOffersDeadlineDateValid
        {
            get { return _isOffersDeadlineDateValid; }
            set { _isOffersDeadlineDateValid = value; }
        }

        [XmlIgnore]
        public bool IsDifferentiatedPositionCountValid
        {
            get { return _isDifferentiatedPositionCountValid; }
            set { _isDifferentiatedPositionCountValid = value; }
        }

        [XmlIgnore]
        public bool IsPublicAttachedDocumentCountValid
        {
            get { return _isPublicAttachedDocumentCountValid; }
            set { _isPublicAttachedDocumentCountValid = value; }
        }

        [XmlIgnore]
        public bool IsAdditionalAttachedDocumentCountValid
        {
            get { return _isAdditionalAttachedDocumentCountValid; }
            set { _isAdditionalAttachedDocumentCountValid = value; }
        }
    }
}
