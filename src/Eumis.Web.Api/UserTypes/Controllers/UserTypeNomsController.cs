using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.UserTypes.Repositories;
using Eumis.Domain.UserTypes;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.UserTypes.Controllers
{
    [RoutePrefix("api/nomenclatures/userTypes")]
    public class UserTypeNomsController : EntityNomsController<UserType, EntityNomVO>
    {
        private IUserTypeNomsRepository userTypeNomsRepository;

        public UserTypeNomsController(IUserTypeNomsRepository userTypeNomsRepository)
            : base(userTypeNomsRepository)
        {
            this.userTypeNomsRepository = userTypeNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetUserTypesByOrganization(int userOrganizationId, string term = null, int offset = 0, int? limit = null)
        {
            return this.userTypeNomsRepository.GetUserTypeNoms(userOrganizationId, term, offset, limit);
        }
    }
}
