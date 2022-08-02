using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.DataObjects
{
    public class ContractDataDO
    {
        public string ProjectRegNumber { get; set; }
        public string ProgrammeCode { get; set; }
        public string AuthorityUin { get; set; }
        public UinType? AuthorityUinType { get; set; }
        public bool IsPrimaryProgramme { get; set; }
        public int ProgrammeId { get; set; }
        public string ContractRegNumber { get; set; }
        public string ContractName { get; set; }
        public string ProgrammeName { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureCode { get; set; }
    }
}
