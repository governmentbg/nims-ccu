using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Domain.Irregularities;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularityDataDO
    {
        public IrregularityDataDO()
        {
        }

        public IrregularityDataDO(IrregularitySignal signal)
        {
            this.RegNumber = signal.RegNumber;
        }

        public int ProgrammeId { get; set; }

        public string RegNumber { get; set; }

        public byte[] Version { get; set; }
    }
}
