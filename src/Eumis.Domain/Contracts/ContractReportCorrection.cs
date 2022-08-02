using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCorrection : IAggregateRoot
    {
        public ContractReportCorrection()
        {
            this.Documents = new List<ContractReportCorrectionDocument>();
        }

        public ContractReportCorrection(
            ContractReportCorrectionType type,
            Sign sign,
            DateTime date,
            int programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            int? contractReportPaymentId)
        {
            this.Type = type;
            this.Sign = sign;
            this.Date = date;
            this.ProgrammeId = programmeId;
            this.ProgrammePriorityId = programmePriorityId;

            switch (type)
            {
                case ContractReportCorrectionType.PaymentVerified:
                case ContractReportCorrectionType.AdvanceCovered:
                    if (!procedureId.HasValue || !contractId.HasValue || !contractReportPaymentId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId, ContractId and ContractReportPaymentId are required for ContractReportCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;
                    this.ContractId = contractId;
                    this.ContractReportPaymentId = contractReportPaymentId;

                    break;
                case ContractReportCorrectionType.ContractVerified:
                    if (!procedureId.HasValue || !contractId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId, ContractId are required for ContractReportCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;
                    this.ContractId = contractId;

                    break;
                case ContractReportCorrectionType.ProgramePriorityVerified:
                    if (!programmePriorityId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProgrammePriorityId is required for ContractReportCorrection with type {0}", type));
                    }

                    this.ProgrammePriorityId = programmePriorityId;

                    break;
                case ContractReportCorrectionType.ProcedureVerified:
                    if (!procedureId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId is required for ContractReportCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;

                    break;
            }

            this.Status = ContractReportCorrectionStatus.Draft;
            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int ContractReportCorrectionId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public ContractReportCorrectionType Type { get; set; }

        public ContractReportCorrectionStatus Status { get; set; }

        public string RegNumber { get; set; }

        public Sign Sign { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public CorrectionTypeExtended? CorrectionType { get; set; }

        public int? FinancialCorrectionId { get; set; }

        public int? IrregularityId { get; set; }

        public int? FlatFinancialCorrectionId { get; set; }

        public decimal? CorrectedApprovedEuAmount { get; set; }

        public decimal? CorrectedApprovedBgAmount { get; set; }

        public decimal? CorrectedApprovedBfpTotalAmount { get; set; }

        public decimal? CorrectedApprovedCrossAmount { get; set; }

        public decimal? CorrectedApprovedSelfAmount { get; set; }

        public decimal? CorrectedApprovedTotalAmount { get; set; }

        public int? CertReportId { get; set; }

        public ContractReportCorrectionCertStatus? CertStatus { get; set; }

        public int? CertCheckedByUserId { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        public decimal? UncertifiedCorrectedApprovedEuAmount { get; set; }

        public decimal? UncertifiedCorrectedApprovedBgAmount { get; set; }

        public decimal? UncertifiedCorrectedApprovedBfpTotalAmount { get; set; }

        public decimal? UncertifiedCorrectedApprovedCrossAmount { get; set; }

        public decimal? UncertifiedCorrectedApprovedSelfAmount { get; set; }

        public decimal? UncertifiedCorrectedApprovedTotalAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedEuAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedBgAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedBfpTotalAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedCrossAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedSelfAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedTotalAmount { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<ContractReportCorrectionDocument> Documents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportCorrectionMap : EntityTypeConfiguration<ContractReportCorrection>
    {
        public ContractReportCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportCorrectionId);

            // Properties
            this.Property(t => t.ContractReportCorrectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.Sign)
                .IsRequired();
            this.Property(t => t.Date)
                .IsRequired();
            this.Property(t => t.IsActivated)
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
            this.ToTable("ContractReportCorrections");
            this.Property(t => t.ContractReportCorrectionId).HasColumnName("ContractReportCorrectionId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.Sign).HasColumnName("Sign");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.CorrectionType).HasColumnName("CorrectionType");
            this.Property(t => t.FinancialCorrectionId).HasColumnName("FinancialCorrectionId");
            this.Property(t => t.IrregularityId).HasColumnName("IrregularityId");
            this.Property(t => t.FlatFinancialCorrectionId).HasColumnName("FlatFinancialCorrectionId");

            this.Property(t => t.CorrectedApprovedEuAmount).HasColumnName("CorrectedApprovedEuAmount");
            this.Property(t => t.CorrectedApprovedBgAmount).HasColumnName("CorrectedApprovedBgAmount");
            this.Property(t => t.CorrectedApprovedBfpTotalAmount).HasColumnName("CorrectedApprovedBfpTotalAmount");
            this.Property(t => t.CorrectedApprovedCrossAmount).HasColumnName("CorrectedApprovedCrossAmount");
            this.Property(t => t.CorrectedApprovedSelfAmount).HasColumnName("CorrectedApprovedSelfAmount");
            this.Property(t => t.CorrectedApprovedTotalAmount).HasColumnName("CorrectedApprovedTotalAmount");

            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.Property(t => t.CertStatus).HasColumnName("CertStatus");
            this.Property(t => t.CertCheckedByUserId).HasColumnName("CertCheckedByUserId");
            this.Property(t => t.CertCheckedDate).HasColumnName("CertCheckedDate");
            this.Property(t => t.UncertifiedCorrectedApprovedEuAmount).HasColumnName("UncertifiedCorrectedApprovedEuAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedBgAmount).HasColumnName("UncertifiedCorrectedApprovedBgAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedBfpTotalAmount).HasColumnName("UncertifiedCorrectedApprovedBfpTotalAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedCrossAmount).HasColumnName("UncertifiedCorrectedApprovedCrossAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedSelfAmount).HasColumnName("UncertifiedCorrectedApprovedSelfAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedTotalAmount).HasColumnName("UncertifiedCorrectedApprovedTotalAmount");

            this.Property(t => t.CertifiedCorrectedApprovedEuAmount).HasColumnName("CertifiedCorrectedApprovedEuAmount");
            this.Property(t => t.CertifiedCorrectedApprovedBgAmount).HasColumnName("CertifiedCorrectedApprovedBgAmount");
            this.Property(t => t.CertifiedCorrectedApprovedBfpTotalAmount).HasColumnName("CertifiedCorrectedApprovedBfpTotalAmount");
            this.Property(t => t.CertifiedCorrectedApprovedCrossAmount).HasColumnName("CertifiedCorrectedApprovedCrossAmount");
            this.Property(t => t.CertifiedCorrectedApprovedSelfAmount).HasColumnName("CertifiedCorrectedApprovedSelfAmount");
            this.Property(t => t.CertifiedCorrectedApprovedTotalAmount).HasColumnName("CertifiedCorrectedApprovedTotalAmount");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}