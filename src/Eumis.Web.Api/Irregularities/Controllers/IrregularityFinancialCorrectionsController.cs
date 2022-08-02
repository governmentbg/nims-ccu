using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Irregularity;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/irregularities/{irregularityId:int}/financialCorrections")]
    public class IrregularityFinancialCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private ICacheManager cacheManager;
        private IIrregularitiesRepository irregularitiesRepository;
        private IIrregularityService irregularityService;

        public IrregularityFinancialCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            ICacheManager cacheManager,
            IIrregularitiesRepository irregularitiesRepository,
            IIrregularityService irregularityService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.cacheManager = cacheManager;
            this.irregularitiesRepository = irregularitiesRepository;
            this.irregularityService = irregularityService;
        }

        [Route("~/api/irregularityFinancialCorrections/{irregularityId:int}")]
        public IList<IrregularityFinancialCorrectionVO> GetFinancialCorrections(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            return this.irregularitiesRepository.GetNotIncludedFinancialCorrections(irregularityId);
        }

        [Route("")]
        public IList<IrregularityFinancialCorrectionVO> GetFinancialCorrectionItems(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.View, irregularityId);

            return this.irregularitiesRepository.GetFinancialCorrections(irregularityId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.FinancialCorrections.Create), IdParam = "irregularityId")]
        public void AddFinancialCorrection(int irregularityId, string version, int[] itemIds)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            byte[] vers = System.Convert.FromBase64String(version);
            var irregularity = this.irregularitiesRepository.FindForUpdate(irregularityId, vers);

            this.irregularityService.AddFinancialCorrections(irregularity, this.accessContext.UserId, itemIds);

            // clear the cache for the updated irregularity
            this.cacheManager.ClearCache(ClaimsCaches.Irregularity, irregularityId);
        }

        [HttpDelete]
        [Route("{itemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.FinancialCorrections.Delete), IdParam = "irregularityId", ChildIdParam = "itemId")]
        public void DeleteFinancialCorrection(int irregularityId, int itemId, string version)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            byte[] vers = System.Convert.FromBase64String(version);
            var irregularity = this.irregularitiesRepository.FindForUpdate(irregularityId, vers);

            irregularity.RemoveFinancialCorrection(itemId);
            this.unitOfWork.Save();

            // clear the cache for the updated irregularity
            this.cacheManager.ClearCache(ClaimsCaches.Irregularity, irregularityId);
        }
    }
}
