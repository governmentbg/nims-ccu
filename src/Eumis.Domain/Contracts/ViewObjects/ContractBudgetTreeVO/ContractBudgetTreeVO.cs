namespace Eumis.Data.ContractReports.ViewObjects.ContractBudgetTree
{
    public class ContractBudgetTreeVO
    {
        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public byte[] Version { get; set; }

        public ContractBudgetLevel0TreeVO Programme { get; set; }
    }
}
