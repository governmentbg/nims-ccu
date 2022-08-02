using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.UserOrganizations.Repositories;
using Eumis.Domain.UserOrganizations;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.UserOrganizations.Controllers
{
    [RoutePrefix("api/nomenclatures/userOrganizations")]
    public class UserOrganizationNomsController : EntityNomsController<UserOrganization, EntityNomVO>
    {
        public UserOrganizationNomsController(
            IUserOrganizationNomsRepository userOrganizationNomsRepository)
            : base(userOrganizationNomsRepository)
        {
        }
    }
}
