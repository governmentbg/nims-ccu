using System;
using System.Xml.Serialization;

namespace R_10047
{
    public partial class ActivitiesBudgetDetailsRef
    {
        private bool _isContractActivityValid = true;
        private bool _isBudgetDetailValid = true;

        [XmlIgnore]
        public bool IsContractActivityValid
        {
            get { return _isContractActivityValid; }
            set { _isContractActivityValid = value; }
        }

        [XmlIgnore]
        public bool IsBudgetDetailValid
        {
            get { return _isBudgetDetailValid; }
            set { _isBudgetDetailValid = value; }
        }
    }
}
