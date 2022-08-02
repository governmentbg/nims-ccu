using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Irregularity;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Counters;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain;
using Eumis.Domain.Irregularities;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Irregularities.DataObjects;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/irregularitySignals")]
    public class IrregularitySignalsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IProjectsRepository projectsRepository;
        private IProceduresRepository proceduresRepository;
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private ICountersRepository countersRepository;
        private IIrregularitySignalsRepository irregularitySignalsRepository;
        private IIrregularitySignalService irregularitySignalService;

        public IrregularitySignalsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IProjectsRepository projectsRepository,
            IProceduresRepository proceduresRepository,
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            ICountersRepository countersRepository,
            IIrregularitySignalsRepository irregularitySignalsRepository,
            IIrregularitySignalService irregularitySignalService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.projectsRepository = projectsRepository;
            this.proceduresRepository = proceduresRepository;
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.countersRepository = countersRepository;
            this.irregularitySignalsRepository = irregularitySignalsRepository;
            this.irregularitySignalService = irregularitySignalService;
        }

        [Route("")]
        public IList<IrregularitySignalVO> GetIrregularitySignals(
            int? contractId = null,
            IrregularitySignalStatus? status = null)
        {
            this.authorizer.AssertCanDo(IrregularitySignalListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            return this.irregularitySignalsRepository.GetIrregularitySignals(programmeIds, this.accessContext.UserId, contractId, status);
        }

        [Route("~/api/projectDossier/{projectId:int}/irregularitySignals")]
        public IList<IrregularitySignalVO> GetIrregularitySignalsForProjectDossier(int projectId, int? contractId = null)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);

            return this.irregularitySignalsRepository.GetIrregularitySignalsForProjectDossier(projectId, contractId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Create))]
        public object CreateIrregularitySignal(string contractNumber = null, string projectNumber = null)
        {
            this.authorizer.AssertCanDo(IrregularitySignalListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNumber);
            var project = this.projectsRepository.FindByRegNumber(projectNumber);
            IrregularitySignal newSignal = null;

            if (this.irregularitySignalService.CanCreateSignal(this.accessContext.UserId, contract, project).Any())
            {
                throw new DomainValidationException("Cannot create irregularity signal.");
            }

            if (contract != null)
            {
                newSignal = new IrregularitySignal(
                    contract.ProgrammeId,
                    contract.ProcedureId,
                    contract.ProjectId,
                    contract.ContractId);
            }
            else if (project != null)
            {
                newSignal = new IrregularitySignal(
                    this.proceduresRepository.GetPrimaryProcedureProgrammeId(project.ProcedureId),
                    project.ProcedureId,
                    project.ProjectId);
            }
            else
            {
                throw new DomainValidationException("Cannot create irregularity signal.");
            }

            this.irregularitySignalsRepository.Add(newSignal);
            this.unitOfWork.Save();

            return new { SignalId = newSignal.IrregularitySignalId };
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateIrregularitySignal(string contractNumber = null, string projectNumber = null)
        {
            this.authorizer.AssertCanDo(IrregularitySignalListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNumber);
            var project = this.projectsRepository.FindByRegNumber(projectNumber);
            var errorList = this.irregularitySignalService.CanCreateSignal(this.accessContext.UserId, contract, project);

            return new ErrorsDO(errorList);
        }

        [Route("{signalId:int}/info")]
        public IrregularitySignalInfoVO GetIrregularitySignalInfo(int signalId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.View, signalId);

            return this.irregularitySignalsRepository.GetInfo(signalId);
        }

        [Route("{signalId:int}/data")]
        public IrregularitySignalBasicDataVO GetIrregularitySignalBasicData(int signalId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.View, signalId);

            return this.irregularitySignalsRepository.GetBasicData(signalId);
        }

        [Route("{signalId:int}")]
        public IrregularitySignalDO GetIrregularitySignalData(int signalId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.View, signalId);

            var signal = this.irregularitySignalsRepository.Find(signalId);

            return new IrregularitySignalDO(signal);
        }

        [HttpPut]
        [Route("{signalId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.SignalData), IdParam = "signalId")]
        public void UpdateIrregularitySignalData(int signalId, IrregularitySignalDO signalDO)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, signalDO.Version);

            signal.UpdateSignalData(
                signalDO.RegDate,
                signalDO.SignalSource,
                signalDO.MASystemRegDate,
                signalDO.SignalKind,
                signalDO.ViolationDesrc,
                signalDO.Actions,
                signalDO.ActRegNum,
                signalDO.ActRegDate,
                signalDO.Comment);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{signalId:int}/updatePartial")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.SignalData), IdParam = "signalId")]
        public void UpdateBasicData(int signalId, IrregularitySignalBasicDataDO signalDO)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            if (this.irregularitySignalService.CanUpdatePartial(signalDO.ProgrammeId, signalId, signalDO.SignalRegNumber).Any())
            {
                throw new InvalidOperationException("Cannot update signal");
            }

            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, signalDO.Version);

            var currentRegNum = this.countersRepository.GetCurrentIrregularitySignalNumber(signal.ProgrammeId).ToString();

            if (signal.RegNumber == currentRegNum)
            {
                this.countersRepository.DecrementCurrentIrregularitySignalNumber(signal.ProgrammeId);
            }

            signal.UpdateSignalBasicData(signalDO.SignalRegNumber);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{signalId:int}/canUpdatePartial")]
        public ErrorsDO CanUpdateBasicData(int signalId, IrregularitySignalBasicDataDO signalDO)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var errorList = this.irregularitySignalService.CanUpdatePartial(signalDO.ProgrammeId, signalId, signalDO.SignalRegNumber);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{signalId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.ChangeStatusToActive), IdParam = "signalId")]
        public void ActivateIrregularitySignal(int signalId, string version)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            byte[] vers = System.Convert.FromBase64String(version);
            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, vers);

            if (signal.IsActivated)
            {
                signal.ChangeStatusToActive();
            }
            else
            {
                this.countersRepository.CreateIrregularitySignalCounter(signal.ProgrammeId);
                var signalNum = this.countersRepository.GetNextIrregularitySignalNumber(signal.ProgrammeId);

                signal.ChangeStatusToActive(signalNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{signalId:int}/canActivate")]
        public ErrorsDO CanActivate(int signalId, IrregularitySignalBasicDataVO signalDO)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var errorList = this.irregularitySignalService.CanActivate(signalDO.ProgrammeId, signalId, signalDO.SignalRegNumber);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{signalId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.ChangeStatusToRemoved), IdParam = "signalId")]
        public void MakeRemoved(int signalId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            if (this.irregularitySignalService.CanSetStatusToRemoved(signalId).Any())
            {
                throw new InvalidOperationException("Cannot remove signal.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, vers);

            signal.ChangeStatusToRemoved(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{signalId:int}/canSetToRemoved")]
        public ErrorsDO CanSetToRemoved(int signalId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var errors = this.irregularitySignalService.CanSetStatusToRemoved(signalId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{signalId:int}/end")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.ChangeStatusToEnded), IdParam = "signalId")]
        public void EndIrregularitySignal(int signalId, string version)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            byte[] vers = System.Convert.FromBase64String(version);
            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, vers);

            signal.ChangeStatusToEnded();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{signalId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.ChangeStatusToDraft), IdParam = "signalId")]
        public void MakeDraft(int signalId, string version)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            byte[] vers = System.Convert.FromBase64String(version);
            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, vers);

            signal.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{signalId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Delete), IdParam = "signalId")]
        public void DeleteIrregularitySignal(int signalId, string version)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            byte[] vers = System.Convert.FromBase64String(version);
            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, vers);

            this.irregularitySignalsRepository.Remove(signal);

            this.unitOfWork.Save();
        }
    }
}
