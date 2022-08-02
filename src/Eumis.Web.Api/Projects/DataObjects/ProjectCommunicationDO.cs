using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectCommunicationDO
    {
        public ProjectCommunicationDO()
        {
        }

        public ProjectCommunicationDO(
            ProjectCommunication communication,
            string projectRegNumber,
            string companyName,
            DateTime? answerDate = null)
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

            this.EvalSessionId = communication.EvalSessionId;
            this.XmlGid = communication.Gid;
            this.Status = status;
            this.StatusNote = communication.StatusNote;
            this.RegNumber = communication.RegNumber;
            this.ProjectRegNumber = projectRegNumber;
            this.CompanyName = companyName;
            this.QuestionDate = communication.Question.MessageDate;
            this.QuestionEndingDate = communication.QuestionEndingDate;
            this.QuestionReadDate = communication.QuestionReadDate;
            this.AnswerDate = answerDate;
            this.Version = communication.Version;
        }

        public int EvalSessionId { get; set; }

        public Guid XmlGid { get; set; }

        public ProjectCommunicationStatus Status { get; set; }

        public string StatusNote { get; set; }

        public string RegNumber { get; set; }

        public string ProjectRegNumber { get; set; }

        public string CompanyName { get; set; }

        public DateTime? QuestionDate { get; set; }

        public DateTime? QuestionEndingDate { get; set; }

        public DateTime? QuestionReadDate { get; set; }

        public DateTime? AnswerDate { get; set; }

        public byte[] Version { get; set; }
    }
}
