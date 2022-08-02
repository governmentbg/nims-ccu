using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data;
using Eumis.Data.Users.Repositories;
using Eumis.Data.Users.ViewObjects;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/users/{userId}/requests")]
    public class UserRequestsController : ApiController
    {
        private IUsersRepository usersRepository;
        private IAuthorizer authorizer;

        public UserRequestsController(
            IUsersRepository usersRepository,
            IAuthorizer authorizer)
        {
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public UserRequestsWrapperVO GetUserRequests(int userId)
        {
            this.authorizer.AssertCanDo(UserActions.View, userId);

            return this.usersRepository.GetUserRequests(userId);
        }
    }
}
