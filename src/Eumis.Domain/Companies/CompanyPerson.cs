using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Companies
{
    public class CompanyPerson
    {
        public CompanyPerson()
        {
        }

        public int CompanyPersonId { get; set; }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public bool IsContact { get; set; }

        public virtual Company Company { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CompanyPersonMap : EntityTypeConfiguration<CompanyPerson>
    {
        public CompanyPersonMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyPersonId);

            // Properties
            this.Property(t => t.CompanyPersonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CompanyId)
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
            this.ToTable("CompanyPersons");
            this.Property(t => t.CompanyPersonId).HasColumnName("CompanyPersonId");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IsContact).HasColumnName("IsContact");

            this.HasRequired(t => t.Company)
                .WithMany()
                .HasForeignKey(t => t.CompanyId);
        }
    }
}
