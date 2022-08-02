using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.OperationalMap.MapNodes.Controllers;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.Controllers
{
    [RoutePrefix("api/programmePriorities/{mapNodeId}/documents")]
    [ActionLogPrefix(typeof(ActionLogGroups.ProgrammePriority.Edit.Documents))]
    public class ProgrammePriorityDocumentsController : MapNodeDocumentsController
    {
        public ProgrammePriorityDocumentsController(
            IUnitOfWork unitOfWork,
            IMapNodesRepository mapNodesRepository,
            IAuthorizer authorizer)
            : base(unitOfWork, mapNodesRepository, authorizer)
        {
        }
    }
}
