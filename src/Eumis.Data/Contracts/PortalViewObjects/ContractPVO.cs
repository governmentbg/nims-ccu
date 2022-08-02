using System;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractPVO
    {
        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public DateTime? ContractDate { get; set; }

        public string RegistrationNumber { get; set; }

        public string ProgrammeName { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureCode { get; set; }

        public string ProjectName { get; set; }

        public string CompanyName { get; set; }
    }
}
