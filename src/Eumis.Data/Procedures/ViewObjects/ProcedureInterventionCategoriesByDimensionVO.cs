using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureInterventionCategoriesByDimensionVO
    {
        public IList<ProcedureInterventionCategoryVO> InterventionFieldCategories { get; set; }

        public IList<ProcedureInterventionCategoryVO> FormOfFinanceCategories { get; set; }

        public IList<ProcedureInterventionCategoryVO> TerritorialDimensionCategories { get; set; }

        public IList<ProcedureInterventionCategoryVO> TerritorialDeliveryMechanismCategories { get; set; }

        public IList<ProcedureInterventionCategoryVO> ThematicObjectiveCategories { get; set; }

        public IList<ProcedureInterventionCategoryVO> ESFSecondaryThemeCategories { get; set; }

        public IList<ProcedureInterventionCategoryVO> EconomicDimensionCategories { get; set; }
    }
}
