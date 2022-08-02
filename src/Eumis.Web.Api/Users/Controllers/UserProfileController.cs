using Eumis.Common.Auth;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Data.Users.ViewObjects;
using Eumis.Domain.Users.PermissionTables;
using Eumis.Web.Api.Users.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/userProfile")]
    public class UserProfileController : ApiController
    {
        private IUsersRepository usersRepository;
        private IProgrammesRepository programmesRepository;
        private IAccessContext accessContext;

        public UserProfileController(
            IUsersRepository usersRepository,
            IProgrammesRepository programmesRepository,
            IAccessContext accessContext)
        {
            this.usersRepository = usersRepository;
            this.programmesRepository = programmesRepository;
            this.accessContext = accessContext;
        }

        [Route("")]
        public UserDO GetUser()
        {
            var user = this.usersRepository.Find(this.accessContext.UserId);

            return new UserDO(user);
        }

        [Route("userInfo")]
        public UserInfoDO GetUserInfo()
        {
            var user = this.usersRepository.Find(this.accessContext.UserId);

            return new UserInfoDO(user);
        }

        [Route("permissions")]
        public PermissionTable GetUserPermissions()
        {
            var user = this.usersRepository.Find(this.accessContext.UserId);

            var programmes = this.programmesRepository.GetProgrammesIdAndShortName();
            var programmeIds = programmes.Keys.ToArray();

            return new PermissionTable(programmes, user.GetUserPermissions(programmeIds), user.GetPermissionTemplate(programmeIds), null);
        }

        [Route("requests")]
        public UserRequestsWrapperVO GetUserRequests()
        {
            return this.usersRepository.GetUserRequests(this.accessContext.UserId);
        }

        [Route("declarations")]
        public IList<UserDeclarationVO> GetUserDeclarations()
        {
            return this.usersRepository.GetUserDeclarations(this.accessContext.UserId);
        }
    }
}
