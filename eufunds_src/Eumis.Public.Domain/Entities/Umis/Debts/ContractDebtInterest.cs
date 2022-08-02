using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Debts
{
    public partial class ContractDebtInterest
    {
        private ContractDebtInterest()
        {
        }

        public ContractDebtInterest(
            int contractDebtId,
            int interestSchemeId,
            int orderNum,
            DateTime dateFrom,
            DateTime dateTo,
            decimal euInterestAmount,
            decimal bgInterestAmount,
            decimal totalInterestAmount,
            decimal euAmount,
            decimal bgAmount,
            decimal totalAmount)
        {
            this.ContractDebtId = contractDebtId;
            this.InterestSchemeId = interestSchemeId;
            this.OrderNum = orderNum;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.EuInterestAmount = euInterestAmount;
            this.BgInterestAmount = bgInterestAmount;
            this.TotalInterestAmount = totalInterestAmount;
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.TotalAmount = totalAmount;
        }

        public void SetAttributes(
            int interestSchemeId,
            DateTime dateTo,
            decimal euInterestAmount,
            decimal bgInterestAmount,
            decimal totalInterestAmount,
            decimal euAmount,
            decimal bgAmount,
            decimal totalAmount)
        {
            this.InterestSchemeId = interestSchemeId;
            this.DateTo = dateTo;
            this.EuInterestAmount = euInterestAmount;
            this.BgInterestAmount = bgInterestAmount;
            this.TotalInterestAmount = totalInterestAmount;
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.TotalAmount = totalAmount;
        }

        public int ContractDebtInterestId { get; set; }
        public int ContractDebtId { get; set; }
        public int InterestSchemeId { get; set; }
        public int OrderNum { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal EuInterestAmount { get; set; }
        public decimal BgInterestAmount { get; set; }
        public decimal TotalInterestAmount { get; set; }
        public decimal EuAmount { get; set; }
        public decimal BgAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ContractDebt ContractDebt { get; set; }
    }

    public class ContractDebtInterestMap : EntityTypeConfiguration<ContractDebtInterest>
    {
        public ContractDebtInterestMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractDebtInterestId);

            // Properties
            this.Property(t => t.ContractDebtInterestId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractDebtId)
                .IsRequired();
            this.Property(t => t.InterestSchemeId)
                .IsRequired();
            this.Property(t => t.OrderNum)
                .IsRequired();
            this.Property(t => t.DateFrom)
                .IsRequired();
            this.Property(t => t.DateTo)
                .IsRequired();
            this.Property(t => t.EuInterestAmount)
                .IsRequired();
            this.Property(t => t.BgInterestAmount)
                .IsRequired();
            this.Property(t => t.TotalInterestAmount)
                .IsRequired();
            this.Property(t => t.EuAmount)
                .IsRequired();
            this.Property(t => t.BgAmount)
                .IsRequired();
            this.Property(t => t.TotalAmount)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractDebtInterests");
            this.Property(t => t.ContractDebtInterestId).HasColumnName("ContractDebtInterestId");
            this.Property(t => t.ContractDebtId).HasColumnName("ContractDebtId");
            this.Property(t => t.InterestSchemeId).HasColumnName("InterestSchemeId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.EuInterestAmount).HasColumnName("EuInterestAmount");
            this.Property(t => t.BgInterestAmount).HasColumnName("BgInterestAmount");
            this.Property(t => t.TotalInterestAmount).HasColumnName("TotalInterestAmount");
            this.Property(t => t.EuAmount).HasColumnName("EuAmount");
            this.Property(t => t.BgAmount).HasColumnName("BgAmount");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");

            // Relationships
            this.HasRequired(t => t.ContractDebt)
                .WithMany(t => t.ContractDebtInterests)
                .HasForeignKey(t => t.ContractDebtId)
                .WillCascadeOnDelete();
        }
    }
}
