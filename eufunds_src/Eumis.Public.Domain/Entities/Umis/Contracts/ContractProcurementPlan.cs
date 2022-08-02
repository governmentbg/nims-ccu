using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractProcurementPlan
    {
        private ContractProcurementPlan()
        {
            this.ContractDifferentiatedPositions = new List<ContractDifferentiatedPosition>();
            this.PublicDocuments = new List<ContractProcurementPlanPublicDocument>();
        }

        public ContractProcurementPlan(
            Guid gid,
            string name,
            int errandAreaId,
            int errandLegalActId,
            int errandTypeId,
            decimal amount,
            string description,
            YesNoNonApplicable maPreliminaryControl,
            YesNoNonApplicable ppaPreliminaryControl,
            string internetAddress,
            decimal expectedAmount,
            string ppaNumber,
            DateTime? planDate,
            DateTime? noticeDate,
            DateTime? offersDeadlineDate)
            : this()
        {
            this.Gid = gid;
            this.Name = name;
            this.ErrandAreaId = errandAreaId;
            this.ErrandLegalActId = errandLegalActId;
            this.ErrandTypeId = errandTypeId;
            this.Amount = amount;
            this.Description = description;
            this.MAPreliminaryControl = maPreliminaryControl;
            this.PPAPreliminaryControl = ppaPreliminaryControl;
            this.InternetAddress = internetAddress;
            this.ExpectedAmount = expectedAmount;
            this.PPANumber = ppaNumber;
            this.PlanDate = planDate;
            this.NoticeDate = noticeDate;
            this.OffersDeadlineDate = offersDeadlineDate;
        }

        public int ContractProcurementPlanId { get; set; }
        public int ContractId { get; set; }
        public Guid Gid { get; set; }

        public string Name { get; set; }
        public int ErrandAreaId { get; set; }
        public int ErrandLegalActId { get; set; }
        public int ErrandTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public YesNoNonApplicable MAPreliminaryControl { get; set; }
        public YesNoNonApplicable PPAPreliminaryControl { get; set; }
        public string InternetAddress { get; set; }
        public decimal ExpectedAmount { get; set; }
        public string PPANumber { get; set; }
        public DateTime? PlanDate { get; set; }
        public DateTime? NoticeDate { get; set; }
        public DateTime? OffersDeadlineDate { get; set; }
        public DateTime? AnnouncedDate { get; set; }
        public DateTime? TerminatedDate { get; set; }

        public virtual Contract Contract { get; set; }
        public virtual ICollection<ContractDifferentiatedPosition> ContractDifferentiatedPositions { get; set; }
        public virtual ICollection<ContractProcurementPlanPublicDocument> PublicDocuments { get; set; }
    }

    public class ContractProcurementPlanMap : EntityTypeConfiguration<ContractProcurementPlan>
    {
        public ContractProcurementPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractProcurementPlanId);

            // Properties
            this.Property(t => t.ContractProcurementPlanId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.ErrandAreaId)
                .IsRequired();
            this.Property(t => t.ErrandLegalActId)
                .IsRequired();
            this.Property(t => t.ErrandTypeId)
                .IsRequired();
            this.Property(t => t.Amount)
                .IsRequired();
            this.Property(t => t.Description)
                .IsRequired();
            this.Property(t => t.MAPreliminaryControl)
                .IsRequired();
            this.Property(t => t.PPAPreliminaryControl)
                .IsRequired();
            this.Property(t => t.InternetAddress)
                .IsOptional();
            this.Property(t => t.ExpectedAmount)
                .IsRequired();
            this.Property(t => t.PPANumber)
                .IsOptional();
            this.Property(t => t.PlanDate)
                .IsOptional();
            this.Property(t => t.NoticeDate)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ContractProcurementPlans");
            this.Property(t => t.ContractProcurementPlanId).HasColumnName("ContractProcurementPlanId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ErrandAreaId).HasColumnName("ErrandAreaId");
            this.Property(t => t.ErrandLegalActId).HasColumnName("ErrandLegalActId");
            this.Property(t => t.ErrandTypeId).HasColumnName("ErrandTypeId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.MAPreliminaryControl).HasColumnName("MAPreliminaryControl");
            this.Property(t => t.PPAPreliminaryControl).HasColumnName("PPAPreliminaryControl");
            this.Property(t => t.InternetAddress).HasColumnName("InternetAddress");
            this.Property(t => t.ExpectedAmount).HasColumnName("ExpectedAmount");
            this.Property(t => t.PPANumber).HasColumnName("PPANumber");
            this.Property(t => t.PlanDate).HasColumnName("PlanDate");
            this.Property(t => t.NoticeDate).HasColumnName("NoticeDate");
            this.Property(t => t.OffersDeadlineDate).HasColumnName("OffersDeadlineDate");
            this.Property(t => t.AnnouncedDate).HasColumnName("AnnouncedDate");
            this.Property(t => t.TerminatedDate).HasColumnName("TerminatedDate");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractProcurementPlans)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
