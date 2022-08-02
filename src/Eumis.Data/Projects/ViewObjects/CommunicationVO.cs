using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class CommunicationVO
    {
        public CommunicationVO(ProjectCommunication communication, string sessionNum, DateTime? answerDate)
        {
            ProjectCommunicationStatus status;
            if (ProjectCommunication.EvalSessionInvisibleStatuses.Contains(communication.Status))
            {
                status = ProjectCommunicationStatus.Question;
            }
            else
            {
                status = communication.Status;
            }

            this.CommunicationId = communication.ProjectCommunicationId;
            this.ProjectId = communication.ProjectId;
            this.SessionId = communication.EvalSessionId;
            this.XmlGid = communication.Gid;
            this.SessionNum = sessionNum;
            this.Status = status;
            this.StatusId = status;
            this.RegNumber = communication.RegNumber;
            this.QuestionDate = communication.Question.MessageDate;
            this.QuestionEndingDate = communication.QuestionEndingDate;
            this.AnswerDate = answerDate;
        }

        public int CommunicationId { get; set; }

        public int ProjectId { get; set; }

        public int SessionId { get; set; }

        public Guid XmlGid { get; set; }

        public string SessionNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectCommunicationStatus Status { get; set; }

        public ProjectCommunicationStatus StatusId { get; set; }

        public string RegNumber { get; set; }

        public DateTime? QuestionDate { get; set; }

        public DateTime? QuestionEndingDate { get; set; }

        public DateTime? AnswerDate { get; set; }

        public InternalFileVO ProjectCommunicationFile { get; set; }

        public IList<InternalFileVO> ProjectCommunicationFileSignatures { get; set; }
    }
}
