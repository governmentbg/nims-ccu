using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertAuthorityChecks.Repositories;
using Eumis.Data.CertAuthorityChecks.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certAuthorityChecks/{certAuthorityCheckId:int}/projects")]
    public class CertAuthorityCheckProjectsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICertAuthorityChecksRepository certAuthorityChecksRepository;

        public CertAuthorityCheckProjectsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            ICertAuthorityChecksRepository certAuthorityChecksRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.certAuthorityChecksRepository = certAuthorityChecksRepository;
        }

        [Route("")]
        public IList<CertAuthorityCheckProjectVO> GetContractItems(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetCertAuthorityCheckProjects(certAuthorityCheckId);
        }

        [Route("~/api/projectCertAuthorityChecks/{certAuthorityCheckId:int}")]
        public IList<CertAuthorityCheckProjectVO> GetProjects(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetNotIncludedProjects(certAuthorityCheckId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Items.Create), IdParam = "certAuthorityCheckId")]
        public void CreateItem(int certAuthorityCheckId, string version, int[] projectsIds)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);
            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            foreach (var projectId in projectsIds)
            {
                certAuthorityCheck.AddCertAuthorityCheckProject(projectId);
            }

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{projectId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.CertificationDocuments.Delete), IdParam = "certAuthorityCheckId", ChildIdParam = "projectId")]
        public void DeleteCertAuthorityCheckProject(int certAuthorityCheckId, int projectId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            var cacp = this.certAuthorityChecksRepository.Find(certAuthorityCheckId);

            cacp.RemoveCertAuthorityCheckProject(projectId);

            this.unitOfWork.Save();
        }
    }
}
