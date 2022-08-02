using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Debts;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public partial class IrregularityVersion : IAggregateRoot
    {

        public int IrregularityVersionId { get; set; }

        public int IrregularityId { get; set; }

        public int OrderNum { get; set; }

        public IrregularityVersionStatus Status { get; set; }

        public string RegNumber { get; set; }

        public int? IrregularityCategoryId { get; set; }

        public int? IrregularityTypeId { get; set; }

        public IrregularityClassification? IrregularityClassification { get; set; }

        public DateTime IrregularityDateFrom { get; set; }

        public DateTime? IrregularityDateTo { get; set; }

        public string EndingActRegNum { get; set; }

        public DateTime? EndingActDate { get; set; }

        public IrregularityCaseState? CaseState { get; set; }

        public DateTime? IrregularityEndDate { get; set; }

        public Year ReportYear { get; set; }

        public Quarter ReportQuarter { get; set; }

        public IrregularityRapporteur? Rapporteur { get; set; }

        public string RapporteurComments { get; set; }

        public bool? IsNewUnlawfulPractice { get; set; }

        public bool? ShouldInformOther { get; set; }

        public IrregularityProcedureStatus? ProcedureStatus { get; set; }

        public int? FinancialStatusId { get; set; }

        public string AppliedPractices { get; set; }

        public string BeneficiaryData { get; set; }

        public string AdminAscertainments { get; set; }

        public string IrregularityDetectedBy { get; set; }

        public decimal? EUCoFinancingPercent { get; set; }

        public ContractDebtExecutionStatus? ContractDebtStatus { get; set; }

        public bool? ShouldDecertifyIrregularExpenses { get; set; }

        public string DecertificationComments { get; set; }

        public string AdminProcedures { get; set; }

        public string PenaltyProcedures { get; set; }

        public bool ShouldReportToOlaf { get; set; }

        public IrregularityReasonNotReportingToOlaf? ReasonNotReportingToOlaf { get; set; }

        public IrregularityCheckTime? CheckTime { get; set; }

        public IrregularityImpairedRegulation ImpairedRegulation { get; set; }

        public ExpensesAmount ExpensesLv { get; set; }

        public ExpensesAmount ExpensesEuro { get; set; }

        public BfpAmount IrregularExpensesLv { get; set; }

        public BfpAmount IrregularExpensesEuro { get; set; }

        public BfpAmount CertifiedExpensesLv { get; set; }

        public BfpAmount CertifiedExpensesEuro { get; set; }

        public BfpAmount PaidLv { get; set; }

        public BfpAmount PaidEuro { get; set; }

        public IrregularitySanction Sanction { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<IrregularityVersionDoc> Documents { get; set; }

        public ICollection<IrregularityVersionInvolvedPerson> InvolvedPersons { get; set; }
    }

    public class IrregularityVersionMap : EntityTypeConfiguration<IrregularityVersion>
    {
        public IrregularityVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityVersionId);

            // Properties
            this.Property(t => t.IrregularityVersionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularityId)
                .IsRequired();
            this.Property(t => t.OrderNum)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.IrregularityDateFrom)
                .IsRequired();
            this.Property(t => t.EndingActRegNum)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.IrregularityDetectedBy)
                .HasMaxLength(500)
                .IsOptional();
            this.Property(t => t.AdminProcedures)
                .HasMaxLength(500)
                .IsOptional();
            this.Property(t => t.PenaltyProcedures)
                .HasMaxLength(500)
                .IsOptional();
            this.Property(t => t.ReportQuarter)
                .IsRequired();
            this.Property(t => t.ReportYear)
                .IsRequired();
            this.Property(t => t.ShouldReportToOlaf)
                .IsRequired();
            this.Property(t => t.Sanction.ProcedureType)
                .IsRequired();
            this.Property(t => t.CreateDate)
                .IsRequired();
            this.Property(t => t.ModifyDate)
                .IsRequired();
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("IrregularityVersions");
            this.Property(t => t.IrregularityVersionId).HasColumnName("IrregularityVersionId");
            this.Property(t => t.IrregularityId).HasColumnName("IrregularityId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");

            this.Property(t => t.IrregularityCategoryId).HasColumnName("IrregularityCategoryId");
            this.Property(t => t.IrregularityTypeId).HasColumnName("IrregularityTypeId");
            this.Property(t => t.IrregularityClassification).HasColumnName("IrregularityClassification");
            this.Property(t => t.IrregularityDateFrom).HasColumnName("IrregularityDateFrom");
            this.Property(t => t.IrregularityDateTo).HasColumnName("IrregularityDateTo");

            this.Property(t => t.EndingActRegNum).HasColumnName("EndingActRegNum");
            this.Property(t => t.EndingActDate).HasColumnName("EndingActDate");
            this.Property(t => t.CaseState).HasColumnName("CaseState");
            this.Property(t => t.IrregularityEndDate).HasColumnName("IrregularityEndDate");

            this.Property(t => t.ReportYear).HasColumnName("ReportYear");
            this.Property(t => t.ReportQuarter).HasColumnName("ReportQuarter");
            this.Property(t => t.Rapporteur).HasColumnName("Rapporteur");
            this.Property(t => t.RapporteurComments).HasColumnName("RapporteurComments");
            this.Property(t => t.IsNewUnlawfulPractice).HasColumnName("IsNewUnlawfulPractice");
            this.Property(t => t.ShouldInformOther).HasColumnName("ShouldInformOther");
            this.Property(t => t.ProcedureStatus).HasColumnName("ProcedureStatus");
            this.Property(t => t.FinancialStatusId).HasColumnName("FinancialStatusId");

            this.Property(t => t.ImpairedRegulation.ImpairedRegulationAct).HasColumnName("ImpairedRegulationAct");
            this.Property(t => t.ImpairedRegulation.ImpairedRegulationNum).HasColumnName("ImpairedRegulationNum");
            this.Property(t => t.ImpairedRegulation.ImpairedRegulationYear).HasColumnName("ImpairedRegulationYear");
            this.Property(t => t.ImpairedRegulation.ImpairedRegulation).HasColumnName("ImpairedRegulation");
            this.Property(t => t.ImpairedRegulation.ImpairedNationalRegulation).HasColumnName("ImpairedNationalRegulation");

            this.Property(t => t.AppliedPractices).HasColumnName("AppliedPractices");
            this.Property(t => t.BeneficiaryData).HasColumnName("BeneficiaryData");
            this.Property(t => t.AdminAscertainments).HasColumnName("AdminAscertainments");
            this.Property(t => t.IrregularityDetectedBy).HasColumnName("IrregularityDetectedBy");

            this.Property(t => t.EUCoFinancingPercent).HasColumnName("EUCoFinancingPercent");

            this.Property(t => t.ExpensesLv.BfpEuAmount).HasColumnName("ExpensesBfpEuAmountLv");
            this.Property(t => t.ExpensesLv.BfpBgAmount).HasColumnName("ExpensesBfpBgAmountLv");
            this.Property(t => t.ExpensesLv.BfpTotalAmount).HasColumnName("ExpensesBfpTotalAmountLv");
            this.Property(t => t.ExpensesLv.SelfAmount).HasColumnName("ExpensesSelfAmountLv");
            this.Property(t => t.ExpensesLv.TotalAmount).HasColumnName("ExpensesTotalAmountLv");

            this.Property(t => t.ExpensesEuro.BfpEuAmount).HasColumnName("ExpensesBfpEuAmountEuro");
            this.Property(t => t.ExpensesEuro.BfpBgAmount).HasColumnName("ExpensesBfpBgAmountEuro");
            this.Property(t => t.ExpensesEuro.BfpTotalAmount).HasColumnName("ExpensesBfpTotalAmountEuro");
            this.Property(t => t.ExpensesEuro.SelfAmount).HasColumnName("ExpensesSelfAmountEuro");
            this.Property(t => t.ExpensesEuro.TotalAmount).HasColumnName("ExpensesTotalAmountEuro");

            this.Property(t => t.IrregularExpensesLv.EuAmount).HasColumnName("IrregularExpensesBfpEuAmountLv");
            this.Property(t => t.IrregularExpensesLv.BgAmount).HasColumnName("IrregularExpensesBfpBgAmountLv");
            this.Property(t => t.IrregularExpensesLv.TotalAmount).HasColumnName("IrregularExpensesBfpTotalAmountLv");

            this.Property(t => t.IrregularExpensesEuro.EuAmount).HasColumnName("IrregularExpensesBfpEuAmountEuro");
            this.Property(t => t.IrregularExpensesEuro.BgAmount).HasColumnName("IrregularExpensesBfpBgAmountEuro");
            this.Property(t => t.IrregularExpensesEuro.TotalAmount).HasColumnName("IrregularExpensesBfpTotalAmountEuro");

            this.Property(t => t.CertifiedExpensesLv.EuAmount).HasColumnName("CertifiedExpensesBfpEuAmountLv");
            this.Property(t => t.CertifiedExpensesLv.BgAmount).HasColumnName("CertifiedExpensesBfpBgAmountLv");
            this.Property(t => t.CertifiedExpensesLv.TotalAmount).HasColumnName("CertifiedExpensesBfpTotalAmountLv");

            this.Property(t => t.CertifiedExpensesEuro.EuAmount).HasColumnName("CertifiedExpensesBfpEuAmountEuro");
            this.Property(t => t.CertifiedExpensesEuro.BgAmount).HasColumnName("CertifiedExpensesBfpBgAmountEuro");
            this.Property(t => t.CertifiedExpensesEuro.TotalAmount).HasColumnName("CertifiedExpensesBfpTotalAmountEuro");

            this.Property(t => t.PaidLv.EuAmount).HasColumnName("PaidBfpEuAmountLv");
            this.Property(t => t.PaidLv.BgAmount).HasColumnName("PaidBfpBgAmountLv");
            this.Property(t => t.PaidLv.TotalAmount).HasColumnName("PaidBfpTotalAmountLv");

            this.Property(t => t.PaidEuro.EuAmount).HasColumnName("PaidBfpEuAmountEuro");
            this.Property(t => t.PaidEuro.BgAmount).HasColumnName("PaidBfpBgAmountEuro");
            this.Property(t => t.PaidEuro.TotalAmount).HasColumnName("PaidBfpTotalAmountEuro");

            this.Property(t => t.ContractDebtStatus).HasColumnName("ContractDebtStatus");
            this.Property(t => t.ShouldDecertifyIrregularExpenses).HasColumnName("ShouldDecertifyIrregularExpenses");
            this.Property(t => t.DecertificationComments).HasColumnName("DecertificationComments");

            this.Property(t => t.Sanction.ProcedureType).HasColumnName("SanctionProcedureType");
            this.Property(t => t.Sanction.ProcedureKind).HasColumnName("SanctionProcedureKind");
            this.Property(t => t.Sanction.ProcedureStartDate).HasColumnName("SanctionProcedureStartDate");
            this.Property(t => t.Sanction.ProcedureExpectedEndDate).HasColumnName("SanctionProcedureExpectedEndDate");
            this.Property(t => t.Sanction.ProcedureEndDate).HasColumnName("SanctionProcedureEndDate");
            this.Property(t => t.Sanction.ProcedureStatus).HasColumnName("SanctionProcedureStatus");
            this.Property(t => t.Sanction.SanctionCategoryId).HasColumnName("SanctionCategoryId");
            this.Property(t => t.Sanction.SanctionTypeId).HasColumnName("SanctionTypeId");
            this.Property(t => t.Sanction.Fines).HasColumnName("SanctionFines");

            this.Property(t => t.AdminProcedures).HasColumnName("AdminProcedures");
            this.Property(t => t.PenaltyProcedures).HasColumnName("PenaltyProcedures");

            this.Property(t => t.ShouldReportToOlaf).HasColumnName("ShouldReportToOlaf");
            this.Property(t => t.ReasonNotReportingToOlaf).HasColumnName("ReasonNotReportingToOlaf");
            this.Property(t => t.CheckTime).HasColumnName("CheckTime");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
