using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10037
{
    public partial class BFPContractContractActivity
    {
        private bool _isCompanyValid = true;
        private bool _isCodeValid = true;
        private bool _isNameValid = true;
        private bool _isExecutionMethodValid = true;
        private bool _isResultValid = true;
        private bool _isStartMonthValid = true;
        private bool _isDurationValid = true;
        private bool _isAmountValid = true;
        private bool _isPeriodValid = true;

        [XmlIgnore]
        public bool IsCompanyValid
        {
            get { return _isCompanyValid; }
            set { _isCompanyValid = value; }
        }

        [XmlIgnore]
        public bool IsCodeValid
        {
            get { return _isCodeValid; }
            set { _isCodeValid = value; }
        }

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsExecutionMethodValid
        {
            get { return _isExecutionMethodValid; }
            set { _isExecutionMethodValid = value; }
        }

        [XmlIgnore]
        public bool IsResultValid
        {
            get { return _isResultValid; }
            set { _isResultValid = value; }
        }

        [XmlIgnore]
        public bool IsStartMonthValid
        {
            get { return _isStartMonthValid; }
            set { _isStartMonthValid = value; }
        }

        [XmlIgnore]
        public bool IsDurationValid
        {
            get { return _isDurationValid; }
            set { _isDurationValid = value; }
        }

        [XmlIgnore]
        public bool IsAmountValid
        {
            get { return _isAmountValid; }
            set { _isAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsPeriodValid
        {
            get { return _isPeriodValid; }
            set { _isPeriodValid = value; }
        }
    }
}
