using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/users")]
    public class EvalSessionUsersController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionsRepository evalSessionsRepository;
        private IAuthorizer authorizer;
        private ICacheManager cacheManager;

        public EvalSessionUsersController(IUnitOfWork unitOfWork, IEvalSessionsRepository evalSessionsRepository, IAuthorizer authorizer, ICacheManager cacheManager)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionsRepository = evalSessionsRepository;
            this.authorizer = authorizer;
            this.cacheManager = cacheManager;
        }

        [Route("")]
        public IList<EvalSessionUsersVO> GetEvalSessionUsers(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSession, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionUsers(evalSessionId);
        }

        [Route("{evalSessionUserId:int}")]
        public EvalSessionUserDO GetEvalSessionUser(int evalSessionId, int evalSessionUserId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSession, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionUser = evalSession.FindEvalSessionUser(evalSessionUserId);

            return new EvalSessionUserDO(evalSessionUser, evalSession.Version);
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionUserDO NewEvalSessionUser(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            return new EvalSessionUserDO(evalSessionId, evalSession.Version);
        }

        [HttpPut]
        [Route("{evalSessionUserId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Users.Edit), IdParam = "evalSessionId", ChildIdParam = "evalSessionUserId")]
        public void UpdateEvalSessionUser(int evalSessionId, int evalSessionUserId, EvalSessionUserDO evalSessionUser)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionUser.Version);

            evalSession.UpdateEvalSessionUser(
                evalSessionUserId,
                evalSessionUser.Position);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Users.Create), IdParam = "evalSessionId")]
        public void AddEvalSessionUser(int evalSessionId, EvalSessionUserDO evalSessionUser)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionUser.Version);

                evalSession.AddEvalSessionUser(
                    evalSessionUser.UserId.Value,
                    evalSessionUser.Type.Value,
                    evalSessionUser.Position);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        [HttpDelete]
        [Route("{evalSessionUserId:int}")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Users.Delete), IdParam = "evalSessionId", ChildIdParam = "evalSessionUserId")]
        public void DeleteEvalSessionUser(int evalSessionId, int evalSessionUserId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            int userId;

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                byte[] vers = System.Convert.FromBase64String(version);
                EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

                userId = evalSession.FindEvalSessionUser(evalSessionUserId).UserId;
                evalSession.RemoveEvalSessionUser(evalSessionUserId);

                this.unitOfWork.Save();

                transaction.Commit();
            }

            // clear the cache for the updated user
            this.cacheManager.ClearCache(ClaimsCaches.User, userId);
        }

        [HttpPost]
        [Route("{evalSessionUserId:int}/activate")]
        public void ActivateEvalSessionUser(int evalSessionId, int evalSessionUserId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            int userId;

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                byte[] vers = System.Convert.FromBase64String(version);
                EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

                userId = evalSession.FindEvalSessionUser(evalSessionUserId).UserId;
                evalSession.ActivateEvalSessionUser(evalSessionUserId);

                this.unitOfWork.Save();

                transaction.Commit();
            }

            // clear the cache for the updated user
            this.cacheManager.ClearCache(ClaimsCaches.User, userId);
        }

        [HttpPost]
        [Route("{evalSessionUserId:int}/deactivate")]
        public void DeactivateEvalSessionUser(int evalSessionId, int evalSessionUserId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            int userId;

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                byte[] vers = System.Convert.FromBase64String(version);
                EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

                userId = evalSession.FindEvalSessionUser(evalSessionUserId).UserId;
                evalSession.DeactivateEvalSessionUser(evalSessionUserId);

                this.unitOfWork.Save();

                transaction.Commit();
            }

            // clear the cache for the updated user
            this.cacheManager.ClearCache(ClaimsCaches.User, userId);
        }

        [HttpPost]
        [Route("{evalSessionUserId:int}/canDelete")]
        public ErrorsDO CanDeleteEvalSessionUser(int evalSessionId, int evalSessionUserId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            var errorList = this.evalSessionsRepository.CanDeleteEvalSessionUser(evalSessionId, evalSessionUserId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("canAdd")]
        public ErrorsDO CanAddEvalSessionUser(int evalSessionId, int userId, EvalSessionUserType userType)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            var errorList = this.evalSessionsRepository.CanAddEvalSessionUser(evalSessionId, userId, userType);

            return new ErrorsDO(errorList);
        }
    }
}
