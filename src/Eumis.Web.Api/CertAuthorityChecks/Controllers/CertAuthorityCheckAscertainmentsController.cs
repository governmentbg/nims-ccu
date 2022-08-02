using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertAuthorityChecks.Repositories;
using Eumis.Data.CertAuthorityChecks.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.CertAuthorityChecks.DataObjects;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/certAuthorityChecks/{certAuthorityCheckId:int}/ascertainments")]
    public class CertAuthorityCheckAscertainmentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICertAuthorityChecksRepository certAuthorityChecksRepository;

        public CertAuthorityCheckAscertainmentsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            ICertAuthorityChecksRepository certAuthorityChecksRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.certAuthorityChecksRepository = certAuthorityChecksRepository;
        }

        [Route("")]
        public IList<CertAuthorityCheckAscertainmentVO> GetCertAuthorityCheckAscertainments(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetCertAuthorityCheckAscertainments(certAuthorityCheckId);
        }

        [Route("{ascertainmentId:int}")]
        public CertAuthorityCheckAscertainmentDO GetCertAuthorityCheckAscertainment(int certAuthorityCheckId, int ascertainmentId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.Find(certAuthorityCheckId);

            var ascertainment = certAuthorityCheck.GetCertAuthorityCheckAscertainment(ascertainmentId);

            return new CertAuthorityCheckAscertainmentDO(ascertainment, certAuthorityCheck.Version);
        }

        [HttpGet]
        [Route("new")]
        public CertAuthorityCheckAscertainmentDO NewCertAuthorityCheckAscertainment(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.Find(certAuthorityCheckId);

            return new CertAuthorityCheckAscertainmentDO(certAuthorityCheckId, certAuthorityCheck.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Ascertainments.Create), IdParam = "certAuthorityCheckId")]
        public object AddCertAuthorityCheckAscertainment(int certAuthorityCheckId, CertAuthorityCheckAscertainmentDO ascertainment)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, ascertainment.Version);

            var nextOrderNum = this.certAuthorityChecksRepository.GetNextOrderNum(certAuthorityCheckId);

            var newAscertainment = certAuthorityCheck.AddCertAuthorityCheckAscertainment(
                nextOrderNum,
                ascertainment.Type.Value,
                ascertainment.Ascertainment,
                ascertainment.Status.Value,
                ascertainment.Recommendation,
                ascertainment.RecommendationDeadline,
                ascertainment.RecommendationExecutionStatus,
                ascertainment.CertAuthorityComment,
                ascertainment.ManagingAuthorityComment);

            this.unitOfWork.Save();

            return new { CertAuthorityCheckId = newAscertainment.CertAuthorityCheckId, AscertainmentId = newAscertainment.CertAuthorityCheckAscertainmentId };
        }

        [HttpPut]
        [Route("{ascertainmentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Ascertainments.Edit), IdParam = "certAuthorityCheckId", ChildIdParam = "ascertainmentId")]
        public void UpdateCertAuthorityCheckAscertainment(int certAuthorityCheckId, int ascertainmentId, CertAuthorityCheckAscertainmentDO ascertainment)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, ascertainment.Version);

            certAuthorityCheck.UpdateCertAuthorityCheckAscertainment(
                ascertainmentId,
                ascertainment.Type.Value,
                ascertainment.Ascertainment,
                ascertainment.Status.Value,
                ascertainment.Recommendation,
                ascertainment.RecommendationDeadline,
                ascertainment.RecommendationExecutionStatus,
                ascertainment.CertAuthorityComment,
                ascertainment.ManagingAuthorityComment);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{ascertainmentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Ascertainments.Delete), IdParam = "certAuthorityCheckId", ChildIdParam = "ascertainmentId")]
        public void DeleteCertAuthorityCheckAscertainment(int certAuthorityCheckId, int ascertainmentId, string version)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);
            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            certAuthorityCheck.RemoveCertAuthorityCheckAscertainment(ascertainmentId);

            this.unitOfWork.Save();
        }
    }
}
