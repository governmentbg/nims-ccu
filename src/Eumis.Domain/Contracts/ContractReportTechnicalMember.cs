using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportTechnicalMember
    {
        private ContractReportTechnicalMember()
        {
        }

        public ContractReportTechnicalMember(
            int contractId,
            int contractReportId,
            int contractReportTechnicalId,
            string name,
            string position,
            string uin,
            PersonalUinType? uinType,
            CommitmentType? commitmentType,
            DateTime? date,
            decimal? hours,
            string activity,
            string result)
        {
            this.ContractReportTechnicalId = contractReportTechnicalId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;
            this.Name = name;
            this.Position = position;
            this.Uin = uin;
            this.UinType = uinType;
            this.CommitmentType = commitmentType;
            this.Date = date;
            this.Hours = hours;
            this.Activity = activity;
            this.Result = result;
        }

        public int ContractReportTechnicalMemberId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Uin { get; set; }

        public PersonalUinType? UinType { get; set; }

        public CommitmentType? CommitmentType { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Hours { get; set; }

        public string Activity { get; set; }

        public string Result { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportTechnicalMemberMap : EntityTypeConfiguration<ContractReportTechnicalMember>
    {
        public ContractReportTechnicalMemberMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportTechnicalMemberId);

            // Properties
            this.Property(t => t.ContractReportTechnicalMemberId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractReportTechnicalId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportTechnicalMembers");
            this.Property(t => t.ContractReportTechnicalMemberId).HasColumnName("ContractReportTechnicalMemberId");
            this.Property(t => t.ContractReportTechnicalId).HasColumnName("ContractReportTechnicalId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.UinType).HasColumnName("UinType");
            this.Property(t => t.CommitmentType).HasColumnName("CommitmentType");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Hours).HasColumnName("Hours");
            this.Property(t => t.Activity).HasColumnName("Activity");
            this.Property(t => t.Result).HasColumnName("Result");
        }
    }
}
