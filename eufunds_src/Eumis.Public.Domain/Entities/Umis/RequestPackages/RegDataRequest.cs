using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.RequestPackages
{
    public class RegDataRequest
    {
        public int RequestPackageId { get; set; }

        public int UserId { get; set; }

        public string Uin { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public virtual RequestPackageUser RequestPackageUser { get; set; }

        internal void SetAttributes(
            string uin,
            string fullname,
            string email,
            string phone,
            string address,
            string position)
        {
            this.Uin = uin;
            this.Fullname = fullname;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.Position = position;
        }
    }

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
        }
    }
}
