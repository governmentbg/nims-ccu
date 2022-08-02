using Eumis.ApplicationServices.Services.Regix;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Companies.PortalViewObjects;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Commuincators.Controllers
{
    [RoutePrefix("api/regix")]
    public class RegixController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IRegixService regixService;

        public RegixController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IRegixService regixService)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.regixService = regixService;
        }

        [Route("")]
        public CompanyPVO GetCompany(string uin, UinType uinType, string code)
        {
            return this.regixService.GetCompany(uin, uinType, code);
        }
    }
}
