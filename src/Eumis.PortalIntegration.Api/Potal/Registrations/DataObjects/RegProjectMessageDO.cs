using Eumis.Domain.Projects;
using System;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects
{
    public class RegProjectMessageDO
    {
        public Guid? Gid { get; set; }

        public Guid? AnswerGid { get; set; }

        public string ProjectRegNumber { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime? MessageDate { get; set; }

        public DateTime? MessageEndingDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime LastSendingDate { get; set; }

        public string Xml { get; set; }

        public byte[] Version { get; set; }

        public ProjectManagingAuthorityCommunicationSubject? Subject { get; set; }
    }
}
