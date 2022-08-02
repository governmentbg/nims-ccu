using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertAuthorityChecks.Repositories;
using Eumis.Data.CertAuthorityChecks.ViewObjects;
using Eumis.Data.Counters;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.CertAuthorityChecks.DataObjects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/certAuthorityChecks")]
    public class CertAuthorityChecksController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICountersRepository countersRepository;
        private ICertAuthorityChecksRepository certAuthorityChecksRepository;

        public CertAuthorityChecksController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            ICountersRepository countersRepository,
            ICertAuthorityChecksRepository certAuthorityChecksRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.countersRepository = countersRepository;
            this.certAuthorityChecksRepository = certAuthorityChecksRepository;
        }

        [Route("")]
        public IList<CertAuthorityCheckVO> GetCertAuthorityChecks(CertAuthorityCheckStatus? status = null, CertAuthorityCheckType? type = null)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckListActions.Search);

            return this.certAuthorityChecksRepository.GetCertAuthorityCheks(status, type);
        }

        [HttpGet]
        [Route("new")]
        public NewCertAuthorityCheckDO CreateCertAuthorityCheck()
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckListActions.Create);

            return new NewCertAuthorityCheckDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Create))]
        public object CreateCertAuthorityCheck(NewCertAuthorityCheckDO newCheckDO)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckListActions.Create);

            var newCertAuthorityCheck = new CertAuthorityCheck(
                newCheckDO.Level.Value,
                newCheckDO.Kind.Value,
                newCheckDO.Type.Value);

            this.certAuthorityChecksRepository.Add(newCertAuthorityCheck);
            this.unitOfWork.Save();

            return new { CertAuthorityCheckId = newCertAuthorityCheck.CertAuthorityCheckId };
        }

        [Route("{certAuthorityCheckId:int}/info")]
        public CertAuthorityCheckInfoVO GetCertAuthorityCheckInfo(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetInfo(certAuthorityCheckId);
        }

        [Route("{certAuthorityCheckId:int}")]
        public CertAuthorityCheckDO GetCertAuthorityCheckData(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.Find(certAuthorityCheckId);

            return new CertAuthorityCheckDO(certAuthorityCheck);
        }

        [HttpPut]
        [Route("{certAuthorityCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.CheckData), IdParam = "certAuthorityCheckId")]
        public void UpdateCertAuthorityCheckData(int certAuthorityCheckId, CertAuthorityCheckDO certAuthorityCheckDO)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, certAuthorityCheckDO.Version);

            certAuthorityCheck.UpdateCheckData(
                certAuthorityCheckDO.Kind,
                certAuthorityCheckDO.Type,
                certAuthorityCheckDO.DateFrom,
                certAuthorityCheckDO.DateTo,
                certAuthorityCheckDO.SubjectType,
                certAuthorityCheckDO.SubjectName,
                certAuthorityCheckDO.Team);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{certAuthorityCheckId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.ChangeStatusToEntered), IdParam = "certAuthorityCheckId")]
        public void EnterCertAuthorityCheck(int certAuthorityCheckId, string version)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);
            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            if (certAuthorityCheck.IsActivated)
            {
                certAuthorityCheck.ChangeStatusToEntered();
            }
            else
            {
                this.countersRepository.CreateCertAuthorityCheckCounter();
                var number = this.countersRepository.GetNextCertAuthorityCheckNumber();

                certAuthorityCheck.ChangeStatusToEntered(number);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{certAuthorityCheckId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.ChangeStatusToDraft), IdParam = "certAuthorityCheckId")]
        public void MakeDraft(int certAuthorityCheckId, string version)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);
            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            certAuthorityCheck.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{certAuthorityCheckId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.ChangeStatusToRemoved), IdParam = "certAuthorityCheckId")]
        public void MakeDraft(int certAuthorityCheckId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);
            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            certAuthorityCheck.ChangeStatusToRemoved(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{certAuthorityCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Delete), IdParam = "certAuthorityCheckId")]
        public void DeleteCertAuthorityCheckAscertainment(int certAuthorityCheckId, string version)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);
            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            this.certAuthorityChecksRepository.Remove(certAuthorityCheck);

            this.unitOfWork.Save();
        }
    }
}
