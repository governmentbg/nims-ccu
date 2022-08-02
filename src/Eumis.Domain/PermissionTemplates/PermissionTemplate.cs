using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Users.PermissionAggregations;

namespace Eumis.Domain.PermissionTemplates
{
    public partial class PermissionTemplate : IAggregateRoot
    {
        private PermissionAggregation permissions = null;

        private PermissionTemplate()
        {
        }

        public PermissionTemplate(string name, PermissionAggregation permissions)
        {
            var currentDate = DateTime.Now;

            this.Name = name;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            this.SetPermissions(permissions);
        }

        public int PermissionTemplateId { get; set; }

        public string Name { get; set; }

        public string PermissionsString { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

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
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class PermissionTemplateMap : EntityTypeConfiguration<PermissionTemplate>
    {
        public PermissionTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.PermissionTemplateId);

            // Properties
            this.Property(t => t.PermissionTemplateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.PermissionsString)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("PermissionTemplates");
            this.Property(t => t.PermissionTemplateId).HasColumnName("PermissionTemplateId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PermissionsString).HasColumnName("PermissionsString");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
