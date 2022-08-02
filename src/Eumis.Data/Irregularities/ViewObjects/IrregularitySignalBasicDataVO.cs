using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularitySignalBasicDataVO
    {
        public int SignalId { get; set; }

        public string SignalRegNumber { get; set; }

        public IrregularitySignalStatus Status { get; set; }

        public bool IsIrregularityFound { get; set; }

        public string IrregularityNumber { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectRegNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string BeneficiaryName { get; set; }

        public string BeneficiaryUin { get; set; }

        public UinType? BeneficiaryUinType { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public byte[] Version { get; set; }
    }
}
