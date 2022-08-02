using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public partial class ProjectCommunication : ProjectCommonCommunication
    {
        public static readonly ProjectCommunicationStatus[] DeletableStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.DraftQuestion,
        };

        public static readonly ProjectCommunicationStatus[] PortalInProgressStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Question,
            ProjectCommunicationStatus.DraftAnswer,
            ProjectCommunicationStatus.AnswerFinalized,
            ProjectCommunicationStatus.PaperAnswer,
        };

        public static readonly ProjectCommunicationStatus[] PortalHistoryStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Answer,
            ProjectCommunicationStatus.Applied,
            ProjectCommunicationStatus.Rejected,
            ProjectCommunicationStatus.Canceled,
            ProjectCommunicationStatus.Expired,
        };

        public static readonly ProjectCommunicationStatus[] FinalStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Applied,
            ProjectCommunicationStatus.Rejected,
            ProjectCommunicationStatus.Canceled,
            ProjectCommunicationStatus.Expired,
        };

        public static readonly ProjectCommunicationStatus[] CancellableStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Question,
            ProjectCommunicationStatus.DraftAnswer,
            ProjectCommunicationStatus.AnswerFinalized,
            ProjectCommunicationStatus.Answer,
            ProjectCommunicationStatus.PaperAnswer,
        };

        public static readonly ProjectCommunicationStatus[] ExpirableStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Question,
            ProjectCommunicationStatus.DraftAnswer,
            ProjectCommunicationStatus.AnswerFinalized,
        };

        public static readonly ProjectCommunicationStatus[] EvalSessionInvisibleStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.DraftAnswer,
            ProjectCommunicationStatus.AnswerFinalized,
        };

        public static readonly ProjectCommunicationStatus[] PrintableStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Answer,
            ProjectCommunicationStatus.Applied,
            ProjectCommunicationStatus.Rejected,
        };

        public static readonly ProjectCommunicationStatus[] AnswerAllowedStatuses = new ProjectCommunicationStatus[]
        {
            ProjectCommunicationStatus.Question,
            ProjectCommunicationStatus.Answer,
        };

        public ProjectCommunication()
        {
        }

        public ProjectCommunication(
            int projectId,
            int projectVersionXmlId,
            int evalSessionId,
            int orderNum,
            string questionXml)
            : base(
                  projectId,
                  projectVersionXmlId,
                  orderNum)
        {
            this.EvalSessionId = evalSessionId;
            this.Status = ProjectCommunicationStatus.DraftQuestion;

            this.SetQuestionXml(questionXml);
        }

        public int EvalSessionId { get; set; }

        public ProjectCommunicationStatus Status { get; set; }

        public override ProjectCommonCommunicationDiscriminator Discriminator
        {
            get
            {
                return ProjectCommonCommunicationDiscriminator.EvalSessionCommunication;
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectCommunicationMap : EntityTypeConfiguration<ProjectCommunication>
    {
        public ProjectCommunicationMap()
        {
            // Properties
            this.Property(t => t.EvalSessionId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
