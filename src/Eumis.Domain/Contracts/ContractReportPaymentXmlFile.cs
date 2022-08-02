using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public class ContractReportPaymentXmlFile : RioXmlFile
    {
        private ContractReportPaymentXmlFile()
        {
        }

        public ContractReportPaymentXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ContractReportPaymentId { get; set; }

        public ContractReportPayment PaymentReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportPaymentXmlFileMap : EntityTypeConfiguration<ContractReportPaymentXmlFile>
    {
        public ContractReportPaymentXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportPaymentId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportPaymentXmlFiles");
            this.Property(t => t.FileId).HasColumnName("ContractReportPaymentXmlFileId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.PaymentReport)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ContractReportPaymentId)
                .WillCascadeOnDelete();
        }
    }
}
