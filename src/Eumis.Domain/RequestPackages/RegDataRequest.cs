using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.RequestPackages
{
    public class RegDataRequest
    {
        private RegDataRequest()
        {
        }

        public RegDataRequest(
            int requestPackageId,
            int userId,
            string uin,
            string fullname,
            string email,
            string phone,
            string address,
            string position,
            int userOrganizationId,
            int userTypeId)
        {
            this.RequestPackageId = requestPackageId;
            this.UserId = userId;
            this.Uin = uin;
            this.Fullname = fullname;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.Position = position;
            this.UserOrganizationId = userOrganizationId;
            this.UserTypeId = userTypeId;
        }

        public int RequestPackageId { get; set; }

        public int UserId { get; set; }

        public string Uin { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public int UserOrganizationId { get; set; }

        public int UserTypeId { get; set; }

        public virtual RequestPackageUser RequestPackageUser { get; set; }

        internal void SetAttributes(
            string uin,
            string fullname,
            string email,
            string phone,
            string address,
            string position,
            int userOrganizationId,
            int userTypeId)
        {
            this.Uin = uin;
            this.Fullname = fullname;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.Position = position;
            this.UserOrganizationId = userOrganizationId;
            this.UserTypeId = userTypeId;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class RegDataRequestMap : EntityTypeConfiguration<RegDataRequest>
    {
        public RegDataRequestMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RequestPackageId, t.UserId });

            // Properties
            this.Property(t => t.RequestPackageId)
                .IsRequired();

            this.Property(t => t.UserId)
                .IsRequired();

            this.Property(t => t.Uin)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Fullname)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Email)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(t => t.Phone)
                .HasMaxLength(100)
                .IsOptional();

            this.Property(t => t.Address)
                .HasMaxLength(300)
                .IsOptional();

            this.Property(t => t.Position)
                .HasMaxLength(300)
                .IsOptional();

            this.Property(t => t.UserOrganizationId)
                .IsRequired();

            this.Property(t => t.UserTypeId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("RegDataRequests");
            this.Property(t => t.RequestPackageId).HasColumnName("RequestPackageId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.Fullname).HasColumnName("Fullname");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.UserOrganizationId).HasColumnName("UserOrganizationId");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
        }
    }
}
