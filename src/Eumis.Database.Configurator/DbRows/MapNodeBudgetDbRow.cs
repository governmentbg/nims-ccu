using Eumis.Database.Configurator.Helpers;

namespace Eumis.Database.Configurator.DbRows
{
    internal class MapNodeBudgetDbRow : IDbRow
    {
        public const string DbTableName = "MapNodeBudgets";
        public const bool UseIdentityInsert = false;

        public int MapNodeId { get; set; }

        public int BudgetPeriodId { get; set; }

        public int ProgrammeId { get; set; }

        public int FinanceSource { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public decimal EuReservedAmount { get; set; }

        public decimal BgReservedAmount { get; set; }

        public string CreateRowInsert()
        {
            return string.Format(
                "INSERT INTO [{0}] ([MapNodeId], [BudgetPeriodId], [ProgrammeId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount], [NextThreeWithAdvances], [NextThreeWithoutAdvances]) VALUES ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})",
                DbTableName,
                SqlScriptHelper.ToString(this.MapNodeId),
                SqlScriptHelper.ToString(this.BudgetPeriodId),
                SqlScriptHelper.ToString(this.ProgrammeId),
                SqlScriptHelper.ToString(this.FinanceSource),
                SqlScriptHelper.ToString(this.EuAmount),
                SqlScriptHelper.ToString(this.BgAmount),
                SqlScriptHelper.ToString(this.EuReservedAmount),
                SqlScriptHelper.ToString(this.BgReservedAmount),
                0,
                0);
        }
    }
}
