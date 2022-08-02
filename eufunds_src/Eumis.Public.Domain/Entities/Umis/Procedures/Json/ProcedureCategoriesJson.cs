using System.Collections.Generic;

namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class ProcedureCategoriesJson
    {
        public ProcedureCategoriesJson()
        {
            this.InterventionField = new List<ProcedureCategoryJson>();
            this.FormOfFinance = new List<ProcedureCategoryJson>();
            this.TerritorialDimension = new List<ProcedureCategoryJson>();
            this.TerritorialDeliveryMechanism = new List<ProcedureCategoryJson>();
            this.ThematicObjective = new List<ProcedureCategoryJson>();
            this.ESFSecondaryTheme = new List<ProcedureCategoryJson>();
            this.EconomicDimension = new List<ProcedureCategoryJson>();
        }

        public IList<ProcedureCategoryJson> InterventionField { get; set; }

        public IList<ProcedureCategoryJson> FormOfFinance { get; set; }

        public IList<ProcedureCategoryJson> TerritorialDimension { get; set; }

        public IList<ProcedureCategoryJson> TerritorialDeliveryMechanism { get; set; }

        public IList<ProcedureCategoryJson> ThematicObjective { get; set; }

        public IList<ProcedureCategoryJson> ESFSecondaryTheme { get; set; }

        public IList<ProcedureCategoryJson> EconomicDimension { get; set; }
    }
}
