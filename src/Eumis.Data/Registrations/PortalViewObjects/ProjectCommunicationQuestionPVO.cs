using Eumis.Common.Localization;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Registrations.PortalViewObjects
{
    public class ProjectCommunicationQuestionPVO
    {
        public ProjectCommunicationQuestionPVO(
            Guid messageGid,
            string registrationNumber,
            DateTime? questionDate,
            DateTime? replyDate,
            DateTime? readDate,
            DateTime? questionEndingDate,
            byte[] version,
            ProjectManagingAuthorityCommunicationStatus status,
            ProjectManagingAuthorityCommunicationSource source,
            ICollection<ProjectCommunicationAnswer> answers)
        {
            ProjectManagingAuthorityCommunicationPortalStatus portalStatus;

            if (source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority)
            {
                switch (status)
                {
                    case ProjectManagingAuthorityCommunicationStatus.Question:
                        if (answers.Any(a => a.Status == ProjectCommunicationAnswerStatus.Answer))
                        {
                            portalStatus = ProjectManagingAuthorityCommunicationPortalStatus.Answered;
                        }
                        else
                        {
                            portalStatus = readDate.HasValue ?
                                ProjectManagingAuthorityCommunicationPortalStatus.PendingAnswer :
                                ProjectManagingAuthorityCommunicationPortalStatus.NotRead;
                        }

                        break;
                    case ProjectManagingAuthorityCommunicationStatus.Canceled:
                        portalStatus = ProjectManagingAuthorityCommunicationPortalStatus.Canceled;
                        break;
                    default:
                        throw new Exception("Invalid type");
                }
            }
            else
            {
                switch (status)
                {
                    case ProjectManagingAuthorityCommunicationStatus.DraftQuestion:
                        portalStatus = ProjectManagingAuthorityCommunicationPortalStatus.Draft;
                        break;
                    case ProjectManagingAuthorityCommunicationStatus.Question:

                        if (answers.Any(a => a.Status == ProjectCommunicationAnswerStatus.Answer))
                        {
                            portalStatus = ProjectManagingAuthorityCommunicationPortalStatus.Answered;
                        }
                        else
                        {
                            portalStatus = ProjectManagingAuthorityCommunicationPortalStatus.Sent;
                        }

                        break;
                    case ProjectManagingAuthorityCommunicationStatus.Canceled:
                        portalStatus = ProjectManagingAuthorityCommunicationPortalStatus.Canceled;
                        break;
                    default:
                        throw new Exception("Invalid type");
                }
            }

            this.CommunicationStatus = portalStatus;
            this.CommunicationStatusText = portalStatus;

            this.MessageGid = messageGid;
            this.RegistrationNumber = registrationNumber;
            this.MessageDate = questionDate;
            this.ReplyDate = replyDate;
            this.ReadDate = readDate;
            this.QuestionEndingDate = questionEndingDate;
            this.IsBeneficiary = source == ProjectManagingAuthorityCommunicationSource.Beneficiary;
            this.Version = version;

            this.Answers = answers
                .Where(a => !(a.Status == ProjectCommunicationAnswerStatus.Draft && a.Source == ProjectCommunicationAnswerSource.ManagingAuthority))
                .OrderByDescending(a => a.OrderNum)
                .Select(answer => new ProjectCommunicationAnswerPVO(answer))
                .ToList();
        }

        public Guid MessageGid { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime? MessageDate { get; set; }

        public DateTime? ReplyDate { get; set; }

        public DateTime? ReadDate { get; set; }

        public DateTime? QuestionEndingDate { get; set; }

        public ProjectManagingAuthorityCommunicationPortalStatus CommunicationStatus { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public ProjectManagingAuthorityCommunicationPortalStatus CommunicationStatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public ProjectManagingAuthorityCommunicationPortalStatus CommunicationStatusTextAlt
        {
            get
            {
                return this.CommunicationStatus;
            }
        }

        public bool IsBeneficiary { get; set; }

        public byte[] Version { get; set; }

        public IList<ProjectCommunicationAnswerPVO> Answers { get; set; }
    }
}
