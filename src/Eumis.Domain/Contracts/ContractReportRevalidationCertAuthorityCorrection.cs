using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportRevalidationCertAuthorityCorrection : IAggregateRoot
    {
        public ContractReportRevalidationCertAuthorityCorrection()
        {
            this.Documents = new List<ContractReportRevalidationCertAuthorityCorrectionDocument>();
        }

        public ContractReportRevalidationCertAuthorityCorrection(
            ContractReportRevalidationCertAuthorityCorrectionType type,
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
                case ContractReportRevalidationCertAuthorityCorrectionType.PaymentRevalidated:
                    if (!procedureId.HasValue || !contractId.HasValue || !contractReportPaymentId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId, ContractId and ContractReportPaymentId are required for ContractReportRevalidationCertAuthorityCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;
                    this.ContractId = contractId;
                    this.ContractReportPaymentId = contractReportPaymentId;
                    break;

                case ContractReportRevalidationCertAuthorityCorrectionType.ContractRevalidated:
                    if (!procedureId.HasValue || !contractId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId, ContractId are required for ContractReportRevalidationCertAuthorityCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;
                    this.ContractId = contractId;
                    break;

                case ContractReportRevalidationCertAuthorityCorrectionType.ProgrammePriorityRevalidated:
                    if (!programmePriorityId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProgrammePriorityId is required for ContractReportRevalidationCertAuthorityCorrection with type {0}", type));
                    }

                    this.ProgrammePriorityId = programmePriorityId;
                    break;

                case ContractReportRevalidationCertAuthorityCorrectionType.ProcedureRevalidated:
                    if (!procedureId.HasValue)
                    {
                        throw new DomainValidationException(string.Format("ProcedureId is required for ContractReportRevalidationCertAuthorityCorrection with type {0}", type));
                    }

                    this.ProcedureId = procedureId;
                    break;
            }

            this.Status = ContractReportRevalidationCertAuthorityCorrectionStatus.Draft;
            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int ContractReportRevalidationCertAuthorityCorrectionId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public ContractReportRevalidationCertAuthorityCorrectionType Type { get; set; }

        public ContractReportRevalidationCertAuthorityCorrectionStatus Status { get; set; }

        public string RegNumber { get; set; }

        public Sign Sign { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public decimal? CertifiedRevalidatedEuAmount { get; set; }

        public decimal? CertifiedRevalidatedBgAmount { get; set; }

        public decimal? CertifiedRevalidatedBfpTotalAmount { get; set; }

        public decimal? CertifiedRevalidatedCrossAmount { get; set; }

        public decimal? CertifiedRevalidatedSelfAmount { get; set; }

        public decimal? CertifiedRevalidatedTotalAmount { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<ContractReportRevalidationCertAuthorityCorrectionDocument> Documents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportRevalidationCertAuthorityCorrectionMap : EntityTypeConfiguration<ContractReportRevalidationCertAuthorityCorrection>
    {
        public ContractReportRevalidationCertAuthorityCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportRevalidationCertAuthorityCorrectionId);

            // Properties
            this.Property(t => t.ContractReportRevalidationCertAuthorityCorrectionId)
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
            this.ToTable("ContractReportRevalidationCertAuthorityCorrections");
            this.Property(t => t.ContractReportRevalidationCertAuthorityCorrectionId).HasColumnName("ContractReportRevalidationCertAuthorityCorrectionId");
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

            this.Property(t => t.CertifiedRevalidatedEuAmount).HasColumnName("CertifiedRevalidatedEuAmount");
            this.Property(t => t.CertifiedRevalidatedBgAmount).HasColumnName("CertifiedRevalidatedBgAmount");
            this.Property(t => t.CertifiedRevalidatedBfpTotalAmount).HasColumnName("CertifiedRevalidatedBfpTotalAmount");
            this.Property(t => t.CertifiedRevalidatedCrossAmount).HasColumnName("CertifiedRevalidatedCrossAmount");
            this.Property(t => t.CertifiedRevalidatedSelfAmount).HasColumnName("CertifiedRevalidatedSelfAmount");
            this.Property(t => t.CertifiedRevalidatedTotalAmount).HasColumnName("CertifiedRevalidatedTotalAmount");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
