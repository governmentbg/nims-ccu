using Eumis.Domain.Companies;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procurements
{
    public class ProcurementDifferentiatedPosition
    {
        private ProcurementDifferentiatedPosition()
        {
            this.Gid = Guid.NewGuid();
        }

        public ProcurementDifferentiatedPosition(
            string name,
            string comment)
            : this()
        {
            this.Name = name;
            this.Comment = comment;
        }

        public int ProcurementDifferentiatedPositionId { get; set; }

        public int ProcurementId { get; set; }

        public string Name { get; set; }

        public Guid Gid { get; set; }

        public string Comment { get; set; }

        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual Procurement Procurement { get; set; }

        internal void SetAttributes(string name, string comment, int? companyId)
        {
            this.Name = name;
            this.Comment = comment;
            this.CompanyId = companyId;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcurementDifferentiatedPositionMap : EntityTypeConfiguration<ProcurementDifferentiatedPosition>
    {
        public ProcurementDifferentiatedPositionMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcurementDifferentiatedPositionId);

            // Properties
            this.Property(t => t.ProcurementDifferentiatedPositionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.ProcurementId)
                .IsRequired();
            this.Property(t => t.CompanyId)
                .IsOptional();
            this.Property(t => t.Comment)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ProcurementDifferentiatedPositions");
            this.Property(t => t.ProcurementDifferentiatedPositionId).HasColumnName("ProcurementDifferentiatedPositionId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProcurementId).HasColumnName("ProcurementId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.Comment).HasColumnName("Comment");

            this.HasRequired(t => t.Procurement)
                .WithMany(t => t.DifferentiatedPositions)
                .HasForeignKey(t => t.ProcurementId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.Company)
                .WithMany()
                .HasForeignKey(t => t.CompanyId);
        }
    }
}
