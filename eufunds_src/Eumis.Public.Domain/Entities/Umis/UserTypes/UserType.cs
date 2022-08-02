using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.UserTypes
{
    public partial class UserType : IAggregateRoot
    {
        public int UserTypeId { get; set; }

        public string Name { get; set; }

        public bool IsSuperUser { get; set; }

        public int PermissionTemplateId { get; set; }

        public int UserOrganizationId { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        private UserType()
        {
        }

        public UserType(string name, bool isSuperUser, int permissionTemplateId, int userOrganizationId)
        {
            var currentDate = DateTime.Now;

            this.Name = name;
            this.IsSuperUser = IsSuperUser;
            this.PermissionTemplateId = permissionTemplateId;
            this.UserOrganizationId = userOrganizationId;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }
    }

    public class UserTypeMap : EntityTypeConfiguration<UserType>
    {
        public UserTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.UserTypeId);

            // Properties
            this.Property(t => t.UserTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(t => t.IsSuperUser)
                .IsRequired();

            this.Property(t => t.PermissionTemplateId)
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
            this.ToTable("UserTypes");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsSuperUser).HasColumnName("IsSuperUser");
            this.Property(t => t.PermissionTemplateId).HasColumnName("PermissionTemplateId");
            this.Property(t => t.UserOrganizationId).HasColumnName("UserOrganizationId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
