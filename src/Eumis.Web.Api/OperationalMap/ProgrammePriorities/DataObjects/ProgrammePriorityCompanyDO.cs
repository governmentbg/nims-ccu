using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.Procedures.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.DataObjects
{
    public class ProgrammePriorityCompanyDO
    {
        public ProgrammePriorityCompanyDO()
        {
        }

        public ProgrammePriorityCompanyDO(ProgrammePriorityCompany priorityCompany)
            : this()
        {
            this.ProgrammePriorityType = priorityCompany?.ProgrammePriorityType;
            this.CompanyId = priorityCompany?.CompanyId;
            this.HigherOrderCompanyId = priorityCompany?.HigherOrderCompanyId;
        }

        public ProgrammePriorityType? ProgrammePriorityType { get; set; }

        public int? CompanyId { get; set; }

        public int? HigherOrderCompanyId { get; set; }
    }
}
