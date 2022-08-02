using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.Users.Repositories;
using Eumis.Data.Users.ViewObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/users/{userId}/declarations")]
    public class UserDeclarationsController : ApiController
    {
        private IUsersRepository usersRepository;
        private IAuthorizer authorizer;

        public UserDeclarationsController(
            IUsersRepository usersRepository,
            IAuthorizer authorizer)
        {
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<UserDeclarationVO> GetUserDeclarations(int userId)
        {
            this.authorizer.AssertCanDo(UserActions.View, userId);

            return this.usersRepository.GetUserDeclarations(userId);
        }
    }
}
