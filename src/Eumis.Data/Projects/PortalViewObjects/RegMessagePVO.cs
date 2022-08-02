using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.PortalViewObjects
{
    public class RegMessagePVO
    {
        public RegMessagePVO(
            Guid gid,
            DateTime sentDate,
            DateTime? questionEndingDate,
            DateTime? questionReadDate,
            ProjectCommunicationStatus status,
            string registrationNumber,
            string procedureCode,
            string procedureName,
            string procedureNameAlt,
            string projectName,
            string projectNameAlt,
            ProjectRegistrationStatus projectRegistrationStatus,
            string companyName,
            string companyNameAlt,
            string question,
            byte[] version,
            ICollection<ProjectCommunicationAnswer> answers)
        {
            RegMessageType msgStatus;

            switch (status)
            {
                case ProjectCommunicationStatus.Question:
                    msgStatus = RegMessageType.New;
                    break;
                case ProjectCommunicationStatus.DraftAnswer:
                    msgStatus = RegMessageType.Draft;
                    break;
                case ProjectCommunicationStatus.AnswerFinalized:
                    msgStatus = RegMessageType.Finalized;
                    break;
                case ProjectCommunicationStatus.PaperAnswer:
                    msgStatus = RegMessageType.PaperSubmitted;
                    break;
                case ProjectCommunicationStatus.Answer:
                    msgStatus = RegMessageType.Submitted;
                    break;
                case ProjectCommunicationStatus.Applied:
                case ProjectCommunicationStatus.Rejected:
                    msgStatus = RegMessageType.Processed;
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

            this.Gid = gid;
            this.SentDate = sentDate;
            this.MessageEndingDate = questionEndingDate;
            this.MessageReadDate = questionReadDate;
            this.Status = msgStatus;
            this.StatusText = msgStatus;
            this.RegistrationNumber = registrationNumber;
            this.ProcedureCode = procedureCode;
            this.ProcedureName = procedureName;
            this.ProcedureNameAlt = procedureNameAlt;
            this.ProjectName = projectName;
            this.ProjectNameAlt = projectNameAlt;
            this.ProjectRegistrationStatus = new EnumPVO<ProjectRegistrationStatus>()
            {
                Description = projectRegistrationStatus,
                Value = projectRegistrationStatus,
            };
            this.CompanyName = companyName;
            this.CompanyNameAlt = companyNameAlt;
            this.Message = question;
            this.SubmitDate = answers
                .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                .Select(a => a.Answer.MessageDate)
                .SingleOrDefault();
            this.Version = version;

            this.Answers = answers
                .OrderByDescending(a => a.OrderNum)
                .Select(answer => new ProjectCommunicationAnswerPVO(answer))
                .ToList();
        }

        public Guid Gid { get; set; }

        public DateTime SentDate { get; set; }

        public DateTime? MessageEndingDate { get; set; }

        public DateTime? MessageReadDate { get; set; }

        public RegMessageType Status { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public RegMessageType StatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public RegMessageType StatusTextAlt
        {
            get
            {
                return this.StatusText;
            }
        }

        public string RegistrationNumber { get; set; }

        public string ProcedureCode { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNameAlt { get; set; }

        public EnumPVO<ProjectRegistrationStatus> ProjectRegistrationStatus { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public string Message { get; set; }

        public DateTime? SubmitDate { get; set; }

        public byte[] Version { get; set; }

        public IList<ProjectCommunicationAnswerPVO> Answers { get; set; }
    }
}
