using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Users;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionResultUser")]
    public class EvalSessionResultUserNomsController : EntityNomsController<User, EntityNomVO>
    {
        public EvalSessionResultUserNomsController(IEvalSessionUserNomsRepository evalSessionUserNomsRepository)
            : base(evalSessionUserNomsRepository)
        {
        }
    }
}
