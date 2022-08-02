using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Allowances
{
    public partial class Allowance : IAggregateRoot
    {
        public Allowance()
        {
            this.Rates = new List<AllowanceRate>();
        }

        public Allowance(string name)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Name = name;
            this.Gid = Guid.NewGuid();

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int AllowanceId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<AllowanceRate> Rates { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AllowanceMap : EntityTypeConfiguration<Allowance>
    {
        public AllowanceMap()
        {
            // Primary Key
            this.HasKey(t => t.AllowanceId);

            // Properties
            this.Property(t => t.AllowanceId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

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
            this.ToTable("Allowances");
            this.Property(t => t.AllowanceId).HasColumnName("AllowanceId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
