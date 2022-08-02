using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Debts
{
    public partial class CorrectionDebt : IAggregateRoot
    {
        private CorrectionDebt()
        {
        }

        public CorrectionDebt(int flatFinancialCorrectionId, DateTime regDate)
            : this()
        {
            this.FlatFinancialCorrectionId = flatFinancialCorrectionId;
            this.RegDate = regDate;

            this.Status = CorrectionDebtStatus.New;
            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int CorrectionDebtId { get; set; }

        public int FlatFinancialCorrectionId { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public CorrectionDebtStatus Status { get; set; }

        public string Comment { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CorrectionDebtMap : EntityTypeConfiguration<CorrectionDebt>
    {
        public CorrectionDebtMap()
        {
            // Primary Key
            this.HasKey(t => t.CorrectionDebtId);

            // Properties
            this.Property(t => t.CorrectionDebtId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FlatFinancialCorrectionId)
                .IsRequired();
            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.RegDate)
                .IsRequired();
            this.Property(t => t.Status)
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
            this.ToTable("CorrectionDebts");
            this.Property(t => t.CorrectionDebtId).HasColumnName("CorrectionDebtId");
            this.Property(t => t.FlatFinancialCorrectionId).HasColumnName("FlatFinancialCorrectionId");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.Status).HasColumnName("Status");

            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
