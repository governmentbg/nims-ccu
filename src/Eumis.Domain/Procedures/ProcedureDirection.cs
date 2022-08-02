using Eumis.Domain.OperationalMap.Directions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureDirection
    {
        public ProcedureDirection()
        {
        }

        public ProcedureDirection(int programmePrirityId, int directionId, int? subDirectionId, decimal? amount = null)
        {
            this.ProgrammePriorityId = programmePrirityId;
            this.DirectionId = directionId;
            this.SubDirectionId = subDirectionId;

            if (amount.HasValue)
            {
                this.Amount = amount.Value;
            }
        }

        public int ProcedureDirectionId { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public int DirectionId { get; set; }

        public int? SubDirectionId { get; set; }

        public decimal? Amount { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual Direction Direction { get; set; }

        public virtual SubDirection SubDirection { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureDirectionMap : EntityTypeConfiguration<ProcedureDirection>
    {
        public ProcedureDirectionMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureDirectionId);

            // Properties
            this.Property(t => t.ProcedureDirectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.ProgrammePriorityId)
                .IsRequired();

            this.Property(t => t.DirectionId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureDirections");
            this.Property(t => t.ProcedureDirectionId).HasColumnName("ProcedureDirectionId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.DirectionId).HasColumnName("DirectionId");
            this.Property(t => t.SubDirectionId).HasColumnName("SubDirectionId");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureDirections)
                .HasForeignKey(d => d.ProcedureId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.Direction)
                .WithMany()
                .HasForeignKey(t => t.DirectionId);

            this.HasOptional(t => t.SubDirection)
                .WithMany()
                .HasForeignKey(t => t.SubDirectionId);
        }
    }
}
