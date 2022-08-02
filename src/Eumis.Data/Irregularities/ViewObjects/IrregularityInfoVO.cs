using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularityInfoVO
    {
        public IrregularityStatus Status { get; set; }

        public string RegNumber { get; set; }

        public string ContractNum { get; set; }

        public byte[] Version { get; set; }
    }
}
