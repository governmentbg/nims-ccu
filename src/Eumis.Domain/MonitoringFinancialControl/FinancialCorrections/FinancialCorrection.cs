using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.FinancialCorrections
{
    public partial class FinancialCorrection : IAggregateRoot
    {
        private FinancialCorrection()
        {
        }

        public FinancialCorrection(
            int orderNum,
            DateTime impositionDate,
            int contractId,
            int? contractContractId,
            int? contractBudgetLevel3AmountId)
        {
            var currentDate = DateTime.Now;

            this.OrderNum = orderNum;
            this.Status = FinancialCorrectionStatus.New;
            this.ImpositionDate = impositionDate;
            this.ContractId = contractId;
            this.ContractContractId = contractContractId;
            this.ContractBudgetLevel3AmountId = contractBudgetLevel3AmountId;

            this.CreateDate = this.ModifyDate = currentDate;
        }

        public int FinancialCorrectionId { get; set; }

        public int OrderNum { get; set; }

        public FinancialCorrectionStatus Status { get; set; }

        public DateTime ImpositionDate { get; set; }

        public int ContractId { get; set; }

        public int? ContractContractId { get; set; }

        public int? ContractBudgetLevel3AmountId { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class FinancialCorrectionMap : EntityTypeConfiguration<FinancialCorrection>
    {
        public FinancialCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => t.FinancialCorrectionId);

            // Properties
            this.Property(t => t.FinancialCorrectionId)
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
            this.Property(t => t.ImpositionDate)
                .IsRequired();
            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.CreateDate)
                .IsRequired();
            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("FinancialCorrections");
            this.Property(t => t.FinancialCorrectionId).HasColumnName("FinancialCorrectionId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ImpositionDate).HasColumnName("ImpositionDate");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractContractId).HasColumnName("ContractContractId");
            this.Property(t => t.ContractBudgetLevel3AmountId).HasColumnName("ContractBudgetLevel3AmountId");

            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
