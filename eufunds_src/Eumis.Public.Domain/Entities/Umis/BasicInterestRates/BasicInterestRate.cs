using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.BasicInterestRates
{
    public partial class BasicInterestRate : IAggregateRoot
    {
        public BasicInterestRate()
        {
            this.Rates = new List<InterestRate>();
        }

        public BasicInterestRate(string name)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Name = name;
            this.Gid = Guid.NewGuid();

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int BasicInterestRateId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<InterestRate> Rates { get; set; }
    }

    public class BasicInterestRateMap : EntityTypeConfiguration<BasicInterestRate>
    {
        public BasicInterestRateMap()
        {
            // Primary Key
            this.HasKey(t => t.BasicInterestRateId);

            // Properties
            this.Property(t => t.BasicInterestRateId)
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
            this.ToTable("BasicInterestRates");
            this.Property(t => t.BasicInterestRateId).HasColumnName("BasicInterestRateId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
