using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularitySignalRegisterItemVO
    {
        public int IrregularitySignalId { get; set; }

        public string IrregularitySignalRegNumber { get; set; }

        public DateTime? MASystemRegDate { get; set; }

        public string ProgrammeName { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string ProjectName { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ViolationDesrc { get; set; }

        public string SignalSource { get; set; }

        public string Actions { get; set; }

        public IrregularitySignalStatus Status { get; set; }

        public string ActRegNum { get; set; }

        public DateTime? ActRegDate { get; set; }

        public string Comment { get; set; }

        public bool IsIrregularityFound { get; set; }

        public string IrregularityRegNumber { get; set; }
    }
}
