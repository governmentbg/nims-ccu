using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Domain.Irregularities;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularitySignalBasicDataDO
    {
        public IrregularitySignalBasicDataDO()
        {
        }

        public IrregularitySignalBasicDataDO(IrregularitySignal signal)
        {
            this.SignalRegNumber = signal.RegNumber;
        }

        public int ProgrammeId { get; set; }

        public string SignalRegNumber { get; set; }

        public byte[] Version { get; set; }
    }
}
