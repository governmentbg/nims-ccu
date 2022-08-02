using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.DataObjects
{
    public class ProjectMassManagingAuthorityCommunicationDO
    {
        public ProjectMassManagingAuthorityCommunicationDO()
        {
        }

        public ProjectMassManagingAuthorityCommunicationDO(ProjectMassManagingAuthorityCommunication communication)
        {
            this.ProcedureMassCommunicationId = communication.ProjectMassManagingAuthorityCommunicationId;
            this.Subject = communication.Subject;
            this.SubjectDesc = communication.Subject;
            this.ProgrammeId = communication.ProgrammeId;
            this.ProcedureId = communication.ProcedureId;
            this.OrderNum = communication.OrderNum;
            this.ModifyDate = communication.ModifyDate;
            this.EndingDate = communication.EndingDate;
            this.Status = communication.Status;
            this.Message = communication.Message;

            this.Version = communication.Version;
        }

        public int ProcedureMassCommunicationId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProcedureId { get; set; }

        public int? OrderNum { get; set; }

        public ProjectMassManagingAuthorityCommunicationStatus Status { get; set; }

        public ProjectManagingAuthorityCommunicationSubject? Subject { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectManagingAuthorityCommunicationSubject? SubjectDesc { get; set; }

        public string Message { get; set; }

        public DateTime? EndingDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
