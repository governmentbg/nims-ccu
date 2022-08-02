using System;

namespace Eumis.PortalIntegration.Api.Potal.ContractReports.DataObjects
{
    public class ContractReportMicroDO
    {
        public Guid? ExcelBlobKey { get; set; }

        public string ExcelName { get; set; }

        public byte[] Version { get; set; }
    }
}
