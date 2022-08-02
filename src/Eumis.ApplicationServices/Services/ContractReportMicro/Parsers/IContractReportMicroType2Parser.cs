using System.Collections.Generic;
using System.IO;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public interface IContractReportMicroType2Parser
    {
        IList<ContractReportMicrosType2Item> ParseExcel(
            int contractReportMicroId,
            Stream excelStream,
            out IList<string> errors,
            out IList<string> warnings);
    }
}
