using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public partial class ProjectCommunicationAnswer
    {
        public ProjectCommunicationAnswer()
        {
            this.Answer = new ProjectCommunicationMessage();
        }

        public ProjectCommunicationAnswer(
            string answerXml,
            int projectVersionXmlId,
            int orderNum,
            ProjectCommunicationAnswerSource source)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.Status = ProjectCommunicationAnswerStatus.Draft;
            this.Source = source;
            this.OrderNum = orderNum;
            this.ProjectVersionXmlId = projectVersionXmlId;

            this.Answer.SetAnswerXml(answerXml);
        }

        public int ProjectCommunicationAnswerId { get; set; }

        public Guid Gid { get; set; }

        public int ProjectCommunicationId { get; set; }

        public int? ProjectVersionXmlId { get; set; }

        public int OrderNum { get; set; }

        public DateTime? ReadDate { get; set; }

        public ProjectCommunicationAnswerStatus Status { get; set; }

        public ProjectCommunicationAnswerSource Source { get; set; }

        public ProjectCommunicationMessage Answer { get; set; }

        public virtual ProjectCommonCommunication ProjectCommunication { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectCommunicationAnswerMap : EntityTypeConfiguration<ProjectCommunicationAnswer>
    {
        public ProjectCommunicationAnswerMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectCommunicationAnswerId);

            // Properties
            this.Property(t => t.ProjectCommunicationAnswerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectCommunicationId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Source)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectCommunicationAnswers");
            this.Property(t => t.ProjectCommunicationAnswerId).HasColumnName("ProjectCommunicationAnswerId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProjectCommunicationId).HasColumnName("ProjectCommunicationId");
            this.Property(t => t.ProjectVersionXmlId).HasColumnName("ProjectVersionXmlId");
            this.Property(t => t.ReadDate).HasColumnName("ReadDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");

            this.Property(t => t.Answer.MessageDate).HasColumnName("SendDate");
            this.Property(t => t.Answer.Content).HasColumnName("Content");
            this.Property(t => t.Answer.Xml).HasColumnName("Xml");
            this.Property(t => t.Answer.Hash).HasColumnName("Hash");

            this.HasRequired(t => t.ProjectCommunication)
                .WithMany(t => t.Answers)
                .HasForeignKey(t => t.ProjectCommunicationId)
                .WillCascadeOnDelete();
        }
    }
}
