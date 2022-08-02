using Eumis.Common.Db;
using Eumis.Domain.Monitorstat;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain
{
    public class MonitorstatModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }

            modelBuilder.Configurations.Add(new MonitorstatSurveyMap());
            modelBuilder.Configurations.Add(new MonitorstaReportMap());
            modelBuilder.Configurations.Add(new MonitorstatMapNodeMap());
        }
    }
}
