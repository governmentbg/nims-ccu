using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.FinancialCorrections
{
    public partial class FinancialCorrectionVersion : IAggregateRoot
    {
        private FinancialCorrectionVersion()
        {
            this.FinancialCorrectionVersionViolations = new List<FinancialCorrectionVersionViolation>();
        }

        // first version of financial correction
        public FinancialCorrectionVersion(int financialCorrectionId)
            : this()
        {
            var createDate = DateTime.Now;

            this.FinancialCorrectionId = financialCorrectionId;
            this.OrderNum = 1;
            this.Status = FinancialCorrectionVersionStatus.Draft;
            this.IsFirstVersion = true;

            this.CreateDate = this.ModifyDate = createDate;
        }

        //make a new draft
        public FinancialCorrectionVersion(FinancialCorrectionVersion financialCorrectionVersion) : this()
        {
            var currentDate = DateTime.Now;

            this.FinancialCorrectionId = financialCorrectionVersion.FinancialCorrectionId;
            this.OrderNum = financialCorrectionVersion.OrderNum + 1;
            this.Status = FinancialCorrectionVersionStatus.Draft;
            this.IsFirstVersion = false;

            this.CreateDate = this.ModifyDate = currentDate;
        }

        public int FinancialCorrectionVersionId { get; set; }
        public int FinancialCorrectionId { get; set; }
        public int OrderNum { get; set; }
        public FinancialCorrectionVersionStatus Status { get; set; }

        public decimal? Percent { get; set; }
        public decimal? EuAmount { get; set; }
        public decimal? BgAmount { get; set; }
        public decimal? BfpAmount { get; set; }
        public decimal? SelfAmount { get; set; }
        public decimal? TotalAmount { get; set; }

        public int? FinancialCorrectionImposingReasonId { get; set; }
        public string Description { get; set; }
        public FinancialCorrectionVersionViolationFoundBy? ViolationFoundBy { get; set; }
        public AmendmentReason? AmendmentReason { get; set; }
        public string Irregularity { get; set; }
        public CorrectionBearer? CorrectionBearer { get; set; }
        public Guid? BlobKey { get; set; }

        public bool IsFirstVersion { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        public virtual Blob File { get; set; }
        public virtual ICollection<FinancialCorrectionVersionViolation> FinancialCorrectionVersionViolations { get; set; }
    }

    public class FinancialCorrectionVersionMap : EntityTypeConfiguration<FinancialCorrectionVersion>
    {
        public FinancialCorrectionVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.FinancialCorrectionVersionId);

            // Properties
            this.Property(t => t.FinancialCorrectionVersionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.IsFirstVersion)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("FinancialCorrectionVersions");

            this.Property(t => t.FinancialCorrectionVersionId).HasColumnName("FinancialCorrectionVersionId");
            this.Property(t => t.FinancialCorrectionId).HasColumnName("FinancialCorrectionId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");

            this.Property(t => t.Percent).HasColumnName("Percent");
            this.Property(t => t.EuAmount).HasColumnName("EuAmount");
            this.Property(t => t.BgAmount).HasColumnName("BgAmount");
            this.Property(t => t.BfpAmount).HasColumnName("BfpAmount");
            this.Property(t => t.SelfAmount).HasColumnName("SelfAmount");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");

            this.Property(t => t.FinancialCorrectionImposingReasonId).HasColumnName("FinancialCorrectionImposingReasonId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ViolationFoundBy).HasColumnName("ViolationFoundBy");
            this.Property(t => t.AmendmentReason).HasColumnName("AmendmentReason");
            this.Property(t => t.Irregularity).HasColumnName("Irregularity");
            this.Property(t => t.CorrectionBearer).HasColumnName("CorrectionBearer");

            this.Property(t => t.IsFirstVersion).HasColumnName("IsFirstVersion");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            //Relationships
            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(d => d.BlobKey);
        }
    }
}
