using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/locations")]
    public class ProcedureLocationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;

        public ProcedureLocationsController(IUnitOfWork unitOfWork, IProceduresRepository proceduresRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureLocationsVO> GetProcedureLocations(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureLocations(procedureId);
        }

        [Route("{locationId:int}")]
        public ProcedureLocationDO GetProcedureLocation(int procedureId, int locationId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureLocation = procedure.FindProcedureLocation(locationId);

            return new ProcedureLocationDO(procedureLocation, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureLocationDO NewProcedureLocation(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureLocationDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Locations.Create), IdParam = "procedureId")]
        public void AddProcedureAppDoc(int procedureId, ProcedureLocationDO location)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, location.Version);

            procedure.AddProcedureLocation(
                location.NutsLevel,
                location.CountryId,
                location.Nuts1Id,
                location.Nuts2Id,
                location.DistrictId,
                location.MunicipalityId,
                location.SettlementId,
                location.ProtectedZoneId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{locationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Locations.Edit), IdParam = "procedureId", ChildIdParam = "locationId")]
        public void UpdateProcedureLocation(int procedureId, int locationId, ProcedureLocationDO location)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, location.Version);

            procedure.UpdateProcedureLocation(
                locationId,
                location.NutsLevel,
                location.CountryId,
                location.Nuts1Id,
                location.Nuts2Id,
                location.DistrictId,
                location.MunicipalityId,
                location.SettlementId,
                location.ProtectedZoneId);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{locationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Locations.Delete), IdParam = "procedureId", ChildIdParam = "locationId")]
        public void DeleteProcedureLocation(int procedureId, int locationId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureLocation(locationId);

            this.unitOfWork.Save();
        }
    }
}
