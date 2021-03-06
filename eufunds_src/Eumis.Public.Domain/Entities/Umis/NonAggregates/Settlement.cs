using Eumis.Public.Common.Localization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class Settlement
    {
        public Settlement()
        {
        }

        public int SettlementId { get; set; }

        public int MunicipalityId { get; set; }

        public string LauCode { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string FullPathName { get; set; }

        public string NameAlt { get; set; }

        public string FullPathNameAlt { get; set; }

        public string FullPath { get; set; }

        public decimal Order { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual string TransName
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

    public class SettlementMap : EntityTypeConfiguration<Settlement>
    {
        public SettlementMap()
        {
            // Primary Key
            this.HasKey(t => t.SettlementId);

            // Properties
            this.Property(t => t.SettlementId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.LauCode)
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.DisplayName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FullPathName)
                .HasMaxLength(1000)
                .IsRequired();

            this.Property(t => t.NameAlt)
                .HasMaxLength(200);

            this.Property(t => t.FullPathNameAlt)
                .HasMaxLength(1000);

            this.Property(t => t.FullPath)
                .HasMaxLength(500)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Settlements");
            this.Property(t => t.SettlementId).HasColumnName("SettlementId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.LauCode).HasColumnName("LauCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.FullPathNameAlt).HasColumnName("FullPathNameAlt");
            this.Property(t => t.FullPath).HasColumnName("FullPath");
            this.Property(t => t.Order).HasColumnName("Order");

            this.HasRequired(t => t.Municipality)
                .WithMany()
                .HasForeignKey(t => t.MunicipalityId);
        }
    }
}
