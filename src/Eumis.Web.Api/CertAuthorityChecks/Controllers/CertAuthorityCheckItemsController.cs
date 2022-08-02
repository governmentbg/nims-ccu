using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertAuthorityChecks.Repositories;
using Eumis.Data.CertAuthorityChecks.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/certAuthorityChecks/{certAuthorityCheckId:int}/items")]
    public class CertAuthorityCheckItemsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICertAuthorityChecksRepository certAuthorityChecksRepository;

        public CertAuthorityCheckItemsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            ICertAuthorityChecksRepository certAuthorityChecksRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.certAuthorityChecksRepository = certAuthorityChecksRepository;
        }

        [Route("~/api/programmeCertAuthorityChecks/{certAuthorityCheckId:int}")]
        public IList<CertAuthorityCheckProgrammeItemVO> GetProgrammes(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetNotIncludedProgrammes(certAuthorityCheckId);
        }

        [Route("programmes")]
        public IList<CertAuthorityCheckProgrammeItemVO> GetProgrammeItems(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetProgrammeItems(certAuthorityCheckId);
        }

        [Route("~/api/programmePriorityCertAuthorityChecks/{certAuthorityCheckId:int}")]
        public IList<CertAuthorityCheckProgrammePriorityItemVO> GetProgrammePriorities(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetNotIncludedProgrammePriorities(certAuthorityCheckId);
        }

        [Route("programmePriorities")]
        public IList<CertAuthorityCheckProgrammePriorityItemVO> GetProgrammePriorityItems(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetProgrammePriorityItems(certAuthorityCheckId);
        }

        [Route("~/api/procedureCertAuthorityChecks/{certAuthorityCheckId:int}")]
        public IList<CertAuthorityCheckProcedureItemVO> GetProcedures(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetNotIncludedProcedures(certAuthorityCheckId);
        }

        [Route("procedures")]
        public IList<CertAuthorityCheckProcedureItemVO> GetProcedureItems(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetProcedureItems(certAuthorityCheckId);
        }

        [Route("~/api/contractCertAuthorityChecks/{certAuthorityCheckId:int}")]
        public IList<CertAuthorityCheckContractItemVO> GetContracts(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetNotIncludedContracts(certAuthorityCheckId);
        }

        [Route("contracts")]
        public IList<CertAuthorityCheckContractItemVO> GetContractItems(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetContractItems(certAuthorityCheckId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Items.Create), IdParam = "certAuthorityCheckId")]
        public void CreateItem(int certAuthorityCheckId, string version, int[] itemIds)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);
            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            foreach (var itemId in itemIds)
            {
                certAuthorityCheck.AddCertAuthorityCheckLevelItem(itemId);
            }

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{itemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Items.Delete), IdParam = "certAuthorityCheckId", ChildIdParam = "itemId")]
        public void DeleteItem(int certAuthorityCheckId, int itemId, string version)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);
            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            certAuthorityCheck.RemoveCertAuthorityCheckLevelItem(itemId);
            this.unitOfWork.Save();
        }
    }
}
