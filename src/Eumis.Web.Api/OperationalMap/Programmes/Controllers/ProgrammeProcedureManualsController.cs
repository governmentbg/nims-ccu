using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Domain;
using Eumis.Domain.OperationalMap.ProcedureManuals;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/programmes/{programmeId}/procedureManuals")]
    public class ProgrammeProcedureManualsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IProgrammesRepository programmesRepository;
        private IUsersRepository usersRepository;
        private IRelationsRepository relationsRepository;
        private IAuthorizer authorizer;

        public ProgrammeProcedureManualsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IProgrammesRepository programmesRepository,
            IUsersRepository usersRepository,
            IRelationsRepository relationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.programmesRepository = programmesRepository;
            this.usersRepository = usersRepository;
            this.relationsRepository = relationsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProgrammeProcedureManualsVO> GetProgrammeProcedureManuals(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            return this.programmesRepository.GetProgrammeProcedureManuals(programmeId);
        }

        [Route("{programmeProcedureManualId:int}")]
        public ProgrammeProcedureManualDO GetProgrammeProcedureManual(int programmeId, int programmeProcedureManualId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            this.relationsRepository.AssertProgrammeHasProcedureManual(programmeId, programmeProcedureManualId);

            var programme = this.programmesRepository.Find(programmeId);
            var programmeProcedureManual = programme.FindProgrammeProcedureManual(programmeProcedureManualId);

            string username = string.Empty;

            if (programmeProcedureManual.ActivatedByUserId != null)
            {
                var user = this.usersRepository.FindWithoutIncludes(programmeProcedureManual.ActivatedByUserId.Value);

                if (user == null)
                {
                    throw new DomainObjectNotFoundException("Cannot find User with id " + programmeProcedureManual.ActivatedByUserId.Value);
                }

                username = $"{user.Fullname}({user.Username})";
            }

            return new ProgrammeProcedureManualDO(programmeProcedureManual, username, programme.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProgrammeProcedureManualDO NewProgrammeProcedureManual(int programmeId)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            var programme = this.programmesRepository.Find(programmeId);

            var orderNum = programme.GetProgrammeProcedureManualNextOrderNum();

            return new ProgrammeProcedureManualDO(programme.MapNodeId, orderNum, programme.Version);
        }

        [HttpPut]
        [Route("{programmeProcedureManualId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ProgrammeProcedureManuals.Edit), IdParam = "programmeId", ChildIdParam = "programmeProcedureManualId")]
        public void UpdateProgrammeProcedureManual(int programmeId, int programmeProcedureManualId, ProgrammeProcedureManualDO programmeProcedureManual)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            this.relationsRepository.AssertProgrammeHasProcedureManual(programmeId, programmeProcedureManualId);

            Programme programme = this.programmesRepository.FindForUpdate(programmeId, programmeProcedureManual.Version);

            programme.UpdateProgrammeProcedureManual(
                programmeProcedureManualId,
                programmeProcedureManual.Name,
                programmeProcedureManual.Description,
                programmeProcedureManual.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeProcedureManualId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ProgrammeProcedureManuals.ChangeStatusToActual), IdParam = "programmeId", ChildIdParam = "programmeProcedureManualId")]
        public void ChangeStatusToActual(int programmeId, int programmeProcedureManualId, string version)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            this.relationsRepository.AssertProgrammeHasProcedureManual(programmeId, programmeProcedureManualId);

            byte[] vers = System.Convert.FromBase64String(version);

            Programme programme = this.programmesRepository.FindForUpdate(programmeId, vers);

            var userId = this.accessContext.UserId;

            programme.ChangeProgrammeProcedureManualStatusToActual(programmeProcedureManualId, userId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canAddProgrammeProcedureManual")]
        public ErrorsDO CanAddProgrammeProcedureManual(int programmeId)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            Programme programme = this.programmesRepository.Find(programmeId);

            var errorList = programme.CanAddProgrammeProcedureManual();

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ProgrammeProcedureManuals.Create), IdParam = "programmeId")]
        public object AddProgrammeProcedureManual(int programmeId, ProgrammeProcedureManualDO programmeProcedureManual)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            Programme programme = this.programmesRepository.FindForUpdate(programmeId, programmeProcedureManual.Version);

            var newProgrammeProcedureManual = new ProgrammeProcedureManual()
            {
                Name = programmeProcedureManual.Name,
                Description = programmeProcedureManual.Description,
                OrderNum = programmeProcedureManual.OrderNum,
                Status = programmeProcedureManual.Status,
                BlobKey = programmeProcedureManual.File.Key,
            };

            programme.AddProgrammeProcedureManual(newProgrammeProcedureManual);

            this.unitOfWork.Save();

            return new { ProcedureManualId = newProgrammeProcedureManual.ProgrammeProcedureManualId };
        }

        [HttpDelete]
        [Route("{programmeProcedureManualId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ProgrammeProcedureManuals.Delete), IdParam = "programmeId", ChildIdParam = "programmeProcedureManualId")]
        public void DeleteProgrammeProcedureManual(int programmeId, int programmeProcedureManualId, string version)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            this.relationsRepository.AssertProgrammeHasProcedureManual(programmeId, programmeProcedureManualId);

            byte[] vers = System.Convert.FromBase64String(version);
            Programme programme = this.programmesRepository.FindForUpdate(programmeId, vers);

            programme.RemoveProgrammeProcedureManual(programmeProcedureManualId);

            this.unitOfWork.Save();
        }
    }
}
