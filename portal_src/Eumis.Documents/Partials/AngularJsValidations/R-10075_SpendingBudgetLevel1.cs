using System.Xml.Serialization;

namespace R_10075
{
    public partial class SpendingBudgetLevel1
    {
        private bool _isTotalCalculatedAmountValid = true;

        [XmlIgnore]
        public bool IsTotalCalculatedAmountValid
        {
            get { return _isTotalCalculatedAmountValid; }
            set { _isTotalCalculatedAmountValid = value; }
        }
    }
}
