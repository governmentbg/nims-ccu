using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProceduresModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProcedureMap());
            modelBuilder.Configurations.Add(new ProcedureIndicatorMap());
            modelBuilder.Configurations.Add(new ProcedureInterventionCategoryMap());
            modelBuilder.Configurations.Add(new ProcedureInvestmentPriorityMap());
            modelBuilder.Configurations.Add(new ProcedureShareMap());
            modelBuilder.Configurations.Add(new ProcedureSpecFieldMap());
            modelBuilder.Configurations.Add(new ProcedureSpecificTargetMap());
            modelBuilder.Configurations.Add(new ProcedureTimeLimitMap());
            modelBuilder.Configurations.Add(new ProcedureTypeMap());
            modelBuilder.Configurations.Add(new ProcedureNumberMap());
            modelBuilder.Configurations.Add(new ProcedureApplicationGuidelineMap());
            modelBuilder.Configurations.Add(new ProcedureDocumentMap());
            modelBuilder.Configurations.Add(new ProcedureApplicationDocMap());
            modelBuilder.Configurations.Add(new ProcedureProgrammeMap());
            modelBuilder.Configurations.Add(new ProcedureBudgetLevel1Map());
            modelBuilder.Configurations.Add(new ProcedureBudgetLevel2Map());
            modelBuilder.Configurations.Add(new ProcedureBudgetLevel3Map());
            modelBuilder.Configurations.Add(new ProcedureBudgetValidationRuleMap());
            modelBuilder.Configurations.Add(new ProcedureEvalTableMap());
            modelBuilder.Configurations.Add(new ProcedureEvalTableXmlMap());
            modelBuilder.Configurations.Add(new ProcedureQuestionMap());
            modelBuilder.Configurations.Add(new ProcedureVersionMap());
            modelBuilder.Configurations.Add(new PublicDiscussionMap());
            modelBuilder.Configurations.Add(new ProcedureIndicativeAnnualWorkingProgrammeMap());
            modelBuilder.Configurations.Add(new ProcedureIndicativeAnnualWorkingProgrammeCandidateMap());
            modelBuilder.Configurations.Add(new ProcedureLocationMap());
        }
    }
}
