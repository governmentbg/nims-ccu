using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularityVO
    {
        public int IrregularityId { get; set; }

        public string SignalNum { get; set; }

        public string ProgrammeName { get; set; }

        public string ContractRegNumber { get; set; }

        public IrregularityStatus Status { get; set; }

        public string RegNumber { get; set; }

        public string Company { get; set; }
    }
}
