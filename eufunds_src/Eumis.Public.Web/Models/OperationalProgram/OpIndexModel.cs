using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Web.InfrastructureClasses.Charts;
using Eumis.Public.Web.InfrastructureClasses.Pies;

namespace Eumis.Public.Web.Models.OperationalProgram
{
    public class OpIndexModel
    {
        public ProgrammeGroupsVO OperationalPrograms { get; set; }

        public PieRendererModel BudgetPie { get; set; }

        public PieRendererModel FinanceSourcePie { get; set; }

        public ChartRendererModel BudgetChart { get; set; }

        public ChartRendererModel ProgrammesExecutionChart { get; set; }

        public ChartRendererModel ProgrammeFinanceSourceAmountsChart { get; set; }
    }
}