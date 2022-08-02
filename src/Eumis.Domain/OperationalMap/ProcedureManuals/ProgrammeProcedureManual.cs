using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.ProcedureManuals
{
    public partial class ProgrammeProcedureManual
    {
        public ProgrammeProcedureManual()
        {
        }

        public int ProgrammeProcedureManualId { get; set; }

        public int MapNodeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int OrderNum { get; set; }

        public ProgrammeProcedureManualStatus Status { get; set; }

        public DateTime? ActivationDate { get; set; }

        public int? ActivatedByUserId { get; set; }

        public Guid BlobKey { get; set; }

        public virtual Blob File { get; set; }

        public virtual Programme Programme { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureManualMap : EntityTypeConfiguration<ProgrammeProcedureManual>
    {
        public ProcedureManualMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgrammeProcedureManualId);

            // Properties
            this.Property(t => t.ProgrammeProcedureManualId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.Description)
                .IsRequired();
            this.Property(t => t.OrderNum)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProgrammeProcedureManuals");
            this.Property(t => t.ProgrammeProcedureManualId).HasColumnName("ProgrammeProcedureManualId");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ActivationDate).HasColumnName("ActivationDate");
            this.Property(t => t.ActivatedByUserId).HasColumnName("ActivatedByUserId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.Programme)
                .WithMany(t => t.ProgrammeProcedureManuals)
                .HasForeignKey(d => d.MapNodeId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
