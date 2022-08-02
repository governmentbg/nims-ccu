using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractDifferentiatedPosition
    {
        private ContractDifferentiatedPosition()
        {
        }

        public ContractDifferentiatedPosition(
            Guid gid,
            string name,
            int? submittedOffersCount,
            int? rankedOffersCount,
            string comment)
        {
            this.Gid = gid;
            this.Name = name;
            this.SubmittedOffersCount = submittedOffersCount;
            this.RankedOffersCount = rankedOffersCount;
            this.Comment = comment;
        }

        public int ContractDifferentiatedPositionId { get; set; }
        public int ContractProcurementPlanId { get; set; }
        public int? ContractContractId { get; set; }
        public Guid Gid { get; set; }

        public string Name { get; set; }
        public int? SubmittedOffersCount { get; set; }
        public int? RankedOffersCount { get; set; }
        public string Comment { get; set; }

        public virtual ContractProcurementPlan ContractProcurementPlan { get; set; }
        public virtual ContractContract ContractContract { get; set; }
    }

    public class ContractDifferentiatedPositionMap : EntityTypeConfiguration<ContractDifferentiatedPosition>
    {
        public ContractDifferentiatedPositionMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractDifferentiatedPositionId);

            // Properties
            this.Property(t => t.ContractDifferentiatedPositionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.SubmittedOffersCount)
                .IsOptional();
            this.Property(t => t.RankedOffersCount)
                .IsOptional();
            this.Property(t => t.Comment)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ContractDifferentiatedPositions");
            this.Property(t => t.ContractDifferentiatedPositionId).HasColumnName("ContractDifferentiatedPositionId");
            this.Property(t => t.ContractProcurementPlanId).HasColumnName("ContractProcurementPlanId");
            this.Property(t => t.ContractContractId).HasColumnName("ContractContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SubmittedOffersCount).HasColumnName("SubmittedOffersCount");
            this.Property(t => t.RankedOffersCount).HasColumnName("RankedOffersCount");
            this.Property(t => t.Comment).HasColumnName("Comment");

            this.HasRequired(t => t.ContractProcurementPlan)
                .WithMany(t => t.ContractDifferentiatedPositions)
                .HasForeignKey(t => t.ContractProcurementPlanId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.ContractContract)
                .WithMany()
                .HasForeignKey(t => t.ContractContractId);
        }
    }
}
