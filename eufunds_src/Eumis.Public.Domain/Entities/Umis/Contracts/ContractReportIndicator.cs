using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportIndicator : IAggregateRoot
    {

        private ContractReportIndicator()
        {
        }

        public ContractReportIndicator(
            int contractReportTechnicalId,
            int contractIndicatorId,
            int contractReportId,
            int contractId,
            bool hasGenderDivision,
            decimal? periodAmountMen,
            decimal? periodAmountWomen,
            decimal periodAmountTotal,
            decimal? cumulativeAmountMen,
            decimal? cumulativeAmountWomen,
            decimal cumulativeAmountTotal,
            decimal? residueAmountMen,
            decimal? residueAmountWomen,
            decimal residueAmountTotal,
            decimal? lastReportCumulativeAmountMen,
            decimal? lastReportCumulativeAmountWomen,
            decimal lastReportCumulativeAmountTotal,
            string comment)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportTechnicalId = contractReportTechnicalId;
            this.ContractIndicatorId = contractIndicatorId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;
            this.HasGenderDivision = hasGenderDivision;

            this.Status = ContractReportIndicatorStatus.Draft;

            if (hasGenderDivision)
            {
                this.PeriodAmountMen = periodAmountMen;
                this.PeriodAmountWomen = periodAmountWomen;
                this.CumulativeAmountMen = cumulativeAmountMen;
                this.CumulativeAmountWomen = cumulativeAmountWomen;
                this.ResidueAmountMen = residueAmountMen;
                this.ResidueAmountWomen = residueAmountWomen;
                this.LastReportCumulativeAmountMen = lastReportCumulativeAmountMen;
                this.LastReportCumulativeAmountWomen = lastReportCumulativeAmountWomen;
            }

            this.PeriodAmountTotal = periodAmountTotal;
            this.CumulativeAmountTotal = cumulativeAmountTotal;
            this.ResidueAmountTotal = residueAmountTotal;
            this.LastReportCumulativeAmountTotal = lastReportCumulativeAmountTotal;
            this.Comment = comment;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportIndicatorId { get; set; }
        public int ContractReportTechnicalId { get; set; }
        public int ContractIndicatorId { get; set; }
        public int ContractReportId { get; set; }
        public int ContractId { get; set; }
        public Guid Gid { get; set; }

        public ContractReportIndicatorStatus Status { get; set; }
        public bool HasGenderDivision { get; set; }
        public ContractReportIndicatorApproval? Approval { get; set; }
        public string Notes { get; set; }
        public int? CheckedByUserId { get; set; }
        public DateTime? CheckedDate { get; set; }

        public decimal? PeriodAmountMen { get; set; }
        public decimal? PeriodAmountWomen { get; set; }
        public decimal PeriodAmountTotal { get; set; }

        public decimal? CumulativeAmountMen { get; set; }
        public decimal? CumulativeAmountWomen { get; set; }
        public decimal CumulativeAmountTotal { get; set; }

        public decimal? ResidueAmountMen { get; set; }
        public decimal? ResidueAmountWomen { get; set; }
        public decimal ResidueAmountTotal { get; set; }

        public decimal? LastReportCumulativeAmountMen { get; set; }
        public decimal? LastReportCumulativeAmountWomen { get; set; }
        public decimal LastReportCumulativeAmountTotal { get; set; }

        public string Comment { get; set; }

        public decimal? ApprovedPeriodAmountMen { get; set; }
        public decimal? ApprovedPeriodAmountWomen { get; set; }
        public decimal? ApprovedPeriodAmountTotal { get; set; }

        public decimal? ApprovedCumulativeAmountMen { get; set; }
        public decimal? ApprovedCumulativeAmountWomen { get; set; }
        public decimal? ApprovedCumulativeAmountTotal { get; set; }

        public decimal? ApprovedResidueAmountMen { get; set; }
        public decimal? ApprovedResidueAmountWomen { get; set; }
        public decimal? ApprovedResidueAmountTotal { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }
    }

    public class ContractReportIndicatorMap : EntityTypeConfiguration<ContractReportIndicator>
    {
        public ContractReportIndicatorMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportIndicatorId);

            // Properties
            this.Property(t => t.ContractReportIndicatorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportTechnicalId)
                .IsRequired();

            this.Property(t => t.ContractIndicatorId)
                .IsRequired();
            
            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.HasGenderDivision)
                .IsRequired();

            this.Property(t => t.PeriodAmountTotal)
                .IsRequired();

            this.Property(t => t.CumulativeAmountTotal)
                .IsRequired();

            this.Property(t => t.ResidueAmountTotal)
                .IsRequired();

            this.Property(t => t.LastReportCumulativeAmountTotal)
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
            this.ToTable("ContractReportIndicators");
            this.Property(t => t.ContractReportIndicatorId).HasColumnName("ContractReportIndicatorId");
            this.Property(t => t.ContractReportTechnicalId).HasColumnName("ContractReportTechnicalId");
            this.Property(t => t.ContractIndicatorId).HasColumnName("ContractIndicatorId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.HasGenderDivision).HasColumnName("HasGenderDivision");
            this.Property(t => t.Approval).HasColumnName("Approval");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.PeriodAmountMen).HasColumnName("PeriodAmountMen");
            this.Property(t => t.PeriodAmountWomen).HasColumnName("PeriodAmountWomen");
            this.Property(t => t.PeriodAmountTotal).HasColumnName("PeriodAmountTotal");

            this.Property(t => t.CumulativeAmountMen).HasColumnName("CumulativeAmountMen");
            this.Property(t => t.CumulativeAmountWomen).HasColumnName("CumulativeAmountWomen");
            this.Property(t => t.CumulativeAmountTotal).HasColumnName("CumulativeAmountTotal");

            this.Property(t => t.ResidueAmountMen).HasColumnName("ResidueAmountMen");
            this.Property(t => t.ResidueAmountWomen).HasColumnName("ResidueAmountWomen");
            this.Property(t => t.ResidueAmountTotal).HasColumnName("ResidueAmountTotal");

            this.Property(t => t.LastReportCumulativeAmountMen).HasColumnName("LastReportCumulativeAmountMen");
            this.Property(t => t.LastReportCumulativeAmountWomen).HasColumnName("LastReportCumulativeAmountWomen");
            this.Property(t => t.LastReportCumulativeAmountTotal).HasColumnName("LastReportCumulativeAmountTotal");

            this.Property(t => t.Comment).HasColumnName("Comment");

            this.Property(t => t.ApprovedPeriodAmountMen).HasColumnName("ApprovedPeriodAmountMen");
            this.Property(t => t.ApprovedPeriodAmountWomen).HasColumnName("ApprovedPeriodAmountWomen");
            this.Property(t => t.ApprovedPeriodAmountTotal).HasColumnName("ApprovedPeriodAmountTotal");

            this.Property(t => t.ApprovedCumulativeAmountMen).HasColumnName("ApprovedCumulativeAmountMen");
            this.Property(t => t.ApprovedCumulativeAmountWomen).HasColumnName("ApprovedCumulativeAmountWomen");
            this.Property(t => t.ApprovedCumulativeAmountTotal).HasColumnName("ApprovedCumulativeAmountTotal");

            this.Property(t => t.ApprovedResidueAmountMen).HasColumnName("ApprovedResidueAmountMen");
            this.Property(t => t.ApprovedResidueAmountWomen).HasColumnName("ApprovedResidueAmountWomen");
            this.Property(t => t.ApprovedResidueAmountTotal).HasColumnName("ApprovedResidueAmountTotal");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
