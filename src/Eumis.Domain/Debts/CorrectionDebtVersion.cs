using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Debts
{
    public partial class CorrectionDebtVersion : IAggregateRoot
    {
        private CorrectionDebtVersion()
        {
        }

        public CorrectionDebtVersion(int correctionDebtId, int createdByUserId)
        {
            this.CorrectionDebtId = correctionDebtId;
            this.OrderNum = 1;
            this.Status = CorrectionDebtVersionStatus.Draft;
            this.CreatedByUserId = createdByUserId;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public CorrectionDebtVersion(CorrectionDebtVersion previousVersion, int createdByUserId)
        {
            this.CorrectionDebtId = previousVersion.CorrectionDebtId;
            this.OrderNum = previousVersion.OrderNum + 1;
            this.Status = CorrectionDebtVersionStatus.Draft;

            this.DebtEuAmount = previousVersion.DebtEuAmount;
            this.DebtBgAmount = previousVersion.DebtBgAmount;
            this.DebtBfpAmount = previousVersion.DebtBfpAmount;

            this.CertEuAmount = previousVersion.CertEuAmount;
            this.CertBgAmount = previousVersion.CertBgAmount;
            this.CertBfpAmount = previousVersion.CertBfpAmount;

            this.ReimbursedEuAmount = previousVersion.ReimbursedEuAmount;
            this.ReimbursedBgAmount = previousVersion.ReimbursedBgAmount;
            this.ReimbursedBfpAmount = previousVersion.ReimbursedBfpAmount;

            this.CreatedByUserId = createdByUserId;
            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int CorrectionDebtVersionId { get; set; }

        public int CorrectionDebtId { get; set; }

        public int OrderNum { get; set; }

        public CorrectionDebtVersionStatus Status { get; set; }

        public decimal? DebtEuAmount { get; set; }

        public decimal? DebtBgAmount { get; set; }

        public decimal? DebtBfpAmount { get; set; }

        public decimal? CertEuAmount { get; set; }

        public decimal? CertBgAmount { get; set; }

        public decimal? CertBfpAmount { get; set; }

        public decimal? ReimbursedEuAmount { get; set; }

        public decimal? ReimbursedBgAmount { get; set; }

        public decimal? ReimbursedBfpAmount { get; set; }

        public string CreateNotes { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CorrectionDebtVersionMap : EntityTypeConfiguration<CorrectionDebtVersion>
    {
        public CorrectionDebtVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.CorrectionDebtVersionId);

            // Properties
            this.Property(t => t.CorrectionDebtVersionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CorrectionDebtId)
                .IsRequired();
            this.Property(t => t.OrderNum)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.DebtEuAmount)
                .IsOptional();
            this.Property(t => t.DebtBgAmount)
                .IsOptional();
            this.Property(t => t.CertEuAmount)
                .IsOptional();
            this.Property(t => t.CertBgAmount)
                .IsOptional();
            this.Property(t => t.ReimbursedEuAmount)
                .IsOptional();
            this.Property(t => t.ReimbursedBgAmount)
                .IsOptional();
            this.Property(t => t.CreateNotes)
                .IsOptional();
            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();
            this.Property(t => t.ModifyDate)
                .IsRequired();
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CorrectionDebtVersions");
            this.Property(t => t.CorrectionDebtVersionId).HasColumnName("CorrectionDebtVersionId");
            this.Property(t => t.CorrectionDebtId).HasColumnName("CorrectionDebtId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");

            this.Property(t => t.DebtEuAmount).HasColumnName("DebtEuAmount");
            this.Property(t => t.DebtBgAmount).HasColumnName("DebtBgAmount");
            this.Property(t => t.DebtBfpAmount).HasColumnName("DebtBfpAmount");

            this.Property(t => t.CertEuAmount).HasColumnName("CertEuAmount");
            this.Property(t => t.CertBgAmount).HasColumnName("CertBgAmount");
            this.Property(t => t.CertBfpAmount).HasColumnName("CertBfpAmount");

            this.Property(t => t.ReimbursedEuAmount).HasColumnName("ReimbursedEuAmount");
            this.Property(t => t.ReimbursedBgAmount).HasColumnName("ReimbursedBgAmount");
            this.Property(t => t.ReimbursedBfpAmount).HasColumnName("ReimbursedBfpAmount");

            this.Property(t => t.CreateNotes).HasColumnName("CreateNotes");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
