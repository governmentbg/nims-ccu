using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectManagingAuthorityCommunicationAnswerDO
    {
        public ProjectManagingAuthorityCommunicationAnswerDO(
            ProjectCommunicationAnswer answer,
            ProjectManagingAuthorityCommunication communication)
        {
            this.ProjectCommunicationId = answer.ProjectCommunicationId;
            this.AnswerGid = answer.Gid;
            this.CommunicationGid = communication.Gid;
            this.OrderNum = answer.OrderNum;
            this.AnswerDate = answer.Answer.MessageDate;
            this.CommunicationRegNumber = communication.RegNumber;
            this.Status = answer.Status;
            this.CommunicationStatus = communication.ManagingAuthorityCommunicationStatus;
            this.Source = answer.Source;
            this.Version = communication.Version;
        }

        public int ProjectCommunicationId { get; set; }

        public Guid CommunicationGid { get; set; }

        public Guid AnswerGid { get; set; }

        public int OrderNum { get; set; }

        public DateTime? AnswerDate { get; set; }

        public string CommunicationRegNumber { get; set; }

        public ProjectCommunicationAnswerStatus Status { get; set; }

        public ProjectManagingAuthorityCommunicationStatus CommunicationStatus { get; set; }

        public ProjectCommunicationAnswerSource Source { get; set; }

        public byte[] Version { get; set; }
    }
}
