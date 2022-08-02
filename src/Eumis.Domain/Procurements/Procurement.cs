using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;

namespace Eumis.Domain.Procurements
{
    public partial class Procurement : IAggregateRoot
    {
        private Procurement()
        {
            this.DifferentiatedPositions = new List<ProcurementDifferentiatedPosition>();
            this.Documents = new List<ProcurementDocument>();
        }

        public Procurement(
            string name,
            string shortName,
            int? errandAreaId,
            int? errandLegalActId,
            int? errandTypeId,
            decimal? prognosysAmount,
            string description,
            string internetAddress,
            decimal? expectedAmount,
            string ppaNumber,
            DateTime? planDate,
            DateTime? offersDeadlineDate,
            DateTime? announcedDate)
            : this()
        {
            this.Status = ProcurementStatus.Draft;
            this.Gid = Guid.NewGuid();

            this.SetAttributes(
                name,
                shortName,
                errandAreaId,
                errandLegalActId,
                errandTypeId,
                prognosysAmount,
                description,
                internetAddress,
                expectedAmount,
                ppaNumber,
                planDate,
                offersDeadlineDate,
                announcedDate);

            this.CreateDate = DateTime.Now;
        }

        public int ProcurementId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public Guid Gid { get; set; }

        public ProcurementStatus Status { get; set; }

        public int? ErrandAreaId { get; set; }

        public int? ErrandLegalActId { get; set; }

        public int? ErrandTypeId { get; set; }

        public decimal? PrognosysAmount { get; set; }

        public string Description { get; set; }

        public string InternetAddress { get; set; }

        public decimal? ExpectedAmount { get; set; }

        public string PPANumber { get; set; }

        public DateTime? PlanDate { get; set; }

        public DateTime? OffersDeadlineDate { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual ICollection<ProcurementDifferentiatedPosition> DifferentiatedPositions { get; set; }

        public virtual ICollection<ProcurementDocument> Documents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcurementMap : EntityTypeConfiguration<Procurement>
    {
        public ProcurementMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcurementId);

            // Properties
            this.Property(t => t.ProcurementId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.ShortName)
                .IsRequired()
                .HasMaxLength(100);
            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.ErrandAreaId)
                .IsOptional();
            this.Property(t => t.ErrandLegalActId)
                .IsOptional();
            this.Property(t => t.ErrandTypeId)
                .IsOptional();
            this.Property(t => t.PrognosysAmount)
                .IsOptional();
            this.Property(t => t.Description)
                .IsOptional();
            this.Property(t => t.InternetAddress)
                .IsOptional();
            this.Property(t => t.ExpectedAmount)
                .IsOptional();
            this.Property(t => t.PPANumber)
                .IsOptional();
            this.Property(t => t.PlanDate)
                .IsOptional();
            this.Property(t => t.AnnouncedDate)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("Procurements");
            this.Property(t => t.ProcurementId).HasColumnName("ProcurementId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.ErrandAreaId).HasColumnName("ErrandAreaId");
            this.Property(t => t.ErrandLegalActId).HasColumnName("ErrandLegalActId");
            this.Property(t => t.ErrandTypeId).HasColumnName("ErrandTypeId");
            this.Property(t => t.PrognosysAmount).HasColumnName("PrognosysAmount");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.InternetAddress).HasColumnName("InternetAddress");
            this.Property(t => t.ExpectedAmount).HasColumnName("ExpectedAmount");
            this.Property(t => t.PPANumber).HasColumnName("PPANumber");
            this.Property(t => t.PlanDate).HasColumnName("PlanDate");
            this.Property(t => t.OffersDeadlineDate).HasColumnName("OffersDeadlineDate");
            this.Property(t => t.AnnouncedDate).HasColumnName("AnnouncedDate");
        }
    }
}
