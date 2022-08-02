using Eumis.Common.Db;
using Eumis.Data.Companies.PortalViewObjects;
using Eumis.Data.Companies.Repositories;
using Eumis.Domain.NonAggregates;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Companies.Controllers
{
    [RoutePrefix("api/companies")]
    public class CompaniesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICompaniesRepository companiesRepository;

        public CompaniesController(
            IUnitOfWork unitOfWork,
            ICompaniesRepository companiesRepository)
        {
            this.unitOfWork = unitOfWork;
            this.companiesRepository = companiesRepository;
        }

        [AllowAnonymous]
        [Route("")]
        public CompanyPVO GetCompany(string uin, UinType uinType)
        {
            return this.companiesRepository.GetPortalCompany(uin, uinType);
        }
    }
}
