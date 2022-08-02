using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes
{
    public class IndicativeAnnualWorkingProgrammeTableProgramme
    {
        public int IndicativeAnnualWorkingProgrammeTableProgrammeId { get; set; }

        public int IndicativeAnnualWorkingProgrammeTableId { get; set; }

        public int ProgrammeId { get; set; }

        public virtual IndicativeAnnualWorkingProgrammeTable IndicativeAnnualWorkingProgrammeTable { get; set; }
    }

    public class IndicativeAnnualWorkingProgrammeTableProgrammeMap : EntityTypeConfiguration<IndicativeAnnualWorkingProgrammeTableProgramme>
    {
        public IndicativeAnnualWorkingProgrammeTableProgrammeMap()
        {
            // Primary Key
            this.HasKey(t => t.IndicativeAnnualWorkingProgrammeTableProgrammeId);

            // Properties
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableProgrammeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableId)
                .IsRequired();

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IndicativeAnnualWorkingProgrammeTableProgrammes");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableProgrammeId).HasColumnName("IndicativeAnnualWorkingProgrammeTableProgrammeId");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableId).HasColumnName("IndicativeAnnualWorkingProgrammeTableId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");

            // Relationships
            this.HasRequired(t => t.IndicativeAnnualWorkingProgrammeTable)
                .WithMany(t => t.IndicativeAnnualWorkingProgrammeTableProgrammes)
                .HasForeignKey(t => t.IndicativeAnnualWorkingProgrammeTableId)
                .WillCascadeOnDelete();
        }
    }
}
