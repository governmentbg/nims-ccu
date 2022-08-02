using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCertAuthorityCorrection : IAggregateRoot
    {
        public ContractReportCertAuthorityCorrection()
        {
            this.Documents = new List<ContractReportCertAuthorityCorrectionDocument>();
        }

        public ContractReportCertAuthorityCorrection(
            ContractReportCertAuthorityCorrectionType type,
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
                case ContractReportCertAuthorityCorrectionType.PaymentCertified:
                    if (!procedureId.HasValue || !contractId.HasValue || !contractReportPaymentId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId, ContractId and ContractReportPaymentId are required for ContractReportCertAuthorityCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;
                    this.ContractId = contractId;
                    this.ContractReportPaymentId = contractReportPaymentId;

                    break;
                case ContractReportCertAuthorityCorrectionType.ContractCertified:
                    if (!procedureId.HasValue || !contractId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId, ContractId are required for ContractReportCertAuthorityCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;
                    this.ContractId = contractId;

                    break;
                case ContractReportCertAuthorityCorrectionType.ProgramePriorityCertified:
                    if (!programmePriorityId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProgrammePriorityId is required for ContractReportCertAuthorityCorrection with type {0}", type));
                    }

                    this.ProgrammePriorityId = programmePriorityId;

                    break;
                case ContractReportCertAuthorityCorrectionType.ProcedureCertified:
                    if (!procedureId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId is required for ContractReportCertAuthorityCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;

                    break;
            }

            this.Status = ContractReportCertAuthorityCorrectionStatus.Draft;
            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int ContractReportCertAuthorityCorrectionId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public ContractReportCertAuthorityCorrectionType Type { get; set; }

        public ContractReportCertAuthorityCorrectionStatus Status { get; set; }

        public string RegNumber { get; set; }

        public Sign Sign { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public decimal? CertifiedEuAmount { get; set; }

        public decimal? CertifiedBgAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedCrossAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<ContractReportCertAuthorityCorrectionDocument> Documents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportCertAuthorityCorrectionMap : EntityTypeConfiguration<ContractReportCertAuthorityCorrection>
    {
        public ContractReportCertAuthorityCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportCertAuthorityCorrectionId);

            // Properties
            this.Property(t => t.ContractReportCertAuthorityCorrectionId)
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
            this.ToTable("ContractReportCertAuthorityCorrections");
            this.Property(t => t.ContractReportCertAuthorityCorrectionId).HasColumnName("ContractReportCertAuthorityCorrectionId");
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

            this.Property(t => t.CertifiedEuAmount).HasColumnName("CertifiedEuAmount");
            this.Property(t => t.CertifiedBgAmount).HasColumnName("CertifiedBgAmount");
            this.Property(t => t.CertifiedBfpTotalAmount).HasColumnName("CertifiedBfpTotalAmount");
            this.Property(t => t.CertifiedCrossAmount).HasColumnName("CertifiedCrossAmount");
            this.Property(t => t.CertifiedSelfAmount).HasColumnName("CertifiedSelfAmount");
            this.Property(t => t.CertifiedTotalAmount).HasColumnName("CertifiedTotalAmount");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
