using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.OperationalMap.MapNodes.Controllers;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/programmes/{mapNodeId}/documents")]
    [ActionLogPrefix(typeof(ActionLogGroups.Programme.Edit.Documents))]
    public class ProgrammeDocumentsController : MapNodeDocumentsController
    {
        public ProgrammeDocumentsController(
            IUnitOfWork unitOfWork,
            IMapNodesRepository mapNodesRepository,
            IAuthorizer authorizer)
            : base(unitOfWork, mapNodesRepository, authorizer)
        {
        }
    }
}
