using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10029
{
    public partial class PreliminaryContract
    {
        private bool _isRequestedFundingAmountValid = true;
        private bool _isCoFinancingBudgetAmountValid = true;

        [XmlIgnore]
        public bool IsRequestedFundingAmountValid
        {
            get { return _isRequestedFundingAmountValid; }
            set { _isRequestedFundingAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsCoFinancingBudgetAmountValid
        {
            get { return _isCoFinancingBudgetAmountValid; }
            set { _isCoFinancingBudgetAmountValid = value; }
        }
    }
}
