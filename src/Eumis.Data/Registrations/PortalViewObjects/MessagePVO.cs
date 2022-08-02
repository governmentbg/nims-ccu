using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Localization;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.Registrations.PortalViewObjects
{
    public class MessagePVO
    {
        public MessagePVO(
            Guid messageGid,
            string registrationNumber,
            DateTime? messageDate,
            ProjectCommunicationStatus status,
            ICollection<ProjectCommunicationAnswer> answers)
        {
            RegMessageType msgStatus;

            switch (status)
            {
                case ProjectCommunicationStatus.Question:
                case ProjectCommunicationStatus.DraftAnswer:
                    msgStatus = RegMessageType.New;
                    break;
                case ProjectCommunicationStatus.AnswerFinalized:
                    msgStatus = RegMessageType.Finalized;
                    break;
                case ProjectCommunicationStatus.PaperAnswer:
                    msgStatus = RegMessageType.PaperSubmitted;
                    break;
                case ProjectCommunicationStatus.Answer:
                case ProjectCommunicationStatus.Applied:
                case ProjectCommunicationStatus.Rejected:
                    msgStatus = RegMessageType.Submitted;
                    break;
                case ProjectCommunicationStatus.Canceled:
                    msgStatus = RegMessageType.Cancelled;
                    break;
                case ProjectCommunicationStatus.Expired:
                    msgStatus = RegMessageType.Expired;
                    break;
                default:
                    throw new Exception("Invalid type");
            }

            this.MessageGid = messageGid;
            this.RegistrationNumber = registrationNumber;
            this.MessageDate = messageDate;
            this.ReplyDate = answers
                .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                .Select(a => a.Answer.MessageDate)
                .SingleOrDefault();

            this.Status = msgStatus;
            this.StatusText = msgStatus;

            this.Answers = answers
                .OrderByDescending(a => a.OrderNum)
                .Select(answer => new ProjectCommunicationAnswerPVO(answer))
                .ToList();
        }

        public Guid MessageGid { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime? MessageDate { get; set; }

        public DateTime? ReplyDate { get; set; }

        public RegMessageType Status { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public RegMessageType StatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public RegMessageType StatusTextAlt
        {
            get
            {
                return this.Status;
            }
        }

        public IList<ProjectCommunicationAnswerPVO> Answers { get; set; }
    }
}
