using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public partial class OtherViolation : IAggregateRoot
    {
        public OtherViolation()
        {
        }

        public int OtherViolationId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class OtherViolationMap : EntityTypeConfiguration<OtherViolation>
    {
        public OtherViolationMap()
        {
            // Primary Key
            this.HasKey(t => t.OtherViolationId);

            // Properties
            this.Property(t => t.OtherViolationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("OtherViolations");
            this.Property(t => t.OtherViolationId).HasColumnName("OtherViolationId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
