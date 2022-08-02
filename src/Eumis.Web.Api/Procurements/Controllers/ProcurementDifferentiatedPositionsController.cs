using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Procurements.Repositories;
using Eumis.Data.Procurements.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procurements.DataOjects;

namespace Eumis.Web.Api.Procurements.Controllers
{
    [RoutePrefix("api/procurements/{procurementId}/differentiatedPositions")]
    public class ProcurementDifferentiatedPositionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProcurementsRepository procurementsRepository;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public ProcurementDifferentiatedPositionsController(
            IUnitOfWork unitOfWork,
            IProcurementsRepository procurementsRepository,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.procurementsRepository = procurementsRepository;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ProcurementDifferentiatedPositionVO> GetProcurementDifferentiatedPositions(int procurementId)
        {
            return this.procurementsRepository.GetProcurementDifferentiatedPositions(procurementId);
        }

        [Route("{documentId:int}")]
        public ProcurementDifferentiatedPositionDO GetProcurementDifferentiatedPosition(int procurementId, int documentId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var procurement = this.procurementsRepository.Find(procurementId);

            var procurementDifferentiatedPosition = procurement.FindProcurementDifferentiatedPosition(documentId);

            return new ProcurementDifferentiatedPositionDO(procurementDifferentiatedPosition, procurement.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcurementDifferentiatedPositionDO NewProcurementDifferentiatedPosition(int procurementId)
        {
            var procurement = this.procurementsRepository.Find(procurementId);

            return new ProcurementDifferentiatedPositionDO(procurementId, procurement.Version);
        }

        [HttpPut]
        [Route("{positionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Edit.DifferentiatedPositions.Edit), IdParam = "procurementId", ChildIdParam = "documentId")]
        public void UpdateProcurementDifferentiatedPosition(int procurementId, int positionId, ProcurementDifferentiatedPositionDO position)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var procurement = this.procurementsRepository.FindForUpdate(procurementId, position.Version);

            procurement.UpdateProcurementDifferentiatedPosition(
                positionId,
                position.Name,
                position.Comment,
                position.CompanyId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Edit.DifferentiatedPositions.Create), IdParam = "procurementId")]
        public object AddProcurementDifferentiatedPosition(int procurementId, ProcurementDifferentiatedPositionDO position)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var procurement = this.procurementsRepository.FindForUpdate(procurementId, position.Version);

            var newProcurementDifferentiatedPosition = procurement.CreateDifferentiatedPosition(
                position.Name,
                position.Comment,
                position.CompanyId);

            this.unitOfWork.Save();

            return new { ProcurementDifferentiatedPositionId = newProcurementDifferentiatedPosition.ProcurementDifferentiatedPositionId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Edit.DifferentiatedPositions.Delete), IdParam = "procurementId", ChildIdParam = "positionId")]
        public void DeleteProcurementDifferentiatedPosition(int procurementId, int positionId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            byte[] vers = System.Convert.FromBase64String(version);

            var procurement = this.procurementsRepository.FindForUpdate(procurementId, vers);

            procurement.RemoveDifferentiatedPosition(positionId);

            this.unitOfWork.Save();
        }
    }
}
