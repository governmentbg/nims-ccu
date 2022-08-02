using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Companies
{
    public class LocalActionGroupMunicipality
    {
        public LocalActionGroupMunicipality()
        {
        }

        public LocalActionGroupMunicipality(int municipalityId)
        {
            this.MunicipalityId = municipalityId;
        }

        public int LocalActionGroupMunicipalityId { get; set; }

        public int CompanyId { get; set; }

        public int MunicipalityId { get; set; }

        public virtual Company Company { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class LocalActionGroupMunicipalityMap : EntityTypeConfiguration<LocalActionGroupMunicipality>
    {
        public LocalActionGroupMunicipalityMap()
        {
            // Primary Key
            this.HasKey(t => t.LocalActionGroupMunicipalityId);

            // Properties
            this.Property(t => t.LocalActionGroupMunicipalityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CompanyId)
                .IsRequired();

            this.Property(t => t.MunicipalityId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("LocalActionGroupMunicipalities");
            this.Property(t => t.LocalActionGroupMunicipalityId).HasColumnName("LocalActionGroupMunicipalityId");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");

            this.HasRequired(t => t.Company)
                .WithMany(t => t.LocalActionGroupMunicipalities)
                .HasForeignKey(t => t.CompanyId)
                .WillCascadeOnDelete();
        }
    }
}
