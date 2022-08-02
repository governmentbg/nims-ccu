using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractContract
    {
        private ContractContract()
        {
            this.ContractSubcontracts = new List<ContractSubcontract>();
            this.ContractContractActivities = new List<ContractContractActivity>();
        }

        public ContractContract(
            Guid gid,
            bool isActive,
            DateTime signDate,
            string number,
            decimal totalAmountExcludingVAT,
            decimal vatAmountIfEligible,
            decimal totalFundedValue,
            int numberAnnexes,
            decimal currentAnnexTotalAmount,
            string comment,
            DateTime startDate,
            DateTime endDate,
            bool hasSubcontractorMember)
            : this()
        {
            this.Gid = gid;
            this.IsActive = isActive;
            this.SignDate = signDate;
            this.Number = number;
            this.TotalAmountExcludingVAT = totalAmountExcludingVAT;
            this.VATAmountIfEligible = vatAmountIfEligible;
            this.TotalFundedValue = totalFundedValue;
            this.NumberAnnexes = numberAnnexes;
            this.CurrentAnnexTotalAmount = currentAnnexTotalAmount;
            this.Comment = comment;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.HasSubcontractorMember = hasSubcontractorMember;
        }

        public int ContractContractId { get; set; }

        public int ContractId { get; set; }

        public int ContractContractorId { get; set; }

        public Guid Gid { get; set; }

        public bool IsActive { get; set; }

        public DateTime SignDate { get; set; }

        public string Number { get; set; }

        public decimal TotalAmountExcludingVAT { get; set; }

        public decimal VATAmountIfEligible { get; set; }

        public decimal TotalFundedValue { get; set; }

        public int NumberAnnexes { get; set; }

        public decimal CurrentAnnexTotalAmount { get; set; }

        public string Comment { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool HasSubcontractorMember { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual ContractContractor ContractContractor { get; set; }

        public virtual ICollection<ContractSubcontract> ContractSubcontracts { get; set; }

        public virtual ICollection<ContractContractActivity> ContractContractActivities { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractContractMap : EntityTypeConfiguration<ContractContract>
    {
        public ContractContractMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractContractId);

            // Properties
            this.Property(t => t.ContractContractId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.IsActive)
                .IsRequired();
            this.Property(t => t.SignDate)
                .IsRequired();
            this.Property(t => t.Number)
                .IsRequired();
            this.Property(t => t.TotalAmountExcludingVAT)
                .IsRequired();
            this.Property(t => t.VATAmountIfEligible)
                .IsRequired();
            this.Property(t => t.TotalFundedValue)
                .IsRequired();
            this.Property(t => t.NumberAnnexes)
                .IsRequired();
            this.Property(t => t.CurrentAnnexTotalAmount)
                .IsRequired();
            this.Property(t => t.Comment)
                .IsOptional();
            this.Property(t => t.StartDate)
                .IsRequired();
            this.Property(t => t.EndDate)
                .IsRequired();
            this.Property(t => t.HasSubcontractorMember)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractContracts");
            this.Property(t => t.ContractContractId).HasColumnName("ContractContractId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.ContractContractorId).HasColumnName("ContractContractorId");
            this.Property(t => t.SignDate).HasColumnName("SignDate");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.TotalAmountExcludingVAT).HasColumnName("TotalAmountExcludingVAT");
            this.Property(t => t.VATAmountIfEligible).HasColumnName("VATAmountIfEligible");
            this.Property(t => t.TotalFundedValue).HasColumnName("TotalFundedValue");
            this.Property(t => t.NumberAnnexes).HasColumnName("NumberAnnexes");
            this.Property(t => t.CurrentAnnexTotalAmount).HasColumnName("CurrentAnnexTotalAmount");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.HasSubcontractorMember).HasColumnName("HasSubcontractorMember");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractContracts)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.ContractContractor)
                .WithMany()
                .HasForeignKey(t => t.ContractContractorId);
        }
    }
}
