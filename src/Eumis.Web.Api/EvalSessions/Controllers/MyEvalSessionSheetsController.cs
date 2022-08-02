using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/myEvalSessions/{evalSessionId}/sheets")]
    public class MyEvalSessionSheetsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IEvalSessionSheetXmlsRepository evalSessionSheetXmlsRepository;

        public MyEvalSessionSheetsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IEvalSessionSheetXmlsRepository evalSessionSheetXmlsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.evalSessionSheetXmlsRepository = evalSessionSheetXmlsRepository;
        }

        [Route("")]
        public IList<EvalSessionSheetsVO> GetEvalSessionSheets(
            int evalSessionId,
            int? project = null,
            ProcedureEvalTableType? evalTableType = null,
            int? distribution = null,
            [FromUri] EvalSessionSheetStatus[] statuses = null)
        {
            this.authorizer.AssertCanDo(MyEvalSession.ViewSessionSheets, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionSheets(evalSessionId, project, evalTableType, distribution, null, this.accessContext.UserId, statuses);
        }

        [Route("{sheetId:int}")]
        public EvalSessionSheetDO GetEvalSessionSheet(int evalSessionId, int sheetId)
        {
            this.authorizer.AssertCanDo(MyEvalSessionSheetActions.Edit, sheetId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionSheet = evalSession.FindEvalSessionSheet(sheetId);

            var evalSessionSheetXml = this.evalSessionSheetXmlsRepository.FindByEvalSessionSheetId(sheetId);

            return new EvalSessionSheetDO(
                evalSessionSheet,
                evalSession.Version,
                evalSessionSheetXml);
        }
    }
}
