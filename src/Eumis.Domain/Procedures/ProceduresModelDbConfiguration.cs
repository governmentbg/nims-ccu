using System.Data.Entity;
using Eumis.Common.Db;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Domain.Procedures.Views;

namespace Eumis.Domain.Procedures
{
    public class ProceduresModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProcedureMap());
            modelBuilder.Configurations.Add(new ProcedureIndicatorMap());
            modelBuilder.Configurations.Add(new ProcedureShareMap());
            modelBuilder.Configurations.Add(new ProcedureSpecFieldMap());
            modelBuilder.Configurations.Add(new ProcedureTimeLimitMap());
            modelBuilder.Configurations.Add(new ProcedureNumberMap());
            modelBuilder.Configurations.Add(new ProcedureDocumentMap());
            modelBuilder.Configurations.Add(new ProcedureApplicationDocMap());
            modelBuilder.Configurations.Add(new ProcedureProgrammeMap());
            modelBuilder.Configurations.Add(new ProcedureBudgetLevel1Map());
            modelBuilder.Configurations.Add(new ProcedureBudgetLevel2Map());
            modelBuilder.Configurations.Add(new ProcedureBudgetLevel3Map());
            modelBuilder.Configurations.Add(new ProcedureBudgetValidationRuleMap());
            modelBuilder.Configurations.Add(new ProcedureEvalTableMap());
            modelBuilder.Configurations.Add(new ProcedureEvalTableXmlMap());
            modelBuilder.Configurations.Add(new ProcedureEvalTableXmlFileMap());
            modelBuilder.Configurations.Add(new ProcedureVersionMap());
            modelBuilder.Configurations.Add(new ProcedureContractReportDocumentMap());
            modelBuilder.Configurations.Add(new ProcedureLocationMap());
            modelBuilder.Configurations.Add(new ProcedureMassCommunicationMap());
            modelBuilder.Configurations.Add(new ProcedureMassCommunicationDocumentMap());
            modelBuilder.Configurations.Add(new ProcedureMassCommunicationRecipientMap());
            modelBuilder.Configurations.Add(new VwBudgetComponentMap());
            modelBuilder.Configurations.Add(new ProcedureApplicationSectionMap());
            modelBuilder.Configurations.Add(new ProcedureApplicationSectionAdditionalSettingMap());
            modelBuilder.Configurations.Add(new ProcedureDirectionMap());
            modelBuilder.Configurations.Add(new ProcedureMonitorstatDocumentMap());
            modelBuilder.Configurations.Add(new ProcedureMonitorstatEconomicActivityMap());
            modelBuilder.Configurations.Add(new ProcedureMonitorstatRequestMap());
            modelBuilder.Configurations.Add(new ProcedureApplicationGuidelineMap());
            modelBuilder.Configurations.Add(new ProcedureDeclarationMap());
            modelBuilder.Configurations.Add(new ProcedureAppFormDeclarationMap());
        }
    }
}
