using Eumis.Domain.OperationalMap.MapNodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.AnnualAccountReports
{
    public class AnnualAccountReportAppendix
    {
        public AnnualAccountReportAppendix()
        {
        }

        public int AnnualAccountReportAppendixId { get; set; }

        public int AnnualAccountReportId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public AnnualAccountReportAppendixType Type { get; set; }

        public string Comment { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportAppendixMap : EntityTypeConfiguration<AnnualAccountReportAppendix>
    {
        public AnnualAccountReportAppendixMap()
        {
            // Primary Key
            this.HasKey(t => t.AnnualAccountReportAppendixId);

            // Properties
            this.Property(t => t.AnnualAccountReportAppendixId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportAppendices");
            this.Property(t => t.AnnualAccountReportAppendixId).HasColumnName("AnnualAccountReportAppendixId");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Comment).HasColumnName("Comment");

            this.HasRequired(t => t.AnnualAccountReport)
                .WithMany(t => t.Appendices)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();
        }
    }
}
