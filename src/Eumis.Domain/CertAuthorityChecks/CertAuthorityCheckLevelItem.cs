using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.CertAuthorityChecks
{
    public partial class CertAuthorityCheckLevelItem
    {
        public int CertAuthorityCheckLevelItemId { get; set; }

        public int CertAuthorityCheckId { get; set; }

        public CertAuthorityCheckLevel Level { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public virtual CertAuthorityCheck Check { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CertAuthorityCheckLevelItemMap : EntityTypeConfiguration<CertAuthorityCheckLevelItem>
    {
        public CertAuthorityCheckLevelItemMap()
        {
            // Primary Key
            this.HasKey(t => t.CertAuthorityCheckLevelItemId);

            // Properties
            this.Property(t => t.CertAuthorityCheckLevelItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CertAuthorityCheckId)
                .IsRequired();
            this.Property(t => t.Level)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CertAuthorityCheckLevelItems");
            this.Property(t => t.CertAuthorityCheckLevelItemId).HasColumnName("CertAuthorityCheckLevelItemId");
            this.Property(t => t.CertAuthorityCheckId).HasColumnName("CertAuthorityCheckId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");

            this.HasRequired(t => t.Check)
                .WithMany(t => t.LevelItems)
                .HasForeignKey(t => t.CertAuthorityCheckId)
                .WillCascadeOnDelete();
        }
    }
}
