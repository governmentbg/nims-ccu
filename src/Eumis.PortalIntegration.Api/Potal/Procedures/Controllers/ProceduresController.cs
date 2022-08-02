using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ProcedureVersion;
using Eumis.Common;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.Procedures.DataObjects;

namespace Eumis.PortalIntegration.Api.Portal.Procedures.Controllers
{
    [RoutePrefix("api/procedures")]
    public class ProceduresController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IProgrammesRepository programmesRepository;
        private IProcedureVersionsRepository procedureVersionsRepository;
        private IProcedureVersionService procedureVersionService;
        private IActionLogger actionLogger;
        private IAccessContext accessContext;

        public ProceduresController(
            IUnitOfWork unitOfWork,
            IProceduresRepository proceduresRepository,
            IProgrammesRepository programmesRepository,
            IProcedureVersionsRepository procedureVersionsRepository,
            IProcedureVersionService procedureVersionService,
            IActionLogger actionLogger,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.programmesRepository = programmesRepository;
            this.procedureVersionsRepository = procedureVersionsRepository;
            this.procedureVersionService = procedureVersionService;
            this.actionLogger = actionLogger;
            this.accessContext = accessContext;
        }

        [AllowAnonymous]
        [Route("activeProceduresTree")]
        public IList<ProcedureProgrammeTreePVO> GetActiveProcedureProgrammesTree()
        {
            return this.proceduresRepository.GetPortalActiveProcedureProgrammesTree();
        }

        [AllowAnonymous]
        [Route("endedProceduresTree")]
        public IList<ProcedureProgrammeTreePVO> GetEndedProcedureProgrammesTree()
        {
            return this.proceduresRepository.GetPortalEndedProcedureProgrammesTree();
        }

        [AllowAnonymous]
        [Route("publicDiscussionProceduresTree")]
        public IList<ProcedureProgrammeTreePVO> GetPublicDiscussionProcedureProgrammesTree()
        {
            return this.proceduresRepository.GetPortalPublicDiscussionProcedureProgrammesTree();
        }

        [AllowAnonymous]
        [Route("archivedPublicDiscussionProceduresTree")]
        public IList<ProcedureProgrammeTreePVO> GetArchivedPublicDiscussionProcedureProgrammesTree()
        {
            return this.proceduresRepository.GetPortalArchivedPublicDiscussionProcedureProgrammesTree();
        }

        [AllowAnonymous]
        [Route("{procedureGid:guid}/info")]
        public object GetProcedureInfo(Guid procedureGid)
        {
            return this.procedureVersionsRepository.GetPortalProcedureInfo(procedureGid);
        }

        [AllowAnonymous]
        [Route("{procedureGid:guid}/infoPublicDiscussion")]
        public object GetProcedurePublicDiscussionInfo(Guid procedureGid)
        {
            return this.proceduresRepository.GetPortalProcedureInfo(procedureGid);
        }

        [AllowAnonymous]
        [Route("{procedureGid:guid}/infoProcedureDiscussions")]
        public IList<ProcedureDiscussionsInfoPVO> GetProcedureDiscussionInfo(Guid procedureGid)
        {
            return this.proceduresRepository.GetPortalProcedureDiscussion(procedureGid);
        }

        [AllowAnonymous]
        [Route("{procedureGid:guid}/appdata")]
        public ProcedureAppDataPVO GetProcedureAppData(Guid procedureGid)
        {
            var procedureVersion = this.procedureVersionsRepository.GetPortalProcedureVersion(procedureGid);

            if (!procedureVersion.ProcedureVersionJson.Version.HasValue ||
                (procedureVersion.ProcedureVersionJson.Version.Value != Eumis.Domain.Procedures.Json.ProcedureVersionJson.CURRENT_JSON_VERSION))
            {
                var procedureStatus = this.proceduresRepository.GetProcedureStatus(procedureVersion.ProcedureId);

                if (procedureStatus == Domain.Procedures.ProcedureStatus.Active ||
                    procedureStatus == Domain.Procedures.ProcedureStatus.Ended ||
                    procedureStatus == Domain.Procedures.ProcedureStatus.Terminated)
                {
                    using (var transaction = this.unitOfWork.BeginTransaction())
                    {
                        // we are passing down the IsActive prop, because it is true, only when
                        // procedureStatus == Active and we want the new ProcedureVersion to be updated
                        // but its IsActive prop to be the same as the old one
                        var newProcedureVersion = this.procedureVersionService.CreateProcedureVersion(procedureVersion.ProcedureId, procedureVersion.IsActive);

                        this.unitOfWork.Save();

                        transaction.Commit();

                        procedureVersion = newProcedureVersion;
                    }
                }
            }

            var currentEndDate = this.proceduresRepository.GetProcedureCurrentEndDate(procedureVersion.ProcedureId);

            foreach (var document in procedureVersion.ProcedureVersionJson.AppDocs)
            {
                var applicationDocument = this.proceduresRepository.FindProcedureAppDoc(document.AppDocId);

                if (applicationDocument.ProgrammeApplicationDocumentId != null)
                {
                    document.Extension = this.programmesRepository.GetProgrammeApplicationDocumentExtension(applicationDocument.ProgrammeApplicationDocumentId.Value);
                }
            }

            return new ProcedureAppDataPVO(procedureVersion.ProcedureVersionJson, procedureVersion.IsActive, currentEndDate);
        }

        [AllowAnonymous]
        [Route("{procedureGid:guid}/actualappdata")]
        public ProcedureAppDataPVO GetProcedureActualAppData(Guid procedureGid)
        {
            int procedureId = this.proceduresRepository.GetId(procedureGid);

            // create a procedure version just to get the latest version json
            // should not be saved!!!
            var procedureVersion = this.procedureVersionService.CreateProcedureVersion(procedureId);
            var currentEndDate = this.proceduresRepository.GetProcedureCurrentEndDate(procedureId);

            return new ProcedureAppDataPVO(procedureVersion.ProcedureVersionJson, false, currentEndDate);
        }

        [AllowAnonymous]
        [Route("{procedureGid:guid}/contractdata")]
        public object GetProcedureContractData(Guid procedureGid, string programmeCode)
        {
            var level2EuPercent = this.proceduresRepository.GetBudgetLevel2EuPercent(procedureGid, programmeCode);

            return new
            {
                level2EuPercent,
            };
        }
    }
}
