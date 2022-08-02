using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Procurements.Repositories;
using Eumis.Data.Procurements.ViewObjects;
using Eumis.Domain.Procurements;
using Eumis.Domain.Users;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Procurements.DataOjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Eumis.Web.Api.Procurements.Controllers
{
    [RoutePrefix("api/procurements")]
    public class ProcurementsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProcurementsRepository procurementsRepository;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;

        public ProcurementsController(
            IUnitOfWork unitOfWork,
            IProcurementsRepository procurementsRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.procurementsRepository = procurementsRepository;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
        }

        [Route("")]
        public IList<ProcurementVO> GetProcurements()
        {
            return this.procurementsRepository.GetProcurements();
        }

        [HttpGet]
        [Route("new")]
        public ProcurementDO NewProcurement()
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            return new ProcurementDO();
        }

        [HttpPost]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Create))]
        public ProcurementDO CreateProcurement(ProcurementDO procurement)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            Procurement newProcurement = new Procurement(
                procurement.Name,
                procurement.ShortName,
                procurement.ErrandAreaId,
                procurement.ErrandLegalActId,
                procurement.ErrandTypeId,
                procurement.PrognosysAmount,
                procurement.Description,
                procurement.InternetAddress,
                procurement.ExpectedAmount,
                procurement.PPANumber,
                procurement.PlanDate,
                procurement.OffersDeadlineDate,
                procurement.AnnouncedDate);

            this.procurementsRepository.Add(newProcurement);

            this.unitOfWork.Save();

            return new ProcurementDO(newProcurement);
        }

        [Route("{procurementId:int}")]
        public ProcurementDO GetProcurement(int procurementId)
        {
            var procurement = this.procurementsRepository.Find(procurementId);

            return new ProcurementDO(procurement);
        }

        [Route("{procurementId:int}/info")]
        public ProcurementInfoVO GetProcurementInfo(int procurementId)
        {
            var info = this.procurementsRepository.GetProcurementInfo(procurementId);

            return info;
        }

        [HttpPut]
        [Route("{procurementId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Edit.BasicData), IdParam = "procurementId")]
        public void UpdateProcurement(int procurementId, ProcurementDO procurement)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            Procurement oldProcurement = this.procurementsRepository.FindForUpdate(procurementId, procurement.Version);

            oldProcurement.SetAttributes(
                procurement.Name,
                procurement.ShortName,
                procurement.ErrandAreaId,
                procurement.ErrandLegalActId,
                procurement.ErrandTypeId,
                procurement.PrognosysAmount,
                procurement.Description,
                procurement.InternetAddress,
                procurement.ExpectedAmount,
                procurement.PPANumber,
                procurement.PlanDate,
                procurement.OffersDeadlineDate,
                procurement.AnnouncedDate);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procurementId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.ChangeStatusToDraft), IdParam = "procurementId")]
        public void ChangeStatusToDraft(int procurementId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            this.ChangeStatus(procurementId, Convert.FromBase64String(version), ProcurementStatus.Draft);
        }

        [HttpPut]
        [Route("{procurementId:int}/changeStatusToActive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.ChangeStatusToActive), IdParam = "procurementId")]
        public void ChangeStatusToActive(int procurementId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            this.ChangeStatus(procurementId, Convert.FromBase64String(version), ProcurementStatus.Active);
        }

        [HttpPut]
        [Route("{procurementId:int}/changeStatusToCanceled")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.ChangeStatusToCanceled), IdParam = "procurementId")]
        public void ChangeStatusToCanceled(int procurementId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            this.ChangeStatus(procurementId, Convert.FromBase64String(version), ProcurementStatus.Canceled);
        }

        [HttpPost]
        [Route("{procurementId:int}/canChangeStatusToActive")]
        public ErrorsDO CanChangeStatusToActive(int procurementId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            Procurement oldProcurement = this.procurementsRepository.Find(procurementId);

            return new ErrorsDO(oldProcurement.CanAcivate());
        }

        [HttpDelete]
        [Route("{procurementId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Delete), IdParam = "procurementId")]
        public void RemoveProcurement(int procurementId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            Procurement oldProcurement = this.procurementsRepository.FindForUpdate(procurementId, version);

            this.procurementsRepository.Remove(oldProcurement);

            this.unitOfWork.Save();
        }

        private void ChangeStatus(int procurementId, byte[] version, ProcurementStatus procurementStatus)
        {
            Procurement oldProcurement = this.procurementsRepository.FindForUpdate(procurementId, version);

            oldProcurement.ChangeStatus(procurementStatus);

            this.unitOfWork.Save();
        }
    }
}
