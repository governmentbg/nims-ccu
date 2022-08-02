using Eumis.Public.Common.Localization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class InterventionCategory
    {
        public InterventionCategory()
        {
        }

        public int InterventionCategoryId { get; set; }

        public Dimension Dimension { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public virtual string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.Format("[{0}] {1}", (int)this.Dimension, this.NameAlt);
                }
                else
                {
                    return string.Format("[{0}] {1}", (int)this.Dimension, this.Name);
                }
            }
        }
    }

    public class InterventionCategoryMap : EntityTypeConfiguration<InterventionCategory>
    {
        public InterventionCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.InterventionCategoryId);

            // Properties
            this.Property(t => t.InterventionCategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Dimension)
                .IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.NameAlt)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("InterventionCategories");
            this.Property(t => t.InterventionCategoryId).HasColumnName("InterventionCategoryId");
            this.Property(t => t.Dimension).HasColumnName("Dimension");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
        }
    }
}
