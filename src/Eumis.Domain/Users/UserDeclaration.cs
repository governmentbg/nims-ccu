using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users
{
    public class UserDeclaration
    {
        public int UserId { get; set; }

        public int DeclarationId { get; set; }

        public User User { get; set; }

        public DateTime AcceptDate { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class UserDeclarationMap : EntityTypeConfiguration<UserDeclaration>
    {
        public UserDeclarationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserId, t.DeclarationId });

            // Properties
            this.Property(t => t.AcceptDate)
            .IsRequired();

            // Table & Column Mappings
            this.ToTable("UserDeclarations");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.DeclarationId).HasColumnName("DeclarationId");
            this.Property(t => t.AcceptDate).HasColumnName("AcceptDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserDeclarations)
                .HasForeignKey(t => t.UserId);
        }
    }
}
