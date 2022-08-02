using System.Collections.Generic;
using System.IO;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public interface IContractReportMicroType1Parser
    {
        IList<ContractReportMicrosType1Item> ParseExcel(
            int contractReportMicroId,
            Stream excelStream,
            out IList<string> errors);
    }
}
