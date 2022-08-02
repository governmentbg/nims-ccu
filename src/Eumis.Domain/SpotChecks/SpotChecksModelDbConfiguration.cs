using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.SpotChecks
{
    public class SpotChecksModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SpotCheckPlaceMap());
            modelBuilder.Configurations.Add(new SpotCheckPlanMap());
            modelBuilder.Configurations.Add(new SpotCheckPlanItemMap());
            modelBuilder.Configurations.Add(new SpotCheckPlanDocMap());
            modelBuilder.Configurations.Add(new SpotCheckPlanTargetMap());
            modelBuilder.Configurations.Add(new SpotCheckMap());
            modelBuilder.Configurations.Add(new SpotCheckItemMap());
            modelBuilder.Configurations.Add(new SpotCheckTargetMap());
            modelBuilder.Configurations.Add(new SpotCheckDocMap());
            modelBuilder.Configurations.Add(new SpotCheckAscertainmentMap());
            modelBuilder.Configurations.Add(new SpotCheckRecommendationMap());
            modelBuilder.Configurations.Add(new SpotCheckRecommendationAscertainmentMap());
            modelBuilder.Configurations.Add(new SpotCheckAscertainmentItemMap());
        }
    }
}
