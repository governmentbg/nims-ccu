using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Web.Api.Companies.DataObjects;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectCompanyDO
    {
        public int ProcedureId { get; set; }

        public int? RegProjectXmlId { get; set; }

        public CandidateDO Company { get; set; }
    }
}
