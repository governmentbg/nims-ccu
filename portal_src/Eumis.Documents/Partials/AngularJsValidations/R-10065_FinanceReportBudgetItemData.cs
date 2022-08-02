using System;
using System.Xml.Serialization;

namespace R_10065
{
    public partial class FinanceReportBudgetItemData
    {
        private bool _isBudgetDetailValid = true;
        private bool _isContractActivityValid = true;
        private bool _isGrandAmountValid = true;
        private bool _isSelfAmountValid = true;
        private bool _isCrossFinancingValid = true;
        private bool _isTotalAmountValid = true;
        private bool _isUnitDefinitionValid = true;
        private bool _isProducedUnitsCountValid = true;
        private bool _isUnitCostValid = true;

        [XmlIgnore]
        public bool IsBudgetDetailValid { get { return _isBudgetDetailValid; } set { _isBudgetDetailValid = value; } }
        [XmlIgnore]
        public bool IsContractActivityValid { get { return _isContractActivityValid; } set { _isContractActivityValid = value; } }
        [XmlIgnore]
        public bool IsGrandAmountValid { get { return _isGrandAmountValid; } set { _isGrandAmountValid = value; } }
        [XmlIgnore]
        public bool IsSelfAmountValid { get { return _isSelfAmountValid; } set { _isSelfAmountValid = value; } }
        [XmlIgnore]
        public bool IsCrossFinancingValid { get { return _isCrossFinancingValid; } set { _isCrossFinancingValid = value; } }
        [XmlIgnore]
        public bool IsTotalAmountValid { get { return _isTotalAmountValid; } set { _isTotalAmountValid = value; } }
        [XmlIgnore]
        public bool IsUnitDefinitionValid { get { return _isUnitDefinitionValid; } set { _isUnitDefinitionValid = value; } }
        [XmlIgnore]
        public bool IsProducedUnitsCountValid { get { return _isProducedUnitsCountValid; } set { _isProducedUnitsCountValid = value; } }
        [XmlIgnore]
        public bool IsUnitCostValid { get { return _isUnitCostValid; } set { _isUnitCostValid = value; } }
    }
}
