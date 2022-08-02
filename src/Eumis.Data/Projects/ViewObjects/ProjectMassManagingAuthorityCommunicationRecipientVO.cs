using System;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectMassManagingAuthorityCommunicationRecipientVO
    {
        public int ProjectId { get; set; }

        public string ProjectRegNumber { get; set; }

        public DateTime? RecieveDate { get; set; }

        public string ProjectName { get; set; }

        public string BeneficiaryName { get; set; }
    }
}
