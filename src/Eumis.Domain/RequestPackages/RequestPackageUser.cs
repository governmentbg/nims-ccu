using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.RequestPackages
{
    public class RequestPackageUser
    {
        public int RequestPackageId { get; set; }

        public int UserId { get; set; }

        public RequestStatus Status { get; set; }

        public string RejectionMessage { get; set; }

        public virtual RequestPackage RequestPackage { get; set; }

        public virtual PermissionRequest PermissionRequest { get; set; }

        public virtual RegDataRequest RegDataRequest { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class RequestPackageUserMap : EntityTypeConfiguration<RequestPackageUser>
    {
        public RequestPackageUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RequestPackageId, t.UserId });

            // Properties
            this.Property(t => t.RequestPackageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RequestPackageUsers");
            this.Property(t => t.RequestPackageId).HasColumnName("RequestPackageId");
            this.Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            this.HasRequired(t => t.RequestPackage)
                .WithMany(t => t.RequestPackageUsers)
                .HasForeignKey(d => d.RequestPackageId);

            this.HasOptional(t => t.PermissionRequest)
                .WithRequired(t => t.RequestPackageUser);

            this.HasOptional(t => t.RegDataRequest)
                .WithRequired(t => t.RequestPackageUser);
        }
    }
}
