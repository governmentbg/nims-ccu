using Eumis.ApplicationServices.Services.ProcedureEvalTableXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.ProcedureEvalTables.Controllers
{
    [RoutePrefix("api/procedureEvalTables")]
    public class ProcedureEvalTablesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository;
        private IProceduresRepository proceduresRepository;
        private IProcedureEvalTableXmlService procedureEvalTableXmlService;

        public ProcedureEvalTablesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository,
            IProceduresRepository proceduresRepository,
            IProcedureEvalTableXmlService procedureEvalTableXmlService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.procedureEvalTableXmlsRepository = procedureEvalTableXmlsRepository;
            this.proceduresRepository = proceduresRepository;
            this.procedureEvalTableXmlService = procedureEvalTableXmlService;
        }

        [Route("{evalTableXmlGid:guid}")]
        public XmlDO GetProcedureEvalTableXml(Guid evalTableXmlGid)
        {
            var procedureId = this.procedureEvalTableXmlsRepository.GetProcedureId(evalTableXmlGid);
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedureEvalTableXml = this.procedureEvalTableXmlsRepository.FindByGid(evalTableXmlGid);

            return new XmlDO
            {
                Xml = procedureEvalTableXml.Xml,
                Version = procedureEvalTableXml.Version,
            };
        }

        [HttpPut]
        [Route("{evalTableXmlGid:guid}")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.EvalTables.UpdateXml), DisablePostData = true)]
        public XmlDO UpdateProcedureEvalTableXml(Guid evalTableXmlGid, XmlDO procedureEvalTableXmlDO)
        {
            var procedureId = this.procedureEvalTableXmlsRepository.GetProcedureId(evalTableXmlGid);
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            if (!this.procedureEvalTableXmlService.CanUpdateEvalTable(evalTableXmlGid))
            {
                throw new DomainValidationException("Cannot update evalTable xml.");
            }

            var procedureEvalTableXml = this.procedureEvalTableXmlsRepository.FindForUpdateByGid(evalTableXmlGid, procedureEvalTableXmlDO.Version);

            procedureEvalTableXml.SetXml(procedureEvalTableXmlDO.Xml);

            this.unitOfWork.Save();

            return new XmlDO
            {
                ModifyDate = procedureEvalTableXml.ModifyDate,
                Version = procedureEvalTableXml.Version,
            };
        }

        [HttpPost]
        [Route("{evalTableXmlGid:guid}/submit")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.EvalTables.Submit))]
        public XmlDO SubmitProcedureEvalTableXml(Guid evalTableXmlGid, XmlDO procedureEvalTableXmlDO)
        {
            var procedureId = this.procedureEvalTableXmlsRepository.GetProcedureId(evalTableXmlGid);
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedureEvalTableXml = this.procedureEvalTableXmlsRepository.FindForUpdateByGid(evalTableXmlGid, procedureEvalTableXmlDO.Version);
            var procedure = this.proceduresRepository.Find(procedureEvalTableXml.ProcedureId);

            procedure.EndProcedureEvalTable(procedureEvalTableXml.ProcedureEvalTableId);

            this.unitOfWork.Save();

            return new XmlDO
            {
                ModifyDate = procedureEvalTableXml.ModifyDate,
                Version = procedureEvalTableXml.Version,
            };
        }
    }
}
