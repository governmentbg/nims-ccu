using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Data.Monitoring.ViewObjects;

namespace Eumis.Web.Api.Monitoring.DataObjects
{
    public class ContractsReportDO
    {
        public IList<ContractsReportItem> Items { get; set; }

        public bool ResultIsClipped { get; set; }
    }
}
