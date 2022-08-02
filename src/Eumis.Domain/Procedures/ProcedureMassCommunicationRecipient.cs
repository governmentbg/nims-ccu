using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureMassCommunicationRecipient
    {
        public ProcedureMassCommunicationRecipient()
        {
        }

        public ProcedureMassCommunicationRecipient(int contractId)
            : this()
        {
            this.ContractId = contractId;
        }

        public int ProcedureMassCommunicationRecipientId { get; set; }

        public int ProcedureMassCommunicationId { get; set; }

        public int ContractId { get; set; }

        public virtual ProcedureMassCommunication Communication { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureMassCommunicationRecipientMap : EntityTypeConfiguration<ProcedureMassCommunicationRecipient>
    {
        public ProcedureMassCommunicationRecipientMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureMassCommunicationRecipientId);

            // Properties
            this.Property(t => t.ProcedureMassCommunicationRecipientId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureMassCommunicationId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureMassCommunicationRecipients");
            this.Property(t => t.ProcedureMassCommunicationRecipientId).HasColumnName("ProcedureMassCommunicationRecipientId");
            this.Property(t => t.ProcedureMassCommunicationId).HasColumnName("ProcedureMassCommunicationId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");

            this.HasRequired(t => t.Communication)
                .WithMany(t => t.Recipients)
                .HasForeignKey(t => t.ProcedureMassCommunicationId)
                .WillCascadeOnDelete();
        }
    }
}
