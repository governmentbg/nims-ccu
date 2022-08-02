using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts
{
    public partial class ContractProcurementPlan
    {
        private ContractProcurementPlan()
        {
            this.ContractDifferentiatedPositions = new List<ContractDifferentiatedPosition>();
        }

        public ContractProcurementPlan(
            Guid gid,
            string name,
            int errandAreaId,
            int errandLegalActId,
            int errandTypeId,
            string description,
            decimal expectedAmount,
            string ppaNumber,
            DateTime? noticeDate,
            DateTime? offersDeadlineDate,
            DateTime? announcedDate,
            DateTime? terminatedDate)
            : this()
        {
            this.Gid = gid;
            this.Name = name;
            this.ErrandAreaId = errandAreaId;
            this.ErrandLegalActId = errandLegalActId;
            this.ErrandTypeId = errandTypeId;
            this.Description = description;
            this.ExpectedAmount = expectedAmount;
            this.PPANumber = ppaNumber;
            this.NoticeDate = noticeDate;
            this.OffersDeadlineDate = offersDeadlineDate;
            this.AnnouncedDate = announcedDate;
            this.TerminatedDate = terminatedDate;
        }

        public int ContractProcurementPlanId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public int ErrandAreaId { get; set; }

        public int ErrandLegalActId { get; set; }

        public int ErrandTypeId { get; set; }

        public string Description { get; set; }

        public decimal ExpectedAmount { get; set; }

        public string PPANumber { get; set; }

        public DateTime? NoticeDate { get; set; }

        public DateTime? OffersDeadlineDate { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public DateTime? TerminatedDate { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual ICollection<ContractDifferentiatedPosition> ContractDifferentiatedPositions { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
            this.Property(t => t.Description)
                .IsRequired();
            this.Property(t => t.ExpectedAmount)
                .IsRequired();
            this.Property(t => t.PPANumber)
                .IsOptional();
            this.Property(t => t.NoticeDate)
                .IsOptional();
            this.Property(t => t.AnnouncedDate)
                .IsOptional();
            this.Property(t => t.TerminatedDate)
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
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ExpectedAmount).HasColumnName("ExpectedAmount");
            this.Property(t => t.PPANumber).HasColumnName("PPANumber");
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
