using Eumis.Common.Localization;
using System;
namespace Eumis.Documents.Contracts
{
    public class ContractMessageXml
    {
        public Guid? gid { get; set; }
        public Guid? answerGid { get; set; }
        public string xml { get; set; }
        public byte[] version { get; set; }
        public string hash { get; set; }
        public ContractMessageHeader regData { get; set; }

        public string registrationNumber { get; set; }
        public DateTime? messageDate { get; set; }
        public DateTime? messageEndingDate { get; set; }
        public DateTime lastSendingDate { get; set; }

        public string companyName { get; set; }
        public string companyNameAlt { get; set; }
        public string companyDisplayName { get { return SystemLocalization.GetDisplayName(companyName, companyNameAlt); } }
        public string projectRegNumber { get; set; }
    }
}
