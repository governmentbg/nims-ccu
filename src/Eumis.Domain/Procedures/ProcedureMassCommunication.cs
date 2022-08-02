using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureMassCommunication : IAggregateRoot
    {
        public ProcedureMassCommunication(int programmeId, int procedureId, string subject, string body)
            : this()
        {
            this.Status = ProcedureMassCommunicationStatus.Draft;
            this.CreateDate = DateTime.Now;

            this.UpdateAttributes(programmeId, procedureId, subject, body);
        }

        public ProcedureMassCommunication()
        {
            this.Documents = new List<ProcedureMassCommunicationDocument>();
            this.Recipients = new List<ProcedureMassCommunicationRecipient>();
        }

        public int ProcedureMassCommunicationId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public ProcedureMassCommunicationStatus Status { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<ProcedureMassCommunicationDocument> Documents { get; set; }

        public List<ProcedureMassCommunicationRecipient> Recipients { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureMassCommunicationMap : EntityTypeConfiguration<ProcedureMassCommunication>
    {
        public ProcedureMassCommunicationMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureMassCommunicationId);

            this.Property(t => t.ProcedureMassCommunicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Subject)
                .IsOptional();

            this.Property(t => t.Body)
                .IsOptional();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureMassCommunications");
            this.Property(t => t.ProcedureMassCommunicationId).HasColumnName("ProcedureMassCommunicationId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
