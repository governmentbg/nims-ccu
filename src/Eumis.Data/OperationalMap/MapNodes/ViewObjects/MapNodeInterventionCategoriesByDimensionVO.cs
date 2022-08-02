using System.Collections.Generic;

namespace Eumis.Data.OperationalMap.MapNodes.ViewObjects
{
    public class MapNodeInterventionCategoriesByDimensionVO
    {
        public IList<MapNodeInterventionCategoryVO> InterventionFieldCategories { get; set; }

        public IList<MapNodeInterventionCategoryVO> FormOfFinanceCategories { get; set; }

        public IList<MapNodeInterventionCategoryVO> TerritorialDimensionCategories { get; set; }

        public IList<MapNodeInterventionCategoryVO> TerritorialDeliveryMechanismCategories { get; set; }

        public IList<MapNodeInterventionCategoryVO> ThematicObjectiveCategories { get; set; }

        public IList<MapNodeInterventionCategoryVO> ESFSecondaryThemeCategories { get; set; }

        public IList<MapNodeInterventionCategoryVO> EconomicDimensionCategories { get; set; }
    }
}
