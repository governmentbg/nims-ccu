using Eumis.Public.Common.Localization;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class CompanyLegalType
    {
        public CompanyLegalType()
        {
        }

        public int CompanyLegalTypeId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string NameAlt { get; set; }

        public decimal Order { get; set; }

        public int CompanyTypeId { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NameAlt;
                }
                else
                {
                    return this.Name;
                }
            }
        }
    }

    public class CompanyLegalTypeMap : EntityTypeConfiguration<CompanyLegalType>
    {
        public CompanyLegalTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyLegalTypeId);

            // Properties
            this.Property(t => t.CompanyLegalTypeId)
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

            this.Property(t => t.CompanyTypeId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CompanyLegalTypes");
            this.Property(t => t.CompanyLegalTypeId).HasColumnName("CompanyLegalTypeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Order).HasColumnName("Order");
            this.Property(t => t.CompanyTypeId).HasColumnName("CompanyTypeId");
        }
    }
}
