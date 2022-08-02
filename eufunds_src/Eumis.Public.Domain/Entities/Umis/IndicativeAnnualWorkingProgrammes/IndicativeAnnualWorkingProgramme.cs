using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes
{
    public partial class IndicativeAnnualWorkingProgramme
    {
        public int IndicativeAnnualWorkingProgrammeId { get; set; }

        public int ProgrammeId { get; set; }

        public IndicativeAnnualWorkingProgrammeYear Year { get; set; }

        public IndicativeAnnualWorkingProgrammeType Type { get; set; }

        public IndicativeAnnualWorkingProgrammeStatus Status { get; set; }

        public string StatusNote { get; set; }

        public int OrderVersionNum { get; set; }

        public int? PublicatedByUserId { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual ICollection<IndicativeAnnualWorkingProgrammeTable> IndicativeAnnualWorkingProgrammeTables { get; set; }
    }
    public class IndicativeAnnualWorkingProgrammeMap : EntityTypeConfiguration<IndicativeAnnualWorkingProgramme>
    {
        public IndicativeAnnualWorkingProgrammeMap()
        {
            // Primary Key
            this.HasKey(t => t.IndicativeAnnualWorkingProgrammeId);

            // Properties
            this.Property(t => t.IndicativeAnnualWorkingProgrammeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.Year)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.OrderVersionNum)
                .IsRequired();

            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IndicativeAnnualWorkingProgrammes");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeId).HasColumnName("IndicativeAnnualWorkingProgrammeId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.OrderVersionNum).HasColumnName("OrderVersionNum");
            this.Property(t => t.PublicatedByUserId).HasColumnName("PublicatedByUserId");
            this.Property(t => t.PublicationDate).HasColumnName("PublicationDate");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
        }
    }
}
