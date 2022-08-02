using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public partial class ProjectMassManagingAuthorityCommunication : IAggregateRoot
    {
        public const string MassCommunicationTemplateXmlKey = "ed80cbb8-d582-492f-93f6-fb1c322cd3a4";

        public ProjectMassManagingAuthorityCommunication()
        {
            this.Documents = new List<ProjectMassManagingAuthorityCommunicationDocument>();
            this.Recipients = new List<ProjectMassManagingAuthorityCommunicationRecipient>();
        }

        public ProjectMassManagingAuthorityCommunication(
            int programmeId,
            int procedureId,
            int orderNum,
            ProjectManagingAuthorityCommunicationSubject? subject,
            string body,
            DateTime? endingDate)
            : this()
        {
            this.Status = ProjectMassManagingAuthorityCommunicationStatus.Draft;
            this.CreateDate = DateTime.Now;

            this.UpdateAttributes(programmeId, procedureId, subject, body, endingDate, orderNum);
        }

        public int ProjectMassManagingAuthorityCommunicationId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public int OrderNum { get; set; }

        public ProjectMassManagingAuthorityCommunicationStatus Status { get; set; }

        public DateTime? EndingDate { get; set; }

        public string Message { get; set; }

        public ProjectManagingAuthorityCommunicationSubject? Subject { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public List<ProjectMassManagingAuthorityCommunicationDocument> Documents { get; set; }

        public List<ProjectMassManagingAuthorityCommunicationRecipient> Recipients { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectMassManagingAuthorityCommunicationMap : EntityTypeConfiguration<ProjectMassManagingAuthorityCommunication>
    {
        public ProjectMassManagingAuthorityCommunicationMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectMassManagingAuthorityCommunicationId);

            this.Property(t => t.ProjectMassManagingAuthorityCommunicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Message)
                .IsOptional();

            this.Property(t => t.Subject)
                .IsOptional();

            this.Property(t => t.EndingDate)
                .IsOptional();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectMassManagingAuthorityCommunications");
            this.Property(t => t.ProjectMassManagingAuthorityCommunicationId).HasColumnName("ProjectMassManagingAuthorityCommunicationId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.EndingDate).HasColumnName("EndingDate");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
