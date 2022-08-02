using Eumis.Data.Core;

namespace Eumis.Data.ContractReports.PortalViewObjects
{
    public class ContractReportMicroItemsPVO<T>
        where T : class
    {
        public string ContractNumber { get; set; }

        public int VersionNum { get; set; }

        public PagePVO<T> Items { get; set; }
    }
}
