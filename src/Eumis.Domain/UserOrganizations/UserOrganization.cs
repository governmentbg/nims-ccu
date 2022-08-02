using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.UserOrganizations
{
    public partial class UserOrganization : IAggregateRoot
    {
        private UserOrganization()
        {
        }

        public UserOrganization(string name)
        {
            var currentDate = DateTime.Now;

            this.Name = name;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int UserOrganizationId { get; set; }

        public string Name { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class UserOrganizationMap : EntityTypeConfiguration<UserOrganization>
    {
        public UserOrganizationMap()
        {
            // Primary Key
            this.HasKey(t => t.UserOrganizationId);

            // Properties
            this.Property(t => t.UserOrganizationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(200)
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
            this.ToTable("UserOrganizations");
            this.Property(t => t.UserOrganizationId).HasColumnName("UserOrganizationId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
