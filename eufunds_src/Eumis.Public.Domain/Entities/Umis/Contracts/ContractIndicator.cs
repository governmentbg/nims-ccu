using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractIndicator
    {
        public int ContractIndicatorId { get; set; }
        public int ContractId { get; set; }
        public int IndicatorId { get; set; }

        public Guid Gid { get; set; }
        public bool IsActive { get; set; }

        public decimal? BaseTotalValue { get; set; }
        public decimal? BaseMenValue { get; set; }
        public decimal? BaseWomenValue { get; set; }
        public decimal? TargetTotalValue { get; set; }
        public decimal? TargetMenValue { get; set; }
        public decimal? TargetWomenValue { get; set; }
        public string Description { get; set; }

        public int? ProgrammePriorityId { get; set; }
        public int? InvestmentPriorityId { get; set; }
        public int? SpecificTargetId { get; set; }
        public FinanceSource? FinanceSource { get; set; }

        public virtual Contract Contract { get; set; }
    }

    public class ContractIndicatorMap : EntityTypeConfiguration<ContractIndicator>
    {
        public ContractIndicatorMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractIndicatorId);

            // Properties
            this.Property(t => t.ContractIndicatorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IndicatorId)
                .IsRequired();
            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.IsActive)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractIndicators");
            this.Property(t => t.ContractIndicatorId).HasColumnName("ContractIndicatorId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.IndicatorId).HasColumnName("IndicatorId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.BaseTotalValue).HasColumnName("BaseTotalValue");
            this.Property(t => t.BaseMenValue).HasColumnName("BaseMenValue");
            this.Property(t => t.BaseWomenValue).HasColumnName("BaseWomenValue");
            this.Property(t => t.TargetTotalValue).HasColumnName("TargetTotalValue");
            this.Property(t => t.TargetMenValue).HasColumnName("TargetMenValue");
            this.Property(t => t.TargetWomenValue).HasColumnName("TargetWomenValue");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.InvestmentPriorityId).HasColumnName("InvestmentPriorityId");
            this.Property(t => t.SpecificTargetId).HasColumnName("SpecificTargetId");
            this.Property(t => t.FinanceSource).HasColumnName("FinanceSource");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractIndicators)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
