using System.Data.Entity;
using Eumis.Common.Db;
using Eumis.Domain.CheckBlankTopics;
using Eumis.Domain.Measures;

namespace Eumis.Domain.NonAggregates
{
    public class NonAggregatesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BudgetPeriodMap());
            modelBuilder.Configurations.Add(new InterventionCategoryMap());
            modelBuilder.Configurations.Add(new RegulationInvestmentPriorityMap());
            modelBuilder.Configurations.Add(new MeasureMap());
            modelBuilder.Configurations.Add(new InstitutionMap());
            modelBuilder.Configurations.Add(new InstitutionPersonMap());
            modelBuilder.Configurations.Add(new InstitutionTypeMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new MunicipalityMap());
            modelBuilder.Configurations.Add(new Nuts1Map());
            modelBuilder.Configurations.Add(new Nuts2Map());
            modelBuilder.Configurations.Add(new SettlementMap());
            modelBuilder.Configurations.Add(new ProtectedZoneMap());
            modelBuilder.Configurations.Add(new BlobMap());
            modelBuilder.Configurations.Add(new CompanyLegalTypeMap());
            modelBuilder.Configurations.Add(new CompanySizeTypeMap());
            modelBuilder.Configurations.Add(new CompanyTypeMap());
            modelBuilder.Configurations.Add(new ErrandLegalActMap());
            modelBuilder.Configurations.Add(new ErrandTypeMap());
            modelBuilder.Configurations.Add(new ErrandAreaMap());
            modelBuilder.Configurations.Add(new KidCodeMap());
            modelBuilder.Configurations.Add(new CheckBlankTopicMap());
            modelBuilder.Configurations.Add(new BfpAmountMap());
            modelBuilder.Configurations.Add(new ExpensesAmountMap());
            modelBuilder.Configurations.Add(new NotificationEventMap());
        }
    }
}
