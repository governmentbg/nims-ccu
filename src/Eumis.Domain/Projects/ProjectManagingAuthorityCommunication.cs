using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public partial class ProjectManagingAuthorityCommunication : ProjectCommonCommunication
    {
        public static readonly ProjectManagingAuthorityCommunicationSubject[] BeneficiarySubjects = new ProjectManagingAuthorityCommunicationSubject[]
        {
            ProjectManagingAuthorityCommunicationSubject.ProjectProposalWithdrawal,
            ProjectManagingAuthorityCommunicationSubject.Complaint,
            ProjectManagingAuthorityCommunicationSubject.ChangesAndCircumstances,
            ProjectManagingAuthorityCommunicationSubject.TourismMinistryReport,
            ProjectManagingAuthorityCommunicationSubject.ManagingAuthorityReport,
        };

        public static readonly ProjectManagingAuthorityCommunicationSubject[] ManagingAuthoritySubjects = new ProjectManagingAuthorityCommunicationSubject[]
        {
            ProjectManagingAuthorityCommunicationSubject.ContractConclusionDocuments,
            ProjectManagingAuthorityCommunicationSubject.ChangesAndCircumstances,
            ProjectManagingAuthorityCommunicationSubject.Message,
            ProjectManagingAuthorityCommunicationSubject.TourismMinistryReport,
        };

        public ProjectManagingAuthorityCommunication()
        {
        }

        public ProjectManagingAuthorityCommunication(
            int projectId,
            int projectVersionXmlId,
            int orderNum,
            string questionXml,
            ProjectManagingAuthorityCommunicationSource source)
            : base(
                  projectId,
                  projectVersionXmlId,
                  orderNum)
        {
            this.ManagingAuthorityCommunicationStatus = ProjectManagingAuthorityCommunicationStatus.DraftQuestion;
            this.Subject = ProjectManagingAuthorityCommunicationSubject.ContractConclusionDocuments;
            this.Source = source;

            this.SetQuestionXml(questionXml);
        }

        public ProjectManagingAuthorityCommunicationStatus ManagingAuthorityCommunicationStatus { get; set; }

        public ProjectManagingAuthorityCommunicationSubject Subject { get; set; }

        public ProjectManagingAuthorityCommunicationSource Source { get; set; }

        public override ProjectCommonCommunicationDiscriminator Discriminator
        {
            get
            {
                return ProjectCommonCommunicationDiscriminator.ManagingAuthorityCommunication;
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectManagingAuthorityCommunicationMap : EntityTypeConfiguration<ProjectManagingAuthorityCommunication>
    {
        public ProjectManagingAuthorityCommunicationMap()
        {
            // Properties
            this.Property(t => t.Subject)
                .IsRequired();
            this.Property(t => t.Source)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Source).HasColumnName("Source");
        }
    }
}
