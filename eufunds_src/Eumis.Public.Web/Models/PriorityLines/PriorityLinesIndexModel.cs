using System.Collections.Generic;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Web.InfrastructureClasses.Charts;
using Eumis.Public.Web.InfrastructureClasses.Pies;

namespace Eumis.Public.Web.Models.PriorityLines
{
    public class PriorityLinesIndexModel
    {
        public List<PriorityAxisModel> PriorityAxises { get; set; }

        public ProgrammePrioritiesSummarizedDataVO SummarizedData { get; set; }

        public PieRendererModel BudgetPie { get; set; }

        public ChartRendererModel BudgetChart { get; set; }
    }
}