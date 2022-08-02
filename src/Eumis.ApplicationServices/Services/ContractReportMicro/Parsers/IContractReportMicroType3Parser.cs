using System.Collections.Generic;
using System.IO;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public interface IContractReportMicroType3Parser
    {
        IList<ContractReportMicrosType3Item> ParseExcel(
            int contractReportMicroId,
            Stream excelStream,
            out IList<string> errors);
    }
}
