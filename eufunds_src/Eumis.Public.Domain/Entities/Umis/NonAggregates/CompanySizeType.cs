using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class CompanySizeType
    {
        public CompanySizeType()
        {
        }

        public int CompanySizeTypeId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Alias { get; set; }

        public decimal Order { get; set; }
    }

    public class CompanySizeTypeMap : EntityTypeConfiguration<CompanySizeType>
    {
        public CompanySizeTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanySizeTypeId);

            // Properties
            this.Property(t => t.CompanySizeTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Alias)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("CompanySizeTypes");
            this.Property(t => t.CompanySizeTypeId).HasColumnName("CompanySizeTypeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Order).HasColumnName("Order");
        }
    }
}
