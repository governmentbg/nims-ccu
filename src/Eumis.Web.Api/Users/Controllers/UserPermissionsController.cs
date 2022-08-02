using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Users.PermissionTables;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/users/{userId}/permissions")]
    public class UserPermissionsController : ApiController
    {
        private IUsersRepository usersRepository;
        private IProgrammesRepository programmesRepository;
        private IAuthorizer authorizer;

        public UserPermissionsController(
            IUsersRepository usersRepository,
            IProgrammesRepository programmesRepository,
            IAuthorizer authorizer)
        {
            this.usersRepository = usersRepository;
            this.programmesRepository = programmesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public PermissionTable GetUserPermissions(int userId)
        {
            this.authorizer.AssertCanDo(UserActions.View, userId);

            var user = this.usersRepository.Find(userId);

            var programmes = this.programmesRepository.GetProgrammesIdAndShortName();
            var programmeIds = programmes.Keys.ToArray();

            return new PermissionTable(programmes, user.GetUserPermissions(programmeIds), user.GetPermissionTemplate(programmeIds), null);
        }
    }
}
