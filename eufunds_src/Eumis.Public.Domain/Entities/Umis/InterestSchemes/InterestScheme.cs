using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.InterestSchemes
{
    public partial class InterestScheme : IAggregateRoot
    {
        public InterestScheme()
        {
        }

        public InterestScheme(
            string name,
            int basicInterestRateId,
            int allowanceId,
            int annualBasis,
            bool isActive)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.Name = name;
            this.BasicInterestRateId = basicInterestRateId;
            this.AllowanceId = allowanceId;
            this.AnnualBasis = annualBasis;
            this.IsActive = isActive;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int InterestSchemeId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public int BasicInterestRateId { get; set; }

        public int AllowanceId { get; set; }

        public int AnnualBasis { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class InterestSchemeMap : EntityTypeConfiguration<InterestScheme>
    {
        public InterestSchemeMap()
        {
            // Primary Key
            this.HasKey(t => t.InterestSchemeId);

            // Properties
            this.Property(t => t.InterestSchemeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.BasicInterestRateId)
                .IsRequired();

            this.Property(t => t.AllowanceId)
                .IsRequired();

            this.Property(t => t.AnnualBasis)
                .IsRequired();

            this.Property(t => t.IsActive)
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
            this.ToTable("InterestSchemes");
            this.Property(t => t.InterestSchemeId).HasColumnName("InterestSchemeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.BasicInterestRateId).HasColumnName("BasicInterestRateId");
            this.Property(t => t.AllowanceId).HasColumnName("AllowanceId");
            this.Property(t => t.AnnualBasis).HasColumnName("AnnualBasis");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
