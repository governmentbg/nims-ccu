using Monitorstat.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.MonitorstatService
{
    public partial class AddProcedure
    {
        public AddProcedure()
        {
        }

        public AddProcedure(ProcedureDO procedure)
            : this()
        {
            this.OperationalProgrammeIdentifier = procedure.ProgrammeCode;
            this.ProcedureIdentifier = procedure.Code;
            this.PriorityAxisIdentifier = procedure.ProgrammePriorityCode;
            this.ProcedureName = procedure.Name;
            this.StartDate = procedure.StartDate;
        }
    }
}
