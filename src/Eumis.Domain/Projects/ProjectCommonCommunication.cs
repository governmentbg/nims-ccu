using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public abstract partial class ProjectCommonCommunication : IEventEmitter, INotificationEventEmitter, IAggregateRoot
    {
        public ProjectCommonCommunication()
        {
            this.Question = new ProjectCommunicationMessage();
            this.Answer = new ProjectCommunicationMessage();

            this.Files = new List<ProjectCommunicationMessageFile>();
            this.Answers = new List<ProjectCommunicationAnswer>();

            ((IEventEmitter)this).Events = new List<IDomainEvent>();
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public ProjectCommonCommunication(
            int projectId,
            int projectVersionXmlId,
            int orderNum)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.ProjectId = projectId;
            this.OrderNum = orderNum;
            this.QuestionProjectVersionXmlId = projectVersionXmlId;

            DateTime currDate = DateTime.Now;
            this.CreateDate = currDate;
            this.ModifyDate = currDate;
        }

        public int ProjectCommunicationId { get; set; }

        public Guid Gid { get; set; }

        public int ProjectId { get; set; }

        public abstract ProjectCommonCommunicationDiscriminator Discriminator { get; }

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

        public virtual ICollection<ProjectCommunicationMessageFile> Files { get; set; }

        public virtual ICollection<ProjectCommunicationAnswer> Answers { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectCommonCommunicationMap : EntityTypeConfiguration<ProjectCommonCommunication>
    {
        public ProjectCommonCommunicationMap()
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

            this.Map<ProjectCommunication>(t => t.Requires("Discriminator").HasValue((int)ProjectCommonCommunicationDiscriminator.EvalSessionCommunication));
            this.Map<ProjectManagingAuthorityCommunication>(t => t.Requires("Discriminator").HasValue((int)ProjectCommonCommunicationDiscriminator.ManagingAuthorityCommunication));
        }
    }
}
