using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public class HistoricContractActivity
    {
        public int HistoricContractActivityId { get; set; }

        public int HistoricContractId { get; set; }

        public string Activity { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }
    }

    public class HistoricContractActivityMap : EntityTypeConfiguration<HistoricContractActivity>
    {
        public HistoricContractActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractActivityId);

            // Properties
            this.Property(t => t.HistoricContractActivityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricContractId)
                .IsRequired();

            this.Property(t => t.Activity)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistoricContractActivities");
            this.Property(t => t.HistoricContractActivityId).HasColumnName("HistoricContractActivityId");
            this.Property(t => t.HistoricContractId).HasColumnName("HistoricContractId");
            this.Property(t => t.Activity).HasColumnName("Activity");

            // Relationships
            this.HasRequired(t => t.HistoricContract)
                .WithMany(t => t.HistoricContractActivities)
                .HasForeignKey(d => d.HistoricContractId)
                .WillCascadeOnDelete();
        }
    }
}
