using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Web.InfrastructureClasses.Charts;
using Eumis.Public.Web.InfrastructureClasses.Pies;
using System.Collections.Generic;

namespace Eumis.Public.Web.Models.ProgrammeGroup
{
    public class ProgrammeGroupIndexModel
    {
        public int ProgrammeGroupId { get; set; }

        public string ProgrammeGroupName { get; set; }

        public IList<ProgrammeBudgetDetailedVO> ProgrammeBudgets { get; set; }

        public OperationalProgramTotalsVO GrandTotals { get; set; }

        public PieRendererModel BudgetPie { get; set; }

        public PieRendererModel FinanceSourcePie { get; set; }

        public ChartRendererModel BudgetChart { get; set; }

        public ChartRendererModel ProgrammesExecutionChart { get; set; }

        public ChartRendererModel ProgrammeFinanceSourceAmountsChart { get; set; }
    }
}
