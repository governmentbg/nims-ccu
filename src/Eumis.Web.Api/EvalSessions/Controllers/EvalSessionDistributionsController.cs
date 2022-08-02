using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Counters;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/distributions")]
    public class EvalSessionDistributionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private ICountersRepository countersRepository;
        private IProceduresRepository proceduresRepository;
        private IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        public EvalSessionDistributionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            ICountersRepository countersRepository,
            IProceduresRepository proceduresRepository,
            IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.countersRepository = countersRepository;
            this.proceduresRepository = proceduresRepository;
            this.procedureEvalTableXmlsRepository = procedureEvalTableXmlsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
        }

        [Route("")]
        public IList<EvalSessionDistributionsVO> GetEvalSessionDistributions(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionDistributions(evalSessionId);
        }

        [Route("{distributionId:int}")]
        public EvalSessionDistributionDO GetEvalSessionDistribution(int evalSessionId, int distributionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionDistribution = evalSession.FindEvalSessionDistribution(distributionId);

            var assessors = this.evalSessionsRepository.GetEvalSessionAsessors(evalSessionId);

            var projects = this.evalSessionsRepository.GetEvalSessionDistributionProjects(evalSessionId, distributionId);

            return new EvalSessionDistributionDO(evalSessionDistribution, assessors, projects, evalSession.Version);
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionDistributionDO NewEvalSessionDistribution(int evalSessionId, ProcedureEvalTableType evalTableType)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);
            var assessors = this.evalSessionsRepository.GetEvalSessionNotDeactivatedAssessors(evalSessionId);
            var projects = this.evalSessionsRepository.GetNewEvalSessionDistributionProjects(evalSessionId);

            foreach (var p in projects)
            {
                var validationErrors = evalSession.ValidateEvalSessionDistributionProject(p.ProjectId, evalTableType);
                if (validationErrors.Any())
                {
                    p.IsDeleted = true;
                    p.IsDeletedNote = string.Join(", ", validationErrors);
                }
            }

            return new EvalSessionDistributionDO(evalSessionId, assessors, projects, evalSession.Version)
            {
                EvalTableType = evalTableType,
            };
        }

        [HttpPost]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Distributions.Create), IdParam = "evalSessionId")]
        public void AddEvalSessionDistribution(int evalSessionId, EvalSessionDistributionDO evalSessionDistribution)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            this.countersRepository.CreateEvalSessionDistributionCounter(evalSessionId);
            var regNumber = this.countersRepository.GetNextEvalSessionDistributionNumber(evalSessionId);
            var projectsIds = this.evalSessionsRepository.GetNewEvalSessionDistributionProjects(evalSessionId).Select(t => t.ProjectId).ToArray();

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionDistribution.Version);
            Procedure procedure = this.proceduresRepository.Find(evalSession.ProcedureId);

            ProcedureEvalTable procedureEvalTable = procedure.ProcedureEvalTables
                .Single(et => et.IsActivated && et.IsActive && et.Type == evalSessionDistribution.EvalTableType);

            ProcedureEvalTableXml procedureEvalTableXml = this.procedureEvalTableXmlsRepository.FindByProcedureEvalTableId(procedureEvalTable.ProcedureEvalTableId);

            string sheetXml = this.documentRestApiCommunicator.CreateEvalSessionSheetXml(procedureEvalTableXml.Xml);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var newEvalSessionDistribution = evalSession.AddEvalSessionDistribution(
                    regNumber,
                    evalSessionDistribution.EvalTableType.Value,
                    evalSessionDistribution.AssessorsPerProject.Value,
                    procedure);

                this.unitOfWork.Save();

                var excludedProjects = evalSessionDistribution.Projects
                    .Where(p => p.IsDeleted)
                    .ToDictionary(p => p.ProjectId, p => p.IsDeletedNote);

                var sheetsProjectsTuple = evalSession.GenerateEvalSessionSheets(
                    newEvalSessionDistribution.EvalSessionDistributionId,
                    projectsIds,
                    evalSessionDistribution.Assessors,
                    excludedProjects);

                var evalSessionSheets = sheetsProjectsTuple.Item1;
                var evalSessionDistributionProjects = sheetsProjectsTuple.Item2;

                this.unitOfWork.BulkInsert<EvalSessionDistributionProject>(evalSessionDistributionProjects);

                this.unitOfWork.BulkInsert<EvalSessionSheet>(evalSessionSheets);

                var parsedSheetXml = XDocument.Parse(sheetXml);
                List<EvalSessionSheetXml> evalSessionSheetXmls = new List<EvalSessionSheetXml>();
                foreach (var sessionSheet in evalSessionSheets)
                {
                    parsedSheetXml.XPathSelectElement("/EvalSheet").SetAttributeValue("id", Guid.NewGuid().ToString());

                    EvalSessionSheetXml evalSessionSheetXml = new Domain.EvalSessions.EvalSessionSheetXml(
                        evalSession.EvalSessionId,
                        sessionSheet.EvalSessionSheetId,
                        procedureEvalTable.EvalType,
                        procedureEvalTable.Type,
                        parsedSheetXml.ToString());

                    evalSessionSheetXmls.Add(evalSessionSheetXml);
                }

                this.unitOfWork.BulkInsert<EvalSessionSheetXml>(evalSessionSheetXmls);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        [HttpPost]
        [Route("{distributionId:int}/canRefuse")]
        public ErrorsDO CanRefuseEvalSessionDistribution(int evalSessionId, int distributionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errorList = this.evalSessionsRepository.CanRefuseEvalSessionDistribution(evalSessionId, distributionId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Transaction]
        [Route("{distributionId:int}/refuse")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Distributions.Refuse), IdParam = "evalSessionId", ChildIdParam = "distributionId")]
        public void RefuseEvalSessionDistribution(int evalSessionId, int distributionId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            var evalSessionSheets = evalSession.RefuseEvalSessionDistribution(distributionId, confirm.Note);

            this.unitOfWork.BulkUpdate<EvalSessionSheet>(evalSessionSheets, ess => ess.Status, ess => ess.StatusDate, ess => ess.StatusNote);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateEvalSessionDistribution(int evalSessionId, ProcedureEvalTableType evalTableType)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);
            Procedure procedure = this.proceduresRepository.Find(evalSession.ProcedureId);

            var errorList = evalSession.CanCreateEvalSessionDistribution(procedure, evalTableType);

            return new ErrorsDO(errorList);
        }
    }
}
