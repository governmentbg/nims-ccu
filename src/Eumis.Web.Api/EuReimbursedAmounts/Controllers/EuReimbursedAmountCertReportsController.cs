using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.EuReimbursedAmount;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.EuReimbursedAmounts.Repositories;
using Eumis.Data.EuReimbursedAmounts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.EuReimbursedAmounts.Controllers
{
    [RoutePrefix("api/euReimbursedAmounts/{euReimbursedAmountId:int}/certReports")]
    public class EuReimbursedAmountCertReportsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IEuReimbursedAmountsRepository euReimbursedAmountsRepository;
        private IEuReimbursedAmountService euReimbursedAmountService;

        public EuReimbursedAmountCertReportsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IEuReimbursedAmountsRepository euReimbursedAmountsRepository,
            IEuReimbursedAmountService euReimbursedAmountService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.euReimbursedAmountsRepository = euReimbursedAmountsRepository;
            this.euReimbursedAmountService = euReimbursedAmountService;
        }

        [Route("")]
        public IList<EuReimbursedAmountCertReportVO> GetCertReportItems(int euReimbursedAmountId)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            return this.euReimbursedAmountsRepository.GetCertReports(euReimbursedAmountId);
        }

        [Route("~/api/certReportEuReimbursedAmounts/{euReimbursedAmountId:int}")]
        public IList<EuReimbursedAmountCertReportVO> GetCertReports(int euReimbursedAmountId)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            return this.euReimbursedAmountsRepository.GetNotIncludedCertReports(euReimbursedAmountId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EuReimbursedAmounts.Edit.CertReports.Create), IdParam = "euReimbursedAmountId")]
        public void CreateItem(int euReimbursedAmountId, string version, int[] itemIds)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var euReimbursedAmount = this.euReimbursedAmountsRepository.FindForUpdate(euReimbursedAmountId, vers);

            this.euReimbursedAmountService.AddCertReports(euReimbursedAmount, this.accessContext.UserId, itemIds);
        }

        [HttpDelete]
        [Route("{itemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EuReimbursedAmounts.Edit.CertReports.Delete), IdParam = "euReimbursedAmountId", ChildIdParam = "itemId")]
        public void DeleteItem(int euReimbursedAmountId, int itemId, string version)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var euReimbursedAmount = this.euReimbursedAmountsRepository.FindForUpdate(euReimbursedAmountId, vers);

            euReimbursedAmount.RemoveCertReport(itemId);
            this.unitOfWork.Save();
        }
    }
}
