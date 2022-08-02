using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public class ProjectMassManagingAuthorityCommunicationRecipient
    {
        public ProjectMassManagingAuthorityCommunicationRecipient()
        {
        }

        public ProjectMassManagingAuthorityCommunicationRecipient(int projectId)
            : this()
        {
            this.ProjectId = projectId;
        }

        public int ProjectMassManagingAuthorityCommunicationRecipientId { get; set; }

        public int ProjectMassManagingAuthorityCommunicationId { get; set; }

        public int ProjectId { get; set; }

        public virtual ProjectMassManagingAuthorityCommunication Communication { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectMassManagingAuthorityCommunicationRecipientMap : EntityTypeConfiguration<ProjectMassManagingAuthorityCommunicationRecipient>
    {
        public ProjectMassManagingAuthorityCommunicationRecipientMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectMassManagingAuthorityCommunicationRecipientId);

            // Properties
            this.Property(t => t.ProjectMassManagingAuthorityCommunicationRecipientId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectMassManagingAuthorityCommunicationId)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectMassManagingAuthorityCommunicationRecipients");
            this.Property(t => t.ProjectMassManagingAuthorityCommunicationRecipientId).HasColumnName("ProjectMassManagingAuthorityCommunicationRecipientId");
            this.Property(t => t.ProjectMassManagingAuthorityCommunicationId).HasColumnName("ProjectMassManagingAuthorityCommunicationId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");

            this.HasRequired(t => t.Communication)
                .WithMany(t => t.Recipients)
                .HasForeignKey(t => t.ProjectMassManagingAuthorityCommunicationId)
                .WillCascadeOnDelete();
        }
    }
}
