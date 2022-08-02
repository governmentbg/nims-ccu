using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.SpotChecks
{
    public partial class SpotCheck : IAggregateRoot
    {
        public SpotCheck()
        {
            this.CheckPlace = new SpotCheckPlace();
            this.Items = new List<SpotCheckItem>();
            this.Targets = new List<SpotCheckTarget>();
            this.Documents = new List<SpotCheckDoc>();
            this.Ascertainments = new List<SpotCheckAscertainment>();
            this.Recommendations = new List<SpotCheckRecommendation>();
        }

        public SpotCheck(
            int programmeId,
            SpotCheckLevel level,
            int? contractId,
            int countryId)
            : this()
        {
            this.ProgrammeId = programmeId;
            this.Level = level;
            this.CheckPlace.CountryId = countryId;
            this.Status = SpotCheckStatus.Draft;
            this.Type = SpotCheckType.NotPlanned;

            if (level == SpotCheckLevel.ContractContract)
            {
                this.ContractId = contractId.Value;
            }

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public SpotCheck(SpotCheckPlan plan)
            : this()
        {
            this.ProgrammeId = plan.ProgrammeId;
            this.Level = plan.Level;
            this.SpotCheckPlanId = plan.SpotCheckPlanId;
            this.ContractId = plan.ContractId;
            this.Status = SpotCheckStatus.Draft;
            this.Type = SpotCheckType.Planned;

            this.CheckPlace = new SpotCheckPlace
            {
                CountryId = plan.CheckPlace.CountryId,
                DistrictId = plan.CheckPlace.DistrictId,
                MunicipalityId = plan.CheckPlace.MunicipalityId,
                SettlementId = plan.CheckPlace.SettlementId,
                Address = plan.CheckPlace.Address,
            };

            foreach (var target in plan.Targets)
            {
                this.Targets.Add(new SpotCheckTarget
                {
                    Name = target.Name,
                    Type = target.Type,
                });
            }

            foreach (var item in plan.Items)
            {
                this.Items.Add(new SpotCheckItem
                {
                    Level = item.Level,
                    ProgrammePriorityId = item.ProgrammePriorityId,
                    ProcedureId = item.ProcedureId,
                    ContractId = item.ContractId,
                    ContractContractId = item.ContractContractId,
                });
            }
        }

        public int SpotCheckId { get; set; }

        public int ProgrammeId { get; set; }

        public SpotCheckLevel Level { get; set; }

        public int? ContractId { get; set; }

        public int? SpotCheckPlanId { get; set; }

        public SpotCheckStatus Status { get; set; }

        public SpotCheckType Type { get; set; }

        public int? CheckNum { get; set; }

        public string RegNumber { get; set; }

        public string Goal { get; set; }

        public string Team { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string OrderNum { get; set; }

        public DateTime? OrderDate { get; set; }

        public string ReportNum { get; set; }

        public DateTime? ReportDate { get; set; }

        public DateTime? ReportRecieveDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public SpotCheckPlace CheckPlace { get; set; }

        public ICollection<SpotCheckItem> Items { get; set; }

        public ICollection<SpotCheckTarget> Targets { get; set; }

        public ICollection<SpotCheckDoc> Documents { get; set; }

        public ICollection<SpotCheckAscertainment> Ascertainments { get; set; }

        public ICollection<SpotCheckRecommendation> Recommendations { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class SpotCheckMap : EntityTypeConfiguration<SpotCheck>
    {
        public SpotCheckMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckId);

            // Properties
            this.Property(t => t.SpotCheckId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.Level)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.Team)
                .HasMaxLength(500)
                .IsOptional();
            this.Property(t => t.OrderNum)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.ReportNum)
                .HasMaxLength(200)
                .IsOptional();
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
            this.ToTable("SpotChecks");
            this.Property(t => t.SpotCheckId).HasColumnName("SpotCheckId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.SpotCheckPlanId).HasColumnName("SpotCheckPlanId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.CheckNum).HasColumnName("CheckNum");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");

            this.Property(t => t.Goal).HasColumnName("Goal");
            this.Property(t => t.Team).HasColumnName("Team");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.ReportNum).HasColumnName("ReportNum");
            this.Property(t => t.ReportDate).HasColumnName("ReportDate");
            this.Property(t => t.ReportRecieveDate).HasColumnName("ReportRecieveDate");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
