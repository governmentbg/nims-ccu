namespace Eumis.Domain.AnnualAccountReports.DataObjects
{
    public class AnnualAccountReportAppendixCreateDO
    {
        public int ProgrammePriorityId { get; set; }

        public AnnualAccountReportAppendixType Type { get; set; }

        public string Comment { get; set; }
    }
}
