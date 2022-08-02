using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Services.ProcedureVersion
{
    public interface IProcedureVersionService
    {
        Eumis.Domain.Procedures.ProcedureVersion CreateProcedureVersion(int procedureId, bool? isActive = null);
    }
}
