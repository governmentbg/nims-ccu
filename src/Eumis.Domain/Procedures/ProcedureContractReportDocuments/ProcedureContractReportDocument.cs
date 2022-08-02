using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures.ProcedureContractReportDocuments
{
    public abstract class ProcedureContractReportDocument
    {
        public static readonly Dictionary<ProcedureContractReportDocumentType, Type> ProcedureReportDocuments = new Dictionary<ProcedureContractReportDocumentType, Type>()
        {
            { ProcedureContractReportDocumentType.AdvancePaymentDocument,  typeof(ProcedureAdvancePaymentDocument) },
            { ProcedureContractReportDocumentType.FinalPaymentDocument,  typeof(ProcedureFinalPaymentDocument) },
            { ProcedureContractReportDocumentType.FinancialReportDocument,  typeof(ProcedureFinancialReportDocument) },
            { ProcedureContractReportDocumentType.IntermediatePaymentDocument,  typeof(ProcedureIntermediatePaymentDocument) },
            { ProcedureContractReportDocumentType.ProcurementDocument,  typeof(ProcedureProcurementDocument) },
            { ProcedureContractReportDocumentType.TechnicalReportDocument,  typeof(ProcedureTechnicalReportDocument) },
        };

        public ProcedureContractReportDocument()
        {
        }

        public ProcedureContractReportDocument(
            string name,
            string extension,
            bool isRequired)
        {
            this.Gid = Guid.NewGuid();
            this.Name = name;
            this.Extension = extension;
            this.IsRequired = isRequired;

            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureContractReportDocumentId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public abstract ProcedureContractReportDocumentType Type { get; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public virtual Procedure Procedure { get; set; }

        public void SetAttributes(
            string name,
            string extension,
            bool isRequired)
        {
            this.Name = name;
            this.Extension = extension;
            this.IsRequired = isRequired;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureContractReportDocumentMap : EntityTypeConfiguration<ProcedureContractReportDocument>
    {
        public ProcedureContractReportDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureContractReportDocumentId);

            this.Property(t => t.ProcedureContractReportDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Ignore(t => t.Type);

            this.Property(t => t.Extension)
                .IsOptional()
                .HasMaxLength(50);

            this.Property(t => t.IsRequired)
                .IsRequired();

            this.Property(t => t.IsActivated)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureContractReportDocuments");
            this.Property(t => t.ProcedureContractReportDocumentId).HasColumnName("ProcedureContractReportDocumentId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Extension).HasColumnName("Extension");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureContractReportDocuments)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();

            this.Map<ProcedureTechnicalReportDocument>(t => t.Requires("Type").HasValue<int>((int)ProcedureContractReportDocumentType.TechnicalReportDocument));
            this.Map<ProcedureFinancialReportDocument>(t => t.Requires("Type").HasValue<int>((int)ProcedureContractReportDocumentType.FinancialReportDocument));
            this.Map<ProcedureAdvancePaymentDocument>(t => t.Requires("Type").HasValue<int>((int)ProcedureContractReportDocumentType.AdvancePaymentDocument));
            this.Map<ProcedureIntermediatePaymentDocument>(t => t.Requires("Type").HasValue<int>((int)ProcedureContractReportDocumentType.IntermediatePaymentDocument));
            this.Map<ProcedureFinalPaymentDocument>(t => t.Requires("Type").HasValue<int>((int)ProcedureContractReportDocumentType.FinalPaymentDocument));
            this.Map<ProcedureProcurementDocument>(t => t.Requires("Type").HasValue<int>((int)ProcedureContractReportDocumentType.ProcurementDocument));
        }
    }
}
