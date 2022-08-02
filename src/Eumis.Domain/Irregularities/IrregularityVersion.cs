using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Debts;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularityVersion : IAggregateRoot
    {
        public IrregularityVersion()
        {
            this.ImpairedRegulation = new IrregularityImpairedRegulation();
            this.ExpensesLv = new ExpensesAmount();
            this.ExpensesEuro = new ExpensesAmount();
            this.IrregularExpensesLv = new BfpAmount();
            this.IrregularExpensesEuro = new BfpAmount();
            this.CertifiedExpensesLv = new BfpAmount();
            this.CertifiedExpensesEuro = new BfpAmount();
            this.PaidLv = new BfpAmount();
            this.PaidEuro = new BfpAmount();
            this.Sanction = new IrregularitySanction();
            this.Documents = new List<IrregularityVersionDoc>();
            this.InvolvedPersons = new List<IrregularityVersionInvolvedPerson>();
        }

        // first irregularity version
        public IrregularityVersion(
            int irregularityId,
            DateTime irregularityDateFrom,
            DateTime? irregularityDateTo,
            Year reportYear,
            Quarter reportQuarter,
            bool shouldReportToOlaf,
            IrregularityReasonNotReportingToOlaf? reasonNotReportingToOlaf,
            IrregularitySanctionProcedureType sanctionProcedureType,
            IrregularitySanctionProcedureKind? sanctionProcedureKind,
            DateTime? sanctionProcedureStartDate,
            DateTime? sanctionProcedureExpectedEndDate,
            DateTime? sanctionProcedureEndDate,
            IrregularitySanctionProcedureStatus? sanctionProcedureStatus,
            int? sanctionCategoryId,
            int? sanctionTypeId,
            string fines,
            DateTime currentDate)
            : this()
        {
            this.IrregularityId = irregularityId;
            this.OrderNum = 1;
            this.Status = IrregularityVersionStatus.Draft;

            this.IrregularityDateFrom = irregularityDateFrom;
            this.IrregularityDateTo = irregularityDateTo;

            this.ReportYear = reportYear;
            this.ReportQuarter = reportQuarter;
            this.ShouldReportToOlaf = shouldReportToOlaf;
            this.ReasonNotReportingToOlaf = shouldReportToOlaf ?
                (IrregularityReasonNotReportingToOlaf?)null :
                reasonNotReportingToOlaf.Value;

            this.Sanction.SetAttributes(
                sanctionProcedureType,
                sanctionProcedureKind,
                sanctionProcedureStartDate,
                sanctionProcedureExpectedEndDate,
                sanctionProcedureEndDate,
                sanctionProcedureStatus,
                sanctionCategoryId,
                sanctionTypeId,
                fines);

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public IrregularityVersion(IrregularityVersion prevVersion)
            : this()
        {
            this.IrregularityId = prevVersion.IrregularityId;
            this.OrderNum = prevVersion.OrderNum + 1;
            this.Status = IrregularityVersionStatus.Draft;
            this.IrregularityCategoryId = prevVersion.IrregularityCategoryId;
            this.IrregularityTypeId = prevVersion.IrregularityTypeId;
            this.IrregularityClassification = prevVersion.IrregularityClassification;
            this.IrregularityDateFrom = prevVersion.IrregularityDateFrom;
            this.IrregularityDateTo = prevVersion.IrregularityDateTo;
            this.EndingActRegNum = prevVersion.EndingActRegNum;
            this.EndingActDate = prevVersion.EndingActDate;
            this.CaseState = prevVersion.CaseState;
            this.IrregularityEndDate = prevVersion.IrregularityEndDate;
            this.ReportYear = prevVersion.ReportYear;
            this.ReportQuarter = prevVersion.ReportQuarter;
            this.Rapporteur = prevVersion.Rapporteur;
            this.RapporteurComments = prevVersion.RapporteurComments;
            this.IsNewUnlawfulPractice = prevVersion.IsNewUnlawfulPractice;
            this.ShouldInformOther = prevVersion.ShouldInformOther;
            this.ProcedureStatus = prevVersion.ProcedureStatus;
            this.FinancialStatusId = prevVersion.FinancialStatusId;
            this.AppliedPractices = prevVersion.AppliedPractices;
            this.BeneficiaryData = prevVersion.BeneficiaryData;
            this.AdminAscertainments = prevVersion.AdminAscertainments;
            this.IrregularityDetectedBy = prevVersion.IrregularityDetectedBy;
            this.EUCoFinancingPercent = prevVersion.EUCoFinancingPercent;
            this.ContractDebtStatus = prevVersion.ContractDebtStatus;
            this.CheckTime = prevVersion.CheckTime;
            this.ShouldDecertifyIrregularExpenses = prevVersion.ShouldDecertifyIrregularExpenses;
            this.DecertificationComments = prevVersion.DecertificationComments;
            this.AdminProcedures = prevVersion.AdminProcedures;
            this.PenaltyProcedures = prevVersion.PenaltyProcedures;
            this.ShouldReportToOlaf = prevVersion.ShouldReportToOlaf;
            this.ReasonNotReportingToOlaf = prevVersion.ReasonNotReportingToOlaf;

            this.ImpairedRegulation = new IrregularityImpairedRegulation
            {
                ImpairedRegulationAct = prevVersion.ImpairedRegulation.ImpairedRegulationAct,
                ImpairedRegulationNum = prevVersion.ImpairedRegulation.ImpairedRegulationNum,
                ImpairedRegulationYear = prevVersion.ImpairedRegulation.ImpairedRegulationYear,
                ImpairedRegulation = prevVersion.ImpairedRegulation.ImpairedRegulation,
                ImpairedNationalRegulation = prevVersion.ImpairedRegulation.ImpairedNationalRegulation,
            };

            this.ExpensesLv = new ExpensesAmount
            {
                BfpEuAmount = prevVersion.ExpensesLv.BfpEuAmount,
                BfpBgAmount = prevVersion.ExpensesLv.BfpBgAmount,
                BfpTotalAmount = prevVersion.ExpensesLv.BfpTotalAmount,
                SelfAmount = prevVersion.ExpensesLv.SelfAmount,
                TotalAmount = prevVersion.ExpensesLv.TotalAmount,
            };

            this.ExpensesEuro = new ExpensesAmount
            {
                BfpEuAmount = prevVersion.ExpensesEuro.BfpEuAmount,
                BfpBgAmount = prevVersion.ExpensesEuro.BfpBgAmount,
                BfpTotalAmount = prevVersion.ExpensesEuro.BfpTotalAmount,
                SelfAmount = prevVersion.ExpensesEuro.SelfAmount,
                TotalAmount = prevVersion.ExpensesEuro.TotalAmount,
            };

            this.IrregularExpensesLv = new BfpAmount
            {
                EuAmount = prevVersion.IrregularExpensesLv.EuAmount,
                BgAmount = prevVersion.IrregularExpensesLv.BgAmount,
                TotalAmount = prevVersion.IrregularExpensesLv.TotalAmount,
            };

            this.IrregularExpensesEuro = new BfpAmount
            {
                EuAmount = prevVersion.IrregularExpensesEuro.EuAmount,
                BgAmount = prevVersion.IrregularExpensesEuro.BgAmount,
                TotalAmount = prevVersion.IrregularExpensesEuro.TotalAmount,
            };

            this.CertifiedExpensesLv = new BfpAmount
            {
                EuAmount = prevVersion.CertifiedExpensesLv.EuAmount,
                BgAmount = prevVersion.CertifiedExpensesLv.BgAmount,
                TotalAmount = prevVersion.CertifiedExpensesLv.TotalAmount,
            };

            this.CertifiedExpensesEuro = new BfpAmount
            {
                EuAmount = prevVersion.CertifiedExpensesEuro.EuAmount,
                BgAmount = prevVersion.CertifiedExpensesEuro.BgAmount,
                TotalAmount = prevVersion.CertifiedExpensesEuro.TotalAmount,
            };

            this.PaidLv = new BfpAmount
            {
                EuAmount = prevVersion.PaidLv.EuAmount,
                BgAmount = prevVersion.PaidLv.BgAmount,
                TotalAmount = prevVersion.PaidLv.TotalAmount,
            };

            this.PaidEuro = new BfpAmount
            {
                EuAmount = prevVersion.PaidEuro.EuAmount,
                BgAmount = prevVersion.PaidEuro.BgAmount,
                TotalAmount = prevVersion.PaidEuro.TotalAmount,
            };

            this.Sanction = new IrregularitySanction
            {
                ProcedureType = prevVersion.Sanction.ProcedureType,
                ProcedureKind = prevVersion.Sanction.ProcedureKind,
                ProcedureStartDate = prevVersion.Sanction.ProcedureStartDate,
                ProcedureExpectedEndDate = prevVersion.Sanction.ProcedureExpectedEndDate,
                ProcedureEndDate = prevVersion.Sanction.ProcedureEndDate,
                ProcedureStatus = prevVersion.Sanction.ProcedureStatus,
                SanctionCategoryId = prevVersion.Sanction.SanctionCategoryId,
                SanctionTypeId = prevVersion.Sanction.SanctionTypeId,
                Fines = prevVersion.Sanction.Fines,
            };

            foreach (var document in prevVersion.Documents)
            {
                this.AddDocument(document.Description, document.FileName, document.FileKey);
            }

            foreach (var involvedPerson in prevVersion.InvolvedPersons)
            {
                switch (involvedPerson.LegalType)
                {
                    case InvolvedPersonLegalType.Person:
                        this.AddInvolvedPerson(
                            involvedPerson.Uin,
                            involvedPerson.UinType,
                            involvedPerson.UndisclosureMotives,
                            involvedPerson.FirstName,
                            involvedPerson.MiddleName,
                            involvedPerson.LastName,
                            involvedPerson.CountryId,
                            involvedPerson.SettlementId,
                            involvedPerson.PostCode,
                            involvedPerson.Street,
                            involvedPerson.Address);
                        break;
                    case InvolvedPersonLegalType.LegalPerson:
                        this.AddInvolvedLegalPerson(
                            involvedPerson.Uin,
                            involvedPerson.UinType,
                            involvedPerson.UndisclosureMotives,
                            involvedPerson.CompanyName,
                            involvedPerson.TradeName,
                            involvedPerson.HoldingName,
                            involvedPerson.CountryId,
                            involvedPerson.SettlementId,
                            involvedPerson.PostCode,
                            involvedPerson.Street,
                            involvedPerson.Address);
                        break;
                }
            }

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
