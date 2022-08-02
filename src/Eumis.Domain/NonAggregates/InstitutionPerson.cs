using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class InstitutionPerson
    {
        public InstitutionPerson()
        {
        }

        public int InstitutionPersonId { get; set; }

        public int InstitutionId { get; set; }

        public int ParentInstitutionPersonId { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public bool IsContact { get; set; }

        public virtual Institution Institution { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class InstitutionPersonMap : EntityTypeConfiguration<InstitutionPerson>
    {
        public InstitutionPersonMap()
        {
            // Primary Key
            this.HasKey(t => t.InstitutionPersonId);

            // Properties
            this.Property(t => t.InstitutionPersonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.InstitutionId)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Position)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Phone)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Fax)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Email)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.IsContact)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("InstitutionPersons");
            this.Property(t => t.InstitutionPersonId).HasColumnName("InstitutionPersonId");
            this.Property(t => t.InstitutionId).HasColumnName("InstitutionId");
            this.Property(t => t.ParentInstitutionPersonId).HasColumnName("ParentInstitutionPersonId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IsContact).HasColumnName("IsContact");

            this.HasRequired(t => t.Institution)
                .WithMany()
                .HasForeignKey(t => t.InstitutionId);
        }
    }
}
