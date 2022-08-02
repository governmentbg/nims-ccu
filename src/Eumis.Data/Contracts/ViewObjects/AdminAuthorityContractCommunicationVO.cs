using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class AdminAuthorityContractCommunicationVO : ContractCommunicationVO
    {
        public string ContractRegNumber { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }
    }
}
