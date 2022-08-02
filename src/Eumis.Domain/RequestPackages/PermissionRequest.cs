using Eumis.Domain.Users.PermissionAggregations;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.RequestPackages
{
    public class PermissionRequest
    {
        private PermissionAggregation permissions = null;

        private PermissionAggregation permissionTemplate = null;

        public int RequestPackageId { get; set; }

        public int UserId { get; set; }

        public string PermissionsString { get; set; }

        public string PermissionTemplateString { get; set; }

        public virtual RequestPackageUser RequestPackageUser { get; set; }

        public PermissionAggregation GetPermissions(int[] porgrammeIds)
        {
            if (this.permissions == null)
            {
                this.permissions = new PermissionAggregation(porgrammeIds, this.PermissionsString);
            }

            return this.permissions;
        }

        public void SetPermissions(PermissionAggregation permissions)
        {
            this.permissions = permissions;
            this.PermissionsString = this.permissions.ToPermissionsString();
        }

        public PermissionAggregation GetPermissionTemplate(int[] porgrammeIds)
        {
            if (this.permissionTemplate == null)
            {
                this.permissionTemplate = new PermissionAggregation(porgrammeIds, this.PermissionTemplateString);
            }

            return this.permissionTemplate;
        }

        public void SetPermissionTemplate(PermissionAggregation permissionTemplate)
        {
            this.permissionTemplate = permissionTemplate;
            this.PermissionTemplateString = this.permissionTemplate.ToPermissionsString();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class PermissionRequestMap : EntityTypeConfiguration<PermissionRequest>
    {
        public PermissionRequestMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RequestPackageId, t.UserId });

            // Properties
            this.Property(t => t.RequestPackageId)
                .IsRequired();

            this.Property(t => t.UserId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("PermissionRequests");
            this.Property(t => t.RequestPackageId).HasColumnName("RequestPackageId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.PermissionsString).HasColumnName("PermissionsString");
            this.Property(t => t.PermissionTemplateString).HasColumnName("PermissionTemplate");
        }
    }
}
