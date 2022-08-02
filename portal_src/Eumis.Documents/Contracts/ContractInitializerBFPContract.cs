using System;
namespace Eumis.Documents.Contracts
{
    public class ContractInitializerBFPContract : ContractInitializer
    {
        public string contractGid { get; set; }

        public string programmeCode { get; set; }

        public string projectRegNumber { get; set; }
        public string contractRegNumber { get; set; }

        public string authorityUin { get; set; }
        public string authorityUinType { get; set; }
    }
}