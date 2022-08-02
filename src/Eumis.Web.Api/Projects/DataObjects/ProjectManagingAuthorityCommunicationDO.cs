using Eumis.Domain.Projects;
using System;
using System.Linq;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectManagingAuthorityCommunicationDO
    {
        public ProjectManagingAuthorityCommunicationDO()
        {
        }

        public ProjectManagingAuthorityCommunicationDO(
            ProjectManagingAuthorityCommunication communication,
            string regNumber,
            string companyName)
        {
            this.ProjectCommunicationId = communication.ProjectCommunicationId;
            this.XmlGid = communication.Gid;
            this.Subject = communication.Subject;
            this.Source = communication.Source;
            this.Status = communication.ManagingAuthorityCommunicationStatus;
            this.OrderNum = communication.OrderNum;
            this.StatusNote = communication.StatusNote;
            this.RegNumber = communication.RegNumber;
            this.ProjectRegNumber = regNumber;
            this.CompanyName = companyName;
            this.QuestionDate = communication.Question.MessageDate;
            this.QuestionEndingDate = communication.QuestionEndingDate;
            this.QuestionReadDate = communication.QuestionReadDate;
            this.AnswerDate = communication
                .Answers
                .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                .Select(a => a.Answer.MessageDate)
                .SingleOrDefault();
            this.Version = communication.Version;
        }

        public int ProjectCommunicationId { get; set; }

        public ProjectManagingAuthorityCommunicationSubject Subject { get; set; }

        public ProjectManagingAuthorityCommunicationSource Source { get; set; }

        public ProjectManagingAuthorityCommunicationStatus Status { get; set; }

        public Guid XmlGid { get; set; }

        public int OrderNum { get; set; }

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
