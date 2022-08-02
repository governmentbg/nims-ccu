using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using System;
using System.Linq;

namespace Eumis.Domain.Test.ExpressionEngine.Extensions
{
    public static class ProcedureExtensions
    {
        public static void SetIdsForConstructorCreatedObjects(
            this Procedure procedure,
            int procedureId,
            int procedureShareId,
            int procedureTimeLimitId)
        {
            procedure.ProcedureId = procedureId;
            procedure.ProcedureShares.Single().ProcedureShareId = procedureShareId;
            procedure.ProcedureTimeLimits.Single().ProcedureTimeLimitId = procedureTimeLimitId;
            procedure.ProcedureNumbers.Single().ProcedureId = procedureId;
        }

        public static void AddProcedureShareAndSetId(
            this Procedure procedure,
            int programmeId,
            int programmePriorityId,
            decimal bgAmount,
            bool isPrimary,
            int procedureShareId)
        {
            procedure.AddProcedureShare(
                programmeId,
                programmePriorityId,
                bgAmount,
                isPrimary);

            procedure.ProcedureShares.Last().ProcedureShareId = procedureShareId;
        }

        public static void AddProcedureSpecFieldAndSetId(
            this Procedure procedure,
            string title,
            string titleAlt,
            string description,
            string descriptionAlt,
            bool isRequired,
            ProcedureSpecFieldMaxLength maxLength,
            int procedureSpecFieldId)
        {
            procedure.AddProcedureSpecField(
                title,
                titleAlt,
                description,
                descriptionAlt,
                isRequired,
                maxLength);

            procedure.ProcedureSpecFields.Last().ProcedureSpecFieldId = procedureSpecFieldId;
        }

        public static void AddProcedureApplicationDocAndSetId(
            this Procedure procedure,
            int? programmeProcedureDocumentId,
            string name,
            bool isRequired,
            bool isSignatureRequired,
            int procedureApplicationDocId)
        {
            procedure.AddProcedureApplicationDoc(
                programmeProcedureDocumentId.Value,
                name,
                isRequired,
                isSignatureRequired);

            procedure.ProcedureApplicationDocs.Last().ProcedureApplicationDocId = procedureApplicationDocId;
        }

        public static void AddProcedureBudgetLevel1AndSetId(
            this Procedure procedure,
            int programmeId,
            int expenseTypeId,
            int procedureBudgetLevel1Id)
        {
            procedure.AddProcedureBudgetLevel1(programmeId, expenseTypeId);

            procedure.ProcedureProgrammes.Single(pp => pp.ProgrammeId == programmeId).ProcedureBudgetLevel1.Last().ProcedureBudgetLevel1Id = procedureBudgetLevel1Id;
        }

        public static void AddProcedureBudgetLevel2AndSetId(
            this Procedure procedure,
            int procedureBudgetLevel1Id,
            int programmeId,
            int programmePriorityId,
            string name,
            ProcedureBudgetLevel2AidMode aidMode,
            int procedureBudgetLevel2Id)
        {
            procedure.AddProcedureBudgetLevel2(
                procedureBudgetLevel1Id,
                programmeId,
                programmePriorityId,
                name,
                string.Empty,
                aidMode);

            var level2Item = procedure.FindProcedureBudgetLevel1(procedureBudgetLevel1Id).ProcedureBudgetLevel2.Last();

            level2Item.ProcedureBudgetLevel2Id = procedureBudgetLevel2Id;
            level2Item.ProcedureShare = new ProcedureShare
            {
                ProgrammePriorityId = programmePriorityId,
                ProgrammeId = programmeId,
            };
        }

        public static void AddProcedureBudgetLevel3AndSetId(
            this Procedure procedure,
            int procedureBudgetLevel2Id,
            string note,
            int procedureBudgetLevel3Id)
        {
            procedure.AddProcedureBudgetLevel3(procedureBudgetLevel2Id, note);

            procedure.FindProcedureBudgetLevel2(procedureBudgetLevel2Id).ProcedureBudgetLevel3.Last().ProcedureBudgetLevel3Id = procedureBudgetLevel3Id;
        }
    }
}
