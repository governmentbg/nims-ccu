using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public partial class SpotCheckPlan : IAggregateRoot
    {
        public SpotCheckPlan()
        {
            this.CheckPlace = new SpotCheckPlace();
            this.Items = new List<SpotCheckPlanItem>();
            this.Targets = new List<SpotCheckPlanTarget>();
            this.Documents = new List<SpotCheckPlanDoc>();
        }

        public SpotCheckPlan(
            int programmeId,
            int? contractId,
            SpotCheckLevel level,
            int countryId,
            Year year,
            Month month) : this()
        {
            this.ProgrammeId = programmeId;
            this.Level = level;
            this.Year = year;
            this.Month = month;
            this.CheckPlace.CountryId = countryId;
            this.IsConfirmed = false;

            if (level == SpotCheckLevel.ContractContract)
            {
                this.ContractId = contractId.Value;
            }

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int SpotCheckPlanId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ContractId { get; set; }

        public SpotCheckLevel Level { get; set; }

        public Year Year { get; set; }

        public Month Month { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public string ConfirmationOrder { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public SpotCheckPlace CheckPlace { get; set; }

        public ICollection<SpotCheckPlanItem> Items { get; set; }

        public ICollection<SpotCheckPlanTarget> Targets { get; set; }

        public ICollection<SpotCheckPlanDoc> Documents { get; set; }
    }

    public class SpotCheckPlanMap : EntityTypeConfiguration<SpotCheckPlan>
    {
        public SpotCheckPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckPlanId);

            // Properties
            this.Property(t => t.SpotCheckPlanId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.Level)
                .IsRequired();
            this.Property(t => t.Year)
                .IsRequired();
            this.Property(t => t.Month)
                .IsRequired();
            this.Property(t => t.IsConfirmed)
                .IsRequired();
            this.Property(t => t.ConfirmationOrder)
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
            this.ToTable("SpotCheckPlans");
            this.Property(t => t.SpotCheckPlanId).HasColumnName("SpotCheckPlanId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Level).HasColumnName("Level");

            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");

            this.Property(t => t.IsConfirmed).HasColumnName("IsConfirmed");
            this.Property(t => t.ConfirmationDate).HasColumnName("ConfirmationDate");
            this.Property(t => t.ConfirmationOrder).HasColumnName("ConfirmationOrder");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
