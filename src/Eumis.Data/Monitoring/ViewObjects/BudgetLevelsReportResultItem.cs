namespace Eumis.Data.Monitoring.ViewObjects
{
    public class BudgetLevelsReportResultItem : BudgetLevelsReportItem
    {
        public int? ProcedureBudgetLevel1Id { get; set; }

        public int? ProcedureBudgetLevel2Id { get; set; }

        public bool AreAmountsNull { get; set; }
    }
}
