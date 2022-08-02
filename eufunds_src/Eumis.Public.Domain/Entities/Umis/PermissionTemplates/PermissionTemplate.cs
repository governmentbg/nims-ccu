using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Users.PermissionAggregations;

namespace Eumis.Public.Domain.Entities.Umis.PermissionTemplates
{
    public partial class PermissionTemplate : IAggregateRoot
    {
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

        private PermissionAggregation permissions = null;
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
