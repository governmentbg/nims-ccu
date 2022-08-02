using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractBFPContract : ContractDocumentXml
    {
    }

    public class ContractBFPContractMetadata
    {
        public DateTime? contractDate { get; set; }
        public string name { get; set; }
        public string registrationNumber { get; set; }

        public string GetContractNumberHeader
        {
            get
            {
                return string.Format("Договор № {0}", this.registrationNumber);
            }
        }
    }

    public class ContractsPVO
    {
        public List<ContractPVO> results { get; set; }

        public int count { get; set; }
    }

    public class ContractPVO
    {
        public Guid gid { get; set; }

        public DateTime? contractDate { get; set; }

        public string registrationNumber { get; set; }

        public string programmeName { get; set; }

        public string procedureName { get; set; }

        public string procedureCode { get; set; }

        public string projectName { get; set; }

        public string companyName { get; set; }
    }
}
    