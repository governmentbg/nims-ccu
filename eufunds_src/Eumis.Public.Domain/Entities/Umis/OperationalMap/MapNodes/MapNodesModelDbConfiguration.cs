using System.Data.Entity;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators;
using Eumis.Public.Common.Db;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators.Views;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public class MapNodesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MapNodeMap());
            modelBuilder.Configurations.Add(new MapNodeWithCategoriesMap());
            modelBuilder.Configurations.Add(new MapNodeRelationMap());
            modelBuilder.Configurations.Add(new MapNodeBudgetMap());
            modelBuilder.Configurations.Add(new MapNodeDocumentMap());
            modelBuilder.Configurations.Add(new MapNodeInstitutionMap());
            modelBuilder.Configurations.Add(new MapNodeInterventionCategoryMap());
            modelBuilder.Configurations.Add(new MapNodeFinanceSourceMap());
            modelBuilder.Configurations.Add(new MapNodeIndicatorMap());
            modelBuilder.Configurations.Add(new MapNodeIndicatorTable3Map());
            modelBuilder.Configurations.Add(new MapNodeIndicatorTable4Map());
            modelBuilder.Configurations.Add(new MapNodeIndicatorTable4aMap());
            modelBuilder.Configurations.Add(new MapNodeIndicatorTable5Map());
            modelBuilder.Configurations.Add(new MapNodeIndicatorTable6Map());
            modelBuilder.Configurations.Add(new MapNodeIndicatorTable12Map());
            modelBuilder.Configurations.Add(new MapNodeIndicatorTable13Map());
            modelBuilder.Configurations.Add(new VwMonitoringMapNodeIndicatorMap());
        }
    }
}
