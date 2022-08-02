using Eumis.Domain.Companies;
using Eumis.Web.Api.Companies.DataObjects;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class CandidateDO : CompanyDO
    {
        public CandidateDO()
            : base()
        {
        }

        public CandidateDO(Company company, bool isCreated)
            : base(company)
        {
            this.IsCreated = isCreated;
        }

        public bool IsCreated { get; set; }
    }
}
