using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public class ContractReportFinancialFile : RioXmlFile
    {
        private ContractReportFinancialFile()
        {
        }

        public ContractReportFinancialFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ContractReportFinancialId { get; set; }

        public ContractReportFinancial FinReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportFinancialFileMap : EntityTypeConfiguration<ContractReportFinancialFile>
    {
        public ContractReportFinancialFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportFinancialId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportFinancialXmlFiles");
            this.Property(t => t.FileId).HasColumnName("ContractReportFinancialXmlFileId");
            this.Property(t => t.ContractReportFinancialId).HasColumnName("ContractReportFinancialId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.FinReport)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ContractReportFinancialId)
                .WillCascadeOnDelete();
        }
    }
}
