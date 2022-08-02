using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureTimeLimit
    {
        public ProcedureTimeLimit()
        {
        }

        public int ProcedureTimeLimitId { get; set; }

        public int ProcedureId { get; set; }

        public DateTime EndDate { get; set; }

        public string Notes { get; set; }

        public virtual Procedure Procedure { get; set; }

        internal void SetAttributes(
            DateTime endDate,
            string notes)
        {
            this.EndDate = endDate;
            this.Notes = notes;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureTimeLimitMap : EntityTypeConfiguration<ProcedureTimeLimit>
    {
        public ProcedureTimeLimitMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureTimeLimitId);

            // Properties
            this.Property(t => t.ProcedureTimeLimitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ProcedureTimeLimits");
            this.Property(t => t.ProcedureTimeLimitId).HasColumnName("ProcedureTimeLimitId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Notes).HasColumnName("Notes");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureTimeLimits)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
