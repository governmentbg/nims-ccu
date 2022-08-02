using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10006
{
    public partial class Contract
    {
        private bool _isRequestedFundingAmountValid = true;
        private bool _isRequestedFundingPartOfCrossFinancingAmountValid = true;
        private bool _isCoFinancingBudgetAmountValid = true;
        private bool _isBudgetEIBAmountValid = true;
        private bool _isBudgetEBRDAmountValid = true;
        private bool _isBudgetWBAmountValid = true;
        private bool _isBudgetOtherMFIAmountValid = true;
        private bool _isBudgetOtherAmountValid = true;
        private bool _isCoFinancingNonBudgetAmountValid = true;
        private bool _isNonBudgetEIBAmountValid = true;
        private bool _isNonBudgetEBRDAmountValid = true;
        private bool _isNonBudgetWBAmountValid = true;
        private bool _isNonBudgetOtherMFIAmountValid = true;
        private bool _isNonBudgetOtherAmountValid = true;
        private bool _isTotalCoFinancingAmountValid = true;
        private bool _isExpectedLeverageLoanAmountValid = true;
        private bool _isOtherContributionsOutsideESIPublicValid = true;
        private bool _isOtherContributionsOutsideESIPrivateValid = true;
        private bool _isOtherContributionsOutsideESITotalValid = true;
        private bool _isTotalEligibleCostsValid = true;
        private bool _isTotalEligibleCostsPublicFundingValid = true;
        private bool _isRatioRequestedFundingTotalEligibleCostsValid = true;
        private bool _isExpectedRevenueValid = true;
        private bool _isIneligibleCostsValid = true;
        private bool _isIneligibleEIBAmountValid = true;
        private bool _isIneligibleEBRDAmountValid = true;
        private bool _isIneligibleWBAmountValid = true;
        private bool _isIneligibleOtherMFIAmountValid = true;
        private bool _isIneligibleOtherAmountValid = true;
        private bool _isTotalProjectCostValid = true;

        [XmlIgnore]
        public bool IsRequestedFundingAmountValid
        {
            get { return _isRequestedFundingAmountValid; }
            set { _isRequestedFundingAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsRequestedFundingPartOfCrossFinancingAmountValid
        {
            get { return _isRequestedFundingPartOfCrossFinancingAmountValid; }
            set { _isRequestedFundingPartOfCrossFinancingAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsCoFinancingBudgetAmountValid
        {
            get { return _isCoFinancingBudgetAmountValid; }
            set { _isCoFinancingBudgetAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsBudgetEIBAmountValid
        {
            get { return _isBudgetEIBAmountValid; }
            set { _isBudgetEIBAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsBudgetEBRDAmountValid
        {
            get { return _isBudgetEBRDAmountValid; }
            set { _isBudgetEBRDAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsBudgetWBAmountValid
        {
            get { return _isBudgetWBAmountValid; }
            set { _isBudgetWBAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsBudgetOtherMFIAmountValid
        {
            get { return _isBudgetOtherMFIAmountValid; }
            set { _isBudgetOtherMFIAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsBudgetOtherAmountValid
        {
            get { return _isBudgetOtherAmountValid; }
            set { _isBudgetOtherAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsCoFinancingNonBudgetAmountValid
        {
            get { return _isCoFinancingNonBudgetAmountValid; }
            set { _isCoFinancingNonBudgetAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsNonBudgetEIBAmountValid
        {
            get { return _isNonBudgetEIBAmountValid; }
            set { _isNonBudgetEIBAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsNonBudgetEBRDAmountValid
        {
            get { return _isNonBudgetEBRDAmountValid; }
            set { _isNonBudgetEBRDAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsNonBudgetWBAmountValid
        {
            get { return _isNonBudgetWBAmountValid; }
            set { _isNonBudgetWBAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsNonBudgetOtherMFIAmountValid
        {
            get { return _isNonBudgetOtherMFIAmountValid; }
            set { _isNonBudgetOtherMFIAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsNonBudgetOtherAmountValid
        {
            get { return _isNonBudgetOtherAmountValid; }
            set { _isNonBudgetOtherAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsExpectedLeverageLoanAmountValid { get { return _isExpectedLeverageLoanAmountValid; } set { _isExpectedLeverageLoanAmountValid = value; } }

        [XmlIgnore]
        public bool IsOtherContributionsOutsideESIPublicValid { get { return _isOtherContributionsOutsideESIPublicValid; } set { _isOtherContributionsOutsideESIPublicValid = value; } }

        [XmlIgnore]
        public bool IsOtherContributionsOutsideESIPrivateValid { get { return _isOtherContributionsOutsideESIPrivateValid; } set { _isOtherContributionsOutsideESIPrivateValid = value; } }

        [XmlIgnore]
        public bool IsOtherContributionsOutsideESITotalValid { get { return _isOtherContributionsOutsideESITotalValid; } set { _isOtherContributionsOutsideESITotalValid = value; } }

        [XmlIgnore]
        public bool IsTotalCoFinancingAmountValid
        {
            get { return _isTotalCoFinancingAmountValid; }
            set { _isTotalCoFinancingAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsTotalEligibleCostsValid
        {
            get { return _isTotalEligibleCostsValid; }
            set { _isTotalEligibleCostsValid = value; }
        }

        [XmlIgnore]
        public bool IsTotalEligibleCostsPublicFundingValid
        {
            get { return _isTotalEligibleCostsPublicFundingValid; }
            set { _isTotalEligibleCostsPublicFundingValid = value; }
        }

        [XmlIgnore]
        public bool IsRatioRequestedFundingTotalEligibleCostsValid
        {
            get { return _isRatioRequestedFundingTotalEligibleCostsValid; }
            set { _isRatioRequestedFundingTotalEligibleCostsValid = value; }
        }

        [XmlIgnore]
        public bool IsExpectedRevenueValid
        {
            get { return _isExpectedRevenueValid; }
            set { _isExpectedRevenueValid = value; }
        }

        [XmlIgnore]
        public bool IsIneligibleCostsValid
        {
            get { return _isIneligibleCostsValid; }
            set { _isIneligibleCostsValid = value; }
        }

        [XmlIgnore]
        public bool IsIneligibleEIBAmountValid
        {
            get { return _isIneligibleEIBAmountValid; }
            set { _isIneligibleEIBAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsIneligibleEBRDAmountValid
        {
            get { return _isIneligibleEBRDAmountValid; }
            set { _isIneligibleEBRDAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsIneligibleWBAmountValid
        {
            get { return _isIneligibleWBAmountValid; }
            set { _isIneligibleWBAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsIneligibleOtherMFIAmountValid
        {
            get { return _isIneligibleOtherMFIAmountValid; }
            set { _isIneligibleOtherMFIAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsIneligibleOtherAmountValid
        {
            get { return _isIneligibleOtherAmountValid; }
            set { _isIneligibleOtherAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsTotalProjectCostValid
        {
            get { return _isTotalProjectCostValid; }
            set { _isTotalProjectCostValid = value; }
        }
    }
}
