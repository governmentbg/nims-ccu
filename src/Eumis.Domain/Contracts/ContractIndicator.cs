using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Contracts
{
    public class ContractIndicator
    {
        private ContractIndicator()
        {
        }

        public ContractIndicator(
            Guid gid,
            int indicatorId,
            bool isActive,
            decimal? baseTotalValue,
            decimal? baseMenValue,
            decimal? baseWomenValue,
            decimal? targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            string description,
            int? programmePriorityId,
            int? investmentPriorityId,
            int? specificTargetId)
        {
            this.IndicatorId = indicatorId;
            this.Gid = gid;
            this.IsActive = isActive;
            this.BaseTotalValue = baseTotalValue;
            this.BaseMenValue = baseMenValue;
            this.BaseWomenValue = baseWomenValue;
            this.TargetTotalValue = targetTotalValue;
            this.TargetMenValue = targetMenValue;
            this.TargetWomenValue = targetWomenValue;
            this.Description = description;
            this.ProgrammePriorityId = programmePriorityId;
            this.InvestmentPriorityId = investmentPriorityId;
            this.SpecificTargetId = specificTargetId;
        }

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

        public virtual Contract Contract { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractIndicators)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
