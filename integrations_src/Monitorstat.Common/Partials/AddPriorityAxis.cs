using Eumis.Common.Config;
using Monitorstat.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.MonitorstatService
{
    public partial class AddPriorityAxis
    {
        public AddPriorityAxis()
        {
        }

        public AddPriorityAxis(ProgrammePriorityDO programmePriority)
            :this()
        {
            this.OperationalProgrammeIdentifier = programmePriority.ProgrammeIdentifier.ToString();

            this.PriorityAxisName = programmePriority.Name;
            this.CodeField = programmePriority.Code;
            this.Status = new Nomenclature
            {
                Id = Guid.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:ProgrammeStatusGuid")),
                Name = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:ProgrammeStatusName"),
            };

            this.SpecificTarget = programmePriority.SpecificTargets;

            this.Definition = programmePriority.Definition;
            this.YearFrom = DateTime.Parse("2014-01-01");
            this.YearTo = DateTime.Parse("2020-12-31");
        }
    }
}
