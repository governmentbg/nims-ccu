using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class CompanyType
    {
        public CompanyType()
        {
        }

        public int CompanyTypeId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Alias { get; set; }

        public decimal Order { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CompanyTypeMap : EntityTypeConfiguration<CompanyType>
    {
        public CompanyTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyTypeId);

            // Properties
            this.Property(t => t.CompanyTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.NameAlt)
                .IsOptional();

            this.Property(t => t.Alias)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("CompanyTypes");
            this.Property(t => t.CompanyTypeId).HasColumnName("CompanyTypeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Order).HasColumnName("Order");
        }
    }
}
