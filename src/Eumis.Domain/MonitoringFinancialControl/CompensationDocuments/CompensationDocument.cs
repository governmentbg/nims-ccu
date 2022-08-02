using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.MonitoringFinancialControl.CompensationDocuments
{
    public partial class CompensationDocument : IAggregateRoot
    {
        public CompensationDocument()
        {
            this.Documents = new List<CompensationDocumentDoc>();
        }

        public CompensationDocument(
            CompensationDocumentType type,
            CompensationSign compensationSign,
            DateTime compensationDocDate,
            int programmeId,
            int procedureId,
            int programmePriorityId,
            int contractId,
            int? contractReportPaymentId)
        {
            this.Type = type;
            this.CompensationSign = compensationSign;
            this.CompensationDocDate = compensationDocDate;
            this.ProgrammeId = programmeId;
            this.ProcedureId = procedureId;
            this.ProgrammePriorityId = programmePriorityId;
            this.ContractId = contractId;

            if (type == CompensationDocumentType.ActuallyPaidAmount || type == CompensationDocumentType.Requested)
            {
                this.ContractReportPaymentId = contractReportPaymentId;
            }

            this.Status = CompensationDocumentStatus.Draft;
            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int CompensationDocumentId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public int ContractId { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public CompensationDocumentType Type { get; set; }

        public CompensationDocumentStatus Status { get; set; }

        public string RegNumber { get; set; }

        public CompensationSign CompensationSign { get; set; }

        public DateTime CompensationDocDate { get; set; }

        public string Description { get; set; }

        public string CompensationReason { get; set; }

        public decimal? BfpEuAmount { get; set; }

        public decimal? BfpBgAmount { get; set; }

        public decimal? BfpCrossAmount { get; set; }

        public decimal? BfpTotalAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<CompensationDocumentDoc> Documents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CompensationDocumentMap : EntityTypeConfiguration<CompensationDocument>
    {
        public CompensationDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.CompensationDocumentId);

            // Properties
            this.Property(t => t.CompensationDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.ProcedureId)
                .IsRequired();
            this.Property(t => t.ProgrammePriorityId)
                .IsRequired();
            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.CompensationSign)
                .IsRequired();
            this.Property(t => t.CompensationDocDate)
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
            this.ToTable("CompensationDocuments");
            this.Property(t => t.CompensationDocumentId).HasColumnName("CompensationDocumentId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.CompensationSign).HasColumnName("CompensationSign");
            this.Property(t => t.CompensationDocDate).HasColumnName("CompensationDocDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CompensationReason).HasColumnName("CompensationReason");

            this.Property(t => t.BfpEuAmount).HasColumnName("BfpEuAmount");
            this.Property(t => t.BfpBgAmount).HasColumnName("BfpBgAmount");
            this.Property(t => t.BfpCrossAmount).HasColumnName("BfpCrossAmount");
            this.Property(t => t.BfpTotalAmount).HasColumnName("BfpTotalAmount");
            this.Property(t => t.SelfAmount).HasColumnName("SelfAmount");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
