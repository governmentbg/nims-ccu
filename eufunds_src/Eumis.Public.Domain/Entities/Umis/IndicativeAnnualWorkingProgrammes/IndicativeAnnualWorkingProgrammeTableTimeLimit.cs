using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes
{
    public class IndicativeAnnualWorkingProgrammeTableTimeLimit
    {
        public int IndicativeAnnualWorkingProgrammeTableTimeLimitId { get; set; }

        public int IndicativeAnnualWorkingProgrammeTableId { get; set; }

        public DateTime EndDate { get; set; }

        public virtual IndicativeAnnualWorkingProgrammeTable IndicativeAnnualWorkingProgrammeTable { get; set; }
    }

    public class IndicativeAnnualWorkingProgrammeTableTimeLimitMap : EntityTypeConfiguration<IndicativeAnnualWorkingProgrammeTableTimeLimit>
    {
        public IndicativeAnnualWorkingProgrammeTableTimeLimitMap()
        {
            // Primary Key
            this.HasKey(t => t.IndicativeAnnualWorkingProgrammeTableTimeLimitId);

            // Properties
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableTimeLimitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableId)
                .IsRequired();

            this.Property(t => t.EndDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IndicativeAnnualWorkingProgrammeTableTimeLimits");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableTimeLimitId).HasColumnName("IndicativeAnnualWorkingProgrammeTableTimeLimitId");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableId).HasColumnName("IndicativeAnnualWorkingProgrammeTableId");
            this.Property(t => t.EndDate).HasColumnName("EndDate");

            this.HasRequired(t => t.IndicativeAnnualWorkingProgrammeTable)
                .WithMany(t => t.IndicativeAnnualWorkingProgrammeTableTimeLimits)
                .HasForeignKey(t => t.IndicativeAnnualWorkingProgrammeTableId)
                .WillCascadeOnDelete();
        }
    }
}
