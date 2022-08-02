using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectCommunicationAnswerDO
    {
        public ProjectCommunicationAnswerDO(
            ProjectCommunicationAnswer answer,
            ProjectCommunication communication,
            ProjectCommunicationFile projectCommunicationFile)
        {
            this.ProjectCommunicationId = answer.ProjectCommunicationId;
            this.AnswerGid = answer.Gid;
            this.CommunicationGid = communication.Gid;
            this.OrderNum = answer.OrderNum;
            this.AnswerDate = answer.Answer.MessageDate;
            this.CommunicationRegNumber = communication.RegNumber;
            this.Status = answer.Status;
            this.CommunicationStatus = communication.Status;
            this.Source = answer.Source;
            this.Version = communication.Version;

            this.ProjectCommunicationFile = projectCommunicationFile != null ? new InternalFileDO(projectCommunicationFile.ProjectCommunicationFileId, projectCommunicationFile.FileName) : null;
            this.ProjectCommunicationFileSignatures = projectCommunicationFile != null ?
                projectCommunicationFile.ProjectCommunicationFileSignatures.Select(p => new InternalFileDO(p.ProjectCommunicationFileSignatureId, p.FileName)).ToList() :
                new List<InternalFileDO>();
        }

        public int ProjectCommunicationId { get; set; }

        public Guid CommunicationGid { get; set; }

        public Guid AnswerGid { get; set; }

        public int OrderNum { get; set; }

        public DateTime? AnswerDate { get; set; }

        public string CommunicationRegNumber { get; set; }

        public ProjectCommunicationAnswerStatus Status { get; set; }

        public ProjectCommunicationStatus CommunicationStatus { get; set; }

        public ProjectCommunicationAnswerSource Source { get; set; }

        public byte[] Version { get; set; }

        public InternalFileDO ProjectCommunicationFile { get; set; }

        public IList<InternalFileDO> ProjectCommunicationFileSignatures { get; set; }
    }
}
