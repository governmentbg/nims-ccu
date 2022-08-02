using System;
using System.Collections.Generic;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Monitoring.ViewObjects
{
    //// TODO add
    //// - LandExpenses(53-59);
    //// - OneTimeExpenses(65-68);
    //// - PaymentApplications(79 - 90)
    //// - Reports(91-105)
    //// - Expenses(106-113)

    public class Anex3ReportVO
    {
        public string CompanyUin { get; set; }

        public string CompanyName { get; set; }

        public string RegNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public YesNoOther? IsVatEligible { get; set; }

        public string CorrespondenceAddress { get; set; }

        public string CorrespondenceEmail { get; set; }

        public string CorrespondencePhone1 { get; set; }

        public string CorrespondencePhone2 { get; set; }

        public string CorrespondenceFax { get; set; }

        public string CompanyContactPerson { get; set; }

        public string CompanyContactPersonPhone { get; set; }

        public string CompanyContactPersonEmail { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public string ManagingAuthority { get; set; }

        public DateTime? DocDate { get; set; }

        public ContractAmountType? AmountType { get; set; }

        public bool IsJointActionPlan { get; set; }

        public bool IncludesSupportFromIYE { get; set; }

        public bool IsSubjectToStateAidRegime { get; set; }

        public bool IncludesPublicPrivatePartnership { get; set; }

        public string Currency { get; set; }

        public string ProgrammeCode { get; set; }

        public List<string> ProgrammePriorities { get; set; }

        public NutsLevel? NutsLevel { get; set; }

        public List<string> InterventionFields { get; set; }

        public List<string> FormOfFinance { get; set; }

        public List<string> TerritorialDimensions { get; set; }

        public List<string> TerritorialDeliveryMechanisms { get; set; }

        public List<string> ThematicObjectives { get; set; }

        public List<string> ESFSecondaryThemes { get; set; }

        public List<string> EconomicDimensions { get; set; }

        public List<string> Locations { get; set; }

        public List<Anex3IndicatorVO> Indicators { get; set; }

        public decimal? ApprovedExpenses { get; set; }

        public decimal? PublicExpenses { get; set; }

        public decimal? PublicAmounts { get; set; }

        public List<Anex3PaymentVO> ReportPayments { get; set; }

        public List<Anex3StandardTablesExpenseVO> StandardTablesExpenses { get; set; }

        public List<Anex3FlatRateExpenseVO> FlatRateExpenses { get; set; }

        public List<Anex3ReimbursedAmountVO> ReimbursedAmounts { get; set; }
    }
}
