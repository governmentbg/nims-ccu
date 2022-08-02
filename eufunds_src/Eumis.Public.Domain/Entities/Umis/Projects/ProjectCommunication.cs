using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public partial class ProjectCommunication : IAggregateRoot, IEventEmitter
    {
        public static ProjectCommunicationStatus[] PortalInProgressStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Question,
            ProjectCommunicationStatus.DraftAnswer,
            ProjectCommunicationStatus.AnswerFinalized,
            ProjectCommunicationStatus.PaperAnswer
        };

        public static ProjectCommunicationStatus[] PortalHistoryStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Answer,
            ProjectCommunicationStatus.Applied,
            ProjectCommunicationStatus.Rejected,
            ProjectCommunicationStatus.Canceled
        };

        public static ProjectCommunicationStatus[] FinalStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Applied,
            ProjectCommunicationStatus.Rejected,
            ProjectCommunicationStatus.Canceled
        };

        public static ProjectCommunicationStatus[] DeletableStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.DraftQuestion
        };

        public static ProjectCommunicationStatus[] CancellableStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Question,
            ProjectCommunicationStatus.DraftAnswer,
            ProjectCommunicationStatus.AnswerFinalized,
            ProjectCommunicationStatus.Answer,
            ProjectCommunicationStatus.PaperAnswer,
        };

        public static ProjectCommunicationStatus[] EvalSessionInvisibleStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.DraftAnswer,
            ProjectCommunicationStatus.AnswerFinalized
        };

        public static ProjectCommunicationStatus[] PrintableStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Answer,
            ProjectCommunicationStatus.Applied,
            ProjectCommunicationStatus.Rejected
        };

        public int ProjectCommunicationId { get; set; }

        public Guid Gid { get; set; }

        public int ProjectId { get; set; }

        public int EvalSessionId { get; set; }

        public ProjectCommunicationStatus Status { get; set; }

        public string StatusNote { get; set; }

        public string RegNumber { get; set; }

        public DateTime? QuestionEndingDate { get; set; }

        public DateTime? QuestionReadDate { get; set; }

        public int OrderNum { get; set; }

        public int QuestionProjectVersionXmlId { get; set; }

        public ProjectCommunicationMessage Question { get; set; }

        public int? AnswerProjectVersionXmlId { get; set; }

        public ProjectCommunicationMessage Answer { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        public ICollection<ProjectCommunicationMessageFile> Files { get; set; }
    }

    public class ProjectCommunicationMap : EntityTypeConfiguration<ProjectCommunication>
    {
        public ProjectCommunicationMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectCommunicationId);

            // Properties
            this.Property(t => t.ProjectCommunicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.EvalSessionId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.QuestionProjectVersionXmlId)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProjectCommunications");
            this.Property(t => t.ProjectCommunicationId).HasColumnName("ProjectCommunicationId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.QuestionEndingDate).HasColumnName("QuestionEndingDate");
            this.Property(t => t.QuestionReadDate).HasColumnName("QuestionReadDate");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.QuestionProjectVersionXmlId).HasColumnName("QuestionProjectVersionXmlId");
            this.Property(t => t.AnswerProjectVersionXmlId).HasColumnName("AnswerProjectVersionXmlId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // Question Mapping
            this.Property(t => t.Question.MessageDate).HasColumnName("QuestionDate");
            this.Property(t => t.Question.Content).HasColumnName("QuestionContent");
            this.Property(t => t.Question.Xml).HasColumnName("QuestionXml");
            this.Property(t => t.Question.Hash).HasColumnName("QuestionHash");

            // Answer Mapping
            this.Property(t => t.Answer.MessageDate).HasColumnName("AnswerDate");
            this.Property(t => t.Answer.Content).HasColumnName("AnswerContent");
            this.Property(t => t.Answer.Xml).HasColumnName("AnswerXml");
            this.Property(t => t.Answer.Hash).HasColumnName("AnswerHash");
        }
    }
}
