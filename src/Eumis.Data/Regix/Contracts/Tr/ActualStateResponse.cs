using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Regix.Contracts.Tr
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class ActualStateResponseType
    {
        public StatusType Status { get; set; }

        public string UIC { get; set; }

        public string Company { get; set; }

        public LegalFormType LegalForm { get; set; }

        public string Transliteration { get; set; }

        public SeatType Seat { get; set; }

        public AddressType SeatForCorrespondence { get; set; }

        public SubjectOfActivityType SubjectOfActivity { get; set; }

        public NKIDType SubjectOfActivityNKID { get; set; }

        public string WayOfManagement { get; set; }

        public string WayOfRepresentation { get; set; }

        public string TermsOfPartnership { get; set; }

        public string TermOfExisting { get; set; }

        public string SpecialConditions { get; set; }

        public string HiddenNonMonetaryDeposit { get; set; }

        public string SharePaymentResponsibility { get; set; }

        public string ConcededEstateValue { get; set; }

        public string CessationOfTrade { get; set; }

        public string AddemptionOfTrader { get; set; }

        public AddemptionOfTraderType AddemptionOfTraderSeatChange { get; set; }

        public CapitalAmountType Funds { get; set; }

        public SharesType Shares { get; set; }

        public CapitalAmountType MinimumAmount { get; set; }

        public CapitalAmountType DepositedFunds { get; set; }

        public NonMonetaryDepositsType NonMonetaryDeposits { get; set; }

        public string BuyBackDecision { get; set; }

        public MandateType BoardOfDirectorsMandate { get; set; }

        public MandateType AdministrativeBoardMandate { get; set; }

        public MandateType BoardOfManagersMandate { get; set; }

        public MandateType BoardOfManagers2Mandate { get; set; }

        public MandateType LeadingBoardMandate { get; set; }

        public MandateType SupervisingBoardMandate { get; set; }

        public MandateType SupervisingBoard2Mandate { get; set; }

        public MandateType ControllingBoardMandate { get; set; }

        public DetailsType Details { get; set; }

        public DateTime? DataValidForDate { get; set; }

        public string LiquidationOrInsolvency { get; set; }
    }

    public partial class ActualStateResponse : ActualStateResponseType
    {
        public ActualStateResponse()
            : base()
        {
        }
    }
}
