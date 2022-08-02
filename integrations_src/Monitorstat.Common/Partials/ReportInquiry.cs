using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.MonitorstatService
{
    public partial class ReportInquiry
    {
        public ReportInquiry()
        {
            this.ReportCodes = new ReportCodes();
        }

        public ReportInquiry(List<string> reports)
            : this()
        {
            this.ReportCodes.AddRange(reports);
        }
    }
}
