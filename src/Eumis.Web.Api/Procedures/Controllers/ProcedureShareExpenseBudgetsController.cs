using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Procedures.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/shareExpenseBudgets")]
    public class ProcedureShareExpenseBudgetsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;

        public ProcedureShareExpenseBudgetsController(IUnitOfWork unitOfWork, IProceduresRepository proceduresRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
        }

        [Route("tree")]
        public ProcedureBudgetTreeVO GetExpenseBudgetTree(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetExpenseBudgetTree(procedureId);
        }

        [HttpGet]
        [Route("newLevel1")]
        public ProcedureBudgetLevel1DO NewProcedureBudgetLevel1(int procedureId, int programmeId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var version = this.proceduresRepository.GetVersion(procedureId);

            return new ProcedureBudgetLevel1DO(procedureId, programmeId, version);
        }

        [HttpPost]
        [Route("level1")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel1.Create), IdParam = "procedureId")]
        public void AddProcedureBudgetLevel1(int procedureId, ProcedureBudgetLevel1DO procedureBudgetLevel1)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureBudgetLevel1.Version);

            procedure.AddProcedureBudgetLevel1(procedureBudgetLevel1.ProgrammeId, procedureBudgetLevel1.ExpenseTypeId.Value);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("level1/{procedureBudgetLevel1Id:int}/canDeleteLevel1")]
        public ErrorsDO CanDeleteProcedureBudgetLevel1(int procedureId, int procedureBudgetLevel1Id)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            Procedure procedure = this.proceduresRepository.Find(procedureId);

            var errors = procedure.CanDeleteProcedureBudgetLevel1(procedureBudgetLevel1Id);

            return new ErrorsDO(errors);
        }

        [HttpDelete]
        [Route("level1/{procedureBudgetLevel1Id:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel1.Delete), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel1Id")]
        public void DeleteProcedureBudgetLevel1(int procedureId, int procedureBudgetLevel1Id, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureBudgetLevel1(procedureBudgetLevel1Id);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("level1/{procedureBudgetLevel1Id:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel1.Deactivate), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel1Id")]
        public void DeactivateProcedureBudgetLevel1(int procedureId, int procedureBudgetLevel1Id, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureBudgetLevel1(procedureBudgetLevel1Id);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("level1/{procedureBudgetLevel1Id:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel1.Activate), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel1Id")]
        public void ActivateProcedureBudgetLevel1(int procedureId, int procedureBudgetLevel1Id, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureBudgetLevel1(procedureBudgetLevel1Id);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("newLevel2")]
        public ProcedureBudgetLevel2DO NewProcedureBudgetLevel2(int procedureId, int procedureBudgetLevel1Id)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.Find(procedureId);

            ProcedureBudgetLevel1 procedureBudgetLevel1 = procedure.FindProcedureBudgetLevel1(procedureBudgetLevel1Id);

            List<ProcedureShare> procedureShares = procedure.GetProcedureSharesByProgramme(procedureBudgetLevel1.ProgrammeId);

            int? programmePriorityId = null;

            if (procedureShares.Count == 1)
            {
                programmePriorityId = procedureShares[0].ProgrammePriorityId;
            }

            return new ProcedureBudgetLevel2DO(
                procedureBudgetLevel1Id,
                procedureId,
                procedureBudgetLevel1.ProgrammeId,
                programmePriorityId,
                procedureBudgetLevel1.ExpenseTypeId,
                procedure.Version);
        }

        [HttpPost]
        [Route("level2")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel2.Create), IdParam = "procedureId")]
        public void AddProcedureBudgetLevel2(int procedureId, ProcedureBudgetLevel2DO procedureBudgetLevel2)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureBudgetLevel2.Version);

            procedure.AddProcedureBudgetLevel2(
                procedureBudgetLevel2.ProcedureBudgetLevel1Id,
                procedureBudgetLevel2.ProgrammeId,
                procedureBudgetLevel2.ProgrammePriorityId.Value,
                procedureBudgetLevel2.Name,
                procedureBudgetLevel2.NameAlt,
                procedureBudgetLevel2.AidMode.Value);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("level2/{procedureBudgetLevel2Id:int}")]
        public ProcedureBudgetLevel2DO GetProcedureBudgetLevel2(int procedureId, int procedureBudgetLevel2Id)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            Procedure procedure = this.proceduresRepository.Find(procedureId);

            ProcedureBudgetLevel2 procedureBudgetLevel2 = procedure.FindProcedureBudgetLevel2(procedureBudgetLevel2Id);

            return new ProcedureBudgetLevel2DO(
                procedureBudgetLevel2.ProcedureBudgetLevel2Id,
                procedureBudgetLevel2.ProcedureBudgetLevel1Id,
                procedureBudgetLevel2.ProcedureShare.ProcedureId,
                procedureBudgetLevel2.ProcedureShare.ProgrammeId,
                procedureBudgetLevel2.ProcedureShare.ProgrammePriorityId,
                procedureBudgetLevel2.ProcedureBudgetLevel1.ExpenseTypeId,
                procedureBudgetLevel2.Name,
                procedureBudgetLevel2.NameAlt,
                procedureBudgetLevel2.AidMode,
                procedure.Version);
        }

        [HttpPut]
        [Route("level2/{procedureBudgetLevel2Id:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel2.Edit), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel2Id")]
        public void EditProcedureBudgetLevel2(int procedureId, int procedureBudgetLevel2Id, ProcedureBudgetLevel2DO procedureBudgetLevel2)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureBudgetLevel2.Version);

            procedure.EditProcedureBudgetLevel2(
                procedureBudgetLevel2Id,
                procedureBudgetLevel2.Name,
                procedureBudgetLevel2.NameAlt,
                procedureBudgetLevel2.AidMode.Value);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("level2/{procedureBudgetLevel2Id:int}/canDeleteLevel2")]
        public ErrorsDO CanDeleteProcedureBudgetLevel2(int procedureId, int procedureBudgetLevel2Id)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            Procedure procedure = this.proceduresRepository.Find(procedureId);

            var errors = procedure.CanDeleteProcedureBudgetLevel2(procedureBudgetLevel2Id);

            return new ErrorsDO(errors);
        }

        [HttpDelete]
        [Route("level2/{procedureBudgetLevel2Id:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel2.Delete), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel2Id")]
        public void DeleteProcedureBudgetLevel2(int procedureId, int procedureBudgetLevel2Id, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureBudgetLevel2(procedureBudgetLevel2Id);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("level2/{procedureBudgetLevel2Id:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel2.Deactivate), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel2Id")]
        public void DeactivateProcedureBudgetLevel2(int procedureId, int procedureBudgetLevel2Id, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureBudgetLevel2(procedureBudgetLevel2Id);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("level2/{procedureBudgetLevel2Id:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel2.Activate), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel2Id")]
        public void ActivateProcedureBudgetLevel2(int procedureId, int procedureBudgetLevel2Id, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureBudgetLevel2(procedureBudgetLevel2Id);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("newLevel3")]
        public ProcedureBudgetLevel3DO NewProcedureBudgetLevel3(int procedureId, int procedureBudgetLevel2Id)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var version = this.proceduresRepository.GetVersion(procedureId);

            return new ProcedureBudgetLevel3DO(
                procedureId,
                procedureBudgetLevel2Id,
                version);
        }

        [HttpPost]
        [Route("level3")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel3.Create), IdParam = "procedureId")]
        public void AddProcedureBudgetLevel3(int procedureId, ProcedureBudgetLevel3DO procedureBudgetLevel3)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureBudgetLevel3.Version);

            procedure.AddProcedureBudgetLevel3(
                    procedureBudgetLevel3.ProcedureBudgetLevel2Id,
                    procedureBudgetLevel3.Note);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("level3/{procedureBudgetLevel3Id:int}")]
        public ProcedureBudgetLevel3DO GetProcedureBudgetLevel3(int procedureId, int procedureBudgetLevel3Id)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            Procedure procedure = this.proceduresRepository.Find(procedureId);

            ProcedureBudgetLevel3 procedureBudgetLevel3 = procedure.FindProcedureBudgetLevel3(procedureBudgetLevel3Id);

            return new ProcedureBudgetLevel3DO(
                procedureId,
                procedureBudgetLevel3.ProcedureBudgetLevel2Id,
                procedureBudgetLevel3.Note,
                procedure.Version);
        }

        [HttpPut]
        [Route("level3/{procedureBudgetLevel3Id:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel3.Edit), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel3Id")]
        public void EditProcedureBudgetLevel3(int procedureId, int procedureBudgetLevel3Id, ProcedureBudgetLevel3DO procedureBudgetLevel3)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureBudgetLevel3.Version);

            procedure.EditProcedureBudgetLevel3(
                procedureBudgetLevel3Id,
                procedureBudgetLevel3.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("level3/{procedureBudgetLevel3Id:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetLevel3.Delete), IdParam = "procedureId", ChildIdParam = "procedureBudgetLevel3Id")]
        public void DeleteProcedureBudgetLevel3(int procedureId, int procedureBudgetLevel3Id, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureBudgetLevel3(procedureBudgetLevel3Id);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("newValidationRule")]
        public ProcedureBudgetValidationRuleDO NewProcedureBudgetValidationRule(int procedureId, int programmeId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var version = this.proceduresRepository.GetVersion(procedureId);

            return new ProcedureBudgetValidationRuleDO(procedureId, programmeId, version);
        }

        [HttpPost]
        [Route("validationRule")]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetValidation.Create), IdParam = "procedureId")]
        public object AddProcedureBudgetValidationRule(int procedureId, ProcedureBudgetValidationRuleDO procedureBudgetValidationRule)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureBudgetValidationRule.Version);

                string conditionExpressionError = procedure.ValidateExpression(procedureBudgetValidationRule.ProgrammeId, procedureBudgetValidationRule.Condition);
                string ruleExpressionError = procedure.ValidateExpression(procedureBudgetValidationRule.ProgrammeId, procedureBudgetValidationRule.Rule);

                if (conditionExpressionError == null && ruleExpressionError == null)
                {
                    procedure.AddProcedureBudgetValidationRule(
                        procedureBudgetValidationRule.ProgrammeId,
                        procedureBudgetValidationRule.Message,
                        procedureBudgetValidationRule.Condition,
                        procedureBudgetValidationRule.Rule);

                    this.unitOfWork.Save();

                    transaction.Commit();
                }

                return new
                {
                    conditionError = conditionExpressionError,
                    ruleError = ruleExpressionError,
                };
            }
        }

        [HttpGet]
        [Route("validationRule/{procedureBudgetValidationRuleId:int}")]
        public ProcedureBudgetValidationRuleDO GetProcedureBudgetValidationRule(int procedureId, int procedureBudgetValidationRuleId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            Procedure procedure = this.proceduresRepository.Find(procedureId);

            ProcedureBudgetValidationRule procedureBudgetValidationRule = procedure.FindProcedureBudgetValidationRule(procedureBudgetValidationRuleId);

            return new ProcedureBudgetValidationRuleDO(
                procedureBudgetValidationRule.ProcedureBudgetValidationRuleId,
                procedureBudgetValidationRule.ProcedureId,
                procedureBudgetValidationRule.ProgrammeId,
                procedureBudgetValidationRule.Message,
                procedureBudgetValidationRule.Condition,
                procedureBudgetValidationRule.Rule,
                procedure.Version);
        }

        [HttpPut]
        [Route("validationRule/{procedureBudgetValidationRuleId:int}")]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetValidation.Edit), IdParam = "procedureId", ChildIdParam = "procedureBudgetValidationRuleId")]
        public object EditProcedureBudgetValidationRule(int procedureId, int procedureBudgetValidationRuleId, ProcedureBudgetValidationRuleDO procedureBudgetValidationRule)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureBudgetValidationRule.Version);

                string conditionExpressionError = procedure.ValidateExpression(procedureBudgetValidationRule.ProgrammeId, procedureBudgetValidationRule.Condition);
                string ruleExpressionError = procedure.ValidateExpression(procedureBudgetValidationRule.ProgrammeId, procedureBudgetValidationRule.Rule);

                if (conditionExpressionError == null && ruleExpressionError == null)
                {
                    procedure.EditProcedureBudgetValidationRule(
                        procedureBudgetValidationRuleId,
                        procedureBudgetValidationRule.Message,
                        procedureBudgetValidationRule.Condition,
                        procedureBudgetValidationRule.Rule);

                    this.unitOfWork.Save();

                    transaction.Commit();
                }

                return new
                {
                    conditionError = conditionExpressionError,
                    ruleError = ruleExpressionError,
                };
            }
        }

        [HttpDelete]
        [Route("validationRule/{procedureBudgetValidationRuleId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BudgetValidation.Delete), IdParam = "procedureId", ChildIdParam = "procedureBudgetValidationRuleId")]
        public void DeleteProcedureBudgetValidationRule(int procedureId, int procedureBudgetValidationRuleId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureBudgetValidationRule(procedureBudgetValidationRuleId);

            this.unitOfWork.Save();
        }
    }
}
