using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.UserTypes.Repositories;
using Eumis.Domain.Users;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/nomenclatures/users")]
    public class UserNomsController : EntityNomsController<User, EntityNomVO>
    {
        private readonly IUserNomsRepository userNomsRepository;
        private readonly IUserClaimsContext currentUserClaimsContext;

        public UserNomsController(
            IUserNomsRepository userNomsRepository,
            IAccessContext accessContext,
            UserClaimsContextFactory userClaimsContextFactory)
            : base(userNomsRepository)
        {
            this.userNomsRepository = userNomsRepository;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        public override EntityNomVO GetNom(int id)
        {
            if (this.currentUserClaimsContext.IsSuperUser)
            {
                return this.userNomsRepository.GetExtendedUserNom(id);
            }
            else
            {
                return base.GetNom(id);
            }
        }
    }
}
