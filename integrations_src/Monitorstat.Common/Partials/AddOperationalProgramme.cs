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
    public partial class AddOperationalProgramme
    {
        public AddOperationalProgramme()
        {
        }

        public AddOperationalProgramme(ProgrammeDO programme)
            : this()
        {
            this.Abbreviation = programme.Abbreviation;
            this.Code = programme.Code;
            this.Name = programme.Name;
            this.Abbreviation = programme.Abbreviation;
            this.Status = new Nomenclature()
            {
                Id = Guid.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:ProgrammeStatusGuid")),
                Name = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:ProgrammeStatusName"),
            };

            this.Group = new Nomenclature()
            { 
                Id = Guid.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:ProgrammeGroupGuid")),
                Name = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:ProgrammeGroupName"),
            };
        }
    }
}
