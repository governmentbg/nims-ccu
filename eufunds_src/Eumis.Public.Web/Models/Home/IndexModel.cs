using System.Collections.Generic;

namespace Eumis.Public.Web.Models.Home
{
    public class IndexModel
    {
        public IndexModel(IEnumerable<NomenclatureModel> programmeGroups, IEnumerable<NomenclatureModel> planningRegions)
        {
            this.ProgrammeGroups = programmeGroups ?? new List<NomenclatureModel>();
            this.PlanningRegions = planningRegions ?? new List<NomenclatureModel>();
        }

        public IEnumerable<NomenclatureModel> ProgrammeGroups { get; set; }

        public IEnumerable<NomenclatureModel> PlanningRegions { get; set; }
    }
}
