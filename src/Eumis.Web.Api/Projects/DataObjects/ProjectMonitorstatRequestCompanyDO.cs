using Eumis.Common.Localization;
using Eumis.Domain.NonAggregates;
using Eumis.Rio;
using Newtonsoft.Json;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectMonitorstatRequestCompanyDO
    {
        public ProjectMonitorstatRequestCompanyDO()
        {
        }

        public ProjectMonitorstatRequestCompanyDO(Rio.Company company)
        {
            this.Uin = company.Uin;
            this.UinType = company.GetEnum<Rio.Company, UinType>(c => c.UinType.Id).Value;
            this.CompanyName = SystemLocalization.GetDisplayName(company.Name, company.NameEN);
        }

        public string Uin { get; set; }

        public UinType UinType { get; set; }

        public string CompanyName { get; set; }

        public int? ProcedureMonitorstatRequestId { get; set; }

        public int? ProjectVersionXmlFileId { get; set; }

        public int? ProgrammeDeclarationId { get; set; }
    }
}
