using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Guidances.Repositories;
using Eumis.Data.Guidances.ViewObjects;
using Eumis.Domain.Guidances;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Guidances.DataObjects;

namespace Eumis.Web.Api.Guidances.Controllers
{
    [RoutePrefix("api/guidances")]
    public class GuidancesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IGuidancesRepository guidancesRepository;
        private IAuthorizer authorizer;
        private UserClaimsContextFactory userClaimsContextFactory;
        private IAccessContext accessContext;
        private IUserClaimsContext currentUserClaimsContext;

        public GuidancesController(
            IUnitOfWork unitOfWork,
            IGuidancesRepository guidancesRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            UserClaimsContextFactory userClaimsContextFactory)
        {
            this.unitOfWork = unitOfWork;
            this.guidancesRepository = guidancesRepository;
            this.authorizer = authorizer;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.accessContext = accessContext;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<GuidanceDataVO> GetGuidances()
        {
            this.authorizer.AssertCanDo(GuidanceListActions.Search);

            return this.guidancesRepository.GetGuidances();
        }

        [Route("~/api/navGuidances")]
        public IList<GuidanceVO> GetNavGuidances()
        {
            return this.guidancesRepository.GetGuidances(GuidanceModule.InternalSystem);
        }

        [Route("{guidanceId:int}")]
        public GuidanceDO GetGuidance(int guidanceId)
        {
            this.authorizer.AssertCanDo(GuidanceActions.View, guidanceId);

            var guidance = this.guidancesRepository.Find(guidanceId);

            var userClaimsContext = this.userClaimsContextFactory(guidance.CreatedByUserId);
            var username = userClaimsContext.Fullname + "(" + userClaimsContext.Username + ")";

            return new GuidanceDO(guidance, username);
        }

        [HttpPut]
        [Route("{guidanceId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Guidances.Edit), IdParam = "guidanceId")]
        public void UpdateGuidance(int guidanceId, GuidanceDO guidance)
        {
            this.authorizer.AssertCanDo(GuidanceActions.Edit, guidanceId);

            var oldGuidance = this.guidancesRepository.FindForUpdate(guidanceId, guidance.Version);

            oldGuidance.UpdateAttributes(
                guidance.Module.Value,
                guidance.Description,
                guidance.File.Key,
                guidance.File.Name);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public GuidanceDO NewGuidance()
        {
            this.authorizer.AssertCanDo(GuidanceListActions.Create);

            var username = this.currentUserClaimsContext.Fullname + "(" + this.currentUserClaimsContext.Username + ")";

            return new GuidanceDO(username);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Guidances.Create))]
        public object CreateGuidance(GuidanceDO guidance)
        {
            this.authorizer.AssertCanDo(GuidanceListActions.Create);

            var newGuidance = new Guidance(
                guidance.File.Key,
                guidance.File.Name,
                guidance.Description,
                guidance.Module.Value,
                this.accessContext.UserId);

            this.guidancesRepository.Add(newGuidance);

            this.unitOfWork.Save();

            return new { GuidanceId = newGuidance.GuidanceId };
        }

        [HttpDelete]
        [Route("{guidanceId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Guidances.Delete), IdParam = "guidanceId")]
        public void DeleteGuidance(int guidanceId, string version)
        {
            this.authorizer.AssertCanDo(GuidanceActions.Edit, guidanceId);

            byte[] vers = System.Convert.FromBase64String(version);
            var guidance = this.guidancesRepository.FindForUpdate(guidanceId, vers);

            this.guidancesRepository.Remove(guidance);

            this.unitOfWork.Save();
        }
    }
}
